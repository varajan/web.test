using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class SettingsPageTests
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

            browser.FindElement(By.XPath("//button[text() = 'Settings']")).Click();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCancelBtnWork()
        {
            browser.FindElement(By.XPath("//button[@id = 'cancel']")).Click();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void PositiveTestLogoutBtnWork()
        {
            browser.FindElement(By.XPath("//div[text() = 'Logout']")).Click();
            string actual = browser.Url;
        
            Assert.AreEqual("http://127.0.0.1:8080/", actual);
        }

        [Test]
        public void TestDateFormatSelection()
        {
            SelectElement dateFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//table//td//select//option[1]")));
            dateFormatSelect.SelectByText("DD-MM-YYYY");
            browser.FindElement(By.XPath("//button[text()='Save']")).Click();
            //NEED HELP IDK how to find OK button on alert "Changes are saved!"
            //Assert.IsTrue("");
           
        }

    }
}
