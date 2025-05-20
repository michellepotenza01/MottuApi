using MottuApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MottuApi.Data
{
    public class MottuDbContext : DbContext
    {
        public MottuDbContext(DbContextOptions<MottuDbContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento entre Moto e Patio
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Patio)  // A moto tem um pátio
                .WithMany(p => p.Motos)  // O pátio pode ter várias motos
                .HasForeignKey(m => m.NomePatio)  // A chave estrangeira é o NomePatio
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento entre Moto e Funcionario
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Funcionario)  // A moto é associada a um funcionário
                .WithMany()  // Não há necessidade de uma coleção de motos no Funcionario
                .HasForeignKey(m => m.UsuarioFuncionario);  // Chave estrangeira de Funcionário

            base.OnModelCreating(modelBuilder);
        }
    }
}
