using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Desafio.Infrastructure.WebDriver
{
    /// <summary>
    /// Interface para a fábrica de WebDriver, definindo o método para criar instâncias do IWebDriver.
    /// </summary>
    public interface IWebDriverFactory
    {
        /// <summary>
        /// Cria uma nova instância de IWebDriver.
        /// </summary>
        /// <returns>Uma nova instância de IWebDriver.</returns>
        IWebDriver Create();
    }

    /// <summary>
    /// Implementação da fábrica de WebDriver, responsável por criar instâncias do ChromeDriver.
    /// </summary>
    public class WebDriverFactory : IWebDriverFactory
    {
        /// <summary>
        /// Cria uma nova instância de ChromeDriver.
        /// </summary>
        /// <returns>Uma nova instância de ChromeDriver.</returns>
        public IWebDriver Create()
        {
            return new ChromeDriver();
        }
    }
}
