using HexagonTest.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HexagonTest.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configurações do modelo de Pessoa
            modelBuilder.Entity<Pessoa>()
                .HasIndex(p => p.CPF)
                .IsUnique();
        }
    }
}