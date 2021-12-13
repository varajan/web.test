﻿using CalculatorTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace CalculatorTests
{
    class CalculatorPageTests : BaseTest
    {
        private LoginPage loginPage;
        private CalculatorPage calculatorPage;

        [SetUp]
        public void OpenLoginPage()
        {
            Driver = GetDriver();
            Driver.Url = BaseUrl;

            loginPage = new LoginPage(Driver);
            loginPage.Login("test", "newyork1");
        }

        [TestCase ("1000", "25", "365", "365", "1250.00", "250.00")]
        [TestCase("1000", "25", "360", "365", "1246.58", "246.58")]
        [TestCase("1000", "25", "25", "365", "1017.12", "17.12")]
        [TestCase("1000", "25", "360", "360", "1250.00", "250.00")]
        [TestCase("1000", "25", "1", "360", "1000.69", "0.69")]
        public void CalculateTest(string amount, string rate, string term, string financialYear, string expectedIncom, string expectedInterest)
        {
            // Arrange
            // Income = Amount/100*Rate * Term/FinYear

            // Act
            calculatorPage = new CalculatorPage(Driver);
            //calculatorPage.DepAmountFld.SendKeys(amount);
            //calculatorPage.RateInterestFld.SendKeys(rate);
            //calculatorPage.InvestTermFld.SendKeys(term);
            ////calculatorPage.FinanceYearRBtn1.Click();
            //calculatorPage.FinancialYear = 365;
            calculatorPage.Calculate(amount,rate,term,financialYear);
            string income = calculatorPage.IncomeFld.GetAttribute("value");
            string interest = calculatorPage.InterestFld.GetAttribute("value");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedIncom, income);
                Assert.AreEqual(expectedInterest, interest);
            });
        }

        //[TestCase("1000", "25", "360", "1250.00", "250.00")]
        //[TestCase("1000", "25", "1", "1000.69", "0.69")]
        //public void CalculateTest360(string amount, string rate, string term, string expectedIncom, string expectedInterest)
        //{
        //    // Arrange
        //    // Income = Amount/100*Rate * Term/FinYear

        //    // Act
        //    calculatorPage = new CalculatorPage(Driver);
        //    calculatorPage.DepAmountFld.SendKeys(amount);
        //    calculatorPage.RateInterestFld.SendKeys(rate);
        //    calculatorPage.InvestTermFld.SendKeys(term);
        //    calculatorPage.FinanceYearRBtn2.Click();
        //    string income = calculatorPage.IncomeFld.GetAttribute("value");
        //    string interest = calculatorPage.InterestFld.GetAttribute("value");

        //    // Assert
        //    Assert.Multiple(() =>
        //    {
        //        Assert.AreEqual(expectedIncom, income);
        //        Assert.AreEqual(expectedInterest, interest);
        //    });
        //}

        //[Test]
        //public void Logout()
        //{
        //    // Arrange
        //    Driver.Url = BaseUrl;

        //    // Act
        //    Driver.FindElement(By.XPath("/html/body/div/div/div")).Click();
        //    Driver.FindElement(By.XPath("/html/body/div/div/div[1]")).Click();
        //    // Act        
        //    Driver.FindElement(By.XPath("//div[contains (text(),'Settings')]")).Click();
        //    Driver.FindElement(By.XPath("//div[contains (text(),'Logout')]")).Click();

        //    // Assert
        //    string currentURL = Driver.Url;
        //    Assert.AreEqual(BaseUrl, currentURL);
        //}
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
            calculatorPage = new CalculatorPage(Driver);
            Driver.Url = $"{BaseUrl}/Deposit";

            // Assert
            Assert.AreEqual(expectedText, calculatorPage.GetLabelText(actualText));
        }

        //[Test]
        //public void Logout_pageback()
        //{
        //    // Arrange
        //    Driver.Url = $"{BaseUrl}/Login";

        //    // Act
        //    Driver.FindElement(By.LinkText("Settings")).Click();
        //    Driver.FindElement(By.LinkText("Logout")).Click();

        //    // Assert
        //    string currentURL = Driver.Url;
        //    Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);
        //}

        [TestCase("", "25", "365")]
        [TestCase("1000","","365")]
        [TestCase("1000", "25", "")]
        public void MandatoryFields(string depamount, string rateinterest, string investTerm)
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            calculatorPage.DepAmountFld.SendKeys(depamount);
            calculatorPage.RateInterestFld.SendKeys(rateinterest);
            calculatorPage.InvestTermFld.SendKeys(investTerm);
            calculatorPage.FinanceYearRBtn1.Click();
            string interest = calculatorPage.InterestFld.GetAttribute("value");

            // Assert          
            Assert.AreEqual("0.00", interest);
        }

        //[TestCase("1","January","2022", "365")]
        //[TestCase("1", "January", "2022", "360")]
        //public void InterestCalculationDependingFromFinancialYear(string day, string month, string year)
        //{
        //    // Act
        //    calculatorPage = new CalculatorPage(Driver);
        //    calculatorPage.DateDayDrdwn.SendKeys(day);
        //    calculatorPage.DateMonthDrdwn.SendKeys(month);
        //    calculatorPage.DateYearDrdwn.SendKeys(year);

        //    string interest = calculatorPage.InterestFld.GetAttribute("value");

        //    // Assert          
        //    Assert.AreEqual("0.00", interest);

        //}

        [TestCase("1/1/2024", "60", "01/03/2024")]
        [TestCase("1/1/2023", "60", "02/03/2023")]
        [TestCase("10/12/2023", "120", "18/04/2023")]
        public void EndDateCalculation(string date, string term, string result)
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            calculatorPage.StartDate = date;
            //calculatorPage.DateDayDrdwn.SelectByText(day);
            //calculatorPage.DateMonthDrdwn.SendKeys(month);
            //calculatorPage.DateYearDrdwn.SendKeys(year);
            calculatorPage.InvestTermFld.SendKeys(term);

            // Assert
            Assert.AreEqual(result, calculatorPage.EndDate);
           
        }

        [Test]

        public void StartDateDefaultValue()
        {
            calculatorPage = new CalculatorPage(Driver);
            string defaultValue = calculatorPage.StartDate; // StartDate = get

            Assert.AreEqual(DateTime.Today.ToString("d/M/yyyy"),defaultValue);
        }




        [Test]
        public void MandatoryFieldFinanceYear()
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            //calculatorPage.DepAmountFld.SendKeys("1000");
            //calculatorPage.RateInterestFld.SendKeys("25");
            //calculatorPage.InvestTermFld.SendKeys("365");
            //var financeYear = calculatorPage.FinanceYearRBtn1.Selected;
            //string interest = calculatorPage.InterestFld.GetAttribute("value");

            // Assert          
            Assert.IsTrue(calculatorPage.FinanceYearRBtn1.Selected);

        }// To check if field Financial Year is mandatory by checking that Interest field must be 0.00,
         // as it is when other fields are not filled.
         //But the better way is to make mesaages for user, and  button - after clicking show message
         //which field need to be filled 

        [Test]
        public void MaxValueValidation()
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            calculatorPage.DepAmountFld.SendKeys("100001");
            calculatorPage.RateInterestFld.SendKeys("101");
            calculatorPage.InvestTermFld.SendKeys("366");

            // Assert
            Assert.Multiple(() =>
                {
                    Assert.AreEqual("100000", calculatorPage.DepAmountFld.GetAttribute("value"));
                    Assert.AreEqual("100", calculatorPage.RateInterestFld.GetAttribute("value"));
                    Assert.AreEqual("365", calculatorPage.InvestTermFld.GetAttribute("value"));
                }
           );
        }

        [Test]
        public void MaxTerm_equal_FinancialYear360()
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            calculatorPage.DepAmountFld.SendKeys("1000");
            calculatorPage.RateInterestFld.SendKeys("25");
            calculatorPage.InvestTermFld.SendKeys("365");
            calculatorPage.FinanceYearRBtn2.Click();
            string finalInvestTerm = calculatorPage.InvestTermFld.GetAttribute("value");

            // Assert
            Assert.AreEqual("360", finalInvestTerm);
        }

        [Test]
        public void MaxTerm_equal_FinancialYear365()
        {
            // Act
            calculatorPage = new CalculatorPage(Driver);
            calculatorPage.DepAmountFld.SendKeys("1000");
            calculatorPage.RateInterestFld.SendKeys("25");
            calculatorPage.InvestTermFld.SendKeys("366");
            calculatorPage.FinanceYearRBtn1.Click();
            string finalInvestTerm = calculatorPage.InvestTermFld.GetAttribute("value");

            // Assert
            Assert.AreEqual("365", finalInvestTerm);
        }
    }
}
