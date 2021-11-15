using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorTests
{
    class CalculatorPageTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Logout()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();           
            driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();            

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/", currentURL);

            driver.Close();
        }
        // After Logout button is removed from Settings to "http://localhost:64177/Deposit", Test need to be update

        [Test]
        public void Deposit_Texts()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Deposit";

            // Act

            // Assert
            Assert.AreEqual("Deposit Amount: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[1]/td[1]")).Text);
            Assert.AreEqual("Rate of intereset: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[2]/td[1]")).Text);
            Assert.AreEqual("Investment Term: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[3]/td[1]")).Text);
            Assert.AreEqual("Start date: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[4]/td[1]")).Text);
            Assert.AreEqual("Financial year: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[5]/td[1]")).Text);
            Assert.AreEqual("Income:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[6]/th[1]")).Text);
            Assert.AreEqual("Interest earned:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[7]/th[1]")).Text);
            Assert.AreEqual("End date:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[8]/th[1]")).Text);
            Assert.AreEqual("* - mandatory fields", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[9]/td")).Text);

            driver.Close();
        }

        [Test]
        public void Mandatory_Fields()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Deposit";

            // Act
            //driver.FindElement(By.Id("login")).SendKeys("test");
            //driver.FindElement(By.Id("password")).SendKeys("newyork1");
            //driver.FindElements(By.Id("login"))[1].Click();
            //driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
            //driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();

            // Assert
            Assert.AreEqual("Deposit Amount: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[1]/td[1]")).Text);
            Assert.AreEqual("Rate of intereset: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[2]/td[1]")).Text);
            Assert.AreEqual("Investment Term: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[3]/td[1]")).Text);
            Assert.AreEqual("Start date: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[4]/td[1]")).Text);
            Assert.AreEqual("Financial year: *", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[5]/td[1]")).Text);
            Assert.AreEqual("Income:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[6]/th[1]")).Text);
            Assert.AreEqual("Interest earned:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[7]/th[1]")).Text);
            Assert.AreEqual("End date:", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[8]/th[1]")).Text);
            Assert.AreEqual("* - mandatory fields", driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[9]/td")).Text);

            driver.Close();
        }

        [Test]
        public void Logout_pageback()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();
            driver.FindElement(By.LinkText("Settings")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", currentURL);


            driver.Close();
        }

        [Test]
        public void CalculateTest()
        {
            // Arrange
            // Income = Amount/100*Rate * Term/FinYear
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();
            driver.FindElement(By.Id("amount")).SendKeys("1000");
            driver.FindElement(By.Id("percent")).SendKeys("25");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();
            string income = driver.FindElement(By.Id("income")).GetAttribute("value");
            string interest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("1250,00", income);
                Assert.AreEqual("250,00", interest);
            });

            driver.Close();
        }
    }
}
