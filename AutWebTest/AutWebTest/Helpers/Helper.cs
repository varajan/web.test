using AutWebTest.Pages;
using FizzWare.NBuilder;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutWebTest.Helpers
{
    public static class Helper
    {
        public static string RandomString(int length)
        {
            var generator = new RandomGenerator();
            var randomString = generator.Phrase(length);
            return randomString;
        }
    }
}
