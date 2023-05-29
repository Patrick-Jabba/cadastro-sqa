using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cadastroSqa.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Required]
        [Column("id_usuario")]
        public int Id { get; set; }

        [Column("nome")]
        [StringLength(40, MinimumLength =3, ErrorMessage ="O nome deve conter entre 3 a 40 caracteres")]
        [Required(ErrorMessage = "O nome é um campo obrigatório.", AllowEmptyStrings = false)] 
        public string Name { get; set; }

        [Column("cpf")]
        [StringLength(14)]
        [Required(ErrorMessage = "O CPF é um campo obrigatório.", AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido!")]
        public string CPF { get; set; }

        [Column("data_nascimento")]
        [Required(ErrorMessage ="O campo de data de nascimento é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        public DateTime DataNascimento { get; set; }

        [Column("endereco")]
        [StringLength(255)]
        public string Endereco { get; set; }

        [Column("sexo")]
        [StringLength(9)]
        public string Sexo { get; set; }

    }
}
