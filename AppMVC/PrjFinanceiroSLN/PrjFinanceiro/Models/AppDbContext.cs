
using Microsoft.EntityFrameworkCore;

namespace PrjFinanceiro.Models

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Animal> Animal { get; set; }
        public DbSet<Voluntario> Voluntario { get; set; }
        

    }
}
