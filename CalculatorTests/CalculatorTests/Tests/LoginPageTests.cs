using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CalculatorTests.Tests
{
    [TestFixture]
    public class LoginPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("12345", "67890", "User not found!")]
        [TestCase("test", "67890", "Incorrect password!")]
        [TestCase("12345", "newyork1", "Incorrect user name!")]
        [TestCase("newyork1", "test", "User not found!")]
        [TestCase("12345", "", "User not found!")]
        [TestCase("", "newyork1", "Incorrect user name!")]
        [TestCase("test", "", "Incorrect password!")]
        [TestCase("", "", "User not found!")]
        public void NegativeLoginTest(string login, string password, string expectedError)
        {
            // arrange
            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginBtn"));

            // act
            loginField.SendKeys(login);
            passwordField.SendKeys(password);
            loginButton.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("errorMessage")));

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual(expectedError, error.Text);
        }

        [Test]
        public void ValidLoginAndValidPasswordLoginTest()
        {
            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("loginBtn"));

            // act
            loginField.SendKeys("test");
            passwordField.SendKeys("newyork1");
            loginButton.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("calculateBtn")));

            // assert
            IWebElement success = driver.FindElement(By.Id("calculateBtn"));
            Assert.AreEqual("Calculate", success.Text);
        }

        [Test]
        public void CheckLabelsTest()
        {
            IWebElement passwordField = driver.FindElement(By.ClassName("pass"));
            IWebElement loginField = driver.FindElement(By.ClassName("user"));

            // assert
            Assert.AreEqual("Password:", passwordField.Text);
            Assert.AreEqual("User:", loginField.Text);
        }
    }
}