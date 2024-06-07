using System.ComponentModel.DataAnnotations;

namespace Desafio.Domain.Entities
{
    /// <summary>
    /// Representa um curso com seus detalhes.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Identificador único do curso.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Título do curso.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nome do professor do curso.
        /// </summary>
        public string Professor { get; set; }

        /// <summary>
        /// Carga horária do curso.
        /// </summary>
        public string CargaHoraria { get; set; }

        /// <summary>
        /// Descrição do curso.
        /// </summary>
        public string Descricao { get; set; }
    }
}
