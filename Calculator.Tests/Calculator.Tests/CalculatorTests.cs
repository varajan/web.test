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

            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            // NEED HELP could not find radiobutton using //input [(text(), '365 days')]. is there a mistake? line 38
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestCalculator360days()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("360");
            browser.FindElement(By.XPath("//input[@type][1]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys(" ");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("0.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys(" ");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys(" ");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void TestStartDateIsToday()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");

            SelectElement daySelect = new SelectElement(element:browser.FindElement(By.XPath("//select [@id = 'day']")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));
            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy");

            Assert.AreEqual(expectedDate, actualDate);

        }

        [Test]
        public void TestSelectAnyStart()
        { 
            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'day']")));
            
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));
            
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));
            daySelect.SelectByText("2");
            monthSelect.SelectByText("April");
            yearSelect.SelectByText("2022");
            string actualDate = daySelect.SelectedOption.Text + " " + monthSelect.SelectedOption.Text + " " + yearSelect.SelectedOption.Text;

            Assert.AreEqual("2 April 2022", actualDate);
        }

        [Test]
        public void TestFinancialYearIsMandatoryField()
        {
            bool d365 = browser.FindElement(By.XPath("//input[@type][2]")).Selected;
            bool d360 = browser.FindElement(By.XPath("//input[@type][1]")).Selected;

            Assert.IsTrue(d365 || d360); //"At least one option should be selected."
            Assert.IsFalse(d365 && d360); //"Only one option should be selected."
        }

        [Test]
        public void TestIncomeIsDisplayed()
        {
            string Income = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("0.00", Income);
        }

        [Test]
        public void TestInterestEarnedIsDisplayed()
        {
            string InterestEarned = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");

            Assert.AreEqual("0.00", InterestEarned);
        }

        [Test]
        public void TestEndDateDataIsCorrect()

        {
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("7");
            DateTime expected = DateTime.Today.AddDays(+7);
           
            string EndDate = browser.FindElement(By.XPath("//input [@id = 'endDate']")).GetAttribute("value");

            Assert.AreEqual(expected.ToString("dd/MM/yyyy"), EndDate);
        }

        [Test]
        public void TestInterestEarnedLayout()
        {          
            string actual = browser.FindElement(By.XPath("//tr[7]//th [contains(text ), 'Interest earned')]")).GetAttribute("align");

            Assert.AreEqual("left", actual);
        }

        [Test]
        public void PositiveTestMaxDepositAmount100000()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100000");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("110000.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("10000.00", actualInterest);
        }

        [Test]
        public void PositiveTestMaxInterestRate100()
        {
            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100000");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual("200 000.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("100000.00", actualInterest);
        }
        

        
    }
}