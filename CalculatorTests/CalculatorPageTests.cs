using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace CalculatorTests
{
    class CalculatorPageTests
    {
        private string BaseUrl => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["BaseUrl"].Value;

        private IWebDriver driver;

        [SetUp]
        public void OpenLoginPage()
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

            driver = new ChromeDriver(chromeDriverService, options);
            driver.Url = BaseUrl;

            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.Id("loginBtn")).Click();
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

        [Test]
        public void Logout()
        {
            // Arrange
            driver.Url = BaseUrl;

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();
            driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();
            // Act        
            driver.FindElement(By.XPath("//div[contains (text(),'Settings')]")).Click();
            driver.FindElement(By.XPath("//div[contains (text(),'Logout')]")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual(BaseUrl, currentURL);

        }
        // After Logout button is removed from Settings to $"{BaseUrl}/Deposit", Test need to be update

        [TestCase("Deposit Amount: *", "Deposit")]
        [TestCase("Rate of interest: *", "Rate")]
        [TestCase("Investment Term: *", "Term")]
        [TestCase("Start date: *", "Start")]
        [TestCase("Financial year: *", "year")]
        [TestCase("Income:", "Income")]
        [TestCase("Interest earned:", "Interest")]
        [TestCase("End date:", "End")]
        [TestCase("* - mandatory fields", "mandatory")]
        public void Deposit_Texts(string expectedText, string actualText)
        {
            // Arrange
            driver.Url = $"{BaseUrl}/Deposit";

            // Act

            // Assert
            Assert.AreEqual(expectedText, driver.FindElement(By.XPath($"//td[contains (text(),'{actualText}')]")).Text);

            driver.Close();
        }

        [Test]
        public void Logout_pageback()
        {
            // Arrange
            driver.Url = $"{BaseUrl}/Login";

            // Act
            driver.FindElement(By.LinkText("Settings")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);


            driver.Close();
        }

        [Test]
        public void CalculateTest()
        {
            // Arrange
            // Income = Amount/100*Rate * Term/FinYear

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("percent")).SendKeys("25");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();
            string income = driver.FindElement(By.Id("income")).GetAttribute("value");
            string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("1250.00", income);
                Assert.AreEqual("250.00", interest);
            });
        }

        [Test]
        public void Mandatory_Fields()
        {
            // Act
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("percent")).SendKeys("25");
            driver.FindElement(By.Id("term")).SendKeys("365");
            string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            // Assert

            Assert.AreEqual("0.00", interest);

        }// To check if field Financial Year is mandatory by checking that Interest field must be 0.00,
         // as it is when other fields are not filled.
         //But the better way is to make mesaages for user, and  button - after clicking show message
         //which field need to be filled 

        [Test]
        public void MaxTerm_equal_FinancialYear()
        {
        ////////    // Act
        ////////    driver.FindElement(By.Id("amount")).SendKeys("1000");
        ////////    driver.FindElement(By.Id("percent")).SendKeys("25");
        ////////    driver.FindElement(By.Id("term")).SendKeys("365");
        ////////    driver.FindElement(By.Id("d360")).Click();
        ////////    string income = driver.FindElement(By.Id("income")).GetAttribute("value");
        ////////    string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

        ////////    // Assert
        ////////    Assert.Multiple(() =>
        ////////    {
        ////////        Assert.AreEqual("1250.00", income);
        ////////        Assert.AreEqual("250.00", interest);
        ////////    });
        }
    }

}
