using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class SeleniumTestSuite1
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.adactin.com/HotelApp/";
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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

        [Test]

        public void HomePage000()
        {
            driver.Navigate().GoToUrl(baseURL);
            Assert.IsTrue(IsElementPresent(By.Id("login")));
            Assert.IsTrue(IsElementPresent(By.LinkText("New User Register Here")));
        }

        [Test]
        public void LoginBookLogout001()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"username\"]")).SendKeys("makrobaktat");
            driver.FindElement(By.XPath("//*[@id=\"password\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"password\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"password\"]")).SendKeys("P455w0rd!");
            driver.FindElement(By.XPath("//*[@id=\"login\"]")).Click();
            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            driver.FindElement(By.XPath("//*[@id=\"location\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"location\"]"))).SelectByText("Sydney");
            driver.FindElement(By.XPath("//option[@value='Sydney']")).Click();
            driver.FindElement(By.XPath("//*[@id=\"room_nos\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"room_nos\"]"))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("//option[@value='2']")).Click();
            driver.FindElement(By.XPath("//*[@id=\"adult_room\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"adult_room\"]"))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"Submit\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"radiobutton_2\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"continue\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"first_name\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"first_name\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"first_name\"]")).SendKeys("Baktat");
            driver.FindElement(By.XPath("//*[@id=\"last_name\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"last_name\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"last_name\"]")).SendKeys("Makroniciusz");
            driver.FindElement(By.XPath("//*[@id=\"address\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"address\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"address\"]")).SendKeys("1 London Road");
            driver.FindElement(By.XPath("//*[@id=\"cc_num\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"cc_num\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"cc_num\"]")).SendKeys("1234567899876543");
            driver.FindElement(By.XPath("//*[@id=\"cc_type\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"cc_type\"]"))).SelectByText("American Express");
            driver.FindElement(By.XPath("//*[@id=\"cc_exp_month\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"cc_exp_month\"]"))).SelectByText("June");
            driver.FindElement(By.XPath("//option[@value='6']")).Click();
            driver.FindElement(By.XPath("//*[@id=\"cc_exp_year\"]")).Click();
            new SelectElement(driver.FindElement(By.XPath("//*[@id=\"cc_exp_year\"]"))).SelectByText("2021");
            driver.FindElement(By.XPath("//option[@value='2021']")).Click();
            driver.FindElement(By.XPath("//*[@id=\"cc_cvv\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"cc_cvv\"]")).Clear();
            driver.FindElement(By.XPath("//*[@id=\"cc_cvv\"]")).SendKeys("123");
            driver.FindElement(By.XPath("//*[@id=\"book_now\"]")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.LinkText("Click here to login again")).Click();
        }

        [Test]
        public void RegisterUser002()
        {
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath("/html/body/table[2]/tbody/tr/td[2]/form/table/tbody/tr[7]/td/a")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("makrobaktat");
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("1234567");
            driver.FindElement(By.Id("re_password")).Clear();
            driver.FindElement(By.Id("re_password")).SendKeys("1234567");
            driver.FindElement(By.Id("full_name")).Clear();
            driver.FindElement(By.Id("full_name")).SendKeys("Berek Herek");
            driver.FindElement(By.Id("email_add")).Clear();
            driver.FindElement(By.Id("email_add")).SendKeys("test@test.tc");
            driver.FindElement(By.Id("tnc_box")).Click();
            driver.FindElement(By.Id("Submit")).Click();
            Assert.AreEqual("Captcha is Empty", driver.FindElement(By.Id("captcha_span")).Text);


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

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
