
using Microsoft.EntityFrameworkCore;

namespace PrjFinanceiro.Models

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agencia> Agencia { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Estado> Estado { get; set; }

    }
}
