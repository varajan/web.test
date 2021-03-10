using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class CalculatorTests
    {
        IWebDriver browser;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestCalculator360days()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("360");
            browser.FindElement(By.Id("d360")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys(" ");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("0.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys(" ");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys(" ");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void TestStartDateIsToday()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");

            SelectElement daySelect = new SelectElement(element:browser.FindElement(By.Id("day")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.Id("month")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.Id("year")));
            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy");

            Assert.AreEqual(expectedDate, actualDate);

        }

        [Test]
        public void TestSelectAnyStart()
        { 
            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.Id("day")));
            
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.Id("month")));
            
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.Id("year")));
            daySelect.SelectByText("2");
            monthSelect.SelectByText("April");
            yearSelect.SelectByText("2022");
            string actualDate = daySelect.SelectedOption.Text + " " + monthSelect.SelectedOption.Text + " " + yearSelect.SelectedOption.Text;

            Assert.AreEqual("2 April 2022", actualDate);
        }

        [Test]
        public void TestFinancialYearIsMandatoryField()
        {
            bool d365 = browser.FindElement(By.Id("d365")).Selected;
            bool d360 = browser.FindElement(By.Id("d360")).Selected;

            Assert.IsTrue(d365 || d360); //"At least one option should be selected."
            Assert.IsFalse(d365 && d360); //"Only one option should be selected."
        }

        [Test]
        public void TestIncomeIsDisplayed()
        {
            string Income = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("0.00", Income);
        }

        [Test]
        public void TestInterestEarnedIsDisplayed()
        {
            string InterestEarned = browser.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual("0.00", InterestEarned);
        }

        [Test]
        public void TestEndDateDataIsCorrect()

        {
            browser.FindElement(By.Id("term")).SendKeys("7");
            DateTime expected = DateTime.Today.AddDays(+7);
           
            string EndDate = browser.FindElement(By.Id("endDate")).GetAttribute("value");

            Assert.AreEqual(expected.ToString("dd/MM/yyyy"), EndDate);
        }

        [Test]
        public void TestInterestEarnedLayout()
        {          
            string actual = browser.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[7]/th[1]")).GetAttribute("align");

            Assert.AreEqual("left", actual);
        }

        [Test]
        public void PositiveTestMaxDepositAmount100000()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100000");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("110000.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10000.00", actualInterest);
        }

        [Test]
        public void PositiveTestMaxInterestRate100()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100000");
            browser.FindElement(By.Id("percent")).SendKeys("100");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");

            Assert.AreEqual("200000.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("100000.00", actualInterest);
        }
        

        
    }
}