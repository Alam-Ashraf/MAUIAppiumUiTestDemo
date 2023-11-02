using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MAUIAppiumUITestDemo.Test
{
    [TestFixture]
    public class BaseTest
    {
        protected AppiumDriver<AppiumWebElement> driver;
        private Uri driverUri = new Uri("http://127.0.0.1:4723/wd/hub");
        public Plateform Plateform;

        public BaseTest()
        {
            Console.WriteLine("BaseTest() constructor called");
        }

        public void GetPlateforms()
        {
            string? plateform = Environment.GetEnvironmentVariable("Platform");

            Console.WriteLine($"Platform = {plateform}");

            if (string.IsNullOrEmpty(plateform))
            {
                plateform = "android";
            }

            if (plateform.ToLower().Equals("android"))
            {
                Plateform = Plateform.Android;
                StartAndroidDriver();
            }
            else if (plateform.ToLower().Equals("ios"))
            {
                Plateform = Plateform.iOS;
                StartiOSDriver();
            }
            else if (plateform.ToLower().Equals("both"))
            {
                Plateform = Plateform.Both;
                StartAndroidDriver();
            }
        }

        public void GetEnvironmentVariable()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();

            foreach (var key in environmentVariables.Keys)
            {
                var value = environmentVariables[key];
                Console.WriteLine($"{key} = {value}");
            }
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp() method called");
            GetPlateforms();
            driver.LaunchApp();
        }

        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("TearDown() method called");

            //Thread.Sleep(2000);

            //driver.Quit();
        }

        private void RuniOS()
        {
            if (Plateform == Plateform.Both)
            {
                StartiOSDriver();
                Plateform = Plateform.None;
                SetUp();
            }
        }

        private void StartAndroidDriver()
        {
            // Set up Android driver
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android 13 - API 33");
            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, "emulator-5554");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, "/Users/alamgeer/Projects/XFAppiumPOC/XFAppiumPOC/XFAppiumPOC.Android/bin/Debug/com.companyname.xfappiumpoc-Signed.apk");

            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.companyname.mauiappiumuitestdemo");
            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc649520cd8e207e2449.MainActivity"); 
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "uiAutomator2");

            driver = new AndroidDriver<AppiumWebElement>(driverUri, appiumOptions);

            Console.WriteLine("----------------- Android Driver Set");
        }


        private void StartiOSDriver()
        {
            // Set up iOS driver
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone 14 Pro Max");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "16.4");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.Udid, "CD1606B3-6829-45AD-9DB8-848E55D08161");
            appiumOptions.AddAdditionalCapability(IOSMobileCapabilityType.BundleId, "com.companyname.mauiappiumuitestdemo");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");

            driver = new IOSDriver<AppiumWebElement>(driverUri, appiumOptions);

            Console.WriteLine("----------------- iOS Driver Set");
        }



        //private bool IsAndroidPlatform()
        //{
        //    return Environment.GetCommandLineArgs().Any(arg => arg.Equals("--android"));
        //}

        //private bool IsiOSPlatform()
        //{
        //    return Environment.GetCommandLineArgs().Any(arg => arg.Equals("--ios"));
        //}

        public string GetElementText(string elementId)
        {
            var element = driver.FindElement(By.Id(elementId));
            var attributName = (Plateform == Plateform.Android) ? "text" : "value";
            return element.GetAttribute(attributName);
        }

        public void SendEmail()
        {
            // Email configuration
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "wft.alamgeer.ashraf@gmail.com";
            string smtpPassword = "nfwo emkh exlx ybtb";
            string senderEmail = "wft.alamgeer.ashraf@gmail.com";
            string recipientEmail = "wft.alamgeer.ashraf@gmail.com";

            // Create an email message
            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = "Mobile UI Automation Report";
            mail.Body = "Please find the attached HTML report.";

            // Attach the HTML report file
            string androidFilePath = "/Users/alamgeer/buildAgentFull (1)/TestResults/Android-TestResults.html";
            string iOSFilePath = "/Users/alamgeer/buildAgentFull (1)/TestResults/iOS-TestResults.html";
            mail.Attachments.Add(new Attachment(androidFilePath, MediaTypeNames.Text.Html));
            mail.Attachments.Add(new Attachment(iOSFilePath, MediaTypeNames.Text.Html));

            // Create an SMTP client and send the email
            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.Port = smtpPort;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true; // Use SSL if your SMTP server supports it

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                mail.Dispose();
                smtpClient.Dispose();
            }
        }
    }


    public enum Plateform
    {
        Android,
        iOS,
        Both,
        None
    }
}