using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

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
        public void PositiveFillFormMaxAmount()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("100000");
            percentField.SendKeys("10");
            termField.SendKeys("20");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));

            Assert.AreEqual("100547.95", incomeField.GetAttribute("value"));
            Assert.AreEqual("547.95", interestField.GetAttribute("value"));
        }

        public void PositiveFillFormMinAmount()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1");
            percentField.SendKeys("99");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1.74", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.74", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveFillFormMaxPercent()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("99.9");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1821.10", incomeField.GetAttribute("value"));
            Assert.AreEqual("821.10", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveFillFormMinPercent()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("0.1");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1000.82", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.82", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveFillFormMaxTerm()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("90");
            termField.SendKeys("365");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1900.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("900.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveFillFormMinTerm()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("10000");
            percentField.SendKeys("90");
            termField.SendKeys("0.1");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("10002.47", incomeField.GetAttribute("value"));
            Assert.AreEqual("2.47", interestField.GetAttribute("value"));
        }

        [Test]
        public void NegativeInvalidAmountMin()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("0");
            percentField.SendKeys("90");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("0.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void NegativeInvalidAmountMax()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("100001");
            percentField.SendKeys("30");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("0.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void NegativeInvalidPercentMin()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("0");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1000.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        public void NegativeInvalidPercentMax()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("100");
            termField.SendKeys("300");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1000.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void NegativeInvalidTermMin()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("90");
            termField.SendKeys("0");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1000.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void NegativeInvalidTermMax()
        {
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("90");
            termField.SendKeys("366");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1000.00", incomeField.GetAttribute("value"));
            Assert.AreEqual("0.00", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveSelectRadioBtn()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement daysRadioBtn360 = driver.FindElement(By.Id("d360"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");
            daysRadioBtn360.Click();

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            Assert.AreEqual("1005.56", incomeField.GetAttribute("value"));
            Assert.AreEqual("5.56", interestField.GetAttribute("value"));
        }

        [Test]
        public void CheckDefaultRadioBtnOption()
        {
            //Arrange
            IWebElement daysRadioBtn365 = driver.FindElement(By.Id("d365"));

            //Assert
            Assert.True(daysRadioBtn365.Selected);
        }

        [Test]
        public void PositiveSelectFutureDate()
        {
            //Arrange
            //IWebElement amountField = driver.FindElement(By.Id("amount"));
            //IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            //amountField.SendKeys("1000");
            //percentField.SendKeys("10");
            termField.SendKeys("20");
            new SelectElement(dayDropdown).SelectByText("7");
            new SelectElement(monthDropdown).SelectByText("June");
            new SelectElement(yearDropdown).SelectByText("2022");

            //Assert
            //IWebElement incomeField = driver.FindElement(By.Id("income"));
            //IWebElement interestField = driver.FindElement(By.Id("interest"));
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("27/06/2022", endDateField.GetAttribute("value"));
            //Assert.AreEqual("1005.48", incomeField.GetAttribute("value"));
            //Assert.AreEqual("5.48", interestField.GetAttribute("value"));
        }

        [Test]
        public void PositiveSelectPastDate()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("20");
            dayDropdown.SendKeys("21"); // why does not work for 1
            monthDropdown.SendKeys("March");
            yearDropdown.SendKeys("2010");

            //Assert
            IWebElement incomeField = driver.FindElement(By.Id("income"));
            IWebElement interestField = driver.FindElement(By.Id("interest"));
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("10/04/2010", endDateField.GetAttribute("value"));
            Assert.AreEqual("1005.48", incomeField.GetAttribute("value"));
            Assert.AreEqual("5.48", interestField.GetAttribute("value"));
        }

        [Test]
        public void CheckBoundaryForDays()
        {
            //Arrange
            IWebElement amountField = driver.FindElement(By.Id("amount"));
            IWebElement percentField = driver.FindElement(By.Id("percent"));
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            amountField.SendKeys("1000");
            percentField.SendKeys("10");
            termField.SendKeys("22");
            dayDropdown.SendKeys("10"); 
            monthDropdown.SendKeys("October");
            yearDropdown.SendKeys("2022");

            //Assert
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("01/11/2022", endDateField.GetAttribute("value"), "Date is incorrect");
        }

        [Test]
        public void CheckDaysInDropdown()
        {
            //Arrange
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IList<string> expectedDays = new List<string>();
            for (int j = 1; j < 32; j++)
            {
                expectedDays.Add(j.ToString());
            }

            //Act
            new SelectElement(monthDropdown).SelectByText("October");
            SelectElement s = new SelectElement(dayDropdown);
            IList<string> actualDays = new List<string>();
            for (int j = 0; j < s.Options.Count; j++)
            {
                actualDays.Add(s.Options.ElementAt(j).Text);
            }
            
            //Assert
            Assert.AreEqual(expectedDays, actualDays);
        }

        [Test]
        public void CheckMonthInDropdown()
        {
            //Arrange
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));

            //Act & Assert
            SelectElement s = new SelectElement(monthDropdown);
            //get all options
            IList<IWebElement> els = s.Options;
            //count options
            int e = els.Count;
            Assert.AreEqual(e, 12);
            for (int j = 0; j < e; j++)
            {
                Console.WriteLine("Option at " + j + " is: " + els.ElementAt(j).Text);
            }
        }

        [Test]
        public void CheckYearInDropdown()
        {
            //Arrange
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act & Assert
            SelectElement s = new SelectElement(yearDropdown);
            //get all options
            IList<IWebElement> els = s.Options;
            //count options
            int e = els.Count;
            Assert.Greater(e, 15);
        }

        [Test]
        public void SelectFeb29LeapYear()
        {
            //Arrange
            IWebElement termField = driver.FindElement(By.Id("term"));
            IWebElement dayDropdown = driver.FindElement(By.Id("day"));
            IWebElement monthDropdown = driver.FindElement(By.Id("month"));
            IWebElement yearDropdown = driver.FindElement(By.Id("year"));

            //Act
            termField.SendKeys("1");
            dayDropdown.SendKeys("28"); 
            monthDropdown.SendKeys("February");
            yearDropdown.SendKeys("2024");

            //Assert
            IWebElement endDateField = driver.FindElement(By.Id("endDate"));
            Assert.AreEqual("29/02/2024", endDateField.GetAttribute("value"), "Date is incorrect");
        }
    }
}
