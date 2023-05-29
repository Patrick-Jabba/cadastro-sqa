using System.ComponentModel.DataAnnotations;

namespace cadastroSqa.Services.DTO
{
    public class AddUsuarioDto
    {
        [StringLength(40, MinimumLength =3, ErrorMessage ="O nome deve conter entre 3 a 40 caracteres")]
        [Required(ErrorMessage = "O nome é um campo obrigatório.", AllowEmptyStrings = false)] 
        public string Name { get; set; }

        [StringLength(14)]
        [Required(ErrorMessage = "O CPF é um campo obrigatório.", AllowEmptyStrings = false)] 
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido!")]
        public string CPF { get; set; }
        
        [Required(ErrorMessage ="O campo de data de nascimento é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        public DateTime DataNascimento { get; set; }

        [StringLength(255)]
        public string Endereco { get; set; }

        [StringLength(9)]
        public string Sexo { get; set; }
    }
}
