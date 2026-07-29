namespace PrjFinanceiro.Services
{
    public interface IEmprestimoService
    {
        decimal CalcularJurosSimples(decimal valor,  double taxa);

    }
}
