using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Core_Automacao.Models.Mobile
{
    public class ElementoMobile
    {
        public TipoIdentificador TipoIdentificadorAndroid { get; private set; }
        public TipoIdentificador TipoIdentificadorIos { get; private set; }
        public string IdentificadorAndroid { get; private set; }
        public string IdentificadorIos { get; private set; }
        public string TextoEsperadoAndroid { get; private set; }
        public string TextoEsperadoIos { get; private set; }
        public string TextoObtido { get; set; }
        public AndroidElement ElementoAndroid { get; set; }
        public IOSElement ElementoIOS { get; set; }
        public bool ElementoVisivelNaTela { get; set; }

        public ElementoMobile(TipoIdentificador tipoIdentificadorAndroid,TipoIdentificador tipoIdentificadorIos,  string identificadorAndroid, string identificadorIos, string textoEsperadoAndroid, string textoEsperadIos)
        {
            TipoIdentificadorAndroid = tipoIdentificadorAndroid;
            TipoIdentificadorIos = tipoIdentificadorIos;
            IdentificadorAndroid = identificadorAndroid;
            IdentificadorIos = identificadorIos;
            TextoEsperadoAndroid = textoEsperadoAndroid;
            TextoEsperadoIos = textoEsperadIos;
        }

        // REMOVER
        public string TextoEsperado { get; private set; }
        public ReadOnlyCollection<AndroidElement> ListaElementoAndroid { get; set; }
        public ReadOnlyCollection<IOSElement> ListaElementoIOS { get; set; }

        // public ElementoMobile(string identificadorAndroid, string identificadorIos = "", string textoEsperado = "")
        // {
        //     IdentificadorAndroid = identificadorAndroid;
        //     IdentificadorIos = identificadorIos;
        //     TextoEsperado = textoEsperado;
        // }
    }
}