using System.ComponentModel.DataAnnotations;

namespace PrjFinanceiro.Models
{
    public class Animal
    {
        [Key]
        public int Idade { get; set; }
        public int Dataresgate { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Status { get; set; }
        public string Porte { get; set; }
        public string Raça { get; set; }
        public int IdAnimal { get; set; }
       
           


    }
}
