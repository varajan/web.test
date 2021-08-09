using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        // Positive Test 365
        public void CalculatorPositiveTest365()
        {
            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            Assert.AreEqual("10.00", actualInterest);


        }

        [Test]
        // Positive Test 360
        public void CalculatorPositiveTes360()
        {
               // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("360");
            driver.FindElement(By.Id("d360")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            Assert.AreEqual("10.00", actualInterest);


        }

        [Test]
        // Positive Test Date365
        public void CalculatorPositiveTestDate365()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();
            driver.FindElement(By.XPath("//*[@id='day']/option[1]")).Click();
            driver.FindElement(By.XPath("//*[@id='month']/option[8]")).Click();
            driver.FindElement(By.XPath("//*[@id='year']/option[12]")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            string actualDate = driver.FindElement(By.Id("endDate")).GetAttribute("value");
            Assert.AreEqual("110.00", actualIncome);
            Assert.AreEqual("10.00", actualInterest);
            Assert.AreEqual("32/07/2022", actualDate);



        }

        [Test]
        // Positive Test Date360
        public void CalculatorPositiveTestDate360()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("360");
            driver.FindElement(By.Id("d360")).Click();
            IWebElement day = driver.FindElement(By.Id("day"));
            SelectElement dayselect = new SelectElement(day);
            dayselect.SelectByText("1");
            //driver.FindElement(By.XPath("//*[@id='day']/option[1]")).Click();
            driver.FindElement(By.XPath("//*[@id='month']/option[8]")).Click();
            driver.FindElement(By.XPath("//*[@id='year']/option[12]")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            string actualDate = driver.FindElement(By.Id("endDate")).GetAttribute("value");
            Assert.AreEqual("110.00", actualIncome);
            Assert.AreEqual("10.00", actualInterest);
            Assert.AreEqual("27/07/2022", actualDate);



        }

        [Test]
        // Positive Test Date360
        public void Month()
        {

            // Act
        
            IWebElement month = driver.FindElement(By.Id("month"));
            SelectElement monthselect = new SelectElement(month);
            List<string> months = new List<string>();
            List<string> expectedMonths = new List<string>()
            {
             "January",
             "February",
            };
            foreach (IWebElement option in monthselect.Options)
            {
                months.Add(option.Text);
            }
            
            // Assert
            Assert.AreEqual(expectedMonths, months);
        }

        [Test]
        // Positive Test Date360
        public void ActualDate()
        {

            // Act

            IWebElement month = driver.FindElement(By.Id("month"));
            SelectElement monthselect = new SelectElement(month);
            string actualmonth = monthselect.SelectedOption.Text;
            string expectedmonth = DateTime.Today.ToString("MMMM", CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(expectedmonth, actualmonth);
        }

       [Test]
        // Negative Mandatory field Deposit Ammount Test 365
        public void CalculatorNegativeMDepositAmmoundTest365()
        {

            // Act
            driver.FindElement(By.Id("amount"));
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("0.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);


        }

        [Test]
        // Negative Mandatory field Deposit Ammount Test 360
        public void CalculatorNegativeMDepositAmmoundTest360()
        {

            // Act
            driver.FindElement(By.Id("amount"));
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term")).SendKeys("360");
            driver.FindElement(By.Id("d360")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("0.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);


        }

        [Test]
        // Negative Mandatory field Rate of intereset Test 365
        public void CalculatorNegativeMRateofinteresetTest365()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent"));
            driver.FindElement(By.Id("term")).SendKeys("365");
            driver.FindElement(By.Id("d365")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);


        }

        [Test]
        // Negative Mandatory field Rate of intereset Test 360
        public void CalculatorNegativeMRateofinteresetTest360()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent"));
            driver.FindElement(By.Id("term")).SendKeys("360");
            driver.FindElement(By.Id("d360")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);


        }

        [Test]
        // Negative Mandatory field Investment Term Test 365
        public void CalculatorNegativeMInvestmentTermTest365()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term"));
            driver.FindElement(By.Id("d365")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        // Negative Mandatory field Investment Term Test 360
        public void CalculatorNegativeMInvestmentTermTest360()
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys("100");
            driver.FindElement(By.Id("percent")).SendKeys("10");
            driver.FindElement(By.Id("term"));
            driver.FindElement(By.Id("d360")).Click();

            // Assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]

        // Settings
        public void CalculatorPositiveTestSettings()
        {
            // Act
            driver.FindElement(By.CssSelector("body > div > div > div")).Click();
           

            // Assert
            string actualurl = driver.Url;
            Assert.AreEqual("http://localhost:64177/Settings", actualurl);
           



        }




















    }
}
