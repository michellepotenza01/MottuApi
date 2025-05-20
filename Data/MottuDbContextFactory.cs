using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MottuApi.Data
{
    public class MottuDbContextFactory : IDesignTimeDbContextFactory<MottuDbContext>
    {
        public MottuDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MottuDbContext>();

            // Usando a configuração do appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Pega o diretório atual
                .AddJsonFile("appsettings.json")  // Carrega o arquivo de configurações
                .Build();

            // Obtém a string de conexão para o Oracle
            var connectionString = configuration.GetConnectionString("OracleConnection");

            // Configura a conexão com o banco Oracle
            optionsBuilder.UseOracle(connectionString);

            return new MottuDbContext(optionsBuilder.Options);
        }
    }
}
