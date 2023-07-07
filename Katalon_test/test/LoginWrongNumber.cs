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
    public class LoginWrongNumber
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
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestCaseSource(nameof(LoginTestLoginTestWrongNumData))]
        public void TheLoginWrongNumberTest(string phoneNumber, string password, bool expected)
        {
            driver.Navigate().GoToUrl("http://localhost:5173/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys(phoneNumber);
            driver.FindElement(By.XPath("//div[@id='root']/div/div/main/div/div/div/div[2]/form/div[2]/div/div/div/div/div/span")).Click();
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            try
            {
                bool actual = IsElementPresent(By.Id("swal2-title"));
                Assert.IsTrue(actual == expected);
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

        public static IEnumerable<TestCaseData> LoginTestLoginTestWrongNumData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("0912356782", "123456", true));
            testCases.Add(new TestCaseData("0912356793", "123456", true));
            testCases.Add(new TestCaseData("0912345671", "123456", true));
           /* testCases.Add(new TestCaseData("0912345670", "123456", true));
            testCases.Add(new TestCaseData("0912345682", "123456", true));
            testCases.Add(new TestCaseData("0912341181", "123456", true));
            testCases.Add(new TestCaseData("0912345682", "123456", true));
            testCases.Add(new TestCaseData("0912544683", "123456", true));
            testCases.Add(new TestCaseData("0123422725", "123456", true));
            testCases.Add(new TestCaseData("0123455731", "123456", true));
            testCases.Add(new TestCaseData("0123430732", "123456", true));
            testCases.Add(new TestCaseData("0123411733", "123456", true));
            testCases.Add(new TestCaseData("0123434734", "123456", true));
            testCases.Add(new TestCaseData("0123412735", "123456", true));
            testCases.Add(new TestCaseData("0123424736", "123456", true));
            testCases.Add(new TestCaseData("0123467737", "123456", true));
           */

            // Add more test cases as needed

            return testCases;
        }
    }
}
