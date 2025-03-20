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
    public class PurchaseOrder
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PurchaseOrder(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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
                Thread.Sleep(1000);

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Purchase Order
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h4[contains(text(), 'Purchase Order')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Purchase Order page is accessible.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Purchase Order header not found.");
                    //ระบุว่า Test Failed
                    Assert.Fail("Failed to access the Purchase Orders page."); 
                }
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
                Thread.Sleep(1000);

                //เลือกมาหนึ่งรายการ (ในที่นี้จะเลือก PO2503022)
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503022']")));
                poListButton.Click();
                Thread.Sleep(3000);
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
                Thread.Sleep(2000);
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
                var filterButton = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-objects-column')]]")));
                filterButton.Click();
                var poFilterButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='Purchase Order No.']")));
                poFilterButton.Click();
                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Save']")));
                saveButton.Click();
                Thread.Sleep(2000);
                var filerButtonTwo = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-objects-column')]]")));
                filerButtonTwo.Click();
                Thread.Sleep(2000);

                //กดปุ่ม Reset การ Filter
                var resetButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Reset']")));
                resetButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseOrderAscendDescend()
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

                //กดหัวข้อตารางเพื่อเรียงลำดับรายการ
                var poNoHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Purchase Order No.')]]")));
                poNoHeaderButton.Click();
                Thread.Sleep(1000);
                poNoHeaderButton.Click();
                Thread.Sleep(1000);
                var poDateHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Purchase Order Date')]]")));
                poDateHeaderButton.Click();
                Thread.Sleep(1000);
                poDateHeaderButton.Click();
                Thread.Sleep(1000);
                var poDeliveryHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Delivery Date')]]")));
                poDeliveryHeaderButton.Click();
                Thread.Sleep(1000);
                poDeliveryHeaderButton.Click();
                Thread.Sleep(1000);
                var poVendorHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Vendor')]]")));
                poVendorHeaderButton.Click();
                Thread.Sleep(1000);
                poVendorHeaderButton.Click();
                Thread.Sleep(1000);
                var poProjectHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Project')]]")));
                poProjectHeaderButton.Click();
                Thread.Sleep(1000);
                poProjectHeaderButton.Click();
                Thread.Sleep(1000);
                var poDepartmentHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Department')]]")));
                poDepartmentHeaderButton.Click();
                Thread.Sleep(1000);
                poDepartmentHeaderButton.Click();
                Thread.Sleep(1000);
                var poStatusHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Status')]]")));
                poStatusHeaderButton.Click();
                Thread.Sleep(1000);
                poStatusHeaderButton.Click();
                Thread.Sleep(1000);
                var poAmountHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Amount')]]")));
                poAmountHeaderButton.Click();
                Thread.Sleep(1000);
                poAmountHeaderButton.Click();
                Thread.Sleep(1000);
                var poReceivedHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Received')]]")));
                poReceivedHeaderButton.Click();
                Thread.Sleep(1000);
                poReceivedHeaderButton.Click();
                Thread.Sleep(1000);
                var poBalanceHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Balance')]]")));
                poBalanceHeaderButton.Click();
                Thread.Sleep(1000);
                poBalanceHeaderButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseOrderSearch()
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

                //ใส่ข้อมูลและกดค้นหา
                var poSearchButton = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search purchase order']")));
                poSearchButton.SendKeys("PO2501014");
                Thread.Sleep(1000);
                var defaultSearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Search']")));
                defaultSearchButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseOrderAdvSearch()
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

                //ใส่ข้อมูลและกดค้นหา
                var barsButton = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-bars')]]")));
                barsButton.Click();
                var poNoSearch = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Enter Purchase Order No.']")));
                poNoSearch.SendKeys("PO2501014");
                Thread.Sleep(1000);
                var advSearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='submit' and @aria-label='Search']")));
                advSearchButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseOrderAdvSearchClear()
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

                //ใส่ข้อมูลและกดค้นหา
                var barsButton = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-bars')]]")));
                barsButton.Click();
                var poNoSearch = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Enter Purchase Order No.']")));
                poNoSearch.SendKeys("PO2501014");
                Thread.Sleep(1000);
                var advSearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='submit' and @aria-label='Search']")));
                advSearchButton.Click();
                Thread.Sleep(1000);

                //กดปุ่ม Clear
                var searchClearButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Clear']")));
                searchClearButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseOrderSave()
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
                Thread.Sleep(1000);

                //กดปุ่ม New Purchase Order
                var newPOButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Purchase Order')]")));
                newPOButton.Click();
                Thread.Sleep(1000);

                //กดเลือก Vendor (Error)
                var vendorDropdownErrorOne = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownErrorOne.Click();
                Thread.Sleep(1000);
                var vendorOptionErrorOne = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Vendor 1'] and div/small[text()='VD-004']]")));
                vendorOptionErrorOne.Click();
                Thread.Sleep(1000);
                var vendorDropdownErrorTwo = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownErrorTwo.Click();
                Thread.Sleep(1000);
                var vendorOptionErrorTwo = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Space Penny'] and div/small[text()='S-P']]")));
                vendorOptionErrorTwo.Click();
                Thread.Sleep(1000);

                //กรอกข้อมูลและ Save
                var vendorDropdownPass = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownPass.Click();
                Thread.Sleep(1000);
                var vendorOptionButton = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Vendor 1'] and div/small[text()='VD-003']]")));
                vendorOptionButton.Click();
                Thread.Sleep(1000);
                var paymentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select payment term')]")));
                paymentDropdown.Click();
                Thread.Sleep(1000);
                var paymentOption = wait.Until(d => d.FindElement(By.XPath("//li[span[text()='100% ก่อนส่งมอบสินค้า']]")));
                paymentOption.Click();
                Thread.Sleep(1000);
                var picDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select sales person')]")));
                picDropdown.Click();
                Thread.Sleep(1000);
                var picOption = wait.Until(d => d.FindElement(By.Id("pic_1")));
                picOption.Click();
                Thread.Sleep(1000);
                var projectDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select project')]")));
                projectDropdown.Click();
                Thread.Sleep(1000);

                var projectOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='พัฒนาเว็บไซต์'] and div/small[text()='PRJ004']]")));
                projectOption.Click();
                Thread.Sleep(1000);
                var templateDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select template')]")));
                templateDropdown.Click();
                Thread.Sleep(1000);
                var templateOption = wait.Until(d => d.FindElement(By.XPath("//li[span[text()='PO with barcode']]")));
                templateOption.Click();
                Thread.Sleep(1000);
                var departmentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select project')]")));
                departmentDropdown.Click();
                Thread.Sleep(1000);
                var departmentOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='ทดสอบ'] and div/small[text()='test123']]")));
                departmentOption.Click();
                Thread.Sleep(1000);

                var itemDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Selectn item')]")));
                itemDropdown.Click();
                Thread.Sleep(1000);
                var itemOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'TEST-004')]")));
                itemOption.Click();
                Thread.Sleep(1000);
                var itemQuantity = wait.Until(d => d.FindElement(By.XPath("//input[@role='spinbutton']")));
                itemQuantity.Clear();
                itemQuantity.SendKeys("5.00");
                Thread.Sleep(1000);
                var itemPrice = wait.Until(d => d.FindElement(By.XPath("(//input[@role='spinbutton'])[2]")));
                itemPrice.Clear();
                itemPrice.SendKeys("200");
                Thread.Sleep(1000);
                var itemDiscount = wait.Until(d => d.FindElement(By.XPath("(//input[@role='spinbutton'])[3]")));
                itemDiscount.Clear();
                itemDiscount.SendKeys("300");
                Thread.Sleep(1000);
                var itemVatDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select Vat')]")));
                itemVatDropdown.Click();
                Thread.Sleep(1000);
                var itemVatOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'VAT 7%')]")));
                itemVatOption.Click();
                Thread.Sleep(1000);
                var remarkField = wait.Until(d => d.FindElement(By.Id("Remark")));
                remarkField.SendKeys("ทดสอบ ทดสอบ Automated Testing");
                Thread.Sleep(1000);

                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                saveButton.Click();
                Thread.Sleep(2000);
                var confirmSave = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button')]")));
                confirmSave.Click();
                Thread.Sleep(6000);
            }
        }
        [Fact]
        public void PurchaseOrderSaveAndApprove()
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
                Thread.Sleep(1000);

                //กดปุ่ม New Purchase Order
                var newPOButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Purchase Order')]")));
                newPOButton.Click();
                Thread.Sleep(1000);

                //กดเลือก Vendor (Error)
                var vendorDropdownErrorOne = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownErrorOne.Click();
                Thread.Sleep(1000);
                var vendorOptionErrorOne = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Vendor 1'] and div/small[text()='VD-004']]")));
                vendorOptionErrorOne.Click();
                Thread.Sleep(1000);
                var vendorDropdownErrorTwo = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownErrorTwo.Click();
                Thread.Sleep(1000);
                var vendorOptionErrorTwo = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Space Penny'] and div/small[text()='S-P']]")));
                vendorOptionErrorTwo.Click();
                Thread.Sleep(1000);

                //กรอกข้อมูลและ Save
                var vendorDropdownPass = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select vendor')]")));
                vendorDropdownPass.Click();
                Thread.Sleep(1000);
                var vendorOptionButton = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='Vendor 1'] and div/small[text()='VD-003']]")));
                vendorOptionButton.Click();
                Thread.Sleep(1000);
                var paymentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select payment term')]")));
                paymentDropdown.Click();
                Thread.Sleep(1000);
                var paymentOption = wait.Until(d => d.FindElement(By.XPath("//li[span[text()='100% ก่อนส่งมอบสินค้า']]")));
                paymentOption.Click();
                Thread.Sleep(1000);
                var picDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select sales person')]")));
                picDropdown.Click();
                Thread.Sleep(1000);
                var picOption = wait.Until(d => d.FindElement(By.Id("pic_1")));
                picOption.Click();
                Thread.Sleep(1000);
                var projectDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select project')]")));
                projectDropdown.Click();
                Thread.Sleep(1000);

                var projectOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='พัฒนาเว็บไซต์'] and div/small[text()='PRJ004']]")));
                projectOption.Click();
                Thread.Sleep(1000);
                var templateDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select template')]")));
                templateDropdown.Click();
                Thread.Sleep(1000);
                var templateOption = wait.Until(d => d.FindElement(By.XPath("//li[span[text()='PO with barcode']]")));
                templateOption.Click();
                Thread.Sleep(1000);
                var departmentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select project')]")));
                departmentDropdown.Click();
                Thread.Sleep(1000);
                var departmentOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='ทดสอบ'] and div/small[text()='test123']]")));
                departmentOption.Click();
                Thread.Sleep(1000);

                var itemDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Selectn item')]")));
                itemDropdown.Click();
                Thread.Sleep(1000);
                var itemOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'TEST-004')]")));
                itemOption.Click();
                Thread.Sleep(1000);
                var itemQuantity = wait.Until(d => d.FindElement(By.XPath("//input[@role='spinbutton']")));
                itemQuantity.Clear();
                itemQuantity.SendKeys("5.00");
                Thread.Sleep(1000);
                var itemPrice = wait.Until(d => d.FindElement(By.XPath("(//input[@role='spinbutton'])[2]")));
                itemPrice.Clear();
                itemPrice.SendKeys("200");
                Thread.Sleep(1000);
                var itemDiscount = wait.Until(d => d.FindElement(By.XPath("(//input[@role='spinbutton'])[3]")));
                itemDiscount.Clear();
                itemDiscount.SendKeys("300");
                Thread.Sleep(1000);
                var itemVatDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select Vat')]")));
                itemVatDropdown.Click();
                Thread.Sleep(1000);
                var itemVatOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'VAT 7%')]")));
                itemVatOption.Click();
                Thread.Sleep(1000);
                var remarkField = wait.Until(d => d.FindElement(By.Id("Remark")));
                remarkField.SendKeys("ทดสอบ ทดสอบ Automated Testing");
                Thread.Sleep(1000);

                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save and Approve')]")));
                saveButton.Click();
                Thread.Sleep(2000);
                var confirmSave = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button')]")));
                confirmSave.Click();
                Thread.Sleep(6000);
            }
        }
        [Fact]
        public void PurchaseOrderDraft()
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

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503029']")));
                poListButton.Click();

                ////Edit
                //var poEditButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Edit']]")));
                //poEditButton.Click();
                //Thread.Sleep(3000);
                //var itemQuantity = wait.Until(d => d.FindElement(By.XPath("//input[@role='spinbutton']")));
                //itemQuantity.Clear();
                //itemQuantity.SendKeys("10.00");
                //Thread.Sleep(2000);
                //var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                //saveButton.Click();
                //Thread.Sleep(2000);
                //var confirmSave = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button')]")));
                //confirmSave.Click();
                //Thread.Sleep(2000);

                ////กดเข้าไปที่หน้า Purchase Order และกลับเข้ารายการเดิม
                //purchaseOrderButton.Click();
                //Thread.Sleep(1000);
                //var poListButtonTwo = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503025']")));
                //poListButtonTwo.Click();
                //Thread.Sleep(1000);

                //Print
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0, 0);");
                Thread.Sleep(5000);

                //var printDropdown = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-angle-down')]]")));
                //printDropdown.Click();
                //Thread.Sleep(1000);
                //var printDropdownTemplate = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select Template')]")));
                //printDropdownTemplate.Click();
                //Thread.Sleep(1000);
                //var printDropdownOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'PO with barcode')]")));
                //printDropdownOption.Click();
                //Thread.Sleep(1000);

                var printButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Print']")));
                printButton.Click();
                Thread.Sleep(2000);

                //Approve

                //Cancel
            }
        }
        [Fact]
        public void PurchaseOrderApproved()
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

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503023']")));
                poListButton.Click();

                //Edit
                var poEditButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Edit']]")));
                poEditButton.Click();
                Thread.Sleep(3000);
                var itemQuantity = wait.Until(d => d.FindElement(By.XPath("//input[@role='spinbutton']")));
                itemQuantity.Clear();
                itemQuantity.SendKeys("10.00");
                Thread.Sleep(2000);
                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                saveButton.Click();
                Thread.Sleep(2000);
                var confirmSave = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button')]")));
                confirmSave.Click();
                Thread.Sleep(2000);

                //กดเข้าไปที่หน้า Purchase Order และกลับเข้ารายการเดิม
                purchaseOrderButton.Click();
                Thread.Sleep(1000);
                var poListButtonTwo = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503023']")));
                poListButtonTwo.Click();
                Thread.Sleep(1000);

                //Print
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0, 0);");
                Thread.Sleep(1000);
                //var printDropdown = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-angle-down')]]")));
                //printDropdown.Click();
                //Thread.Sleep(1000);
                //var printDropdownTemplate = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select Template')]")));
                //printDropdownTemplate.Click();
                //Thread.Sleep(1000);
                //var printDropdownOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'PO with barcode')]")));
                //printDropdownOption.Click();
                //Thread.Sleep(1000);
                var printButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Print']")));
                printButton.Click();
                Thread.Sleep(2000);

                //Approve

                //Cancel
            }
        }
        [Fact]
        public void PurchaseOrderCanceled()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void PurchaseOrderCopy()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void PurchaseOrderCopyEdit()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void PurchaseOrderExport()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);

                //กดปุ่ม Export
                var purchaseOrderExport = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Export']")));
                purchaseOrderExport.Click();
                Thread.Sleep(4000);
            }
        }
        [Fact]
        public void PurchaseOrderApprove()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);
                
                //เลือก Checkbox
                var pruchaseOrderCheckboxOne = wait.Until(d => d.FindElement(By.XPath("//tr[@data-p-index='5']//input[@type='checkbox']")));
                pruchaseOrderCheckboxOne.Click();
                Thread.Sleep(1000);
                var pruchaseOrderCheckboxTwo = wait.Until(d => d.FindElement(By.XPath("//tr[@data-p-index='6']//input[@type='checkbox']")));
                pruchaseOrderCheckboxTwo.Click();
                Thread.Sleep(10000);

                //กดปุ่ม Approve
                var purchaseOrderApprove = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Approve']")));
                purchaseOrderApprove.Click();
                Thread.Sleep(10000);
            }
        }
        [Fact]
        public void PurchaseOrderGear()
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
                Thread.Sleep(1000);

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void PurchaseOrderSpecial()
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

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503031']")));
                poListButton.Click();

                var poMore = wait.Until(d => d.FindElement(By.XPath("//button[.//span[text()='More Actions']]")));
                poMore.Click();
                Thread.Sleep(10000);
                var poMark = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Mark as Sent']")));
                poMark.Click();
                Thread.Sleep(10000);

                ////Edit
                //var poEditButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Edit']]")));
                //poEditButton.Click();
                //Thread.Sleep(3000);
                //var itemQuantity = wait.Until(d => d.FindElement(By.XPath("//input[@role='spinbutton']")));
                //itemQuantity.Clear();
                //itemQuantity.SendKeys("10.00");
                //Thread.Sleep(2000);
                //var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                //saveButton.Click();
                //Thread.Sleep(2000);
                //var confirmSave = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button')]")));
                //confirmSave.Click();
                //Thread.Sleep(2000);

                ////กดเข้าไปที่หน้า Purchase Order และกลับเข้ารายการเดิม
                //purchaseOrderButton.Click();
                //Thread.Sleep(1000);
                //var poListButtonTwo = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503025']")));
                //poListButtonTwo.Click();
                //Thread.Sleep(1000);

                //Print
                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                //js.ExecuteScript("window.scrollTo(0, 0);");
                //Thread.Sleep(5000);

                //var printDropdown = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-angle-down')]]")));
                //printDropdown.Click();
                //Thread.Sleep(1000);
                //var printDropdownTemplate = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select Template')]")));
                //printDropdownTemplate.Click();
                //Thread.Sleep(1000);
                //var printDropdownOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'PO with barcode')]")));
                //printDropdownOption.Click();
                //Thread.Sleep(1000);

                //var printButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Print']")));
                //printButton.Click();
                //Thread.Sleep(2000);

                //Approve

                //Cancel
            }
        }
    }
}