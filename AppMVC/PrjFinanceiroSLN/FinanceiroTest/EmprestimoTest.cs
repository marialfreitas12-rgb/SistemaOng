
using PrjFinanceiro.Services;

namespace FinanceiroTest
{
    public class EmprestimoTest
    {
           

        [Fact]
        public void CalculoJurosSimples_Retorna1100()
        {
            //arrange
            decimal valorEmprestado = 1000;
            double taxa = 0.10;
            decimal valorEsperado = 100;

            EmprestimoService emprestimo= new EmprestimoService();
            decimal resultado = emprestimo.CalcularJurosSimples(valorEmprestado, taxa);

            Assert.Equal(valorEsperado, resultado);
       


        }
    }
}