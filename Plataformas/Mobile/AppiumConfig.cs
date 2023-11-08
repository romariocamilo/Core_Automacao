using Core_Automacao.Models.Mobile;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using Newtonsoft.Json;

namespace Core_Automacao.Plataformas.Mobile
{
    public class AppiumConfig
    {
        protected AndroidDriver<AndroidElement> driverAndroid;
        protected IOSDriver<IOSElement> driverIos;

        // Construtor que configura qual driver está sendo utilizado
        protected AppiumConfig(PlataformaMobile plataformaMobile)
        {
            switch (plataformaMobile)
            {
                case PlataformaMobile.Android:
                    driverAndroid = ConfiguraDriverAndroid(RetornaCapabilitiesConfiguradas(LeJsonCapabilities(plataformaMobile)), RetornaDadosCoenexaoAppium(LeDadosConexaoAppium()));
                    break;

                case PlataformaMobile.iOS:
                    driverIos = ConfiguraDriverIos(RetornaCapabilitiesConfiguradas(LeJsonCapabilities(plataformaMobile)), RetornaDadosCoenexaoAppium(LeDadosConexaoAppium()));
                    break;

                default:
                    throw new System.Exception("Plataforma inválida");
            }
        }

        private AndroidDriver<AndroidElement> ConfiguraDriverAndroid(Dictionary<string, object> capabilities, DadosConexaoAppium dadosConexaoAppium)
        {
            try
            {
                AppiumOptions options = new AppiumOptions();

                foreach (var cabability in capabilities)
                {
                    options.AddAdditionalCapability(cabability.Key, cabability.Value);
                }

                string pathConexao = string.Format("http://{0}:{1}/{2}", dadosConexaoAppium.HostRemoto, dadosConexaoAppium.PortaRemota, dadosConexaoAppium.PathRemoto);

                var driver = new AndroidDriver<AndroidElement>(new Uri(pathConexao), options);
                double esperaImplicita = Convert.ToDouble(capabilities.FirstOrDefault(c => c.Key == "appium:implicitWait").Value);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(esperaImplicita);

                return driver;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível instânciar o DRIVER ANDROID.\n{ex.Message}");
            }
        }
        
        private IOSDriver<IOSElement> ConfiguraDriverIos(Dictionary<string, object> capabilities, DadosConexaoAppium dadosConexaoAppium)
        {
            try
            {
                AppiumOptions options = new AppiumOptions();

                foreach (var cabability in capabilities)
                {
                    options.AddAdditionalCapability(cabability.Key, cabability.Value);
                }

                string pathConexao = string.Format("http://{0}:{1}/{2}", dadosConexaoAppium.HostRemoto, dadosConexaoAppium.PortaRemota, dadosConexaoAppium.PathRemoto);

                var driver = new IOSDriver<IOSElement>(new Uri(pathConexao), options);
                double esperaImplicita = Convert.ToDouble(capabilities.FirstOrDefault(c => c.Key == "appium:implicitWait").Value);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(esperaImplicita);

                return driver;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível instânciar o DRIVER iOS.\n{ex.Message}");
            }
        }

        private string LeJsonCapabilities(PlataformaMobile platformaMobile)
        {
            string nomeArquivo = string.Empty;

            switch (platformaMobile)
            {
                case PlataformaMobile.Android:
                    try
                    {
                        nomeArquivo = "Capabilities//CapabilitiesAndroid.json";
                        return File.ReadAllText(nomeArquivo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Não encontramos o caminho '{nomeArquivo}' no seu projeto. \n{ex.Message}");
                    }

                case PlataformaMobile.iOS:
                    try
                    {
                        nomeArquivo = "Capabilities//CapabilitiesIos.json";
                        return File.ReadAllText(nomeArquivo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Não encontramos o caminho '{nomeArquivo}' no seu projeto. \n{ex.Message}");
                    }

                default:
                    throw new System.Exception("Plataforma inválida");
            }
        }

        private string LeDadosConexaoAppium()
        {
            string nomeArquivo = string.Empty;

            try
            {
                nomeArquivo = "Conexao//DadosConexao.json";
                return File.ReadAllText(nomeArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não encontramos o caminho '{nomeArquivo}' no seu projeto. \n{ex.Message}");
            }
        }

        private Dictionary<string, object> RetornaCapabilitiesConfiguradas(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Seu json de configuração deve conter o formato de {{ \"nomeCapability\": \"valorCapability\" }} \n{ex.Message}");
            }
        }

        private DadosConexaoAppium RetornaDadosCoenexaoAppium(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<DadosConexaoAppium>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Seu json de configuração deve conter o formato de {{ \"nomeCapability\": \"valorCapability\" }} \n{ex.Message}");
            }
        }
    }
}