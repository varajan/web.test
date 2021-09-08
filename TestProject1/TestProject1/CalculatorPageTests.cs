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

        //Positive Test 365 Calculation Income and Interest earned
        [TestCase(365, "100.00", "10.00", "365", "110.00", "10.00")]
        //Positive Test 360 Calculation Income and Interest earned
        [TestCase(360, "100.00", "10.00", "360", "110.00", "10.00")]
        public void CalculatorPositiveTest365(int year,string amount, string persent, string term, string expectedIncome, string expectedInterest)
        {
            //Act
            driver.FindElement(By.Id("amount")).SendKeys(amount);
            driver.FindElement(By.Id("percent")).SendKeys(persent);
            driver.FindElement(By.Id("term")).SendKeys(term);
            driver.FindElement(By.Id("d"+year)).Click();

            //Asser
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual(expectedInterest, actualInterest);
            Assert.AreEqual(expectedIncome, actualIncome);
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
        // Month
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
             "March",
             "April",
             "May",
             "June",
             "July",
             "August",
             "September",
             "October",
             "November",
             "December"
            };
            foreach (IWebElement option in monthselect.Options)
            {
                months.Add(option.Text);
            }
            
            // Assert
            Assert.AreEqual(expectedMonths, months);
        }

        [Test]
        // Days
        public void Day()
        {

            // Act
            IWebElement day = driver.FindElement(By.Id("day"));
            SelectElement dayselect = new SelectElement(day);
            List<string> days = new List<string>();
            List<string> expectedDays = new List<string>();
            for(int i = 1; i<32; i++)
            {
                expectedDays.Add(i.ToString());
            }
            foreach (IWebElement option in dayselect.Options)
            {
                days.Add(option.Text);
            }

            // Assert
            Assert.AreEqual(expectedDays, days);
        }

        [Test]
        // Years
        public void Year()
        {

            // Act
            IWebElement year = driver.FindElement(By.Id("year"));
            SelectElement yearselect = new SelectElement(year);
            List<string> years = new List<string>();
            List<string> expectedYears = new List<string>();
            for (int i = 2010; i < 2026; i++)
            {
                expectedYears.Add(i.ToString());
            }
            foreach (IWebElement option in yearselect.Options)
            {
                years.Add(option.Text);
            }

            // Assert
            Assert.AreEqual(expectedYears, years);
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

        // Negative Test Mandatory field Deposit Amount * - Empty
        [TestCase(365, "", "10.00", "365", "0.00", "0.00")]

        // Negative Test Mandatory field Rate of intereset: * - Empty
        [TestCase(365, "100.00", "", "365", "0.00", "0.00")]

        // Negative Test Mandatory field Investment Term: * - Empty
        [TestCase(365, "100.00", "10.00", "", "0.00", "0.00")]

        // Negative Test Mandatory field Financial year: * - Empty
        [TestCase(null, "100.00", "10.00", "", "0.00", "0.00")]
        public void CalculatorNegativeMandatoryFeildsEmpty(int? year, string amount, string persent, string term, string expectedIncome, string expectedInterest)
        {

            // Act
            driver.FindElement(By.Id("amount")).SendKeys(amount);
            driver.FindElement(By.Id("percent")).SendKeys(persent);
            driver.FindElement(By.Id("term")).SendKeys(term);
            if (year != null)driver.FindElement(By.Id("d" + year)).Click();

            //Asser
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual(expectedInterest, actualInterest);
            Assert.AreEqual(expectedIncome, actualIncome);
           

        }

        // Negative Test Mandatory field Financial year: * - Empty
        [TestCase("100.00", "100.00", "100", "0.00", "0.00")]
        public void CalculatorNegativeMandatoryRadioYearEmpty(string amount, string persent, string term, string expectedIncome, string expectedInterest)
        {
            // Act
            driver.FindElement(By.Id("amount")).SendKeys(amount);
            driver.FindElement(By.Id("percent")).SendKeys(persent);
            driver.FindElement(By.Id("term")).SendKeys(term);

            //Asser
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual(expectedInterest, actualInterest);
            Assert.AreEqual(expectedIncome, actualIncome);
           

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
