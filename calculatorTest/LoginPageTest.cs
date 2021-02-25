using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace calculatorTest
{
    public class LoginPageTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Incorrect_User_Login_And_Password()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = "http://localhost:64177/Login";
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "1234";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("@#$%");
            browser.FindElements(By.Id("login"))[1].Click();


            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("'" + name + "' user doesn't exist!", error.Text);
            browser.Quit();


        }
        [Test]
        public void Test_Incorrect_User_Login_And_Password_Incorrect_Error()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = "http://localhost:64177/Login";
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "test";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            Assert.AreEqual("http://localhost:64177/Deposit", browser.Url);
            browser.Quit();
        }
    }
}
