using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTests.Pages
{
    public class CalculatorPage
    {
        private IWebDriver _driver;
        private string value;

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

        public SelectElement DateDayDrdwn
        {
            get
            {
                return new SelectElement(_driver.FindElement(By.Id("day")));
            }
        }
        public SelectElement DateMonthDrdwn
        {
            get
            {
                return new SelectElement(_driver.FindElement(By.Id("month")));
            }
        }
        public SelectElement DateYearDrdwn
        {
            get
            {
                return new SelectElement(_driver.FindElement(By.Id("year")));
            }
        }

        //10/08/2022
        public string StartDate
        {
            get
            {
                string day = DateDayDrdwn.SelectedOption.Text;
                //string month = DateMonthDrdwn.Text;
                // var month = DateTime.ParseExact(DateMonthDrdwn.SelectedOption.Text, "MMMM", CultureInfo.InvariantCulture).Month;
                var month = DateTime.ParseExact(DateMonthDrdwn.SelectedOption.Text, "MMMM", CultureInfo.InvariantCulture).Month;
                string year = DateYearDrdwn.SelectedOption.Text;

                return $"{day}/{month}/{year}";
            }
            set // set(value)
            {
                string day = value.Split('/')[0];
                int month = int.Parse(value.Split('/')[1]) - 1;
                string year = value.Split('/')[2];

                DateDayDrdwn.SelectByText(day);
                DateMonthDrdwn.SelectByIndex(month);
                DateYearDrdwn.SelectByText(year);
            }
        }
        public int FinancialYear
        {
            //Int x = FinancialYear
            get
            {
                if (_driver.FindElement(By.Id("d365")).Selected)
                    return 365;
                else return 360;
            }
            //FinancialYear = value
            set
            {
                if (value == 365)
                    _driver.FindElement(By.Id("d365")).Click();
                else _driver.FindElement(By.Id("d360")).Click();
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
        public string EndDate
        {
            get
            {
                return _driver.FindElement(By.Id("endDate")).GetAttribute("value");
            }
        }

        public string GetLabelText(string actualText)
        {
            return _driver.FindElement(By.XPath($"//*[contains (text(),'{actualText}')]")).Text;
        }

        public void Calculate(string amount, string rate, string term, string financialYear)
        {
            DepAmountFld.SendKeys(amount);
            RateInterestFld.SendKeys(rate);
            InvestTermFld.SendKeys(term);
            FinancialYear = int.Parse(financialYear);
            }
    }
}
