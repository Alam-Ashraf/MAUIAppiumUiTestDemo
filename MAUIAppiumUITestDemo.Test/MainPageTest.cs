using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIAppiumUITestDemo.Test
{
    [TestFixture]
    public class MainPageTest : BaseTest
    {
        public MainPageTest() : base()
        {
        }

        [Test, Order(1)]
        // [Order(1)]
        public void TestLogin()
        {
            GetPlateforms();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var userName = driver.FindElement(By.Id("UserName"));
            userName.SendKeys("user@email.com");

            driver.FindElement(By.Id("UserPassword")).SendKeys("123456");
            driver.FindElement(By.Id("LoginButton")).Click();

            Assert.Pass();
        }

        [Test, Order(2)]
        [TestCase(10, 8, 18)]
        //[TestCase(45, 45, 90)]
        //[Order(2)]
        public void TestAddFunctionality(int a, int b, int res)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("Num1")).SendKeys(a.ToString());
            driver.FindElement(By.Id("Num2")).SendKeys(b.ToString());

            driver.FindElement(By.Id("Addition")).Click();

            Thread.Sleep(2000);

            var text = driver.FindElement(By.Id("ResultText")).Text;

            Console.WriteLine($"ResultText Label = {text}");

            Assert.IsNotNull(text);
            Assert.IsTrue(text.Equals(res.ToString()));

            ClearFields();
        }

        [Test, Order(3)]
        [TestCase(10, 8, 80)]
        [TestCase(11, 5, 55)]
        //[Order(3)]
        public void TestMultiplyFunctionality(int a, int b, int res)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("Num1")).SendKeys(a.ToString());
            driver.FindElement(By.Id("Num2")).SendKeys(b.ToString());

            driver.FindElement(By.Id("Multiplication")).Click();
            var text = driver.FindElement(By.Id("ResultText")).Text;

            Console.WriteLine($"ResultText Label = {text}");

            Assert.IsNotNull(text);
            Assert.IsTrue(text.Equals(res.ToString()));

            ClearFields();

            //SendEmail();
        }

        private void ClearFields()
        {
            driver.FindElement(By.Id("Num1")).Clear();
            driver.FindElement(By.Id("Num2")).Clear();
        }

        [OneTimeTearDown]
        public void SuiteTeardown()
        {
            // This method runs once after all test cases in the test suite
            // Clean up resources, release environment, etc.

            Console.WriteLine("SuiteTeardown() method called");
        }

    }
}
