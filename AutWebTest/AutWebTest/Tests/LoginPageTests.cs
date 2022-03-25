using AutWebTest.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutWebTest.Tests
{
    public class LoginPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:5000/";
        }

        [TearDown]
        public void TearDown() => driver.Dispose();

        [Test]
        public void ClickEmptyLogin()
        {
            var loginPage = new LoginPage(driver);
            loginPage.LoginButton.Click();
            Assert.AreEqual("User not found!", loginPage.ErrorMessage.Text);
        }
    }
}