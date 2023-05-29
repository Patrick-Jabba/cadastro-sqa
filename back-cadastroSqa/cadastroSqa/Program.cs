using cadastroSqa.Data;
using cadastroSqa.Exceptions;
using cadastroSqa.Exceptions.Interfaces;
using cadastroSqa.Interfaces;
using cadastroSqa.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IUsuarioException, UsuarioException>();
builder.Services.AddScoped<IPaginacaoException, PaginacaoException>();

// AddAutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DbContext -> postgres
var configuration = new ConfigurationBuilder()
    .AddUserSecrets<CadastroSqaContext>()
    .Build();

// Dados sensíveis guardados por secrets
// var connectionString = configuration["CONEXAODB"];

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRINGS");

builder.Services.AddDbContext<CadastroSqaContext>(options => 
{
    options.UseLazyLoadingProxies().UseNpgsql(connectionString, 
    assembly => assembly.MigrationsAssembly(typeof(CadastroSqaContext).Assembly.FullName));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CadastroSqaContext>();
    dbContext.Database.Migrate();
}

app.Run("http://*:8080");
