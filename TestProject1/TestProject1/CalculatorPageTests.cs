using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    class CalculatorPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://localhost:64177/Login";
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();
        }
        [TearDown]
        public void CleanUP()
        {
            driver.Quit();
        }

        [Test]
        // Login with Valid login an password
        public void LoginPositiveTest()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();
            // Assert
            string actualIncome  = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            Assert.AreEqual("10.00", actualInterest);


        }
    }
}
