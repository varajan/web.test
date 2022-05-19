using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Test2.Tests
{
    internal class CalculatorPageTests
    {
        public IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/Calculator";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test]
        public void Names()
        {
            IWebElement depAm = driver.FindElement(By.XPath("(*//td[contains ( text(), 'Deposit' )])"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            SelectElement startDaySelect = new SelectElement(startDay);

            rateInt.SendKeys("100");
            term.SendKeys("365");
            //startDay.SendKeys("01");
            startDaySelect.SelectByText("1");
            startMonth.SendKeys("January");
            startYear.SendKeys("2022");
            ////*[contains ( text(), '365 days' )]/input
            termBut.Click();
            calcBut.Click();
            Thread.Sleep(600);
            IWebElement endDay = driver.FindElement(By.Id("endDate"));
            //XPath("//*[contains ( text(), 'End Date' )]/..//input"));
            /*new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TextToBePresentInElement(er, expected));*/
            string expected = "Deposit amount: *";
            Thread.Sleep(500);
            Assert.AreEqual(expected, depAm);

        }
        [Test]
        public void SecondPageEndDate()
        {

            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            SelectElement startDaySelect = new SelectElement(startDay);
            depAm.SendKeys("100");
            rateInt.SendKeys("100");
            term.SendKeys("365");
            //startDay.SendKeys("01");
            startDaySelect.SelectByText("1");
            startMonth.SendKeys("January");
            startYear.SendKeys("2022");
            ////*[contains ( text(), '365 days' )]/input
            termBut.Click();
            calcBut.Click();
            Thread.Sleep(600);
            IWebElement endDay = driver.FindElement(By.Id("endDate"));
            //XPath("//*[contains ( text(), 'End Date' )]/..//input"));
            /*new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TextToBePresentInElement(er, expected));*/
            string expected = "01/01/2023";
            Thread.Sleep(800);
            Assert.AreEqual(expected, endDay.GetAttribute("value"));
        }

        [Test]

        public void VerifMonth()

        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            SelectElement startMonthSeletct = new SelectElement(startMonth);
            foreach (IWebElement element in startMonthSeletct.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }

        [Test]

        public void VarifY()

        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>();
            IWebElement startYear = driver.FindElement(By.Id("year"));
            SelectElement startYearSelect = new SelectElement(startYear);

            for (int i = 2010; i < 2030; i++)
            {
                expected.Add(i.ToString());
            }
            foreach (IWebElement element in startYearSelect.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }

        [Test]

        public void FinfncialYearPos()
        {

            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            //IWebElement termBut2 = driver.FindElement(By.XPath("//*[contains ( text(), '360 days' )]"));
            SelectElement startDaySelect = new SelectElement(startDay);
            depAm.SendKeys("100000");
            rateInt.SendKeys("50");
            term.SendKeys("365");
            startDaySelect.SelectByText("10");
            startMonth.SendKeys("January");
            startYear.SendKeys("2022");
            //int termDep = Convert.ToInt32(term);
            termBut.Click();
            calcBut.Click();
            Thread.Sleep(600);
            IWebElement income = driver.FindElement(By.Id("income"));
            string expected = "150,000.00";
            Thread.Sleep(800);
            Assert.AreEqual(expected, income.GetAttribute("value"));
        }

        [Test]

        public void FinancialTermN()
        {
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            depAm.SendKeys("100000");
            rateInt.SendKeys("50");
            term.SendKeys("366");
            Thread.Sleep(600);
            IWebElement term1 = driver.FindElement(By.Id("term"));
            string expected = "0";
            Assert.AreEqual(expected, term1);
        }

        [Test]

        public void InterestN()
        {

            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement startDay = driver.FindElement(By.Id("day"));
            IWebElement startMonth = driver.FindElement(By.Id("month"));
            IWebElement startYear = driver.FindElement(By.Id("year"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("(//input[@type='radio'])[2]"));
            //IWebElement termBut2 = driver.FindElement(By.XPath("//*[contains ( text(), '360 days' )]"));
            SelectElement startDaySelect = new SelectElement(startDay);
            depAm.SendKeys("100000");
            rateInt.SendKeys("101");
            /*term.SendKeys("365");
            startDaySelect.SelectByText("10");
            startMonth.SendKeys("January");
            startYear.SendKeys("2022");
            //int termDep = Convert.ToInt32(term);
            termBut.Click();
            calcBut.Click();*/
            Thread.Sleep(600);
            IWebElement rateInt1 = driver.FindElement(By.Id("percent"));
            string expected = "0";
            Thread.Sleep(800);
            Assert.AreEqual(expected, rateInt1);
        }
    }
       
}