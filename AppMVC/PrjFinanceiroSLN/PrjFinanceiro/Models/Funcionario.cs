using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace PrjFinanceiro.Models
{
    public class Funcionario
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string EstadoUF { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }

    }
}
