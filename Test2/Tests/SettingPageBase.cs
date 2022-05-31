using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Test2.Tests
{

    internal class SettingPage
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/Settings";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        private void WaitForReady()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("save")));
        }

        [Test]

        public void CurrencyVer()
        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>() { "$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia" };
            IWebElement curren = driver.FindElement(By.Id("currency"));
            SelectElement currenSeletct = new SelectElement(curren);
            foreach (IWebElement element in currenSeletct.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }

        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        [TestCase("MM dd yyyy")]

        public void DateFormat(string format)
        {
            IWebElement dateForm = driver.FindElement(By.XPath("//select[@id='dateFormat']"));
            SelectElement dateFormSeletct = new SelectElement(dateForm);
            dateFormSeletct.SelectByText(format);
            IWebElement btnSave = driver.FindElement(By.Id("save"));
            btnSave.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.TitleContains("Deposite"));
            IWebElement endDate = driver.FindElement(By.Id("endDate"));
            string expectedDate = DateTime.Now.ToString(format);
            Assert.AreEqual(expectedDate, endDate.GetAttribute("value"));
        }

        [TestCase("$ - US dollar", "$")]
        [TestCase("€ - euro", "€")]
        [TestCase("£ - Great Britain Pound", "£")]
        [TestCase("₴ - Ukrainian hryvnia", "₴")]

        public void Currency(string curren,string simcur)
        {
            IWebElement currenc = driver.FindElement(By.XPath(".//select[@id='currency']"));
            IWebElement btnSave = driver.FindElement(By.Id("save"));
            SelectElement currencSelect = new SelectElement(currenc);
            currencSelect.SelectByText(curren);
            WaitForReady();
            btnSave.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.UrlContains("Calculator"));
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
            IWebElement simb = driver.FindElement(By.Id("currency"));
            Assert.AreEqual(simcur, simb.Text);
        }

        [Test]

        public void Logut()
        {
            IWebElement logut = driver.FindElement(By.XPath("//div[@class='login link btn btn-link']"));
            logut.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.TitleContains("Login"));
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [TestCase("123,456,789.00", "200,000.00", "100,000.00")]
        [TestCase("123.456.789,00", "200.000,00", "100.000,00")]
        [TestCase("123 456 789.00", "200 000.00", "100 000.00")]
        [TestCase("123 456 789,00", "200 000,00", "100 000,00")]

        public void NumbForm(string formn, string forminc, string forminter)
        {
            IWebElement namber = driver.FindElement(By.XPath(".//select[@id='numberFormat']"));
            IWebElement btnSave = driver.FindElement(By.Id("save"));
            SelectElement namberSelect = new SelectElement(namber);
            namberSelect.SelectByText(formn);
            WaitForReady();
            btnSave.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
             .Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            IWebElement depAm = driver.FindElement(By.Id("amount"));
            IWebElement rateInt = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement calcBut = driver.FindElement(By.Id("calculateBtn"));
            IWebElement termBut = driver.FindElement(By.XPath("//input[@type='radio']"));
            depAm.SendKeys("100000");
            rateInt.SendKeys("100");
            term.SendKeys("365");
            termBut.Click();
            calcBut.Click();
            IWebElement income = driver.FindElement(By.Id("income"));
            IWebElement interest = driver.FindElement(By.Id("interest"));
            Assert.AreEqual(forminc, income.GetAttribute("value"));
            Assert.AreEqual(forminter, interest.GetAttribute("value"));
        }
    }
}
