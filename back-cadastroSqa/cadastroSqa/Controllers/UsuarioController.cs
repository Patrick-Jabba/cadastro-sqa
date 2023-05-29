using cadastroSqa.Exceptions;
using cadastroSqa.Exceptions.Interfaces;
using cadastroSqa.Interfaces;
using cadastroSqa.Models;
using cadastroSqa.Services.DTO;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace cadastroSqa.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _interfaces;
        private readonly IUsuarioException _usuarioException;
        private readonly IPaginacaoException _paginacaoException;
        
        public UsuarioController(
            IUsuario interfaces, 
            IUsuarioException usuarioException, 
            IPaginacaoException paginacaoException
        )
        {
            _interfaces = interfaces;
            _usuarioException = usuarioException;
            _paginacaoException = paginacaoException;
        }

        public static IEnumerable<String> messageException(Result resultado)
        {
            return resultado.Reasons.Select(r => r.Message);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var usuarios = _interfaces.GetAll();
            if(usuarios != null) return Ok(usuarios);

            return NotFound("Cadastros não encontrados.");
        }

        [HttpGet("pagina/{pagina:int}/itensPorPagina/{itensPorPagina:int}")]
        public IActionResult CadastrosPorPaginacao(int pagina, int itensPorPagina)
        {
            Result resultadoPagina, resultadoItens;
            
            resultadoPagina = _paginacaoException.ValidarPagina(pagina);
            if(resultadoPagina.IsFailed) return BadRequest(messageException(resultadoPagina));

            resultadoItens = _paginacaoException.ValidarTamanho(itensPorPagina);
            if(resultadoItens.IsFailed) return BadRequest(messageException(resultadoItens));

            PaginacaoUsuario cadastros = _interfaces.cadastroPaginacao(pagina, itensPorPagina);

            return Ok(cadastros);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var usuario = _interfaces.GetById(id);
            if(usuario != null) return Ok(usuario);

            return NotFound("Cadastro não encontrado."); 
        }

        [HttpPost]
        public IActionResult InsertUser([FromBody] AddUsuarioDto dto)
        {
            Result validaFormatoCPF;
            Result validarCPFExistente;
            Result validarDataNascimento;

            validaFormatoCPF = _usuarioException.ValidaFormatoCPF(dto.CPF);
            if(validaFormatoCPF.IsFailed) return BadRequest(messageException(validaFormatoCPF));

            validarCPFExistente = _usuarioException.ValidarCPFExistente(dto.CPF);
            if(validarCPFExistente.IsFailed) return BadRequest(messageException(validarCPFExistente));

            validarDataNascimento = _usuarioException.ValidarDataNascimento(dto.DataNascimento);
            if(validarDataNascimento.IsFailed) return BadRequest(messageException(validarDataNascimento));

            var usuario = _interfaces.Create(dto);
            if(usuario != null) return Ok(usuario);

            return BadRequest("Falha ao cadastrar usuário.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUsuarioDto dto)
        {
            Result validaFormatoCPF;
            Result validarDataNascimento;

            validaFormatoCPF = _usuarioException.ValidaFormatoCPF(dto.CPF);
            if(validaFormatoCPF.IsFailed) return BadRequest(messageException(validaFormatoCPF));

            validarDataNascimento = _usuarioException.ValidarDataNascimento(dto.DataNascimento);
            if(validarDataNascimento.IsFailed) return BadRequest(messageException(validarDataNascimento));
            
            var usuario = _interfaces.Update(id, dto);
            if(usuario != null) return Ok("Cadastro atualizado com sucesso!");

            return BadRequest("Falha ao atualizar cadastro.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var usuario = _interfaces.Remove(id);
            if(usuario != false) return Ok("Cadastro removido com sucesso.");

            return BadRequest("Falha ao remover cadastro.");
        }
    }
}