using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class LoginPageTests
    {
        IWebDriver browser;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }


        [Test]
        public void PositiveTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void IncorrectLoginTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test1");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("newyork11");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("Incorrect password!", actual);
        }

        [Test]
        public void IncorrectLoginPasswordTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test1");
            browser.FindElement(By.Id("password")).SendKeys("newyork11");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("'test1' user doesn't exist!", actual);
        }

        [Test]
        public void EmptyLoginPasswordTest()
        {
            browser.FindElement(By.Id("login")).SendKeys(" ");
            browser.FindElement(By.Id("password")).SendKeys(" ");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void EmptyLoginTest()
        {
            browser.FindElement(By.Id("login")).SendKeys(" ");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void EmptyPasswordTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys(" ");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void UpperCaseLoginTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("TEST");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void UpperCasePasswordTest()
        {
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("NEWYORK1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("Incorrect password!", actual);
        }

        [Test]
        public void SpaceInLoginTest()
        {
            browser.FindElement(By.Id("login")).SendKeys(" test");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();
            string actual = browser.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void RemindPassBtnIsDisplayedTest()
        {
            new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));
            IWebElement btn = browser.FindElement(By.Id("remindBtn"));
            string actual = btn.Text;

            Assert.AreEqual("Remind password", actual);
        }

        [Test]
        public void LoginFieldName()
        {
            string LoginName = browser.FindElement(By.ClassName("user")).GetAttribute("innerText");

            Assert.AreEqual("User", LoginName);
        }

        [Test]
        public void PasswordFieldName()
        {
            string PasswordName = browser.FindElement(By.ClassName("pass")).GetAttribute("innerText");

            Assert.AreEqual("Password", PasswordName);
        }



    }
    

    }
