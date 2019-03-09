using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Diagnostics;


namespace HotelAppSEWebDriver
{

    [SetUpFixture]
    public class GlobalSetup
    {
        //Kill GeckoDriver
        [OneTimeSetUp]
        public void KillGeckoDriver()
        {
            foreach (var proc in Process.GetProcessesByName("geckodriver"))
            {
                proc.Kill();
            }
        }
    }



    public class TestMethods
    {
        internal IWebDriver driver;
        internal StringBuilder verificationErrors;
        internal string baseURL;
        internal string username = "makrobaktat";
        internal string password = "adactin123";
        internal string location = "Sydney";
        internal bool acceptNextAlert = true;



        //implement Setup method in parent
        public virtual void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.adactin.com/HotelApp/";
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        //implement TearDown method in parent class
        public virtual void TeardownTest()
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


        //implement reusable methods
        public void LoginMethod(string username, string password)
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).SendKeys(username);
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).SendKeys(password);
            driver.FindElement(By.XPath(appSettings["Btn_Login_Login"])).Click();
        }

        public void SearchMethod(string location)
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText(location);
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
        }

        public virtual bool IsElementPresent(By by)
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

        public virtual bool IsAlertPresent()
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

        public virtual string CloseAlertAndGetItsText()
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
