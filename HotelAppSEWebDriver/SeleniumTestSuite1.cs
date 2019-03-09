using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using HotelAppSEWebDriver;


namespace SeleniumTests
{
   

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

        public void Test000_HomePage()
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            Assert.IsTrue(IsElementPresent(By.XPath(appSettings["Btn_Login_Login"])));
            Assert.IsTrue(IsElementPresent(By.LinkText("New User Register Here")));
        }

        [Test]
        public void Test001_LoginBookLogout()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);
            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            SearchMethod(location);

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
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.LinkText("Click here to login again")).Click();
        }

        [Test]
        public void Test002_BookedItinerary()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);

            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));

            SearchMethod(location);

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
            driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"]))).SelectByText("2021");
            driver.FindElement(By.XPath("//option[@value='2021']")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).SendKeys("123");
            driver.FindElement(By.XPath(appSettings["Btn_Book_Book"])).Click();
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            var b = driver.FindElement(By.XPath("(//input[contains(@type,'text')])[3]")).GetAttribute("value").ToString();
            Assert.AreEqual(a, b);

        }

        [Test]
        public void Test003_RegisterUser()
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath(appSettings["Lnk_Login_Register"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Register_UserName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_UserName"])).SendKeys("makrobaktat");
            driver.FindElement(By.XPath(appSettings["Txt_Register_Password"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_Password"])).SendKeys("1234567");
            driver.FindElement(By.XPath(appSettings["Txt_Register_ConfirmPassword"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_ConfirmPassword"])).SendKeys("1234567");
            driver.FindElement(By.XPath(appSettings["Txt_Register_FullName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_FullName"])).SendKeys("Berek Herek");
            driver.FindElement(By.XPath(appSettings["Txt_Register_Email"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_Email"])).SendKeys("test@test.tc");
            driver.FindElement(By.XPath(appSettings["Chb_Register_Terms"])).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Register_Register"])).Click();
            Assert.AreEqual("Captcha is Empty", driver.FindElement(By.Id("captcha_span")).Text);
            driver.FindElement(By.LinkText("Go back to Login page")).Click();

        }

        [Test]
        public void Test004_RegisterUserResetButton()
        {
            var appSettings = ConfigurationManager.AppSettings;

            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.XPath(appSettings["Lnk_Login_Register"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Register_UserName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_UserName"])).SendKeys("makrobaktat");
            driver.FindElement(By.XPath(appSettings["Txt_Register_PassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_PassWord"])).SendKeys("1234567");
            driver.FindElement(By.XPath(appSettings["Txt_Register_ConfirmPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_ConfirmPassWord"])).SendKeys("1234567");
            driver.FindElement(By.XPath(appSettings["Txt_Register_FullName"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_FullName"])).SendKeys("Berek Herek");
            driver.FindElement(By.XPath(appSettings["Txt_Register_Email"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Register_Email"])).SendKeys("test@test.tc");
            driver.FindElement(By.XPath(appSettings["Chb_Register_Terms"])).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Register_Reset"])).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Register_Register"])).Click();
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
        public void Test005_SearchSelect()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);


            Assert.IsTrue(IsElementPresent(By.XPath(appSettings["Lnk_Search_LogOut"])));
            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText("Sydney");
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Register_Register"])).Click();
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

        public void Test006_TitleCheck()
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
        
        public void Test007_ResetPassword()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(username, password);
            driver.FindElement(By.XPath(appSettings["Lnk_Search_ChangePassWord"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassWord"])).SendKeys(password);
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassWord"])).SendKeys("adactin456");
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).SendKeys("adactin456");
            driver.FindElement(By.XPath(appSettings["Btn_PassWord_Submit"])).Click();
            password = "adactin456";
            LoginMethod(username, password);
            driver.FindElement(By.XPath(appSettings["Lnk_Search_ChangePassWord"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassword"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_CurrentPassword"])).SendKeys(password);
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassword"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_NewPassword"])).SendKeys("adactin123");
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_PassWord_ConfirmPassWord"])).SendKeys("adactin123");
            driver.FindElement(By.XPath(appSettings["Btn_PassWord_Submit"])).Click();
            var result = driver.FindElement(By.XPath("//span[@class='reg_error'][contains(.,'Your Password is successfully updated!!!')]")).Text;
            Assert.AreEqual("Your Password is successfully updated!!!", result);
            password = "adactin123";

        }

        [Test]
        public void Test008_CancelItinerary()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(username, password);

            SearchMethod(location);

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
            driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"]))).SelectByText("2021");
            driver.FindElement(By.XPath("//option[@value='2021']")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).SendKeys("123");
            driver.FindElement(By.XPath(appSettings["Btn_Book_Book"])).Click();
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            driver.FindElement(By.XPath("(//input[@type='button'])[1]")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            var b = driver.FindElement(By.XPath("//label[contains(@class,'reg_error')]")).Text;
            Assert.AreEqual("0 result(s) found. Show all", b);

        }

        [Test]

        public void Test009_SearchFieldValidation()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(username, password);
            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
            string message = driver.FindElement(By.XPath("//span[contains(.,'Please Select a Location')]")).Text;
            Assert.AreEqual("Please Select a Location", message);
        }

        [Test]

        public void Test010_CheckLoginGreeting()
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
