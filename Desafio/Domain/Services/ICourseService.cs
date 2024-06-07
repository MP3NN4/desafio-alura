using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio.Domain.Entities;

namespace Desafio.Domain.Services
{
    /// <summary>
    /// Interface para o serviço de cursos, definindo os métodos necessários para buscar e manipular cursos.
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Busca cursos com base no termo de pesquisa fornecido.
        /// </summary>
        /// <param name="searchTerm">Termo de pesquisa para buscar cursos.</param>
        /// <returns>Uma lista de cursos encontrados.</returns>
        Task<List<Course>> GetCourses(string searchTerm);
    }
}
