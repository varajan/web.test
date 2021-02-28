using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace calculatorTest
{
    public class LoginPageTest
    {

        const string BASEURL = "http://localhost:64177/";
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        public void Test_User_Login_And_Password_Invalid_Values()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
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
        public void Test_User_Login_And_Password_Valide_Values()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "test";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            Assert.AreEqual(BASEURL + "Deposit", browser.Url);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_Both_Blanck()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User name and password cannot be empty!", error.Text);
            browser.Quit();
        }
    }
}
