namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; }

        public Cliente Cliente { get; set; }
        public Agencia Agencia { get; set; }

    }
}
