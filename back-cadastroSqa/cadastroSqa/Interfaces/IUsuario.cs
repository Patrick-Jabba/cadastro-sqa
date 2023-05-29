using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cadastroSqa.Models;
using cadastroSqa.Services.DTO;

namespace cadastroSqa.Interfaces
{
    public interface IUsuario : IBase<AddUsuarioDto, ReadUsuarioDto>, IUpdate<UpdateUsuarioDto, ReadUsuarioDto>
    {
        PaginacaoUsuario cadastroPaginacao(int pagina, int itensPorPagina);
    }
}