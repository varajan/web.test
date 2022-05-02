using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests.Tests
{
    [TestFixture]
    public class LoginPageTests
    {
        [Test]
        public void EmptyFieldsTests()
        {
            //Check login with empty fields
           var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            loginButton.Click();
            Thread.Sleep(500);

            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();
        }
        
    }
}
