using System.Text.RegularExpressions;
using cadastroSqa.Data;
using FluentResults;

namespace cadastroSqa.Exceptions
{
  public class UsuarioException : IUsuarioException
  {
    private readonly CadastroSqaContext _context;

    public UsuarioException(CadastroSqaContext context) 
    {
      _context = context;
    }

    public UsuarioException() { }

    public Result ValidaFormatoData(string data)
    {
      // Expressão regular para verificar o formato da data no padrão "AAAA-MM-DDTHH:mm:ss.fffZ"
      string formatoDataRegex = @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z$";

      // Verifica se a data corresponde ao formato esperado
      bool isFormatoValido = Regex.IsMatch(data, formatoDataRegex);

      if(!isFormatoValido) return Result.Fail("O formato de data é inválido.");

      return Result.Ok();
    }

    public Result ValidarDataNascimento(DateTime dataNascimento)
    {
      int idadeMinima = 18;
      DateTime dataMinima = DateTime.Today.AddYears(-idadeMinima);

      if (dataNascimento > dataMinima) return Result.Fail("A data de nascimento informada representa um menor de idade.");

      return Result.Ok();
    }

    public Result ValidarCPFExistente(string cpf)
    {
      var cpfNoBanco = _context.Usuarios.FirstOrDefault(u => u.CPF.Equals(cpf));

      if(cpfNoBanco != null) return Result.Fail("O CPF informado já foi cadastrado.");

      return Result.Ok();
    }

    public Result ValidaFormatoCPF(string cpf)
    {
        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (!IsCPFValido(cpf)) return Result.Fail("CPF inválido.");

        return Result.Ok();
    }

    private bool IsCPFValido(string cpf)
    {
      cpf = cpf.Trim().Replace(".", "").Replace("-", "");

      if (cpf.Length != 11 || cpf == "00000000000") return false;

      // Validação do formato utilizando regex
      Regex regex = new Regex(@"^\d{11}$");
      if (!regex.IsMatch(cpf)) return false;

      // Verificação dos dígitos verificadores
      int[] digitos = cpf.Select(c => int.Parse(c.ToString())).ToArray();
      int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

      int soma1 = digitos.Take(9).Select((digit, index) => digit * multiplicadores1[index]).Sum();
      int remendo1 = soma1 % 11;
      int digito1 = remendo1 < 2 ? 0 : 11 - remendo1;

      int soma2 = digitos.Take(10).Select((digit, index) => digit * multiplicadores2[index]).Sum();
      int remendo2 = soma2 % 11;
      int digito2 = remendo2 < 2 ? 0 : 11 - remendo2;

      return digitos[9] == digito1 && digitos[10] == digito2;
    }

  }
}