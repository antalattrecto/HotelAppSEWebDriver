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
using System.Reflection;
using ExcelDataReader;
using System.Data;
using System.IO;
using CSV;
using CsvHelper;
using System.Collections.Generic;


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
        internal bool acceptNextAlert = true;
        internal string baseURL;

        public void CSVDefualtVaules()
        {
            var appSettings = ConfigurationManager.AppSettings;

            using (var reader = new StreamReader(appSettings["defaultValuesFilePath"]))

            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<CSVDefaults>();
                {

                    foreach (var record in records)
                    {

                        Username = record.Username;
                        Password = record.Password;
                        Location = record.DefaultLocation;
                    }
                    

                }

            }
        }



        public string Username { get; set; }
        public string Password { get; set; }
        public string Location { get; set; }




        //implement Setup method in parent
        public virtual void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = ConfigurationManager.AppSettings["build1Url"];
            verificationErrors = new StringBuilder();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CSVDefualtVaules();
        }


        //implement TearDown method in parent class
        public virtual void TeardownTest()
        {
            try
            {
               // GlobalSetup obj = new GlobalSetup();
                //obj.KillGeckoDriver();
                driver.Quit();
                
                
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }


        //implement reusable methods
        public void LoginMethod(string Username, string Password)
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Username"])).SendKeys(Username);
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Login_Password"])).SendKeys(Password);
            driver.FindElement(By.XPath(appSettings["Btn_Login_Login"])).Click();
        }

        public void LogOutMethod()
        {
            var appSettings = ConfigurationManager.AppSettings;

            
            driver.FindElement(By.XPath(appSettings["Lnk_Search_LogOut"])).Click();
            driver.FindElement(By.LinkText("Click here to login again")).Click();

        }

        public void SearchMethod(string Location)
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText(Location);
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
        }


        public void DataDrivenSearchMethod()
        {

            var appSettings = ConfigurationManager.AppSettings;

            using (var reader = new StreamReader(appSettings["filePath"]))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<CSVReaderClass>();

                foreach (var record in records)
                {
                    string locationResult = record.Location;                    

                    driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
                    new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText(locationResult);
                    var a = driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]));
                    driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"])).Click();
                    new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"]))).SelectByText("2 - Two");
                    driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"])).Click();
                    new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"]))).SelectByText("2 - Two");
                    driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
                    driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
                    string textResult = driver.FindElement(By.XPath("//input[@name='location_1'][contains(@id,'1')]")).GetAttribute("value");
                    Assert.IsTrue(textResult.Contains(locationResult));
                    driver.FindElement(By.XPath(appSettings["Btn_Search_Cancel"])).Click();

                }

            }
                  
        }

        public void BookMethod()
        {
            var appSettings = ConfigurationManager.AppSettings;


            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
            driver.FindElement(By.XPath("//*[@id=\"radiobutton_2\"]")).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Select_Continue"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_FirstName"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_FirstName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_FirstName"])).SendKeys("Baktat");
            driver.FindElement(By.XPath(appSettings["Txt_Book_LastName"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_LastName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_LastName"])).SendKeys("Makroniciusz");
            driver.FindElement(By.XPath(appSettings["Txt_Book_Address"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_Address"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_Address"])).SendKeys("1 London Road");
            driver.FindElement(By.XPath(appSettings["Txt_Book_CCNo"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CCNo"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CCNo"])).SendKeys("1234567899876543");
            driver.FindElement(By.XPath(appSettings["Lst_Book_CCType"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCType"]))).SelectByText("American Express");
            driver.FindElement(By.XPath(appSettings["Lst_Book_CCMonth"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCMonth"]))).SelectByText("June");
            driver.FindElement(By.XPath("//option[@value='6']")).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"]))).SelectByText("2021");
            driver.FindElement(By.XPath("//option[@value='2021']")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).SendKeys("123");
            driver.FindElement(By.XPath(appSettings["Btn_Book_Book"])).Click();

        }

        public void ChangePassword(string Password, string newPassword)
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.FindElement(By.XPath(appSettings["Lnk_Search_ChangePassWord"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassWord"])).SendKeys(Password);
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassWord"])).SendKeys(newPassword);
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).SendKeys(newPassword);
            driver.FindElement(By.XPath(appSettings["Btn_PassWord_Submit"])).Click();
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
