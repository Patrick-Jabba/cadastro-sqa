using cadastroSqa.Exceptions;

namespace cadastroSqa.Tests;

public class UsuarioTestes
{
    UsuarioException validador = new UsuarioException();

     [Fact]
    public void DeveValidarOFormatoDoCPF()
    {
        // Arrange
        var formatoCPFValido = "229.166.240-66";

        // Act
        var resultado = validador.ValidaFormatoCPF(formatoCPFValido);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
    }

    [Fact]
    public void NaoDeveValidarFormatoCPFInvalido()
    {
        // Arrange
        var formatoCPFInvalido = "123.789.101-33";

        //Act
        var resultado  = validador.ValidaFormatoCPF(formatoCPFInvalido);

        // Assert
        Assert.True(resultado.IsFailed);
        Assert.Equal("CPF inválido.", resultado.Errors.First().Message);

    }

    [Fact]
    public void ValidaFormatoData()
    {
        // Arrange
        var dataValida = "1678-12-10T00:00:00.000Z";

        // Act
        var resultado = validador.ValidaFormatoData(dataValida);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
    }

    [Theory]
    [InlineData("2022-01-01")] 
    [InlineData("01/01/2022")] 
    [InlineData("2022/01/01")] 
    [InlineData("20220101")] 
    public void NaoDeveValidarFormatosDeDataDiverso(string data)
    {
        // Arrange
        // var dataValida = "1678-12-10T00:00:00.000Z";
        // Act
        var resultado = validador.ValidaFormatoData(data);

        // Assert
        Assert.True(resultado.IsFailed);
        Assert.Equal("O formato de data é inválido.", resultado.Errors.First().Message);
    }

    [Fact]
    public void DeveImpedirUmaDataNascimentoQueRepresenteUmMenorDeIdade()
    {
        // Arrange
        // var validador = new UsuarioException();
        var dataNascimentoMenorIdade = new DateTime(2016, 1, 1);

        // Act
        var resultado = validador.ValidarDataNascimento(dataNascimentoMenorIdade);

        // Assert
        Assert.True(resultado.IsFailed);
        Assert.Equal("A data de nascimento informada representa um menor de idade.", resultado.Errors.First().Message);
    }

    [Fact]
    public void DeveValidarUmaDataNascimentoValidaMaiorIdade()
    {
        // Arrange
        var dataNascimentoMaiorIdade = new DateTime(1999, 12, 27);

        // Act
        var resultado = validador.ValidarDataNascimento(dataNascimentoMaiorIdade);

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.Empty(resultado.Errors);
    }
    
}