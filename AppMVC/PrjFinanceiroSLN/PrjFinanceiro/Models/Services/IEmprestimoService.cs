namespace PrjFinanceiro.Models.Services
{
    public interface IEmprestimoService
    {
        public decimal CalcularJurosSimples(decimal valor, double taxa);
    }
}
