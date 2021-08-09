using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestProject1
{
    public class LoginPageTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://localhost:64177/Login";
        }

        [TearDown]
        public void CleanUP() 
        {
            driver.Quit();
        }

        [Test]
        // Login with Valid login an password
        public void LoginPositiveTest()
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            // Assert
            string actualurl = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", actualurl);
            
        }

        [TestCase("","", "User name and password cannot be empty!")]
        [TestCase("", "newyork1", "User name and password cannot be empty!")]
        [TestCase("test", "", "User name and password cannot be empty!")]
        [TestCase("test", "newyork2", "Incorrect password!")]
        [TestCase("test1", "newyork1", "Incorrect user name!")]
        [TestCase("newyork1", "test", "'newyork1' user doesn't exist!")]
        
        public void LoginNegativeTest(string login,string password,string error)
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys(login);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual(error, actualerror);

        }

        [Test]
        // Verify Remind Button exist
        public void RemindButtonTest()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));
            // Act
            driver.FindElement(By.XPath("//*[@id='remindBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
           
        }
    }

  
}   