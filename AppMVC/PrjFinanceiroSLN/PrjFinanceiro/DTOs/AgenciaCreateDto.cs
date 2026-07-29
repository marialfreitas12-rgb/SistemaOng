using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.DTOs
{
    public class AgenciaCreateDto
    {
        [Required(ErrorMessage = "O nome da agência é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado/UF é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve conter exatamente 2 caracteres.")]
        public string EstadoUF { get; set; }
    }
}
