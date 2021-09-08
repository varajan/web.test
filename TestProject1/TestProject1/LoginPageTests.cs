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

        // Negative Test Login and Password fields - Empty
        [TestCase("","", "User name and password cannot be empty!")]
        // Negative Test Login - Empty
        [TestCase("", "newyork1", "User name and password cannot be empty!")]
        // Negative Test Password - Empty
        [TestCase("test", "", "User name and password cannot be empty!")]
        // Negative Test Login - Correct and Password - Incorrect
        [TestCase("test", "newyork2", "Incorrect user name or password")]
        // Negative Test Login - Incorrect and Password - Correct
        [TestCase("test1", "newyork1", "Incorrect user name or password")]
        // Negative Test Login - Incorrect and Password - Incorrect
        [TestCase("test1", "newyork2", "Incorrect user name or password")]
        // Negative Test Login - (Correct Password value) and Password - (Correct Login value)
        [TestCase("newyork1", "test", "Incorrect user name or password")]
        
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
        // OnClick Remind Button - iframe 
        public void RemindButtonTest()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));
            // Act
            IWebElement remindbutton = driver.FindElement(By.XPath("//*[@id='remindBtn']"));
            driver.FindElement(By.XPath("//*[@id='remindBtn']")).Click();

            // Assert
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindPasswordView")));
            IWebElement remindpasswordview = driver.FindElement(By.XPath("//*[@id='remindPasswordView']"));
            

        }
    }

  
}   