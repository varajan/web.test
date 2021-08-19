using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestsCalculator
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PositiveTest()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // Act 
            loginFld.SendKeys("test");
            passFld.SendKeys("newyork1");
            loginBtn.Click();
            
            // Assert
            string actualUrl = driver.Url;
            string expectedUrl = "http://localhost:64177/Deposit";
            Assert.AreEqual(expectedUrl, actualUrl);
            driver.Close();
        }

        [Test]
        public void NegativeText()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passFld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // Act
            loginFld.SendKeys("negativeTest");
            passFld.SendKeys("newyork1");
            loginBtn.Click();

            // Assert
            IWebElement errorMessage = driver.FindElement(By.Id("errorMessage"));
            Assert.IsTrue(errorMessage.Displayed);
            Assert.AreEqual("Incorrect user name!", errorMessage.Text);
            driver.Close();
        }


    }
}