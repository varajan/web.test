using CalculatorTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CalculatorTests
{
    class CalculatorPageTests : BaseTest
    {
        private LoginPage loginPage;

        [SetUp]
        public void OpenLoginPage()
        {
            Driver = GetDriver();
            Driver.Url = BaseUrl;

            loginPage = new LoginPage(Driver);
            loginPage.Login("test", "newyork1");

            //Driver.FindElement(By.Id("login")).SendKeys("test");
            //Driver.FindElement(By.Id("password")).SendKeys("newyork1");
            //Driver.FindElement(By.Id("loginBtn")).Click();
        }

        [Test]
        public void Logout()
        {
            // Arrange
            Driver.Url = BaseUrl;

            // Act
            Driver.FindElement(By.Id("login")).SendKeys("test");
            Driver.FindElement(By.Id("password")).SendKeys("newyork1");
            Driver.FindElements(By.Id("login"))[1].Click();
            Driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
            Driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();
            // Act        
            Driver.FindElement(By.XPath("//div[contains (text(),'Settings')]")).Click();
            Driver.FindElement(By.XPath("//div[contains (text(),'Logout')]")).Click();

            // Assert
            string currentURL = Driver.Url;
            Assert.AreEqual(BaseUrl, currentURL);

        }
        // After Logout button is removed from Settings to $"{BaseUrl}/Deposit", Test need to be update

        [TestCase("Deposit Amount: *", "Amount")]
        [TestCase("Rate of interest: *", "Rate")]
        [TestCase("Investment Term: *", "Term")]
        [TestCase("Start date: *", "Start")]
        [TestCase("Financial year: *", "year")]
        [TestCase("Income:", "Income")]
        [TestCase("Interest earned:", "Interest")]
        [TestCase("End date:", "End")]
        [TestCase("* - mandatory fields", "mandatory")]
        public void Deposit_Texts(string expectedText, string actualText)
        {
            // Arrange
            Driver.Url = $"{BaseUrl}/Deposit";

            // Act

            // Assert
            Assert.AreEqual(expectedText, Driver.FindElement(By.XPath($"//*[contains (text(),'{actualText}')]")).Text);

            Driver.Close();
        }

        [Test]
        public void Logout_pageback()
        {
            // Arrange
            Driver.Url = $"{BaseUrl}/Login";

            // Act
            Driver.FindElement(By.LinkText("Settings")).Click();
            Driver.FindElement(By.LinkText("Logout")).Click();

            // Assert
            string currentURL = Driver.Url;
            Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);


            Driver.Close();
        }

        [Test]
        public void CalculateTest()
        {
            // Arrange
            // Income = Amount/100*Rate * Term/FinYear

            // Act
            Driver.FindElement(By.Id("amount")).SendKeys("1000");
            Driver.FindElement(By.Id("percent")).SendKeys("25");
            Driver.FindElement(By.Id("term")).SendKeys("365");
            Driver.FindElement(By.Id("d365")).Click();
            string income = Driver.FindElement(By.Id("income")).GetAttribute("value");
            string interest = Driver.FindElement(By.Id("interest")).GetAttribute("value");

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
            Driver.FindElement(By.Id("amount")).SendKeys("1000");
            Driver.FindElement(By.Id("percent")).SendKeys("25");
            Driver.FindElement(By.Id("term")).SendKeys("365");
            string interest = Driver.FindElement(By.Id("interest")).GetAttribute("value");

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
