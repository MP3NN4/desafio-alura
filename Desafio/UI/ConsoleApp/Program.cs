using System;
using System.Threading.Tasks;
using Desafio.Application.Services;
using Desafio.Domain.Repositories;
using Desafio.Domain.Services;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.WebDriver;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Desafio.UI.ConsoleApp
{
    /// <summary>
    /// Classe principal da aplicação console.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método principal que é o ponto de entrada da aplicação.
        /// </summary>
        /// <param name="args">Argumentos de linha de comando.</param>
        static async Task Main(string[] args)
        {
            // Configura a construção da configuração para ler o arquivo appsettings.json.
            // Este arquivo contém configurações de conexão e outras definições importantes.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configura o provedor de serviços, que gerencia a injeção de dependências.
            // Aqui, estamos registrando o ApplicationDbContext, repositórios e serviços.
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddTransient<ICourseRepository, CourseRepository>()
                .AddTransient<IWebDriverFactory, WebDriverFactory>()
                .AddTransient<ICourseService, CourseService>()
                .BuildServiceProvider();

            // Obtém o serviço de curso através do provedor de serviços.
            // O serviço é utilizado para buscar cursos que contenham o termo "RPA".
            var courseService = serviceProvider.GetService<ICourseService>();
            var courses = await courseService.GetCourses("RPA");

            // Verifica se algum curso foi encontrado.
            // Se nenhum curso for encontrado, exibe uma mensagem no console.
            if (courses.Count == 0)
            {
                Console.WriteLine("Nenhum curso foi encontrado.");
            }

            // Itera sobre os cursos encontrados e exibe suas informações no console.
            foreach (var course in courses)
            {
                Console.WriteLine($"Título: {course.Title}");
                Console.WriteLine($"Professor: {course.Professor}");
                Console.WriteLine($"Carga Horária: {course.CargaHoraria}");
                Console.WriteLine($"Descrição: {course.Descricao}");
                Console.WriteLine();
            }
        }
    }
}
