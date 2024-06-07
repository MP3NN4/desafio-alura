using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Desafio.Infrastructure.Data
{
    /// <summary>
    /// Fábrica para criar instâncias do ApplicationDbContext em tempo de design.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Cria uma nova instância do ApplicationDbContext.
        /// </summary>
        /// <param name="args">Argumentos passados para o método de criação.</param>
        /// <returns>Uma nova instância de ApplicationDbContext.</returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Configura o construtor de configuração para ler o arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Cria o construtor de opções do DbContext e define a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            // Retorna uma nova instância do ApplicationDbContext configurada com as opções
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
