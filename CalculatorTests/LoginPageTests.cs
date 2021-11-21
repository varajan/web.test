using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests
{
    public class LoginPageTests
    {
        private IWebDriver driver;
        private string BaseUrl => ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["BaseUrl"].Value;

        [SetUp]
        public void OpenLoginPage()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;

            var options = new ChromeOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore,
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--silent");
            options.AddArgument("log-level=3");

            driver = new ChromeDriver(chromeDriverService, options);
            driver.Url = BaseUrl;
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

        [Test]
        public void Login_Label_Text()
        {
            // Act
            // driver.FindElement(By.ClassName("user"));

            // Assert
            IWebElement userFieldText = driver.FindElement(By.ClassName("user"));
            IWebElement passwordFieldText = driver.FindElement(By.ClassName("pass"));
            Assert.AreEqual("Password:", passwordFieldText.Text);
            Assert.AreEqual("User:", userFieldText.Text);
        }

        [TestCase("test", "password", "Incorrect login or password!")]
        [TestCase("", "", "User name and password cannot be empty!")]
        [TestCase("", "newyork1", "User name and password cannot be empty!")]
        [TestCase("test", "", "User name and password cannot be empty!")]
        [TestCase("test1", "newyork", "'test1' user doesn't exist!")] //Login_Not_Existing_Account
        public void Failed_Login_Error_Texts(string login, string password, string expectedError)
        {
            // Act
            driver.FindElement(By.Id("login")).SendKeys(login);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            IWebElement error = driver.FindElement(By.Id("errorMessage"));
            Assert.AreEqual(expectedError, error.Text);

            //I won't dublicate test for incorrect login
            //It will be good to use iterations for one test with different parameters
        }

        //[Test]
        //public void Failed_Login_Attempts_Error()
        //{
        //    // Act
        //    driver.FindElement(By.Id("login")).SendKeys("test");
        //    driver.FindElement(By.Id("password")).SendKeys("password");
        //    for (int i = 0; i < 5; i++)
        //    {
        //        driver.FindElements(By.Id("login"))[1].Click();
        //    }
        //    // Assert
        //    IWebElement error = driver.FindElement(By.Id("errorMessage"));
        //    Assert.AreEqual("Failed logins more then 5 times. Please wait 15 min for next try.", error.Text);

        //    driver.Close();
        //    // it may be necessary to add a check that the user has not logged in
        //}

        [Test]
        public void Success_Login()
        {
            // Act
            driver.FindElement(By.Id("login")).SendKeys("test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);
        }

        [Test]
        public void Login_Not_Case_Sensitive()
        {
            // Act
            driver.FindElement(By.Id("login")).SendKeys("Test");
            driver.FindElement(By.Id("password")).SendKeys("newyork1");
            driver.FindElements(By.Id("login"))[1].Click();

            // Assert
            string currentURL = driver.Url;
            Assert.AreEqual($"{BaseUrl}/Deposit", currentURL);

        } // login should not be case sensitive (this is in practice)

        [Test]
        public void Login_Buttons_Exist()
        {
            // Act
            bool  remindBtn = driver.FindElement(By.Id("remind")).Displayed;
            bool loginBtn = driver.FindElements(By.Id("login"))[1].Displayed;

            // Assert
            Assert.AreEqual(true, remindBtn, "button is not displayed");  
            Assert.IsTrue(loginBtn); // = Assert.AreEqual(true, loginBtn);
        }

        [Test]
        public void Remind_Password_Success()
        {
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
        }

        [Test]
        public void Remind_Password_Validation_Email()
        {
            // Act
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            driver.FindElement(By.Id("email")).SendKeys("test@testcom");
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            string errorText = driver.FindElement(By.Id("message")).Text;

            // Assert
            Assert.AreEqual("Invalid email", errorText);

        } //Also here I would like to add some iterations to different email validations, so as not to write a lot of code

        [Test]
        public void Remind_Password_Email_Not_Exist()
        {
            // Act
            driver.FindElement(By.Id("remind")).Click();
            driver.SwitchTo().Frame("remindPasswordView");
            driver.FindElement(By.Id("email")).SendKeys("test12@test.com");
            driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
            string errorText = driver.FindElement(By.Id("message")).Text;

            // Assert
            Assert.AreEqual("No user was found", errorText);
        }

        [Test]
        public void Remind_Password_Empty_Form()
        {
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

        }//After sending the email, need to clear the iframe from the address and message
    }
}