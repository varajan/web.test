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

        [Test]
        public void InvalidLoginAndInvalidPasswordLoginTest()
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
            passwordField.SendKeys("67890");
            loginButton.Click();
            Thread.Sleep(500);


            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User not found!", error.Text);

            driver.Quit();
        }

        [Test]
        public void ValidLoginAndInvalidPasswordLoginTest()
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
            passwordField.SendKeys("67890");
            loginButton.Click();
            Thread.Sleep(500);

            // assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect password!", error.Text);

            driver.Quit();

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
            IWebElement error = driver.FindElement(By.Id("errorMesage"));
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
    }

}