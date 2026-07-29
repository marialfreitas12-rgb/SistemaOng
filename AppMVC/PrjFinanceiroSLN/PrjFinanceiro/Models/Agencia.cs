using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Agencia
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string EstadoUF { get; set; }
    }
}
