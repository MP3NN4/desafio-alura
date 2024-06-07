using Microsoft.EntityFrameworkCore;
using Desafio.Domain.Entities;

namespace Desafio.Infrastructure.Data
{
    /// <summary>
    /// Contexto do Entity Framework responsável por gerenciar a conexão com o banco de dados
    /// e fornecer acesso às entidades do domínio.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Construtor do ApplicationDbContext.
        /// </summary>
        /// <param name="options">Opções para configurar o contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet representando a entidade Course no banco de dados.
        /// </summary>
        public DbSet<Course> Courses { get; set; }
    }
}
