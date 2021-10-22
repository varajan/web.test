using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace CalculatorTests
{
    public class LoginPageTests
    { 
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Login_Label_Text()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            // driver.FindElement(By.ClassName("user"));

            // Assert
            IWebElement userFieldText = driver.FindElement(By.ClassName("user"));
            IWebElement passwordFieldText = driver.FindElement(By.ClassName("pass"));
            Assert.AreEqual("Password:", passwordFieldText.Text);
            Assert.AreEqual("User:", userFieldText.Text);


            driver.Close();
        }


        [Test]
        public void Failed_Login_Error_Text()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("password");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect login or password!", error.Text);

            driver.Close();
            //I won't dublicate test for incorrect login
            //It will be good to use iterations for one test with different parameters
        }

        [Test]
        public void Login_Empty_Fields_Error_Text()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User name and password cannot be empty!", error.Text);

            driver.Close();
            //Same, won't dublicate test for empty login or password

        }

        [Test]
        public void Failed_Login_Attempts_Error()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("password");
            for (int i = 0; i < 5; i++)
            {
                driver.FindElements(By.Id("login"))[1].Click();
            }
            // Assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Failed logins more then 5 times. Please wait 15 min for next try.", error.Text);

            driver.Close();
            // it may be necessary to add a check that the user has not logged in
        }

        [Test]
        public void Success_Login()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", currentURL);


            driver.Close();
        }

        [Test]
        public void Login_Not_Case_Sensitive()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("login")).SendKeys("Test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual("http://localhost:64177/Deposit", currentURL);


            driver.Close();
        } // login should not be case sensitive (this is in practice)

        [Test]
        public void Login_Not_Existing_Account()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            string login = "Test1";
            driver.FindElement(By.Id("login")).SendKeys(login);
            driver.FindElement(By.Id("password")).SendKeys("newyork");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert           
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual($"'{login}' user doesn't exist!", error.Text);

            driver.Close();
        }

        [Test]
        public void Login_Buttons_Exist()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            bool  remindBtn = driver.FindElement(By.Id("remind")).Displayed;
            bool loginBtn = driver.FindElements(By.Id("login"))[1].Displayed;

            // Assert
            Assert.AreEqual(true, remindBtn, "button is not displayed");
            Assert.AreEqual(true, loginBtn);

            driver.Close();

            //It seems it works but if  completely remove an element from the code, there is an exception,
            //I want to use some clear message?
            //I tried to apply try-catch, but using it the test passes
            //even if there is no element at all in the code, how I can display that the test failed?
            //try
            //{
            //    bool remindBtn = driver.FindElement(By.Id("remind")).Displayed;
            //    bool loginBtn = driver.FindElements(By.Id("login"))[1].Displayed;
            //}
            //catch (OpenQA.Selenium.NoSuchElementException)
            //{
            //    System.Diagnostics.Debug.WriteLine("Element was not found");
            //}
        }

        [Test]
        public void Remind_Password_Success()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("remind")).Click();            
            _ = driver.SwitchTo().Frame("remindPasswordView");
            string playerEmail = "test@test.com";
            driver.FindElement(By.Id("email")).SendKeys(playerEmail);
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            string alertText = driver.SwitchTo().Alert().Text;

            // Assert
            Assert.AreEqual($"Email with instructions was sent to {playerEmail}", alertText);         
            driver.SwitchTo().Alert().Accept();

            driver.Close();
        }

        [Test]
        public void Remind_Password_Validation_Email()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            driver.FindElement(By.Id("email")).SendKeys("test@testcom");
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            string errorText = driver.FindElement(By.Id("message")).Text;

            // Assert
            Assert.AreEqual("Invalid email", errorText);

            driver.Close();
        } //Also here I would like to add some iterations to different email validations, so as not to write a lot of code

        [Test]
        public void Remind_Password_Email_Not_Exist()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            driver.FindElement(By.Id("email")).SendKeys("test12@test.com");
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            string errorText = driver.FindElement(By.Id("message")).Text;

            // Assert
            Assert.AreEqual("No user was found", errorText);

            driver.Close();
        }

        [Test]
        public void Remind_Password_Empty_Form()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";

            // Act
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            driver.FindElement(By.Id("email")).SendKeys("test@test.com");
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            string email = driver.FindElement(By.Id("email")).GetAttribute("value");

            // Assert
            Assert.AreEqual("", email);

            driver.Close();
        }//After sending the email, need to clear the iframe from the address and message
    }
}