using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject1
{
    public class LoginPageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        // Login with Valid login an password
        public void LoginPositiveTest()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            // Assert
            string actualurl = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", actualurl);
            driver.Quit();
        }

        [Test]
        // Login with Empty login an password fields
        public void LoginNegativeEmptyLoginPasswordTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
            driver.Quit();
        }

        [Test]
        // Login with Valid login and Invalid password
        public void NegativeValidLoginInvalidPasswordTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork2");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("Incorrect password!", actualerror);
            driver.Quit();
        }

        [Test]
        // Login with Valid password and Invalid login
        public void NegativeValidPasswordInvalidLoginTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test1");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("Incorrect user name!", actualerror);
            driver.Quit();
        }

        [Test]
        // Login wiith Empty Login and Filled password
        public void NegativeEmptyLoginTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
            driver.Quit();
        }

        [Test]
        // Login with Empty Password and Filled login
        public void NegativeEmptyPasswordTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("User name and password cannot be empty!", actualerror);
            driver.Quit();
        }

        [Test]
        // Login with Valid password value in login field AND Valid login value in password field
        public void NegativePasswordLoginTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("newyork1");
            driver.FindElement(By.Id("password")).SendKeys("test");
            driver.FindElement(By.XPath("//button[@id='login']")).Click();

            //Assert
            string actualurl = driver.Url;
            string actualerror = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            Assert.AreEqual("'newyork1' user doesn't exist!", actualerror);
            driver.Quit();
        }

        [Test]
        // Verify Remind Button exist
        public void RemindButtonTest()
        {
            // Arrage
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
           driver.FindElement(By.XPath("//*[@id='remind']")).Click();

            //Assert
            string actualurl = driver.Url;
            Assert.AreEqual("http://localhost:64177/Login", actualurl);
            driver.Quit();
        }
    }

  
}   