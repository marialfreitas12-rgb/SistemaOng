using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Doacao
    {
        [Key]
        public int IdDoacao { get; set; }

        public int DataDoacao { get; set; }

        public string Status { get; set; }

        public int IdAnimal { get; set; }
        public int IdPessoa { get; set; }
    }
}