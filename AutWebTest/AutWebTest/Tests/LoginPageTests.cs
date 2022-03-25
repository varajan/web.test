using AutWebTest.Pages;
using AutWebTest.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutWebTest.Tests
{
    public class LoginPageTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:5000/";
            loginPage = new LoginPage(driver);
        }

        [TearDown]
        public void TearDown() => driver.Dispose();

        [Test]
        public void LoginWithEmptyField()
        {
            loginPage.Login(string.Empty, string.Empty);
            Assert.AreEqual("User not found!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithEmptyName()
        {

            loginPage.Login(Helper.RandomString(10), string.Empty);
            Assert.AreEqual("User not found!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithEmptyPass()
        {
            loginPage.Login(string.Empty, Helper.RandomString(10));
            Assert.AreEqual("User not found!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            loginPage.Login(Helper.RandomString(10), Helper.RandomString(10));
            Assert.AreEqual("User not found!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithInvalidName()
        {
            loginPage.Login(Helper.RandomString(10), "newyork1");
            Assert.AreEqual("Incorrect user name!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithInvalidPass()
        {
            loginPage.Login("test", Helper.RandomString(10));
            Assert.AreEqual("Incorrect password!", loginPage.ErrorMessage);
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            loginPage.Login("test", "newyork1");
            Assert.AreEqual("http://localhost:5000/Calculator", driver.Url);
        }
    }
}