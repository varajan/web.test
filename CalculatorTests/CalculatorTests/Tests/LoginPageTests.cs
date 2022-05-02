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
        public void LoginWithEmptyFieldsTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();
        }

        [Test]
        public void LoginWithEmptyPasswordTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("Test");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect password!", error.Text);

            driver.Quit();
        }
    }
}
