using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

internal class SettingPage
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions { AcceptInsecureCertificates = true };
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Url = "https://localhost:5001/Settings";
    }
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
    private void WaitForReady()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("save")));
    }

    [Test]

    public void CurrencyVer()
    {
        //IWebElement currenc = driver.FindElement(By.XPath(".//select[@id='currency'']/././option[1]"));
        List<string> actuale = new List<string>();
        List<string> expected = new List<string>() { "$ - US dollar", "€ - euro", "£ - Great Britain Pound", "₴ - Ukrainian hryvnia" };
        IWebElement curren = driver.FindElement(By.Id("currency"));
        SelectElement currenSeletct = new SelectElement(curren);
        foreach (IWebElement element in currenSeletct.Options)
        {
            actuale.Add(element.Text);
        }
        Assert.AreEqual(expected, actuale);
    }

    [Test]

    public void Currency()
    {
        IWebElement currenc = driver.FindElement(By.XPath(".//select[@id='currency']/././option[1]"));
        IWebElement btnSave = driver.FindElement(By.Id("save"));
        WaitForReady();
        btnSave.Click();
        /* new WebDriverWait(driver, TimeSpan.FromSeconds(2))
              .Until(ExpectedConditions.UrlContains("Calculator"));*/
        Thread.Sleep(600);
        string ActualUrl = driver.Url;
        string expectedUrl = "https://localhost:5001/Calculator";
        Assert.AreEqual(expectedUrl, ActualUrl);
        IWebElement simbol = driver.FindElement(By.Id("currency"));
        Assert.AreEqual("$", simbol.Text);

    }
}
