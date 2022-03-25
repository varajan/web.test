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

        private IWebElement LoginField
        {
            get
            {
                return driver.FindElement(By.Id("login"));
            }
        }

        private IWebElement PasswordField => driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => driver.FindElement(By.Id("loginBtn"));
        public string ErrorMessage => driver.FindElement(By.Id("errorMessage")).Text;

        public void Login(string login, string password)
        {
            //if (LoginField.GetProperty. != string.Empty)
            //    LoginField.Clear();
            //if (PasswordField.GetProperty. != string.Empty)
            //    PasswordField.Clear();
            LoginField.SendKeys(login);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
