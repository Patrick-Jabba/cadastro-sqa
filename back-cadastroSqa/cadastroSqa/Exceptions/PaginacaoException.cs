using FluentResults;

namespace cadastroSqa.Exceptions.Interfaces
{
    public class PaginacaoException : IPaginacaoException
    {
        public Result ValidarPagina(int id)
        {
            if(id <= 0) return Result.Fail("Página não é válida");

            return Result.Ok();
        }

        public Result ValidarTamanho(int id)
        {
            if(id <= 0) return Result.Fail("Tamanho não é válido");

            return Result.Ok();
        }
    }
}