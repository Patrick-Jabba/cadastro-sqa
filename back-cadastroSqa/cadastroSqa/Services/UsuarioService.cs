using AutoMapper;
using cadastroSqa.Data;
using cadastroSqa.Interfaces;
using cadastroSqa.Models;
using cadastroSqa.Services.DTO;

namespace cadastroSqa.Services
{
  public class UsuarioService : IUsuario
    {
        private readonly CadastroSqaContext _context;

        private readonly IMapper _mapper;

        public UsuarioService(CadastroSqaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  
        }

        public IEnumerable<ReadUsuarioDto> GetAll()
    {
      var listaUsuarios = _context.Usuarios.ToList();

      IEnumerable<ReadUsuarioDto> usuariosDto = _mapper.Map<List<ReadUsuarioDto>>(listaUsuarios);

      return usuariosDto;
    }

    public virtual PaginacaoUsuario cadastroPaginacao(int pagina, int itensPorPagina)
    {
      try
      {
        int skip = itensPorPagina * (pagina -1);
        int take = itensPorPagina;

        List<Usuario> usuarios = _context.Usuarios.OrderByDescending(u => u.Id)
                                .ThenByDescending(u => u.Name)
                                .Skip(skip)
                                .Take(take)
                                .ToList();

        int totalUsuarios = _context.Usuarios.Count();                              

        int totalPaginas = (int)Math.Ceiling((double)totalUsuarios / (double)itensPorPagina);

        return new PaginacaoUsuario(
            _mapper.Map<List<ReadUsuarioDto>>(usuarios), totalPaginas, pagina, totalUsuarios);
      }
      catch (System.Exception)
      {
        
        throw;
      }
    }

    public ReadUsuarioDto GetById(int id)
    {
      Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
      ReadUsuarioDto usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);

      return usuarioDto;
    }

    public ReadUsuarioDto Create(AddUsuarioDto obj)
    {
      Usuario usuario = _mapper.Map<Usuario>(obj);
      _context.Usuarios.Add(usuario);   
      _context.SaveChanges();

      ReadUsuarioDto usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);

      return usuarioDto;
    }

    public ReadUsuarioDto Update(int id, UpdateUsuarioDto obj)
    {
      Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

      if(usuario != null) 
      {
        _mapper.Map(obj, usuario);
        ReadUsuarioDto usuarioDto = _mapper.Map<ReadUsuarioDto>(usuario);
        _context.SaveChanges();

        return usuarioDto;
      }

      return null;
    }

    public bool Remove(int id)
    {
      Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

      if(usuario != null)
      {
        _context.Remove(usuario);
        _context.SaveChanges();
        return true;
      }

      return false;
    }
  }
}