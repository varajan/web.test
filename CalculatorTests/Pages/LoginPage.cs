using OpenQA.Selenium;

namespace CalculatorTests.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement LoginFld
        {
            get
            {
                return _driver.FindElement(By.Id("login"));
            }
        }

        public IWebElement PasswordFld
        {
            get // IWebElement get()
            {
                return _driver.FindElement(By.Id("password"));
            }
        }

        public IWebElement LoginBtn => _driver.FindElement(By.Id("loginBtn"));
        public IWebElement RemindBtn => _driver.FindElement(By.Id("remindBtn"));

        public string Error => _driver.FindElement(By.Id("errorMessage")).Text;
        public string UserFieldText => _driver.FindElement(By.ClassName("user")).Text;
        public string PasswordFieldText => _driver.FindElement(By.ClassName("pass")).Text;

        public void Login(string login, string password)
        {
            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();
        }

        public IWebDriver OpenFrame()
        {
            _driver.FindElement(By.Id("remindBtn")).Click();
            return _driver.SwitchTo().Frame("remindPasswordView");
        }

        public void RemindPass(string email)
        {
            _driver.FindElement(By.Id("email")).SendKeys(email);
            _driver.FindElement(By.XPath("//button[contains(text(),'Send')]")).Click();
        }
    }
}
