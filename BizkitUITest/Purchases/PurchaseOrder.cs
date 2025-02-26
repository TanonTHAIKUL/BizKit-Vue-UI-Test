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

namespace Purchases
{
    [Collection("Sequential Tests")]
    public class PurchaseOrder
    {
        private readonly ITestOutputHelper testOutputHelper;

        public PurchaseOrder(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void PurchaseOrderAccess()
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
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(5000);
            }
        }
        [Fact]
        public void PurchaseOrderCheckbox()
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
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);

                //กด Checkbox ติ๊กถูกและกดอีกครั้งเพื่อติ๊กออก
                var checkbox = wait.Until(d => d.FindElement(By.CssSelector("input.p-checkbox-input")));
                checkbox.Click();
                Thread.Sleep(2000);
                checkbox.Click();
                Thread.Sleep(5000);
            }
        }
        [Fact]
        public void PurchaseOrderFilter()
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
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //กดปุ่ม Filter ใบสั่งซื้อ
                var filterButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-objects-column')]]")));
                filterButton.Click();

                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='เลขที่ใบสั่งซื้อ']")));
                poList.Click();

                Thread.Sleep(5000);
            }
        }
        [Fact]
        public void PurchaseOrderList()
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
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Order หนึ่งรายการ (PO2502017 คือเลขที่ใบของรายการที่เลือกมา)
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2502017']")));
                poList.Click();
                Thread.Sleep(5000);
            }
        }
    }
}