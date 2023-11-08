using OpenQA.Selenium;

namespace Core_Automacao.Models.Web
{
    public class ElementoWeb
    {
        public string IdentificadorWeb { get; private set; }
        public string TextoEsperado { get; private set; }
        public string TextoObtido { get; set; }
        public IWebElement ElementoWebBrowser { get; set; }
        public bool ElementoVisivelNaTela { get; set; }

        public ElementoWeb(string identificadorWeb, string textoEsperado)
        {
            IdentificadorWeb = identificadorWeb;
            TextoEsperado = textoEsperado;
        }
    }
}