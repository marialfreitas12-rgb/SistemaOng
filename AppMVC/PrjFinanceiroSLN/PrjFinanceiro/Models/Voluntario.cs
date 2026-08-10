using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{

    public class Voluntario
    {
        [Key]
        public int Idvoluntario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Disponibilidade { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

    }
}
