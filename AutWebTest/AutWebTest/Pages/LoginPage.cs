using OpenQA.Selenium;

namespace AutWebTest.Pages
{
    internal class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement LoginField
        {
            get
            {
                return driver.FindElement(By.Id("login"));
            }
        }

        public IWebElement PasswordField => driver.FindElement(By.Id("Password"));
        public IWebElement LoginButton => driver.FindElement(By.Id("loginBtn"));
        public IWebElement ErrorMessage => driver.FindElement(By.Id("errorMessage"));
    }
}
