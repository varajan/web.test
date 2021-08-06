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


        [Test]
        // Login with Empty login an password fields
        public void LoginNegativeEmptyLoginPasswordTest()
        {

            // Act
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
           
        }

        [TestCase("","", "User name and password cannot be empty!")]
        [TestCase("test", "newyork2", "Incorrect password!")]
        // Login with Empty login an password fields
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
        // Login with Valid login and Invalid password
        public void NegativeValidLoginInvalidPasswordTest()
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork2");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("Incorrect password!", actualerror);
            
        }

        [Test]
        // Login with Valid password and Invalid login
        public void NegativeValidPasswordInvalidLoginTest()
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test1");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("Incorrect user name!", actualerror);
            
        }

        [Test]
        // Login with Empty Login and Filled password
        public void NegativeEmptyLoginTest()
        {

            // Act
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
            
        }

        [Test]
        // Login with Empty Password and Filled login
        public void NegativeEmptyPasswordTest()
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
            
        }

        [Test]
        // Login with Valid password value in login field AND Valid login value in password field
        public void NegativePasswordLoginTest()
        {

            // Act
            driver.FindElement(By.Id("login")).SendKeys("newyork1");
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.XPath("//*[@id='loginBtn']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("'newyork1' user doesn't exist!", actualerror);
            
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