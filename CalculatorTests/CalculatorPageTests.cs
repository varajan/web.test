using System.Configuration;
using System.Reflection;
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
        private string BaseUrl => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["BaseUrl"].Value;

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
            // Arrange
            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            IWebDriver driver = new ChromeDriver(options);
            driver.Url = BaseUrl;

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();
            driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
            driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();
            // Act        
            driver.FindElement(By.XPath("//div[contains (text(),'Settings')]")).Click();
            driver.FindElement(By.XPath("//div[contains (text(),'Logout')]")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual(BaseUrl, currentURL);

        }
        // After Logout button is removed from Settings to $"{BaseUrl}/Deposit", Test need to be update

        [Test]
        public void Deposit_Texts(string expectedText, string actualText)
        {
            // Arrange
            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            IWebDriver driver = new ChromeDriver(options);
            driver.Url = $"{BaseUrl}/Deposit";

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
            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            IWebDriver driver = new ChromeDriver(options);
            driver.Url = $"{BaseUrl}/Deposit";

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
            // Arrange
            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            IWebDriver driver = new ChromeDriver(options);
            driver.Url = $"{BaseUrl}/Login";

            // Act
            driver.FindElement(By.LinkText("Settings")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);


            driver.Close();
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
