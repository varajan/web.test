using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Calculator.Tests
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PositiveTest()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElements(By.Id("login")) [1].Click();
            string actual = browser.Url;
            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
            browser.Quit ();

        }
    }
}