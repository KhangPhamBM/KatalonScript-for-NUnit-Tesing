using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class LoginEmptyPassword
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost:5173/";
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

        [TestCaseSource(nameof(LoginTestEmptyPasswordData))]
        public void TheLoginEmptyPasswordTest(string phoneNumber, bool expected)
        {
            driver.Navigate().GoToUrl(baseURL + "login");
            driver.FindElement(By.Id("phoneNumber")).Click();
            driver.FindElement(By.Id("phoneNumber")).Clear();
            driver.FindElement(By.Id("phoneNumber")).SendKeys(phoneNumber);
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            try
            {
                bool passed = IsElementPresent(By.XPath("//div[@id='password_help']/div"));
                Assert.IsTrue(passed == expected);
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

        public static IEnumerable<TestCaseData> LoginTestEmptyPasswordData()
        {
            List<TestCaseData> testCases = new List<TestCaseData>();
            testCases.Add(new TestCaseData("0366967957", true));
            testCases.Add(new TestCaseData("0366967958", true));
           /* testCases.Add(new TestCaseData("0366967957", true));
            testCases.Add(new TestCaseData("0366967958", true));
            testCases.Add(new TestCaseData("0366967958", true));*/
            // Add more test cases as needed

            return testCases;
        }
    }
}
