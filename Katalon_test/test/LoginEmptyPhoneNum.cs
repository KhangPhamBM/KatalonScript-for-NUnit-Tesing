using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginEmptyPhoneNum
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            //Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestCaseSource(nameof(LoginTestLoginTestEmptyPhonenumberData))]
        public void TheLoginEmptyPhoneNumTest(string password, bool expected)
        {
            driver.Navigate().GoToUrl("http://localhost:5173/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys("");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//div[@id='root']/div/div/main/div/div/div/div[2]/form/div[2]/div/div/div/div/div/span")).Click();
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            try
            {
                bool pass = IsElementPresent(By.XPath("//div[contains(text(),'Phone cannot be blank')]"));
                Assert.IsTrue(pass == expected);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IEnumerable<TestCaseData> LoginTestLoginTestEmptyPhonenumberData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("123456", true));
            testCases.Add(new TestCaseData("1234567", true));
            /*testCases.Add(new TestCaseData("12345678", true));
            testCases.Add(new TestCaseData("123452", true));
            testCases.Add(new TestCaseData("12343333", true));
            testCases.Add(new TestCaseData("03667967958", true));*/
            // Add more test cases as needed

            return testCases;
        }

    }
}
