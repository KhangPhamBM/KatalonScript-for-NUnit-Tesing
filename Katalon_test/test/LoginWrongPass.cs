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
    public class LoginWrongPass
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
        
        [TestCaseSource(nameof(LoginTestLoginTestWrongPasswordData))]
        public void TheLoginWrongPassTest(string phonenumber, string password, bool expected)
        {
            driver.Navigate().GoToUrl("http://localhost:5173/login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys(phonenumber);
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

        public static IEnumerable<TestCaseData> LoginTestLoginTestWrongPasswordData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("0912356782", "123456", true));
            testCases.Add(new TestCaseData("0912356793", "123456", true));
          /*  testCases.Add(new TestCaseData("0912345678", "123456", true));
            testCases.Add(new TestCaseData("0912345679", "123456", true));
            testCases.Add(new TestCaseData("0912345680", "123456", true));
            testCases.Add(new TestCaseData("0912345681", "123456", true));
            testCases.Add(new TestCaseData("0912345682", "123456", true));
            testCases.Add(new TestCaseData("0912345683", "123456", true));
            testCases.Add(new TestCaseData("0123456725", "123456", true));
            testCases.Add(new TestCaseData("0123456731", "123456", true));
            testCases.Add(new TestCaseData("0123456732", "123456", true));
            testCases.Add(new TestCaseData("0123456733", "123456", true));
            testCases.Add(new TestCaseData("0123456734", "123456", true));
            testCases.Add(new TestCaseData("0123456735", "123456", true));
            testCases.Add(new TestCaseData("0123456736", "123456", true));
            testCases.Add(new TestCaseData("0123456737", "123456", true));
          */

            // Add more test cases as needed

            return testCases;
        }
    }
}
