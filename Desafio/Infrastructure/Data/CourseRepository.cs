using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;

namespace Desafio.Infrastructure.Data
{
    /// <summary>
    /// Implementação do repositório de cursos, responsável por realizar operações de persistência
    /// relacionadas à entidade Course no banco de dados.
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor da classe CourseRepository.
        /// </summary>
        /// <param name="context">Contexto do Entity Framework.</param>
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Salva uma lista de cursos no banco de dados.
        /// </summary>
        /// <param name="courses">Lista de cursos a serem salvos.</param>
        /// <returns>Uma tarefa representando a operação assíncrona.</returns>
        public async Task SaveCourses(List<Course> courses)
        {
            _context.Courses.AddRange(courses);
            await _context.SaveChangesAsync();
        }
    }
}
