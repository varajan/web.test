using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestsCalculator
{
    public class CalculatorPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/";

            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.Id("loginBtn")).Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void PositiveFillForm()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));

            Assert.AreEqual("1005.48", incomeField.GetAttribute("value"));
            Assert.AreEqual("5.48", interestField.GetAttribute("value"));
        }

    }
}
