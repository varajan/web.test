using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CalculatorTests
{
    class CalculatorPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void OpenLoginPage()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.Id("loginBtn")).Click();
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

        [Test]
        public void Logout()
        {
            // Act        
            driver.FindElement(By.XPath("//div[contains (text(),'Settings')]")).Click();
            driver.FindElement(By.XPath("//div[contains (text(),'Logout')]")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/", currentURL);
        }
        // After Logout button is removed from Settings to "http://localhost:64177/Deposit", Test need to be update

        [Test]
        public void Deposit_Texts(string expectedText, string actualText)
        {
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Deposit Amount: *", driver.FindElement(By.XPath("//td[contains (text(),'Deposit')]")).Text);
                Assert.AreEqual("Rate of intereset: *", driver.FindElement(By.XPath("//td[contains (text(),'Rate')]")).Text);
                Assert.AreEqual("Investment Term: *", driver.FindElement(By.XPath("//td[contains (text(),'Term')]")).Text);
                Assert.AreEqual("Start date: *", driver.FindElement(By.XPath("//td[contains (text(), 'Start')]")).Text);
                Assert.AreEqual("Financial year: *", driver.FindElement(By.XPath("//td[contains (text(),'year')]")).Text);
                Assert.AreEqual("Income:", driver.FindElement(By.XPath("//th[contains (text(),'Income')]")).Text);              
                Assert.AreEqual("Interest earned:", driver.FindElement(By.XPath("//th[contains (text(),'Interest')]")).Text);
                Assert.AreEqual("End date:", driver.FindElement(By.XPath("//th[contains (text(),'End')]")).Text);
                Assert.AreEqual("* - mandatory fields", driver.FindElement(By.XPath("//td[contains (text(),'mandatory')]")).Text);
            });
            //Assert.Multiple(() =>
            //{
            //    Assert.AreEqual("1250,00", income);
            //    Assert.AreEqual("250,00", interest);
            //});
        }

        [Test]
        public void Logout_pageback()
        {
            // Act
            driver.FindElement(By.LinkText("Settings")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", currentURL);
        }

        [Test]
        public void CalculateTest()
        {
            // Arrange
            // Income = Amount/100*Rate * Term/FinYear

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("percent")).SendKeys("25");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();
            string income = driver.FindElement(By.Id("income")).GetAttribute("value");
            string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("1250.00", income);
                Assert.AreEqual("250.00", interest);
            });
        }

        [Test]
        public void Mandatory_Fields()
        {
            // Act
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("percent")).SendKeys("25");
            driver.FindElement(By.Id("term")).SendKeys("365");
            string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            // Assert

            Assert.AreEqual("0.00", interest);

        }// To check if field Financial Year is mandatory by checking that Interest field must be 0.00,
         // as it is when other fields are not filled.
         //But the better way is to make mesaages for user, and  button - after clicking show message
         //which field need to be filled 

        [Test]
        public void MaxTerm_equal_FinancialYear()
        {
        ////////    // Act
        ////////    driver.FindElement(By.Id("amount")).SendKeys("1000");
        ////////    driver.FindElement(By.Id("percent")).SendKeys("25");
        ////////    driver.FindElement(By.Id("term")).SendKeys("365");
        ////////    driver.FindElement(By.Id("d360")).Click();
        ////////    string income = driver.FindElement(By.Id("income")).GetAttribute("value");
        ////////    string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

        ////////    // Assert
        ////////    Assert.Multiple(() =>
        ////////    {
        ////////        Assert.AreEqual("1250.00", income);
        ////////        Assert.AreEqual("250.00", interest);
        ////////    });
        }
    }

}
