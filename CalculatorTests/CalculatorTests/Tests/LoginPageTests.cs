using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.Url = "https://localhost:5001";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void LoginWithEmptyFieldsTest()
        {           
            // arrange
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

           
        }

        [Test]
        public void ValidLoginWithEmptyPasswordTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("test");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect password!", error.Text);

            driver.Quit();
        }

        [Test]
        public void InvalidLoginWithEmptyPasswordTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("12345");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();
        }

        [Test]
        public void ValidLoginWithEmptyLoginTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            passwordField.SendKeys("newyork1");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect user name!", error.Text);

            driver.Quit();
        }

        [Test]
        public void InvalidLoginWithEmptyLoginTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            passwordField.SendKeys("67890");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();

        }

        [TestCase("12345", "67890", "User not found!")]
        [TestCase("test", "67890", "Incorrect password!")]

        public void NegativeLoginTest(string login, string password, string expectedError)
        {
            // arrange
            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys(login);
            passwordField.SendKeys(password);
            loginButton.Click();
            Thread.Sleep(500);


            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual(expectedError, error.Text);
        }

     

        [Test]
        public void InvalidLoginAndValidPasswordLoginTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("12345");
            passwordField.SendKeys("newyork1");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect user name!", error.Text);

            driver.Quit();

        }

        [Test]
        public void ValidLoginAndValidPasswordLoginTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("test");
            passwordField.SendKeys("newyork1");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement success = driver.FindElement(By.Id("calculateBtn"));
            Assert.AreEqual("Calculate", success.Text);

            driver.Quit();
        }

        [Test]
        public void MixedValidLoginAndValidPasswordLoginTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElements(By.Id("login"))[0];
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElements(By.Id("login"))[1];

            // act
            loginField.SendKeys("newyork1");
            passwordField.SendKeys("test");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();
        }

        [Test]
        public void PasswordBugTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement passwordField = driver.FindElement(By.ClassName("pass"));

            // assert
            Assert.AreEqual("Password:", passwordField.Text);
        }

        [Test]
        public void UserBugTest()
        {
            // arrange
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";

            IWebElement loginField = driver.FindElement(By.ClassName("user"));

            // assert
            Assert.AreEqual("User:", loginField.Text);
        }
    }
}