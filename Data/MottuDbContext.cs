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
                .OnDelete(DeleteBehavior.Cascade); // Se a moto for removida, o pátio não será alterado

            // Relacionamento entre Moto e Funcionario
            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Funcionario)  // A moto é associada a um funcionário
                .WithMany()  // Não há necessidade de uma coleção de motos no Funcionario
                .HasForeignKey(m => m.UsuarioFuncionario);  // Chave estrangeira de Funcionário

            // Relacionamento entre Funcionario e Patio
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Patio)  // O funcionário é associado a um pátio
                .WithMany()  // Não há necessidade de uma coleção de funcionários no Patio
                .HasForeignKey(f => f.NomePatio)  // Chave estrangeira é o NomePatio
                .OnDelete(DeleteBehavior.Cascade);  // Se o funcionário for removido, o pátio não será alterado

            base.OnModelCreating(modelBuilder);
        }
    }
}
