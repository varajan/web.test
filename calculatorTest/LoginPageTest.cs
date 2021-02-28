using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace calculatorTest
{
    public class LoginPageTest
    {

        const string BASEURL = "http://localhost:64177/";
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        public void Test_User_Login_And_Password_Invalid_Values()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "1234";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("@#$%");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("'" + name + "' user doesn't exist!", error.Text);
            browser.Quit();


        }
        [Test]
        public void Test_User_Login_And_Password_Valide_Values()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "test";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            Assert.AreEqual(BASEURL + "Deposit", browser.Url);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_Both_Blanck()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = " ";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys(" ");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("User name and password cannot be empty!", error.Text);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_Admin_Admin()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "Admin";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("Admin");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("'" + name + "' user doesn't exist!", error.Text);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_Space_In_User_And_Correct_Password()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "_test";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect user name!", error.Text);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_Add_t_After_User_Password_Correct()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "testt";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect user name!", error.Text);
            browser.Quit();
        }
        [Test]
        public void Test_User_Login_And_Password_User_Uppercase_And_Password_Correct()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "TEST";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("newyork1");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect user name!", error.Text);
            browser.Quit(); 
        }
        [Test]
        public void Test_User_Login_And_Password_Valid_User_And_Upper_Password()
        {
            IWebDriver browser = new ChromeDriver();
            browser.Url = (BASEURL + "Login");
            IWebElement loginfield = browser.FindElement(By.Id("login"));
            string name = "test";
            loginfield.SendKeys(name);
            IWebElement passwordfield = browser.FindElement(By.Id("password"));
            passwordfield.SendKeys("NEWYOURK1");
            browser.FindElements(By.Id("login"))[1].Click();
            IWebElement error = browser.FindElement(By.Id("errorMessage"));
            Assert.AreEqual("Incorrect password!", error.Text);
            browser.Quit();

        }
    }
}
