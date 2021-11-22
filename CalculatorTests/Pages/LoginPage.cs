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

        public string Error => _driver.FindElement(By.Id("errorMessage")).Text;

        public void Login(string login, string password)
        {
            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();
        }
    }
}
