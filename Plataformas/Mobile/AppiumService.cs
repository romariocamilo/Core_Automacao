using Core_Automacao.Models.Mobile;
using OpenQA.Selenium.Appium.MultiTouch;
using System.Collections.ObjectModel;

namespace Core_Automacao.Plataformas.Mobile
{
    public class AppiumService : AppiumConfig
    {
        private PlataformaMobile _plataformaMobile;

        public AppiumService(PlataformaMobile plataformaMobile) : base(plataformaMobile)
        {
            _plataformaMobile = plataformaMobile;
        }

        // Métodos que buscam elementos IOS e ANDROID
        #region  Métodos de Busca

        private ElementoMobile BuscaPorId(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ElementoAndroid = driverAndroid.FindElementById(elementoMobile.IdentificadorAndroid);
                        elementoMobile.TextoObtido = elementoMobile.ElementoAndroid.Text;
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ElementoIOS = driverIos.FindElementById(elementoMobile.IdentificadorIos);
                        elementoMobile.TextoObtido = elementoMobile.ElementoIOS.Text;
                        return elementoMobile;
                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não encontrou o elemento com ID: \nIdentificador Android: {elementoMobile.IdentificadorAndroid} \nIdentificador iOS: {elementoMobile.IdentificadorIos} \n{ex.Message}");
            }
        }

        private ElementoMobile BuscaElementosPorId(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ListaElementoAndroid = driverAndroid.FindElementsById(elementoMobile.IdentificadorAndroid);
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ListaElementoIOS = driverIos.FindElementsById(elementoMobile.IdentificadorIos);
                        return elementoMobile;
                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não encontrou o elemento com ID: \nIdentificador Android: {elementoMobile.IdentificadorAndroid} \nIdentificador iOS: {elementoMobile.IdentificadorIos} \n{ex.Message}");
            }
        }

        private ElementoMobile BuscaPorIdAcessibilidade(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ElementoAndroid = driverAndroid.FindElementByAccessibilityId(elementoMobile.IdentificadorAndroid);
                        elementoMobile.TextoObtido = elementoMobile.ElementoAndroid.Text;
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ElementoIOS = driverIos.FindElementByAccessibilityId(elementoMobile.IdentificadorIos);
                        elementoMobile.TextoObtido = elementoMobile.ElementoIOS.Text;
                        return elementoMobile;
                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não encontrou o elemento com ID: \nIdentificador Android: {elementoMobile.IdentificadorAndroid} \nIdentificador iOS: {elementoMobile.IdentificadorIos} \n{ex.Message}");
            }
        }

        private ElementoMobile EM_TESTE_BuscaPorIdAcessibilidadeEParteDoTexto(ElementoMobile elementoMobile, string parteTextoParaBucar)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ListaElementoAndroid = (ReadOnlyCollection<OpenQA.Selenium.Appium.Android.AndroidElement>)driverAndroid.FindElementsByAccessibilityId(elementoMobile.IdentificadorAndroid);

                        foreach (var elementoAndroid in elementoMobile.ListaElementoAndroid)
                        {
                            if (elementoAndroid.Text.Contains(parteTextoParaBucar))
                            {
                                elementoMobile.ElementoAndroid = elementoAndroid;
                                elementoMobile.TextoObtido = elementoAndroid.Text;
                                break;
                            }
                        }

                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ListaElementoIOS = (ReadOnlyCollection<OpenQA.Selenium.Appium.iOS.IOSElement>)driverIos.FindElementsByAccessibilityId(elementoMobile.IdentificadorIos);

                        foreach (var elementoIos in elementoMobile.ListaElementoIOS)
                        {
                            if (elementoIos.Text.Contains(parteTextoParaBucar))
                            {
                                elementoMobile.ElementoIOS = elementoIos;
                                elementoMobile.TextoObtido = elementoIos.Text;
                                break;
                            }
                        }

                        return elementoMobile;

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Não encontrou o elemento com ID: \nIdentificador Android: {elementoMobile.IdentificadorAndroid} \nIdentificador iOS: {elementoMobile.IdentificadorIos} \n{ex.Message}");
            }
        }

        #endregion

        // Métodos de espera
        public void AguardaElementoSumirDaTela(ElementoMobile elementoMobile, int tempoDeEsperaSegundos)
        {
            TimeSpan esperaDriveOriginal;

            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    esperaDriveOriginal = driverAndroid.Manage().Timeouts().ImplicitWait;
                    driverAndroid.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(tempoDeEsperaSegundos);

                    try
                    {
                        while (true)
                        {
                            BuscaPorId(elementoMobile);
                        }
                    }
                    catch (Exception ex)
                    {
                        driverAndroid.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        new Exception(ex.Message + "\nElemento ficou oculto");
                    }
                    break;

                case PlataformaMobile.iOS:
                    esperaDriveOriginal = driverIos.Manage().Timeouts().ImplicitWait;
                    driverIos.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(tempoDeEsperaSegundos);

                    try
                    {
                        while (true)
                        {
                            BuscaPorId(elementoMobile);
                        }
                    }
                    catch (Exception ex)
                    {
                        driverIos.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        new Exception(ex.Message + "\nElemento ficou oculto");
                    }
                    break;
            }
        }


        #region  Métodos de Scroll

        public void ScrollCarroselParaDireitaPorIdParandoComTexto(ElementoMobile elementoMobileCarrossel, ElementoMobile elementoMobileCardsCarrossel, string textoCondicaoParada)
        {
            TouchAction acao = null;
            bool encontrouElemento = false;
            bool limiteParada = false;
            int tentativasScroll = 5;
            int contadorParada = 0;

            while (!encontrouElemento && !limiteParada)
            {
                int ondeComecaOCarrosselAlturaYVertical = 0;
                int ondeComecaOCarrosselLarguraXHorizontal = 0;
                int alturaInicialCarroselYVertical = 0;
                int laguraCarroselXHorizontal = 0;
                int meioCarroselYVertical = 0;
                int fimCarrosselAlturaYVertical = 0;
                int fimCarrosselLarguraXHorizontal = 0;

                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        acao = new TouchAction(driverAndroid);

                        elementoMobileCarrossel = BuscaPorId(elementoMobileCarrossel);

                        ondeComecaOCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoAndroid.Location.Y;
                        ondeComecaOCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoAndroid.Location.X;

                        alturaInicialCarroselYVertical = elementoMobileCarrossel.ElementoAndroid.Size.Height;
                        laguraCarroselXHorizontal = elementoMobileCarrossel.ElementoAndroid.Size.Width;

                        meioCarroselYVertical = elementoMobileCarrossel.ElementoAndroid.Size.Height / 2;

                        fimCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoAndroid.Location.Y + alturaInicialCarroselYVertical;
                        fimCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoAndroid.Location.X + laguraCarroselXHorizontal;

                        elementoMobileCardsCarrossel = BuscaElementosPorId(elementoMobileCardsCarrossel);
                        var elementoAndroid = elementoMobileCardsCarrossel.ListaElementoAndroid.FirstOrDefault(elem => elem.Text == textoCondicaoParada);

                        if (elementoAndroid is null)
                        {
                            acao.Press(fimCarrosselLarguraXHorizontal - 50, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Wait(1000)
                                .MoveTo(ondeComecaOCarrosselLarguraXHorizontal, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Release()
                                .Perform();

                            contadorParada++;

                            if (contadorParada == tentativasScroll) limiteParada = true;
                        }
                        else
                        {
                            encontrouElemento = true;
                        }
                        break;

                    case PlataformaMobile.iOS:
                        acao = new TouchAction(driverIos);

                        elementoMobileCarrossel = BuscaPorId(elementoMobileCarrossel);

                        ondeComecaOCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoIOS.Location.Y;
                        ondeComecaOCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoIOS.Location.X;

                        alturaInicialCarroselYVertical = elementoMobileCarrossel.ElementoIOS.Size.Height;
                        laguraCarroselXHorizontal = elementoMobileCarrossel.ElementoIOS.Size.Width;

                        meioCarroselYVertical = elementoMobileCarrossel.ElementoIOS.Size.Height / 2;

                        fimCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoIOS.Location.Y + alturaInicialCarroselYVertical;
                        fimCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoIOS.Location.X + laguraCarroselXHorizontal;

                        elementoMobileCardsCarrossel = BuscaElementosPorId(elementoMobileCardsCarrossel);
                        var elementoIos = elementoMobileCardsCarrossel.ListaElementoIOS.FirstOrDefault(elem => elem.Text == textoCondicaoParada);

                        if (elementoIos is null)
                        {
                            acao.Press(fimCarrosselLarguraXHorizontal - 50, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Wait(1000)
                                .MoveTo(ondeComecaOCarrosselLarguraXHorizontal, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Release()
                                .Perform();

                            contadorParada++;

                            if (contadorParada == tentativasScroll) limiteParada = true;
                        }
                        else
                        {
                            encontrouElemento = true;
                        }
                        break;

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        public void EM_TESTE_ScrollCarroselParaDireitaPorIdAcessibilidadeParandoComTexto(ElementoMobile elementoMobileCarrossel, ElementoMobile elementoMobileCardsCarrossel, string textoCondicaoParada)
        {
            TimeSpan esperaDriveOriginal = new TimeSpan();

            TouchAction acao = null;
            bool encontrouElemento = false;
            bool limiteParada = false;
            int tentativasScroll = 5;
            int contadorParada = 0;

            while (!encontrouElemento && !limiteParada)
            {
                int ondeComecaOCarrosselAlturaYVertical = 0;
                int ondeComecaOCarrosselLarguraXHorizontal = 0;
                int alturaInicialCarroselYVertical = 0;
                int laguraCarroselXHorizontal = 0;
                int meioCarroselYVertical = 0;
                int fimCarrosselAlturaYVertical = 0;
                int fimCarrosselLarguraXHorizontal = 0;

                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        acao = new TouchAction(driverAndroid);

                        elementoMobileCarrossel = BuscaPorIdAcessibilidade(elementoMobileCarrossel);

                        ondeComecaOCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoAndroid.Location.Y;
                        ondeComecaOCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoAndroid.Location.X;

                        alturaInicialCarroselYVertical = elementoMobileCarrossel.ElementoAndroid.Size.Height;
                        laguraCarroselXHorizontal = elementoMobileCarrossel.ElementoAndroid.Size.Width;

                        meioCarroselYVertical = elementoMobileCarrossel.ElementoAndroid.Size.Height / 2;

                        fimCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoAndroid.Location.Y + alturaInicialCarroselYVertical;
                        fimCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoAndroid.Location.X + laguraCarroselXHorizontal;

                        esperaDriveOriginal = driverAndroid.Manage().Timeouts().ImplicitWait;
                        driverAndroid.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                        elementoMobileCardsCarrossel = EM_TESTE_BuscaPorIdAcessibilidadeEParteDoTexto(elementoMobileCardsCarrossel, textoCondicaoParada);
                        driverAndroid.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;

                        var elementoAndroid = elementoMobileCardsCarrossel.ListaElementoAndroid.FirstOrDefault(elem => elem.Text == textoCondicaoParada);

                        if (elementoAndroid is null)
                        {
                            acao.Press(fimCarrosselLarguraXHorizontal - 50, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Wait(1000)
                                .MoveTo(ondeComecaOCarrosselLarguraXHorizontal, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Release()
                                .Perform();

                            contadorParada++;

                            if (contadorParada == tentativasScroll) limiteParada = true;
                        }
                        else
                        {
                            encontrouElemento = true;
                        }
                        break;

                    case PlataformaMobile.iOS:
                        acao = new TouchAction(driverIos);

                        elementoMobileCarrossel = BuscaPorIdAcessibilidade(elementoMobileCarrossel);

                        ondeComecaOCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoIOS.Location.Y;
                        ondeComecaOCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoIOS.Location.X;

                        alturaInicialCarroselYVertical = elementoMobileCarrossel.ElementoIOS.Size.Height;
                        laguraCarroselXHorizontal = elementoMobileCarrossel.ElementoIOS.Size.Width;

                        meioCarroselYVertical = elementoMobileCarrossel.ElementoIOS.Size.Height / 2;

                        fimCarrosselAlturaYVertical = elementoMobileCarrossel.ElementoIOS.Location.Y + alturaInicialCarroselYVertical;
                        fimCarrosselLarguraXHorizontal = elementoMobileCarrossel.ElementoIOS.Location.X + laguraCarroselXHorizontal;

                        esperaDriveOriginal = driverIos.Manage().Timeouts().ImplicitWait;
                        driverIos.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

                        elementoMobileCardsCarrossel = EM_TESTE_BuscaPorIdAcessibilidadeEParteDoTexto(elementoMobileCardsCarrossel, textoCondicaoParada);

                        driverIos.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;

                        var elementoIos = elementoMobileCardsCarrossel.ListaElementoIOS.FirstOrDefault(elem => elem.Text == textoCondicaoParada);

                        if (elementoIos is null)
                        {
                            acao.Press(fimCarrosselLarguraXHorizontal - 50, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Wait(1000)
                                .MoveTo(ondeComecaOCarrosselLarguraXHorizontal, fimCarrosselAlturaYVertical - meioCarroselYVertical)
                                .Release()
                                .Perform();

                            contadorParada++;

                            if (contadorParada == tentativasScroll) limiteParada = true;
                        }
                        else
                        {
                            encontrouElemento = true;
                        }
                        break;

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        public void ScrollDeElementoOrigemParaElementoDestino(ElementoMobile elementoMobileOrigem, ElementoMobile elementoMobileDestino)
        {
            TouchAction acao = null;

            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:

                    if (elementoMobileOrigem.ElementoAndroid is null)
                    {
                        throw new Exception("Elemento Android de origem não pode ser nulo, favor preencha o elemento para utilizar esse método");
                    }
                    if (elementoMobileDestino.ElementoAndroid is null)
                    {
                        throw new Exception("Elemento Android de destino não pode ser nulo, favor preencha o elemento para utilizar esse método");
                    }

                    acao = new TouchAction(driverAndroid);

                    acao.LongPress(elementoMobileDestino.ElementoAndroid.Location.X, elementoMobileDestino.ElementoAndroid.Location.Y)
                        .Wait(1000)
                        .MoveTo(elementoMobileOrigem.ElementoAndroid.Location.X, elementoMobileOrigem.ElementoAndroid.Location.Y)
                        .Release();

                    acao.Perform();

                    break;

                case PlataformaMobile.iOS:
                    if (elementoMobileOrigem.ElementoIOS is null)
                    {
                        throw new Exception("Elemento iOS de origem não pode ser nulo, favor preencha o elemento para utilizar esse método");
                    }
                    if (elementoMobileDestino.ElementoIOS is null)
                    {
                        throw new Exception("Elemento iOS de destino não pode ser nulo, favor preencha o elemento para utilizar esse método");
                    }

                    acao = new TouchAction(driverIos);

                    acao.Press(elementoMobileDestino.ElementoIOS.Location.X, elementoMobileDestino.ElementoIOS.Location.Y)
                        .Wait(1000)
                        .MoveTo(elementoMobileOrigem.ElementoIOS.Location.X, elementoMobileOrigem.ElementoIOS.Location.Y)
                        .Release()
                        .Perform();

                    break;
            }
        }

        public void ScrollEmListaDeElementosParandoComTexto(ElementoMobile elementoMobile, string textoCondicaoParada)
        {
            TouchAction acao = null;
            bool encontrouElemento = false;
            int contadorParada = 0;

            while (!encontrouElemento && contadorParada <= 5)
            {
                BuscaElementosPorId(elementoMobile);

                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:

                        foreach (var elemento in elementoMobile.ListaElementoAndroid)
                        {
                            if (elemento.Text.Contains(textoCondicaoParada))
                            {
                                encontrouElemento = true;
                                elementoMobile.TextoObtido = elemento.Text;
                                break;
                            }
                        }

                        if (!encontrouElemento)
                        {
                            acao = new TouchAction(driverAndroid);

                            acao.Press(elementoMobile.ListaElementoAndroid.Last().Location.X, elementoMobile.ListaElementoAndroid.Last().Location.Y)
                                .Wait(1000)
                                .MoveTo(elementoMobile.ListaElementoAndroid.First().Location.X, elementoMobile.ListaElementoAndroid.First().Location.Y)
                                .Release()
                                .Perform();

                            contadorParada++;
                        }

                        break;

                    case PlataformaMobile.iOS:

                        foreach (var elemento in elementoMobile.ListaElementoIOS)
                        {
                            if (elemento.Text.Contains(textoCondicaoParada))
                            {
                                encontrouElemento = true;
                                elementoMobile.TextoObtido = elemento.Text;
                                break;
                            }
                        }

                        if (!encontrouElemento)
                        {
                            acao = new TouchAction(driverIos);

                            acao.Press(elementoMobile.ListaElementoIOS.Last().Location.X, elementoMobile.ListaElementoIOS.Last().Location.Y)
                                .Wait(1000)
                                .MoveTo(elementoMobile.ListaElementoIOS.First().Location.X, elementoMobile.ListaElementoIOS.First().Location.Y)
                                .Release()
                                .Perform();

                            contadorParada++;
                        }

                        break;
                }
            }

            if (!elementoMobile.TextoObtido.Contains(elementoMobile.TextoEsperado))
            {
                throw new Exception($"Não foi encontrado o elemento com os identificadores:\nAndroid {elementoMobile.IdentificadorAndroid}\niOS: {elementoMobile.IdentificadorIos}\n Com o textoCondicaoParada: {elementoMobile.TextoEsperado}");
            }
        }

        public void ScrollDeBaixoParaCima()
        {
            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    int startXAndroid = driverAndroid.Manage().Window.Size.Width / 2;
                    int startYAndroid = (int)(driverAndroid.Manage().Window.Size.Width * 1.4);
                    int endYAndroid = (int)(driverAndroid.Manage().Window.Size.Width * 0.1);

                    TouchAction touchActionAndroid = new TouchAction(driverAndroid);
                    touchActionAndroid.LongPress(startXAndroid, startYAndroid)
                                .Wait(1000)
                                .MoveTo(startXAndroid, endYAndroid)
                                .Release()
                                .Perform();
                    break;

                case PlataformaMobile.iOS:
                    int startX = driverIos.Manage().Window.Size.Width / 2;
                    int startY = (int)(driverIos.Manage().Window.Size.Width * 1.5);
                    int endY = (int)(driverIos.Manage().Window.Size.Width * 0.1);

                    TouchAction touchAction = new TouchAction(driverIos);
                    touchAction.Press(startX, startY)
                                .Wait(1000)
                                .MoveTo(startX, endY)
                                .Release()
                                .Perform();
                    break;
            }
        }

        #endregion

    }
}