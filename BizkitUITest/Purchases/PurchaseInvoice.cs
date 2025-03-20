using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using System.Collections.Generic;
using System;
using System.Linq;
using Xunit.Abstractions;
using SeleniumExtras.WaitHelpers;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;

namespace Purchases
{
    [Collection("Sequential Tests")]
    public class PurchaseInvoice
    {
        private readonly ITestOutputHelper testOutputHelper;

        public PurchaseInvoice(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void PurchaseInvoiceAccess()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                //ขยายให้เต็มจอเพื่อกันปัญหาแถบทางซ้ายหาย
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var PurchaseInvoiceButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/PurchaseInvoices/list']")));
                PurchaseInvoiceButton.Click();
                Thread.Sleep(2000);
            }
        }
        
    }
}