using OpenQA.Selenium;
using Core_Automacao.Models.Web;

namespace Core_Automacao.Plataformas.Web
{
    public class WebService : WebConfig
    {
        public WebService(string browser, string pathDriver, string[] argumentos = null) : base(browser, pathDriver, argumentos) { }

        public void NavegaParaUrl(string url) 
        {
            driver.Navigate().GoToUrl(url);
        }

        public IWebElement BuscaPorId(ElementoWeb elementoWeb) 
        {
            return driver.FindElement(By.Id(elementoWeb.IdentificadorWeb));
        }

        public IWebElement BuscaPorName(ElementoWeb elementoWeb) 
        {
            return driver.FindElement(By.Name(elementoWeb.IdentificadorWeb));
        }

        public void ClicaPorId(ElementoWeb elementoWeb) 
        {
            BuscaPorId(elementoWeb).Click();
        }

        public void ClicaPorName(ElementoWeb elementoWeb) 
        {
            BuscaPorName(elementoWeb).Click();
        }

        public void EscrevePorId(ElementoWeb elementoWeb, string textoParaEscrever) 
        {
            BuscaPorId(elementoWeb).SendKeys(textoParaEscrever);
        }

        public void EscrevePorName(ElementoWeb elementoWeb, string textoParaEscrever) 
        {
            BuscaPorName(elementoWeb).SendKeys(textoParaEscrever);
        }

        public void Dispose()
        {
            driver.Dispose();
        }
    }
}