using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        [Test]
        public void NegativeTest()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001/";
            IWebElement loginFeeld = driver.FindElement(By.Id("login"));
            IWebElement passwordFeeld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElements(By.Id("login"))[1];
            //IWebElement loginBut2 = driver.FindElement(By.ClassName("btn btn-sm btn-success"));
            loginFeeld.SendKeys("tes");
            passwordFeeld.SendKeys("newyour");
            loginBut.Click();
            IWebElement er = driver.FindElement(By.Id("errorMessage"));
            string expected = "User not found!";
            Assert.AreEqual(expected, er.Text);
            driver.Quit();
        }

        [Test]
        public void PositiveTest()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001/";
            IWebElement loginFeeld = driver.FindElement(By.Id("login"));
            IWebElement passwordFeeld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElements(By.Id("login"))[1];
            //IWebElement loginBut2 = driver.FindElement(By.ClassName("btn btn-sm btn-success"));
            loginFeeld.SendKeys("test");
            passwordFeeld.SendKeys("newyork1");
            loginBut.Click();
            Thread.Sleep(500);
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
            driver.Quit();
            }

        [Test]
        public void NegativePassword()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001/";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passwordFld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElements(By.Id("login"))[1];
            loginFld.SendKeys("test");
            passwordFld.SendKeys("newyork");
            loginBut.Click();
            Thread.Sleep(500);
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            string exepted = "Incorrect password!";
            Assert.AreEqual(exepted, error.Text);
            driver.Quit();
        }
        [Test]
        public void NewUser()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001/";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passwFld = driver.FindElement(By.Id("password"));
            IWebElement loginBut = driver.FindElements(By.Id("login"))[1];
            loginFld.SendKeys("Alex");
            passwFld.SendKeys("12345678");
            loginBut.Click();
            Thread.Sleep(500);
            string ActualUrl = driver.Url;
            string expectedUrl = "https://localhost:5001/Calculator";
            Assert.AreEqual(expectedUrl, ActualUrl);
            driver.Quit();
        }
        [Test]
        public void NegativLoginTest()
        {
            var options = new ChromeOptions { AcceptInsecureCertificates = true };
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://localhost:5001";
            IWebElement loginFld = driver.FindElement(By.Id("login"));
            IWebElement passwFld = driver.FindElement(By.Id("password"));
            IWebElement LoginBut = driver.FindElements(By.Id("login"))[1];
            loginFld.SendKeys("Test");
            passwFld.SendKeys("newyork1");
            LoginBut.Click();
            Thread.Sleep(500);
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            string expected = "Incorrect user name!";
            Assert.AreEqual(expected, error.Text);
            driver.Quit();
        }
            
    }
}
