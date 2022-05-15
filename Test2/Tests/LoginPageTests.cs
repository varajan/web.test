using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test2.Tests
{
    internal class LoginPageTests
    {
        public IWebDriver driver;

        [SetUp]
        public void SetaUp()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://localhost:5001/";
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("tes", "newyork1", "User not found!")]
        [TestCase("test", "newyork", "Incorrect password!")]
        public void NegativeTest(string login, string password, string expected)
        {
            IWebElement loginFeeld = driver.FindElement(By.Id("login"));
            IWebElement passwordFeeld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElement(By.Id("loginBtn"));
            //IWebElement loginBut2 = driver.FindElement(By.ClassName("btn btn-sm btn-success"));
            loginFeeld.SendKeys(login);
            passwordFeeld.SendKeys(password);
            loginBut.Click();
            IWebElement er = driver.FindElement(By.Id("errorMessage"));
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.TextToBePresentInElement(er, expected));
            Assert.AreEqual(expected, er.Text);
        }

        [TestCase("test")]
        [TestCase("TEST")]
        public void PositiveLoginTest(string login)
        {
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passwFld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElement(By.Id("loginBtn"));
            loginFld.SendKeys(login);
            passwFld.SendKeys("newyork1");
            loginBut.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(2))
              .Until(ExpectedConditions.UrlContains("Calculator"));
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
        }

        [Test]
        public void LabelPass()
        {
            IWebElement labPass = driver.FindElement(By.ClassName("pass"));
            Thread.Sleep(600);
            string expected = "Password:";
            Assert.AreEqual(expected, labPass.Text);
        }
    }

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
            Assert.AreEqual(expected, endDay);            
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

        public void VariY()

        {
            List<string> actuale = new List<string>();
            List<string> expected = new List<string>();
            IWebElement startMonth = driver.FindElement(By.Id("year"));
            SelectElement startMonthSeletct = new SelectElement(startMonth);

            for (int i = 2010; i < 2030; i++)
            {
                expected.Add(i.ToString());
            }
            foreach (IWebElement element in startMonthSeletct.Options)
            {
                actuale.Add(element.Text);
            }
            Assert.AreEqual(expected, actuale);
        }
    }
}
