﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace SeleniumTests
{
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

    [TestFixture]
    public class SeleniumTestSuite1 : TestMethods
    {        
        


        //from parent
        [SetUp]
        public override void SetupTest()
        {
            base.SetupTest();
        }

        [TearDown]
        public override void TeardownTest()
        {
            base.TeardownTest();
        }

        [Test]

        public void HomePage000()
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            Assert.IsTrue(IsElementPresent(By.XPath(appSettings["Btn_Login_Login"])));
            Assert.IsTrue(IsElementPresent(By.LinkText("New User Register Here")));
        }

        [Test]
        public void LoginBookLogout001()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);
            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            SearchMethod(location);

            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
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
        public void BokkedItinerary002()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);

            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));

            SearchMethod(location);

            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
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
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.Id("order_id_text")).Clear();
            driver.FindElement(By.Id("order_id_text")).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            var b = driver.FindElement(By.XPath("(//input[contains(@type,'text')])[3]")).GetAttribute("value").ToString();
            Assert.AreEqual(a, b);

        }

        [Test]
        public void RegisterUser003()
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
            driver.FindElement(By.LinkText("Go back to Login page")).Click();

        }

        [Test]
        public void RegisterUserResetButton004()
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
            driver.FindElement(By.Id("Reset")).Click();
            driver.FindElement(By.Id("Submit")).Click();
            Assert.AreEqual("Username is Empty", driver.FindElement(By.Id("username_span")).Text);
            Assert.AreEqual("Password is Empty", driver.FindElement(By.Id("password_span")).Text);
            Assert.AreEqual("Confirm Password is Empty", driver.FindElement(By.Id("re_password_span")).Text);
            Assert.AreEqual("Full Name is Empty", driver.FindElement(By.Id("full_name_span")).Text);
            Assert.AreEqual("Email Address is Empty", driver.FindElement(By.Id("email_add_span")).Text);
            Assert.AreEqual("Captcha is Empty", driver.FindElement(By.Id("captcha_span")).Text);
            Assert.AreEqual("You must agree to Terms and Conditions", driver.FindElement(By.Id("tnc_span")).Text);
            driver.FindElement(By.LinkText("Go back to Login page")).Click();

        }

        [Test]
        public void SearchSelect005()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);


            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText("Sydney");
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.Id("Submit")).Click();
            var a = driver.FindElement(By.XPath("(//input[@type='text'])[2]")).GetAttribute("value");
            //Assert.AreEqual("Sydney", a);

            if (a.Equals("Sydney", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Correct " + a);
            }
            else
            {
                Console.WriteLine("Incorrect" + a);
            }

        }

        [Test]

        public void TitleCheck006()
        {
            driver.Navigate().GoToUrl(baseURL);
            string title = driver.Title;
            //Assert.AreEqual("AdactIn.com - Hotel Reservation System", title);

            if (title.Equals("AdactIn.com - Hotel Reservation System"))
                Console.WriteLine("Test Passed - Title is: {0}", title);

            else
                Console.WriteLine("Test Failed - Title is: {0}", title);

        }

        [Test]
        
        public void ResetPassword007()
        {
            LoginMethod(username, password);
            driver.FindElement(By.XPath("//a[contains(.,'Change Password')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@name,'current_pass')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'current_pass')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@name,'new_password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'new_password')]")).SendKeys("adactin456");
            driver.FindElement(By.XPath("//input[contains(@name,'re_password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'re_password')]")).SendKeys("adactin456");
            driver.FindElement(By.XPath("//input[contains(@type,'submit')]")).Click();
            password = "adactin456";
            LoginMethod(username, password);
            driver.FindElement(By.XPath("//a[contains(.,'Change Password')]")).Click();
            driver.FindElement(By.XPath("//input[contains(@name,'current_pass')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'current_pass')]")).SendKeys(password);
            driver.FindElement(By.XPath("//input[contains(@name,'new_password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'new_password')]")).SendKeys("adactin123");
            driver.FindElement(By.XPath("//input[contains(@name,'re_password')]")).Clear();
            driver.FindElement(By.XPath("//input[contains(@name,'re_password')]")).SendKeys("adactin123");
            driver.FindElement(By.XPath("//input[contains(@type,'submit')]")).Click();
            var result = driver.FindElement(By.XPath("//span[@class='reg_error'][contains(.,'Your Password is successfully updated!!!')]")).Text;
            Assert.AreEqual("Your Password is successfully updated!!!", result);
            password = "adactin123";

        }

        [Test]
        public void CancelItinerary008()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);

            SearchMethod(location);

            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
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
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.Id("order_id_text")).Clear();
            driver.FindElement(By.Id("order_id_text")).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            driver.FindElement(By.XPath("(//input[@type='button'])[1]")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.Id("order_id_text")).Clear();
            driver.FindElement(By.Id("order_id_text")).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            var b = driver.FindElement(By.XPath("//label[contains(@class,'reg_error')]")).Text;
            Assert.AreEqual("0 result(s) found. Show all", b);

        }

        [Test]

        public void SearchFieldValidation009()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(username, password);
            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
            string message = driver.FindElement(By.XPath("//span[contains(.,'Please Select a Location')]")).Text;
            Assert.AreEqual("Please Select a Location", message);
        }

        [Test]

        public void CheckLoginGreeting010()
        {
            LoginMethod(username, password);

            string greeting = driver.FindElement(By.XPath("//input[@type='text'][contains(@id,'show')]")).GetAttribute("value");

            if (greeting.Equals("Hello makrobaktat!", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Test Passed! Greeting is: {0}", greeting);
            }

            else
                Console.WriteLine("Test Failed! Greeting is: {0}", greeting);
        }



        public override bool IsElementPresent(By by)
        {
           return base.IsElementPresent(by);
            
        }

        public override bool IsAlertPresent()
        {
            return base.IsAlertPresent();
        }

        public override string CloseAlertAndGetItsText()
        {
            return base.CloseAlertAndGetItsText();
        }



    }
}
