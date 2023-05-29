using cadastroSqa.Models;
using Microsoft.EntityFrameworkCore;

namespace cadastroSqa.Data
{
    public class CadastroSqaContext : DbContext
    {
        public CadastroSqaContext() { }

        public CadastroSqaContext(DbContextOptions<CadastroSqaContext> options) : base(options) { } 

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
