namespace cadastroSqa.Services.DTO
{
    public class ReadUsuarioDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Endereco { get; set; }

        public string Sexo { get; set; }
    }
}