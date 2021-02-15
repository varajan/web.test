using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace web.test.tests
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Empty_Login_and_Password()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            driver.FindElements(By.Id("login")).Last().Click();
            string error = driver.FindElement(By.Id("errorMessage")).Text;

            Assert.AreEqual("User name and password cannot be empty!", error);

            driver.Quit();
        }
    }
}