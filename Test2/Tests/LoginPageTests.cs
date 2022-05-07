using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
