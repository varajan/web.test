using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Error_Text()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("password");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect login or password!", error.Text);

            driver.Close();
        }


        [Test]
        public void Success_Login()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();
   
            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", currentURL);
            
            driver.Close();
        }

    }
}