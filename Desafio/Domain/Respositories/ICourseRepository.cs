using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio.Domain.Entities;

namespace Desafio.Domain.Repositories
{
    /// <summary>
    /// Interface para o repositório de cursos, definindo os métodos necessários para manipulação de cursos.
    /// </summary>
    public interface ICourseRepository
    {
        /// <summary>
        /// Salva uma lista de cursos no repositório.
        /// </summary>
        /// <param name="courses">Lista de cursos a serem salvos.</param>
        /// <returns>Uma tarefa representando a operação assíncrona.</returns>
        Task SaveCourses(List<Course> courses);
    }
}
