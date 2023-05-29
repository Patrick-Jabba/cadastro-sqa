using FluentResults;

namespace cadastroSqa.Exceptions.Interfaces
{
    public interface IPaginacaoException
    {
        Result ValidarPagina(int id);

        Result ValidarTamanho(int id);
    }
}