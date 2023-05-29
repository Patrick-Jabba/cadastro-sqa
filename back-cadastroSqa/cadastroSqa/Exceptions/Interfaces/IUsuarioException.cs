using FluentResults;

namespace cadastroSqa.Exceptions
{
  public interface IUsuarioException
  {
    Result ValidaFormatoData(string data);
    Result ValidarDataNascimento(DateTime dataNascimento);

    Result ValidarCPFExistente(string cpf);

    Result ValidaFormatoCPF(string cpf);
  }
}