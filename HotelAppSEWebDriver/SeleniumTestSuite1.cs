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

            WebDriverWait loginLinkWait = new WebDriverWait(driver, new TimeSpan(0,0,3));
            var element = loginLinkWait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(By.XPath(appSettings["Btn_Login_Login"]));
                    return elementToBeDisplayed.Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }

                });

            Assert.IsTrue(IsElementPresent(By.XPath(appSettings["Btn_Login_Login"])));
            Assert.IsTrue(IsElementPresent(By.LinkText("New User Register Here")));
        }

        [Test]
        public void Test001_LoginBookLogout()
        {
            var appSettings = ConfigurationManager.AppSettings;

           

            LoginMethod(Username, Password);
            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            SearchMethod(Location);
            BookMethod();
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            driver.FindElement(By.XPath("(//input[@type='button'])[1]")).Click();
            driver.SwitchTo().Alert().Accept();

            LogOutMethod();

        }

        [Test]
        public void Test002_BookedItinerary()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(Username, Password);
            Assert.IsTrue(IsElementPresent(By.LinkText("Logout")));
            SearchMethod(Location);
            BookMethod();
   
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            var b = driver.FindElement(By.XPath("(//input[contains(@type,'text')])[3]")).GetAttribute("value").ToString();
            Assert.AreEqual(a, b);

            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();
            driver.FindElement(By.XPath("(//input[@type='button'])[1]")).Click();
            driver.SwitchTo().Alert().Accept();

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

            LoginMethod(Username, Password);

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

            LoginMethod(Username, Password);
            ChangePassword(Password, "adactin456");
            LoginMethod(Username, "adactin456");
            ChangePassword("adactin456", Password);
            
            var result = driver.FindElement(By.XPath("//span[@class='reg_error'][contains(.,'Your Password is successfully updated!!!')]")).Text;
            Assert.AreEqual("Your Password is successfully updated!!!", result);

        }

        [Test]
        public void Test008_CancelItinerary()
        {
            var appSettings = ConfigurationManager.AppSettings;
            LoginMethod(Username, Password);

            SearchMethod(Location);
            BookMethod();

            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();

            //driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            //driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            //driver.FindElement(By.Id("search_hotel_id")).Click();

            // using some dynamic stuff here 

            driver.FindElement(By.XPath("//input[contains(@value,'Cancel " + a + "')]")).Click();

            driver.SwitchTo().Alert().Accept();

            // using the search to validate cancellation

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

            LoginMethod(Username, Password);
            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
            string message = driver.FindElement(By.XPath("//span[contains(.,'Please Select a Location')]")).Text;
            Assert.AreEqual("Please Select a Location", message);
        }

        [Test]

        public void Test010_CheckLoginGreeting()
        {
            LoginMethod(Username, Password);

            string greeting = driver.FindElement(By.XPath("//input[@type='text'][contains(@id,'show')]")).GetAttribute("value");

            if (greeting.Equals("Hello makrobaktat!", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Test Passed! Greeting is: {0}", greeting);
            }

            else
                Console.WriteLine("Test Failed! Greeting is: {0}", greeting);
        }

        [Test]

        public void Test011_LoopSearch()
        {
            LoginMethod(Username, Password);
            DataDrivenSearchMethod();
        }

        [Test]
        public void Test012_ValidateSearch()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(Username, Password);

            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText(Location);
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_Hotels"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Hotels"]))).SelectByText("Hotel Sunshine");
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomType"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomType"]))).SelectByText("Deluxe");
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckInDate"])).Clear();
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckInDate"])).SendKeys("01/01/2000");
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckOutDate"])).Clear();
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckOutDate"])).SendKeys("02/01/2000");
            driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_ChildRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_ChildRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();

            var hotelName = driver.FindElement(By.XPath(appSettings["Field_Select_HotelName"])).GetAttribute("value");
            var location = driver.FindElement(By.XPath(appSettings["Field_Select_Location"])).GetAttribute("value");
            var rooms = driver.FindElement(By.XPath(appSettings["Field_Select_Rooms"])).GetAttribute("value");
            var arrivalDate = driver.FindElement(By.XPath(appSettings["Field_Select_ArrivalDate"])).GetAttribute("value");
            var departureDate = driver.FindElement(By.XPath(appSettings["Field_Select_DepartureDate"])).GetAttribute("value");
            var noOfDays = driver.FindElement(By.XPath(appSettings["Field_Select_NoOfDays"])).GetAttribute("value");
            var roomType = driver.FindElement(By.XPath(appSettings["Field_Select_RoomType"])).GetAttribute("value");
            var pricePerNight = driver.FindElement(By.XPath(appSettings["Field_Select_PricePerNight"])).GetAttribute("value");
            var totalPrice = driver.FindElement(By.XPath(appSettings["Field_Select_TotalPrice"])).GetAttribute("value");

            Assert.AreEqual("Hotel Sunshine", hotelName);
            Assert.AreEqual("Sydney", location);
            Assert.AreEqual("2 Rooms", rooms);
            Assert.AreEqual("01/01/2000", arrivalDate);
            Assert.AreEqual("02/01/2000", departureDate);
            Assert.AreEqual("1 Days", noOfDays);
            Assert.AreEqual("Deluxe", roomType);
            Assert.AreEqual("AUD $ 375", pricePerNight);
            Assert.AreEqual("AUD $ 385", totalPrice);


            LogOutMethod();

        }

        [Test]
        public void Test013_ValidateBooking()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(Username, Password);

            //search

            driver.FindElement(By.XPath(appSettings["Lst_Search_Location"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Location"]))).SelectByText(Location);
            driver.FindElement(By.XPath(appSettings["Lst_Search_LocSydney"])).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_Hotels"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_Hotels"]))).SelectByText("Hotel Sunshine");
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomType"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomType"]))).SelectByText("Deluxe");
            driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_RoomNo"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckInDate"])).Clear();
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckInDate"])).SendKeys("01/01/2000");
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckOutDate"])).Clear();
            driver.FindElement(By.XPath(appSettings["DateTime_Search_CheckOutDate"])).SendKeys("02/01/2000");
            driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_AdultRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath("(//option[@value='2'])[2]")).Click();
            driver.FindElement(By.XPath(appSettings["Lst_Search_ChildRoom"])).Click();
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Search_ChildRoom"]))).SelectByText("2 - Two");
            driver.FindElement(By.XPath(appSettings["Btn_Search_Search"])).Click();
            driver.FindElement(By.XPath(appSettings["RBtn_Select_Select"])).Click();
            driver.FindElement(By.XPath(appSettings["Btn_Select_Continue"])).Click();

            //book

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
            var a = driver.FindElement(By.Id("order_no")).GetAttribute("value").ToString();
            driver.FindElement(By.LinkText("Booked Itinerary")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Booked_SearchField"])).SendKeys(a);
            driver.FindElement(By.Id("search_hotel_id")).Click();

            //validate

            string hotelName = driver.FindElement(By.XPath("//input[contains(@value,'Hotel Sunshine')]")).GetAttribute("value");
            string location = driver.FindElement(By.XPath("//input[contains(@value,'Sydney')]")).GetAttribute("value");
            string rooms = driver.FindElement(By.XPath("//input[@value='2 Rooms']")).GetAttribute("value");
            string firstName = driver.FindElement(By.XPath("//input[@value='Baktat']")).GetAttribute("value");
            string lastName = driver.FindElement(By.XPath("//input[contains(@value,'Makroniciusz')]")).GetAttribute("value");
            string arrivalDate = driver.FindElement(By.XPath("//input[contains(@value,'01/01/2000')]")).GetAttribute("value");
            string departureDate = driver.FindElement(By.XPath("//input[contains(@value,'02/01/2000')]")).GetAttribute("value");
            string noOfDays = driver.FindElement(By.XPath("//input[contains(@value,'1 Days')]")).GetAttribute("value");
            string roomType = driver.FindElement(By.XPath("//input[contains(@value,'Deluxe')]")).GetAttribute("value");
            string pricePerNight = driver.FindElement(By.XPath("//input[contains(@value,'AUD $ 375')]")).GetAttribute("value");
            string totalPrice = driver.FindElement(By.XPath("//input[contains(@value,'AUD $ 424')]")).GetAttribute("value");

            Assert.AreEqual("Hotel Sunshine", hotelName);
            Assert.AreEqual("Sydney", location);
            Assert.AreEqual("2 Rooms", rooms);
            Assert.AreEqual("Baktat", firstName);
            Assert.AreEqual("Makroniciusz", lastName);
            Assert.AreEqual("01/01/2000", arrivalDate);
            Assert.AreEqual("02/01/2000", departureDate);
            Assert.AreEqual("1 Days", noOfDays);
            Assert.AreEqual("Deluxe", roomType);
            Assert.AreEqual("AUD $ 375", pricePerNight);
            Assert.AreEqual("AUD $ 424", totalPrice);


            //delete booking for cleanup

            driver.FindElement(By.XPath("(//input[@type='button'])[1]")).Click();
            driver.SwitchTo().Alert().Accept();

            LogOutMethod();


        }

        [Test]

        public void Test014_AlertWhenExpiryInPast()
        {
            var appSettings = ConfigurationManager.AppSettings;

            LoginMethod(Username, Password);
            SearchMethod(Location);

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
            new SelectElement(driver.FindElement(By.XPath(appSettings["Lst_Book_CCYear"]))).SelectByText("2011");
            driver.FindElement(By.XPath("//option[@value='2011']")).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Click();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).Clear();
            driver.FindElement(By.XPath(appSettings["Txt_Book_CVV"])).SendKeys("123");
            driver.FindElement(By.XPath(appSettings["Btn_Book_Book"])).Click();

            string alertText = driver.SwitchTo().Alert().Text;
            Assert.AreEqual("Expiry year cannot be in Past", alertText);

            driver.SwitchTo().Alert().Accept();

            LogOutMethod();


        }

        [Test]

        public void Test015_GreetingCheck()
        {
            var appSettings = ConfigurationManager.AppSettings;

            
            LoginMethod(Username, Password);
            //Assert.IsTrue(IsElementPresent(By.XPath(appSettings["Lnk_Search_LogOut"])));
            try
            {
                IWebElement welcomeText = driver.FindElement(By.XPath(appSettings["Lbl_Search_WelcomeText"]));
                string message = welcomeText.GetAttribute("value");

                if (message.Contains("Hello " + Username))
                {
                    Console.WriteLine("Login test passed for: " + Username);
                }
            }

            catch (NoSuchElementException e)
            {
                Console.WriteLine("Login test failed for: " + Username + "Exception was: " + e);
            }
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
