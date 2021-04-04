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
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void IncorrectLoginTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test1");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains(text(), 'Incorrect user name!')]")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork11");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains (text(), 'Incorrect password!')]")).Text;

            Assert.AreEqual("Incorrect password!", actual);
        }

        [Test]
        public void IncorrectLoginPasswordTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test1");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork11");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[@id = 'errorMessage']")).Text;

            Assert.AreEqual("'test1' user doesn't exist!", actual);
            //NEED HELP in line 68: I could not find it by //tr[4]//th[contains (text(), ''test1' user doesn't exist!')]
            //is it because of 'test1' signs or what do you think?
        }

        [Test]
        public void EmptyLoginPasswordTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys(" ");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys(" ");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains (text(), 'User name and password cannot be empty!')]")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void EmptyLoginTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys(" ");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains (text(), 'User name and password cannot be empty!')]")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void EmptyPasswordTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys(" ");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains (text(), 'User name and password cannot be empty!')]")).Text;

            Assert.AreEqual("User name and password cannot be empty!", actual);
        }

        [Test]
        public void UpperCaseLoginTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("TEST");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains(text(), 'Incorrect user name!')]")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void UpperCasePasswordTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("NEWYORK1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains (text(), 'Incorrect password!')]")).Text;

            Assert.AreEqual("Incorrect password!", actual);
        }

        [Test]
        public void SpaceInLoginTest()
        {
            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys(" test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();
            string actual = browser.FindElement(By.XPath("//tr[4]//th[contains(text(), 'Incorrect user name!')]")).Text;

            Assert.AreEqual("Incorrect user name!", actual);
        }

        [Test]
        public void RemindPassBtnIsDisplayedTest()
        {
            new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[text() = 'Remind password']")));
            IWebElement btn = browser.FindElement(By.XPath("//button[text() = 'Remind password']"));
            string actual = btn.Text;

            Assert.AreEqual("Remind password", actual);
        }

        [Test]
        public void LoginFieldName()
        {
            string LoginName = browser.FindElement(By.XPath("//*[@class='user']")).GetAttribute("innerText");

            Assert.AreEqual("User", LoginName);
        }

        [Test]
        public void PasswordFieldName()
        {
            string PasswordName = browser.FindElement(By.XPath("//*[@class='pass']")).GetAttribute("innerText");

            Assert.AreEqual("Password", PasswordName);
        }

        






    }
    

    }
