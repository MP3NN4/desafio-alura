using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio.Domain.Entities;
using Desafio.Domain.Repositories;
using Desafio.Domain.Services;
using Desafio.Infrastructure.WebDriver;
using OpenQA.Selenium;

namespace Desafio.Application.Services
{
    /// <summary>
    /// Implementação do serviço de cursos, responsável por buscar cursos no site da Alura,
    /// extrair informações detalhadas de cada curso e salvar essas informações no repositório de cursos.
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IWebDriverFactory _webDriverFactory;

        /// <summary>
        /// Construtor da classe CourseService.
        /// </summary>
        /// <param name="courseRepository">Repositório de cursos.</param>
        /// <param name="webDriverFactory">Fábrica de WebDriver.</param>
        public CourseService(ICourseRepository courseRepository, IWebDriverFactory webDriverFactory)
        {
            _courseRepository = courseRepository;
            _webDriverFactory = webDriverFactory;
        }

        /// <summary>
        /// Busca cursos no site da Alura com base no termo de pesquisa fornecido,
        /// extrai informações detalhadas e salva essas informações no repositório de cursos.
        /// </summary>
        /// <param name="searchTerm">Termo de pesquisa para buscar cursos.</param>
        /// <returns>Lista de cursos encontrados.</returns>
        public async Task<List<Course>> GetCourses(string searchTerm)
        {
            // Cria uma instância do WebDriver
            var driver = _webDriverFactory.Create();

            // Navega para a página principal da Alura
            driver.Navigate().GoToUrl("https://www.alura.com.br/");

            // Localiza a caixa de busca e insere o termo de pesquisa
            IWebElement searchBox = driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));
            searchBox.SendKeys(searchTerm);
            searchBox.Submit();

            // Aguarda 3 segundos para garantir que a página carregue completamente
            System.Threading.Thread.Sleep(3000);

            // Localiza os links dos cursos na página de resultados
            var courseLinks = driver.FindElements(By.CssSelector(".busca-resultado a"));

            if (courseLinks.Count == 0)
            {
                Console.WriteLine("Nenhum link de curso encontrado na página de resultados.");
            }

            var courses = new List<Course>();
            foreach (var courseLink in courseLinks)
            {
                try
                {
                    // Verifica se o texto do link contém "Curso"
                    if (!courseLink.Text.StartsWith("Curso")) continue;

                    // Obtém a URL do curso e abre em uma nova aba
                    string courseUrl = courseLink.GetAttribute("href");
                    Console.WriteLine($"Abrindo link do curso: {courseUrl}");

                    ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    driver.Navigate().GoToUrl(courseUrl);

                    // Aguarda 3 segundos para garantir que a página do curso carregue completamente
                    System.Threading.Thread.Sleep(3000);

                    // Extrai as informações do curso
                    var titleText = driver.FindElement(By.CssSelector(".curso-banner-course-title"));
                    var categoryText = driver.FindElement(By.CssSelector(".course--banner-text-category"));
                    var titleElement = titleText.Text + " " +categoryText.Text;
                    var professorElement = driver.FindElement(By.CssSelector(".instructor-title--name"));
                    var cargaHorariaElement = driver.FindElement(By.CssSelector(".courseInfo-card-wrapper-infos"));
                    var descricaoElement = driver.FindElement(By.CssSelector(".course-list"));

                    var title = titleElement ?? "N/A";
                    var professor = professorElement?.Text ?? "N/A";
                    var cargaHoraria = cargaHorariaElement?.Text ?? "N/A";
                    var descricao = descricaoElement?.Text ?? "N/A";

                    // Adiciona o curso à lista
                    courses.Add(new Course
                    {
                        Title = title,
                        Professor = professor,
                        CargaHoraria = cargaHoraria,
                        Descricao = descricao
                    });

                    Console.WriteLine($"Curso encontrado: {title}");

                    // Fecha a aba do curso e volta para a aba de resultados
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine($"Elemento não encontrado: {e.Message}");
                    // Fecha a aba e volta para a aba de resultados se ocorrer um erro
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
            }

            // Fecha o WebDriver
            driver.Quit();

            // Salva os cursos no repositório
            await _courseRepository.SaveCourses(courses);
            return courses;
        }
    }
}
