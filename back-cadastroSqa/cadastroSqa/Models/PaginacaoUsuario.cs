using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastroSqa.Services.DTO;

namespace cadastroSqa.Models
{
    public class PaginacaoUsuario
    {
        public PaginacaoUsuario(List<ReadUsuarioDto> conteudos, int totalPaginas, int paginaAtual, int totalItens)
        {
            this.Conteudos = conteudos;
            this.TotalPaginas = totalPaginas;
            this.PaginaAtual = paginaAtual;
            this.TotalItens = totalItens;
        }
        public List<ReadUsuarioDto> Conteudos {get; set;}

        public int TotalPaginas {get; set;}

        public int TotalItens {get; set;}

        public int PaginaAtual {get; set;}

    }
}