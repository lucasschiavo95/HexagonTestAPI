using System.ComponentModel.DataAnnotations;

namespace HexagonTest.API.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Idade é obrigatório")]
        [Range(0, 120, ErrorMessage = "A idade deve estar entre 0 e 120")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo Estado Civil é obrigatório")]
        [StringLength(20)]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [StringLength(14)]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF deve estar no formato 000.000.000-00")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(2)]
        public string Estado { get; set; }
    }
}