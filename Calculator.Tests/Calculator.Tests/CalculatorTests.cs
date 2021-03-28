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

            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("test");
            browser.FindElement(By.XPath("//td//input[@type]")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//td//button [text()='Login']")).Click();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestCalculator360days()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("360");
            browser.FindElement(By.XPath("//input[@type][1]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys(" ");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("0.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys(" ");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys(" ");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);
        }

        [Test]
        public void TestStartDateIsToday()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");

            SelectElement daySelect = new SelectElement(element:browser.FindElement(By.XPath("//tr[4]//td[2]//select[1]")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//tr[4]//td[2]//select[2]")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//tr[4]//td[2]//select[3]")));
            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy");

            Assert.AreEqual(expectedDate, actualDate);

        }

        [Test]
        public void TestSelectAnyStart()
        { 
            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.XPath("//tr[4]//td[2]//select[1]")));
            
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//tr[4]//td[2]//select[2]")));
            
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//tr[4]//td[2]//select[3]")));
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
            string Income = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("0.00", Income);
        }

        [Test]
        public void TestInterestEarnedIsDisplayed()
        {
            string InterestEarned = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("0.00", InterestEarned);
        }

        [Test]
        public void TestEndDateDataIsCorrect()

        {
            browser.FindElement(By.Id("term")).SendKeys("7");
            DateTime expected = DateTime.Today.AddDays(+7);
           
            string EndDate = browser.FindElement(By.XPath("//tr[8]//th[2]//input[@id]")).GetAttribute("value");

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
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100000");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("10");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("110000.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("10000.00", actualInterest);
        }

        [Test]
        public void PositiveTestMaxInterestRate100()
        {
            browser.FindElement(By.XPath("//td//input[@id]")).SendKeys("100000");
            browser.FindElement(By.XPath("//tr[2]//td[2]//input[@id]")).SendKeys("100");
            browser.FindElement(By.XPath("//tr[3]//td[2]//input[@id]")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            string actualIncome = browser.FindElement(By.XPath("//tr[6]//th[2]//input[@id]")).GetAttribute("value");

            Assert.AreEqual("200000.00", actualIncome);
            string actualInterest = browser.FindElement(By.XPath("//tr[7]//th[2]//input[@id]")).GetAttribute("value");
            Assert.AreEqual("100000.00", actualInterest);
        }
        

        
    }
}