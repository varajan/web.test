using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace web.test.tests
{
    class DepositePageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            driver.Url = "http://localhost:64177/Login";

            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.Id("loginBtn")).Click();

            new WebDriverWait(driver, TimeSpan.FromMilliseconds(10000)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("amount")));
        }

        [TearDown]
        public void AfterEachTest()
        {
            driver.Quit();
        }

        // Ammount
        //Rate
        //Term
        //Year


        [Test]
        public void Income_And_Interest_for_365()
        {
            //Arrange
            Decimal amount = 1234m; // range [0; 100,000]
            Decimal rate = 13.17m; // range [0; 100] 
            Decimal term = 364m; // range [0; 365]

            Decimal exp_interest = Math.Round (amount * rate * term / 100 / 365, 2);
            Decimal exp_income = amount + exp_interest;
            driver.FindElement(By.Id("d365")).Click();


            //Act
            driver.FindElement(By.Id("amount")).SendKeys("" + amount);
            driver.FindElement(By.Id("percent")).SendKeys("" + rate);
            driver.FindElement(By.Id("term")).SendKeys("" + term);

            Decimal act_interest = Convert.ToDecimal(driver.FindElement(By.Id("interest")).GetAttribute("value"));
            Decimal act_income = Convert.ToDecimal(driver.FindElement(By.Id("income")).GetAttribute("value"));

            //Assert
            Assert.AreEqual(exp_interest, act_interest);
            Assert.AreEqual(exp_income, act_income);
        }
    }
}
