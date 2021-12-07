using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected string BaseUrl => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["BaseUrl"].Value;

        protected IWebDriver GetDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;

            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            return new ChromeDriver(chromeDriverService, options);
        }

        [TearDown]
        public void Close()
        {
            Driver.Quit();
        }
    }
}
