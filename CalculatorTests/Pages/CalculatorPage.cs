using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTests.Pages
{
    public class CalculatorPage
    {
        private IWebDriver _driver;
        public CalculatorPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement DepAmountFld
        {
            get
            {
              return _driver.FindElement(By.Id("amount"));
            }
        }

        public IWebElement RateInterestFld
        {
            get
            {
                return _driver.FindElement(By.Id("percent"));
            }
        }

        public IWebElement InvestTermFld
        {
            get
            {
               return _driver.FindElement(By.Id("term"));
            }
        }

        public IWebElement DateDayDrdwn
        {
            get
            {
                return _driver.FindElement(By.Id("day"));
            }
        }
        public IWebElement DateMonthDrdwn
        {
            get
            {
                return _driver.FindElement(By.Id("month"));
            }
        }
        public IWebElement DateYearDrdwn
        {
            get
            {
                return _driver.FindElement(By.Id("year"));
            }
        }
        public IWebElement FinanceYearRBtn1
        {
            get
            {
                return _driver.FindElement(By.Id("d365"));
            }
        }
        public IWebElement FinanceYearRBtn2
        {
            get
            {
                return _driver.FindElement(By.Id("d360"));
            }
        }
        public IWebElement IncomeFld
        {
            get
            {
                return _driver.FindElement(By.Id("income"));
            }
        }
        public IWebElement InterestFld
        {
            get
            {
                return _driver.FindElement(By.Id("interest"));
            }
        }
        public IWebElement EndDateFld
        {
            get
            {
                return _driver.FindElement(By.Id("endDate"));
            }
        }

        public string GetLabelText(string actualText)
        {
           return _driver.FindElement(By.XPath($"//*[contains (text(),'{actualText}')]")).Text;
        }
    }
}
