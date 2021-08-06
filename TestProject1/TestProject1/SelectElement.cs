using OpenQA.Selenium;

namespace TestProject1
{
    internal class SelectElement
    {
        private IWebElement element;

        public SelectElement(IWebElement element)
        {
            this.element = element;
        }
    }
}