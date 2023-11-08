using System.ComponentModel;
using Core_Automacao.Models.Mobile;
using OpenQA.Selenium.Appium.Android;

namespace Core_Automacao.Plataformas.Mobile
{
    public class AppiumServiceNew : AppiumService
    {
        private PlataformaMobile _plataformaMobile;

        public AppiumServiceNew(PlataformaMobile plataformaMobile) : base(plataformaMobile)
        {
            _plataformaMobile = plataformaMobile;
        }

        // Métodos que buscam elementos IOS e ANDROID
        #region  Métodos de Busca

        public ElementoMobile BuscaElementoMobile(ElementoMobile elementoMobile)
        {
            if (_plataformaMobile is PlataformaMobile.Android)
            {
                switch (elementoMobile.TipoIdentificadorAndroid)
                {
                    case TipoIdentificador.Id:
                        return BuscaElementoPorId(elementoMobile);

                    case TipoIdentificador.IdAcessibilidade:
                        return BuscaElementoPorIdAcessibilidade(elementoMobile);

                    case TipoIdentificador.Xpath:
                        return BuscaElementoPorXpath(elementoMobile);

                    default:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Tipo do identificador informado no elementoMobile é inválido.\n Tipo identificador informado: {elementoMobile.TipoIdentificadorAndroid}");
                }
            }
            else
            {
                switch (elementoMobile.TipoIdentificadorIos)
                {
                    case TipoIdentificador.Id:
                        return BuscaElementoPorId(elementoMobile);

                    case TipoIdentificador.IdAcessibilidade:
                        return BuscaElementoPorIdAcessibilidade(elementoMobile);

                    case TipoIdentificador.Xpath:
                        return BuscaElementoPorXpath(elementoMobile);

                    default:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Tipo do identificador informado no elementoMobile é inválido.\n Tipo identificador informado: {elementoMobile.TipoIdentificadorIos}");
                }
            }
        }

        public ElementoMobile BuscaElementoMobileDaListaPeloTextoDesejado(List<ElementoMobile> listaElementoMobile, string textoParaCliclar)
        {
            try
            {
                if (listaElementoMobile.FirstOrDefault(el => el.TextoObtido.Contains(textoParaCliclar)) is not null)
                {
                    return listaElementoMobile.FirstOrDefault(el => el.TextoObtido.Contains(textoParaCliclar));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não foi encontrado um elemento na lista que continha o texto {textoParaCliclar}");
            }
        }

        private ElementoMobile BuscaElementoPorId(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ElementoAndroid = driverAndroid.FindElementById(elementoMobile.IdentificadorAndroid);
                        elementoMobile.TextoObtido = elementoMobile.ElementoAndroid.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ElementoIOS = driverIos.FindElementById(elementoMobile.IdentificadorIos);
                        elementoMobile.TextoObtido = elementoMobile.ElementoIOS.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    default:
                        throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento ANDROID com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorAndroid}.\nExceção Oirignal: {ex.Message}");

                    case PlataformaMobile.iOS:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento iOS com identificador {elementoMobile.TipoIdentificadorIos} de valor {elementoMobile.IdentificadorIos}.\nExceção Oirignal: {ex.Message}");

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        private ElementoMobile BuscaElementoPorIdAcessibilidade(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ElementoAndroid = driverAndroid.FindElementByAccessibilityId(elementoMobile.IdentificadorAndroid);
                        elementoMobile.TextoObtido = elementoMobile.ElementoAndroid.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ElementoIOS = driverIos.FindElementByAccessibilityId(elementoMobile.IdentificadorIos);
                        elementoMobile.TextoObtido = elementoMobile.ElementoIOS.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    default:
                        throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento ANDROID com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorAndroid}.\nExceção Oirignal: {ex.Message}");

                    case PlataformaMobile.iOS:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento iOS com identificador {elementoMobile.TipoIdentificadorIos} de valor {elementoMobile.IdentificadorIos}.\nExceção Oirignal: {ex.Message}");

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        private ElementoMobile BuscaElementoPorXpath(ElementoMobile elementoMobile)
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        elementoMobile.ElementoAndroid = driverAndroid.FindElementByXPath(elementoMobile.IdentificadorAndroid);
                        elementoMobile.TextoObtido = elementoMobile.ElementoAndroid.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    case PlataformaMobile.iOS:
                        elementoMobile.ElementoIOS = driverIos.FindElementByXPath(elementoMobile.IdentificadorIos);
                        elementoMobile.TextoObtido = elementoMobile.ElementoIOS.Text;
                        elementoMobile.ElementoVisivelNaTela = true;
                        return elementoMobile;

                    default:
                        throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento ANDROID com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorAndroid}.\nExceção Oirignal: {ex.Message}");

                    case PlataformaMobile.iOS:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento iOS com identificador {elementoMobile.TipoIdentificadorIos} de valor {elementoMobile.IdentificadorIos}.\nExceção Oirignal: {ex.Message}");

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }


        public List<ElementoMobile> BuscaVariosElementoMobile(ElementoMobile elementoMobile)
        {
            if (_plataformaMobile is PlataformaMobile.Android)
            {
                switch (elementoMobile.TipoIdentificadorAndroid)
                {
                    case TipoIdentificador.Id:
                        return BuscaVariosElementoPorId(elementoMobile);

                    case TipoIdentificador.Class:
                        return BuscaVariosElementoPorClasse(elementoMobile);

                    default:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Tipo do identificador informado no elementoMobile é inválido.\n Tipo identificador informado: {elementoMobile.TipoIdentificadorAndroid}");
                }
            }
            else
            {
                switch (elementoMobile.TipoIdentificadorIos)
                {
                    case TipoIdentificador.Id:
                        return BuscaVariosElementoPorId(elementoMobile);

                    case TipoIdentificador.Class:
                        return BuscaVariosElementoPorClasse(elementoMobile);

                    default:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Tipo do identificador informado no elementoMobile é inválido.\n Tipo identificador informado: {elementoMobile.TipoIdentificadorIos}");
                }
            }
        }

        private List<ElementoMobile> BuscaVariosElementoPorId(ElementoMobile elementoMobile)
        {
            try
            {
                ElementoMobile elementoMobileParaInserirNaLista;
                List<ElementoMobile> listaElementoMobile = new List<ElementoMobile>();

                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:

                        foreach (var elementoMobileClass in driverAndroid.FindElementsById(elementoMobile.IdentificadorAndroid))
                        {
                            elementoMobileParaInserirNaLista = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid,
                                                                                    elementoMobile.TipoIdentificadorIos,
                                                                                    elementoMobile.IdentificadorAndroid,
                                                                                    elementoMobile.IdentificadorIos,
                                                                                    elementoMobile.TextoEsperadoAndroid,
                                                                                    elementoMobile.TextoEsperadoIos)
                            {
                                ElementoAndroid = elementoMobileClass,
                                TextoObtido = elementoMobileClass.Text,
                                ElementoVisivelNaTela = true
                            };

                            listaElementoMobile.Add(elementoMobileParaInserirNaLista);
                        }

                        return listaElementoMobile;

                    case PlataformaMobile.iOS:

                        foreach (var elementoMobileClass in driverIos.FindElementsById(elementoMobile.IdentificadorIos))
                        {
                            elementoMobileParaInserirNaLista = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid,
                                                                                    elementoMobile.TipoIdentificadorIos,
                                                                                    elementoMobile.IdentificadorAndroid,
                                                                                    elementoMobile.IdentificadorIos,
                                                                                    elementoMobile.TextoEsperadoAndroid,
                                                                                    elementoMobile.TextoEsperadoIos)
                            {
                                ElementoIOS = elementoMobileClass,
                                TextoObtido = elementoMobileClass.Text,
                                ElementoVisivelNaTela = true
                            };

                            listaElementoMobile.Add(elementoMobileParaInserirNaLista);
                        }

                        return listaElementoMobile;

                    default:
                        throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento ANDROID com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorAndroid}.\nExceção Oirignal: {ex.Message}");

                    case PlataformaMobile.iOS:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento iOS com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorIos}.\nExceção Oirignal: {ex.Message}");

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        private List<ElementoMobile> BuscaVariosElementoPorClasse(ElementoMobile elementoMobile)
        {
            try
            {
                ElementoMobile elementoMobileParaInserirNaLista;
                List<ElementoMobile> listaElementoMobile = new List<ElementoMobile>();

                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:

                        foreach (var elementoMobileClass in driverAndroid.FindElementsByClassName(elementoMobile.IdentificadorAndroid))
                        {
                            elementoMobileParaInserirNaLista = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid,
                                                                                    elementoMobile.TipoIdentificadorIos,
                                                                                    elementoMobile.IdentificadorAndroid,
                                                                                    elementoMobile.IdentificadorIos,
                                                                                    elementoMobile.TextoEsperadoAndroid,
                                                                                    elementoMobile.TextoEsperadoIos)
                            {
                                ElementoAndroid = elementoMobileClass,
                                TextoObtido = elementoMobileClass.Text,
                                ElementoVisivelNaTela = true
                            };

                            listaElementoMobile.Add(elementoMobileParaInserirNaLista);
                        }

                        return listaElementoMobile;

                    case PlataformaMobile.iOS:

                        foreach (var elementoMobileClass in driverIos.FindElementsByClassName(elementoMobile.IdentificadorIos))
                        {
                            elementoMobileParaInserirNaLista = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid,
                                                                                    elementoMobile.TipoIdentificadorIos,
                                                                                    elementoMobile.IdentificadorAndroid,
                                                                                    elementoMobile.IdentificadorIos,
                                                                                    elementoMobile.TextoEsperadoAndroid,
                                                                                    elementoMobile.TextoEsperadoIos)
                            {
                                ElementoIOS = elementoMobileClass,
                                TextoObtido = elementoMobileClass.Text,
                                ElementoVisivelNaTela = true
                            };

                            listaElementoMobile.Add(elementoMobileParaInserirNaLista);
                        }

                        return listaElementoMobile;

                    default:
                        throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento ANDROID com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorAndroid}.\nExceção Oirignal: {ex.Message}");

                    case PlataformaMobile.iOS:
                        throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não encontrou o elemento iOS com identificador {elementoMobile.TipoIdentificadorAndroid} de valor {elementoMobile.IdentificadorIos}.\nExceção Oirignal: {ex.Message}");

                    default:
                        throw new Exception("Plataforma inválida");
                }
            }
        }

        #endregion


        public void ClicaNoElementoMobile(ElementoMobile elementoMobile)
        {
            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    BuscaElementoMobile(elementoMobile).ElementoAndroid.Click();
                    break;

                case PlataformaMobile.iOS:
                    BuscaElementoMobile(elementoMobile).ElementoIOS.Click();
                    break;

                default:
                    throw new Exception("EXCEÇÃO CUSTOMIZADA: Plataforma inválida");
            }
        }

        public void ClicaNoElementoMobileDaListaPeloTextoDesejado(List<ElementoMobile> listaElementoMobile, string textoParaCliclar)
        {
            try
            {
                if (listaElementoMobile.FirstOrDefault(el => el.TextoObtido.Contains(textoParaCliclar)) is not null)
                {
                    listaElementoMobile.FirstOrDefault(el => el.TextoObtido.Contains(textoParaCliclar)).ElementoAndroid.Click();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"EXCEÇÃO CUSTOMIZADA: Não foi encontrado um elemento na lista que continha o texto {textoParaCliclar}");
            }
        }

        public void ClicCasoApareca(ElementoMobile elementoMobile, int tempoDeEsperaSegundos)
        {
            TimeSpan esperaDriveOriginal = new TimeSpan();

            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        esperaDriveOriginal = driverAndroid.Manage().Timeouts().ImplicitWait;
                        driverAndroid.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(tempoDeEsperaSegundos);
                        BuscaElementoMobile(elementoMobile).ElementoAndroid.Click();
                        driverAndroid.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        break;

                    case PlataformaMobile.iOS:
                        esperaDriveOriginal = driverIos.Manage().Timeouts().ImplicitWait;
                        driverIos.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(tempoDeEsperaSegundos);
                        BuscaElementoMobile(elementoMobile).ElementoIOS.Click();
                        driverIos.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        break;
                }
            }
            catch (Exception ex)
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        driverAndroid.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        new Exception(ex.Message + "\nElemento não apareceu, segue o fluxo");
                        break;

                    case PlataformaMobile.iOS:
                        driverIos.Manage().Timeouts().ImplicitWait = esperaDriveOriginal;
                        new Exception(ex.Message + "\nElemento não apareceu, segue o fluxo");
                        break;
                }
            }
        }

        public void EscreveNoElementoMobile(ElementoMobile elementoMobile, string texto)
        {
            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    BuscaElementoMobile(elementoMobile).ElementoAndroid.SendKeys(texto);
                    break;

                case PlataformaMobile.iOS:
                    BuscaElementoMobile(elementoMobile).ElementoIOS.SendKeys(texto);
                    break;
            }
        }

        public void EscreveTecladoNativo(ElementoMobile elementoMobile, string valor, bool comandoEspecialIos = false)
        {
            ClicaNoElementoMobile(elementoMobile);

            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    foreach (var caractere in valor.ToString().ToUpper())
                    {
                        switch (caractere)
                        {
                            case '0':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_0);
                                break;
                            case '1':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_1);
                                break;
                            case '2':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_2);
                                break;
                            case '3':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_3);
                                break;
                            case '4':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_4);
                                break;
                            case '5':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_5);
                                break;
                            case '6':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_6);
                                break;
                            case '7':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_7);
                                break;
                            case '8':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_8);
                                break;
                            case '9':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_9);
                                break;
                            case 'A':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_A);
                                break;
                            case 'B':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_B);
                                break;
                            case 'C':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_C);
                                break;
                            case 'D':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_D);
                                break;
                            case 'E':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_E);
                                break;
                            case 'F':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_F);
                                break;
                            case 'G':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_G);
                                break;
                            case 'H':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_H);
                                break;
                            case 'I':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_I);
                                break;
                            case 'J':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_J);
                                break;
                            case 'K':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_K);
                                break;
                            case 'L':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_L);
                                break;
                            case 'M':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_M);
                                break;
                            case 'N':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_N);
                                break;
                            case 'O':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_O);
                                break;
                            case 'P':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_P);
                                break;
                            case 'Q':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_Q);
                                break;
                            case 'R':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_R);
                                break;
                            case 'S':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_S);
                                break;
                            case 'T':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_T);
                                break;
                            case 'U':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_U);
                                break;
                            case 'V':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_V);
                                break;
                            case 'W':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_W);
                                break;
                            case 'X':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_X);
                                break;
                            case 'Y':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_Y);
                                break;
                            case 'Z':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_Z);
                                break;
                            case ' ':
                                driverAndroid.PressKeyCode(AndroidKeyCode.Keycode_SPACE);
                                break;
                            case '<':
                                driverAndroid.PressKeyCode(AndroidKeyCode.BackSpace);
                                break;
                            default:
                                throw new System.Exception("Valor inválido");
                        }
                    }
                    break;

                case PlataformaMobile.iOS:

                    if (!comandoEspecialIos)
                    {
                        foreach (var caractere in valor.ToString().ToUpper())
                        {
                            driverIos.IsKeyboardShown();
                            ElementoMobile elementoMobileTecladoIos = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid, elementoMobile.TipoIdentificadorIos, "", identificadorIos: caractere.ToString(), caractere.ToString(), "");

                            switch (caractere)
                            {
                                case '0':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '1':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '2':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '3':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '4':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '5':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '6':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '7':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '8':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case '9':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                case 'A':
                                    ClicaNoElementoMobile(elementoMobileTecladoIos);
                                    break;
                                default:
                                    throw new System.Exception("Valor inválido");
                            }
                        }
                        break;
                    }
                    else
                    {
                        driverIos.IsKeyboardShown();
                        ElementoMobile elementoMobileTecladoIos = new ElementoMobile(elementoMobile.TipoIdentificadorAndroid, elementoMobile.TipoIdentificadorIos, "", valor, "", valor);

                        switch (valor)
                        {
                            case "Apagar":
                                //ClicaPorIdAcessibilidade(elementoMobileTecladoIos);
                                break;
                            default:
                                throw new System.Exception("Valor inválido. \nValores devem ser 'Apagar'");
                        }
                    }
                    break;
            }
        }

        public void OcultaTecladoNativo()
        {
            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    driverAndroid.HideKeyboard();
                    break;

                case PlataformaMobile.iOS:
                    driverIos.HideKeyboard();
                    break;
            }
        }

        public void Desliga_Conexao()
        {
            try
            {
                switch (_plataformaMobile)
                {
                    case PlataformaMobile.Android:
                        driverAndroid.Dispose();
                        break;

                    case PlataformaMobile.iOS:
                        driverIos.Dispose();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"O driver utilizado está null.\n{ex.Message}");
            }
        }

        public void ResetaApp()
        {
            switch (_plataformaMobile)
            {
                case PlataformaMobile.Android:
                    driverAndroid.ResetApp();
                    break;

                case PlataformaMobile.iOS:
                    driverIos.ResetApp();
                    break;
            }
        }

    }
}