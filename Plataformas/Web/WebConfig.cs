using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;

namespace Core_Automacao.Plataformas.Web
{
    public class WebConfig
    {
        protected IWebDriver driver;

        public WebConfig(string browser, string pathDriver, string[] argumentos = null)
        {
            switch (browser)
            {
                case "chrome":
                    if (argumentos is not null)
                    {
                        ChromeOptions chromeOptions = (ChromeOptions)ConfiguraOptionDriverChromeFirefox(browser, argumentos);
                        driver = new ChromeDriver(pathDriver, chromeOptions);
                    }
                    else
                    {
                        driver = new ChromeDriver(pathDriver);
                    }

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    break;

                case "firefox":
                    if (argumentos is not null)
                    {
                        FirefoxOptions firefoxOptions = (FirefoxOptions)ConfiguraOptionDriverChromeFirefox(browser, argumentos);
                        driver = new FirefoxDriver(pathDriver, firefoxOptions);
                    }
                    else
                    {
                        driver = new FirefoxDriver(pathDriver);
                    }

                    break;

                case "edge":
                    driver = new EdgeDriver(pathDriver);
                    break;

                case "safari":
                    driver = new SafariDriver(pathDriver);
                    break;
                default:
                    throw new Exception("Browser inválido");
            }
        }

        private DriverOptions ConfiguraOptionDriverChromeFirefox(string browser, string[] argumentos)
        {
            switch (browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();

                    foreach (var argumento in argumentos)
                    {
                        chromeOptions.AddArgument(argumento);
                    }

                    return chromeOptions;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();

                    foreach (var argumento in argumentos)
                    {
                        firefoxOptions.AddArgument(argumento);
                    }

                    return firefoxOptions;

                default:
                    throw new Exception("Browser inválido");
            }
        }
    }
}