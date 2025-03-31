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
                    _testOutputHelper.WriteLine("❌ Test Failed: Purchase Order page is not accessible.");
                    Assert.Fail("❌ Test Failed: Purchase Order page is not accessible."); 
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(1000);

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503063']")));
                poListButton.Click();

                //ตรวจสอบว่าเลข PO ด้านบนขวาแสดงผลถูกต้อง
                var poNumberElement = wait.Until(d => d.FindElement(
                    By.XPath("//div[contains(@class, 'text-2xl') and text()='PO2503063']")));
                string actualPO = poNumberElement.Text.Trim();

                Assert.Equal("PO2503063", actualPO);
                _testOutputHelper.WriteLine($"✔ Test Passed Correct PO Number displayed: {actualPO}");
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);

                //ใส่ข้อมูลและกดค้นหา
                string targetPO = "PO2501014";

                var searchInput = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search purchase order']")));
                searchInput.Clear();
                searchInput.SendKeys(targetPO);

                var searchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Search']")));
                searchButton.Click();
                Thread.Sleep(3000);

                var resultPO = wait.Until(d => d.FindElement(By.XPath($"//span[contains(@class, 'p-button-label') and text()='{targetPO}']")));

                //แสดงผลการทดสอบ
                Assert.True(resultPO.Displayed, $"❌ Test Failed: {targetPO} was not displayed in the search results.");
                _testOutputHelper.WriteLine($"✔ Test Passed: {targetPO} was successfully found in the search results.");
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(2000);

                string targetPO = "PO2501014";

                //ใส่ข้อมูลและกดค้นหา
                var barsButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(@class, 'p-button') and i[contains(@class, 'pi-bars')]]")));
                barsButton.Click();
                var poNoSearch = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Enter Purchase Order No.']")));
                poNoSearch.Clear();
                poNoSearch.SendKeys(targetPO);
                Thread.Sleep(1000);
                var advSearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='submit' and @aria-label='Search']")));
                advSearchButton.Click();
                Thread.Sleep(3000);

                var resultPO = wait.Until(d => d.FindElement(By.XPath($"//span[contains(@class, 'p-button-label') and text()='{targetPO}']")));

                //แสดงผลการทดสอบ
                Assert.True(resultPO.Displayed, $"❌ Test Failed: {targetPO} was not displayed in the search results.");
                _testOutputHelper.WriteLine($"✔ Test Passed: {targetPO} was successfully found in the search results.");
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(1000);

                //กดปุ่ม New Purchase Order
                var newPOButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Purchase Order')]")));
                newPOButton.Click();
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
                var picDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select person in charge')]")));
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
                var departmentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select department')]")));
                departmentDropdown.Click();
                Thread.Sleep(1000);
                var departmentOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='ทดสอบ'] and div/small[text()='test123']]")));
                departmentOption.Click();
                Thread.Sleep(1000);
                var shippingDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select shipping address')]")));
                shippingDropdown.Click();
                Thread.Sleep(1000);
                var shippingOption = wait.Until(d => d.FindElement(By.XPath("//div[span[text()='Office'] and small[contains(text(), '90 CW-Tower')]]")));
                shippingOption.Click();
                Thread.Sleep(1000);

                var itemDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select item')]")));
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
                remarkField.SendKeys("ทดสอบ Automated Testing");
                Thread.Sleep(1000);

                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                saveButton.Click();
                Thread.Sleep(1000);

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");

                //ตรวจสอบว่าสถานะของเอกสารที่เพิ่มเข้ามาคือ Draft
                bool isApproved = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Draft", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Test Failed: Can't add new purchase order.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully added new purchase order.");
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();
                Thread.Sleep(1000);

                //กดปุ่ม New Purchase Order
                var newPOButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Purchase Order')]")));
                newPOButton.Click();
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
                var picDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select person in charge')]")));
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
                var departmentDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select department')]")));
                departmentDropdown.Click();
                Thread.Sleep(1000);
                var departmentOption = wait.Until(d => d.FindElement(By.XPath("//li[div/span[text()='ทดสอบ'] and div/small[text()='test123']]")));
                departmentOption.Click();
                Thread.Sleep(1000);
                var shippingDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select shipping address')]")));
                shippingDropdown.Click();
                Thread.Sleep(1000);
                var shippingOption = wait.Until(d => d.FindElement(By.XPath("//div[span[text()='Office'] and small[contains(text(), '90 CW-Tower')]]")));
                shippingOption.Click();
                Thread.Sleep(1000);

                var itemDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and contains(., 'Select item')]")));
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
                remarkField.SendKeys("ทดสอบ Automated Testing");
                Thread.Sleep(1000);

                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save and Approve')]")));
                saveButton.Click();
                Thread.Sleep(1000);

                var confirmSaveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-confirmdialog-accept-button') and @aria-label='Save']")));
                confirmSaveButton.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");

                //ตรวจสอบว่าสถานะของเอกสารที่เพิ่มเข้ามาคือ Draft
                bool isApproved = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Approved", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Test Failed: Can't add new purchase order.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully added new purchase order.");
            }
        }
        [Fact]
        public void PurchaseOrderDraftPrint()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503067']")));
                poListButton.Click();

                //Print
                var printDropdown = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-angle-down')]]")));
                printDropdown.Click();
                Thread.Sleep(1000);
                var originalNo = wait.Until(d => d.FindElement(By.XPath("//label[text()='No of Originals']/following-sibling::span//input")));
                originalNo.Clear();
                originalNo.SendKeys("2");
                var copyNo = wait.Until(d => d.FindElement(By.XPath("//label[text()='No of Copies']/following-sibling::span//input")));
                copyNo.Clear();
                copyNo.SendKeys("2");
                //กดปุ่ม Print
                var printButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='button' and @aria-label='Print']")));
                printButton.Click();
                Thread.Sleep(2000);

                var originalTab = driver.CurrentWindowHandle;

                //หลักจากกดปุ่ม Print รอแท็บใหม่ขึ้นมา
                wait.Until(d => d.WindowHandles.Count > 1);

                //สลับไปที่หน้าแท็บใหม่
                var newTab = driver.WindowHandles.Last();
                driver.SwitchTo().Window(newTab);
                Thread.Sleep(2000);

                //ตรวจหา PDF Viewer (iframe, embed, or object)
                bool pdfLoaded = wait.Until(d =>
                {
                    try
                    {
                        var viewer = d.FindElement(By.XPath("//embed | //iframe | //object"));
                        return viewer.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(pdfLoaded, "❌ Test Failed: Failed to print the documents.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully printed documents.");
            }
        }
        [Fact]
        public void PurchaseOrderDraftApprove()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503045']")));
                poListButton.Click();

                //กดปุ่ม Approve
                var approveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Approve']]")));
                approveButton.Click();

                //รอจนกว่าสถานะ "Draft" จะเปลี่ยนเป็น "Approved"
                bool isApproved = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Approved", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Test Failed: Can't approve the document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully approved document.");
            }
        }
        [Fact]
        public void PurchaseOrderDraftEdit()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503044']")));
                poListButton.Click();

                //กดปุ่ม Edit เพื่อแก้ไข
                var editButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Edit']]")));
                editButton.Click();

                IWebElement discountInput = wait.Until(d => d.FindElement(By.XPath("//label[contains(text(), 'Discount')]/following::input[@role='spinbutton'][1]")));
                Thread.Sleep(1000);

                //เลื่อนจอลงมาด้านล่าง
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", discountInput);
                Thread.Sleep(3000);

                //ใส่ข้อมูลส่วนลด
                discountInput.Clear();
                discountInput.SendKeys("10");

                //กดปุ่ม Save เพื่อบันทึกข้อมูล
                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Save']]")));
                saveButton.Click();
                Thread.Sleep(3000);

                //ดึงข้อมูลราคามา
                var subtotalElement = wait.Until(d => d.FindElement(By.XPath("//label[text()='Subtotal']/following-sibling::div")));

                string subtotalText = subtotalElement.Text.Trim();
                string subtotalClean = new string(subtotalText.Where(c => char.IsDigit(c) || c == '.').ToArray());
                decimal subtotal = decimal.Parse(subtotalClean);

                //คำนวณราคาที่ควรจะเป็นหลักจากส่วนลด 10%
                decimal expectedTotal = Math.Round(subtotal * 0.9m, 2);

                //อ่านค่าราคาหลักจากลบส่วนลดแล้ว
                var totalElement = wait.Until(d => d.FindElement(By.XPath("//label[text()='Base Total Amount']/following-sibling::div")));

                string totalText = totalElement.Text.Trim();
                string totalClean = new string(totalText.Where(c => char.IsDigit(c) || c == '.').ToArray());
                decimal actualTotal = decimal.Parse(totalClean);

                //แจ้งผลการทดสอบ
                Assert.Equal(expectedTotal, actualTotal);

                //แสดงผลของการคำนวณ
                _testOutputHelper.WriteLine($"✔ Subtotal: {subtotal}, Expected Total (after 10%): {expectedTotal}, Actual Total: {actualTotal}");
            }
        }
        [Fact]
        public void PurchaseOrderDraftCopy()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503064']")));
                poListButton.Click();

                //บันทึกหมายเลขหน้าเก่าเพื่อไปเทียบ
                var originalPO = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'text-2xl') and starts-with(text(), 'PO')]"))).Text.Trim();

                //กดปุ่ม More Action ดูคำสั่งอื่น ๆ
                var moreActionsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='More Actions']]")));
                moreActionsButton.Click();

                //กดปุ่ม Copy
                var copyButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Copy']")));
                copyButton.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                //กดปุ่ม Save เพื่อบันทึกข้อมูล
                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Save']]")));
                saveButton.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");

                //บันทึกหมายเลขหน้าหลังจากสำเนาเพื่อไปเทียบ
                var newPO = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'text-2xl') and starts-with(text(), 'PO')]"))).Text.Trim();

                //แสดงผลการทดสอบ
                Assert.True(originalPO != newPO, "❌ Test Failed: Can't copy document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully copied document");
            }
        }
        [Fact]
        public void PurchaseOrderDraftCancel()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503055']")));
                poListButton.Click();

                //กดปุ่ม More Action ดูคำสั่งอื่น ๆ
                var moreActionsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='More Actions']]")));
                moreActionsButton.Click();

                //กดปุ่ม Cancel
                var cancelButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Cancel']")));
                cancelButton.Click();

                //รอจนกว่าสถานะ "Draft" จะเปลี่ยนเป็น "Cancelled"
                bool isCancelled = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Cancelled", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isCancelled, "❌ Test Failed: Can't cancel the document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully cancelled document.");
            }
        }
        [Fact]
        public void PurchaseOrderApprovedPrint()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503065']")));
                poListButton.Click();

                //Print
                var printDropdown = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-angle-down')]]")));
                printDropdown.Click();
                Thread.Sleep(1000);
                var originalNo = wait.Until(d => d.FindElement(By.XPath("//label[text()='No of Originals']/following-sibling::span//input")));
                originalNo.Clear();
                originalNo.SendKeys("2");
                var copyNo = wait.Until(d => d.FindElement(By.XPath("//label[text()='No of Copies']/following-sibling::span//input")));
                copyNo.Clear();
                copyNo.SendKeys("2");
                //กดปุ่ม Print
                var printButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='button' and @aria-label='Print']")));
                printButton.Click();
                Thread.Sleep(2000);

                var originalTab = driver.CurrentWindowHandle;

                //หลักจากกดปุ่ม Print รอแท็บใหม่ขึ้นมา
                wait.Until(d => d.WindowHandles.Count > 1);

                //สลับไปที่หน้าแท็บใหม่
                var newTab = driver.WindowHandles.Last();
                driver.SwitchTo().Window(newTab);
                Thread.Sleep(2000);

                //ตรวจหา PDF Viewer (iframe, embed, or object)
                bool pdfLoaded = wait.Until(d =>
                {
                    try
                    {
                        var viewer = d.FindElement(By.XPath("//embed | //iframe | //object"));
                        return viewer.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(pdfLoaded, "❌ Test Failed: Failed to print the documents.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully printed documents.");
            }
        }
        [Fact]
        public void PurchaseOrderApprovedCopy()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503066']")));
                poListButton.Click();

                //บันทึกหมายเลขหน้าเก่าเพื่อไปเทียบ
                var originalPO = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'text-2xl') and starts-with(text(), 'PO')]"))).Text.Trim();

                //กดปุ่ม More Action ดูคำสั่งอื่น ๆ
                var moreActionsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='More Actions']]")));
                moreActionsButton.Click();

                //กดปุ่ม Copy
                var copyButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Copy']")));
                copyButton.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                //กดปุ่ม Save เพื่อบันทึกข้อมูล
                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Save']]")));
                saveButton.Click();

                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");

                //บันทึกหมายเลขหน้าหลังจากสำเนาเพื่อไปเทียบ
                var newPO = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class, 'text-2xl') and starts-with(text(), 'PO')]"))).Text.Trim();

                //แสดงผลการทดสอบ
                Assert.True(originalPO != newPO, "❌ Test Failed: Can't copy document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully copied document");
            }
        }
        [Fact]
        public void PurchaseOrderApprovedReceived()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503065']")));
                poListButton.Click();

                //กดปุ่ม Received
                var receivedButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Received']]")));
                receivedButton.Click();
                var acceptButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Accept']]")));
                acceptButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Create Item Received
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Create Item Received')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully access item received menu.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Can't access item received menu.");
                    //ระบุว่า Test Failed
                    Assert.Fail("❌ Test Failed: Can't access item received menu.");
                }
            }
        }
        [Fact]
        public void PurchaseOrderApprovedCancelApproval()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503066']")));
                poListButton.Click();

                //กดปุ่ม Cancel Approval
                var cancelapprovalButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Cancel Approval']]")));
                cancelapprovalButton.Click();

                //รอจนกว่าสถานะ "Approved" จะเปลี่ยนเป็น "Draft"
                bool isApproved = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Draft", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Test Failed: Can't cancel approval.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully cancelled approval.");
            }
        }
        [Fact]
        public void PurchaseOrderApprovedSent()
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

                //กด Dropdown ของเมนูหลัก Purchases
                var purchasesDropdown = wait.Until(d => d.FindElement(By.XPath("//span[text()='Purchases']")));
                purchasesDropdown.Click();

                //กดเข้าไปที่หน้า Purchase Order
                var purchaseOrderButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/purchaseOrders/list']")));
                purchaseOrderButton.Click();

                //เลือกมาหนึ่งรายการ
                var poListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='PO2503070']")));
                poListButton.Click();

                //กดปุ่ม More Action ดูคำสั่งอื่น ๆ
                var moreActionsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='More Actions']]")));
                moreActionsButton.Click();

                //กดปุ่ม Mark as Sent
                var sentButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Mark as Sent']")));
                sentButton.Click();

                //รอจนกว่าสถานะ "Approved" จะเปลี่ยนเป็น "Sent"
                bool isSent = wait.Until(d =>
                {
                    try
                    {
                        var statusLabel = d.FindElement(By.XPath("//span[contains(@class, 'p-tag-label')]"));
                        return statusLabel.Text.Trim().Equals("Sent", StringComparison.OrdinalIgnoreCase);
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });

                //แจ้งผลการทดสอบ
                Assert.True(isSent, "❌ Test Failed: Can't mark the document as sent.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully marked document as sent.");
            }
        }
        [Fact]
        public void PurchaseOrderExport()
        {
            //ตั้งค่า Folder สำหรับเก็บไฟล์ที่ดาวน์โหลด
            string downloadPath = @"C:\Users\Admin\Downloads\ChromeDownloads\BizKit_Files";

            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            using (IWebDriver driver = new ChromeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
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

                //ตรวจสอบชื่อไฟล์ใน Folder ก่อนจะกด Export
                string[] filesBefore = Directory.GetFiles(downloadPath, "purchase_orders*.csv");

                //กดปุ่ม Export
                var exportButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Export']")));
                exportButton.Click();

                //ตั้งค่าตัวแปรว่าเป็น false เพราะยังไม่ได้ดาวน์โหลดไฟล์
                bool fileDownloaded = false;
                //รอดาวน์โหลดไฟล์สูงสุด 10 วินาที
                DateTime startTime = DateTime.Now;

                while ((DateTime.Now - startTime).TotalSeconds < 10)
                {
                    string[] filesAfter = Directory.GetFiles(downloadPath, "purchase_orders*.csv");
                    if (filesAfter.Length > filesBefore.Length) //ตรวจสอบว่ามีไฟล์ใหม่ใน Folder ที่ชื่อว่า "purchase_orders(ไฟล์ซ้ำ).csv" หรือไม่
                    {
                        fileDownloaded = true;
                        break; //จะหยุดการตรวจสอบหากพบไฟล์ใหม่
                    }
                    Thread.Sleep(1000); //รอ 1 วินาทีก่อนตรวจสอบไฟล์อีกครั้ง
                }

                //ไม่ผ่านการทดสอบ เพราะว่าผ่านไปแล้ว 10 วินาทียังไม่มีไฟล์ใหม่เพิ่มเข้ามา ตัวแปร fileDownloaded จึงยังเป็น false
                Assert.True(fileDownloaded, "❌ Test Failed: 'purchase_orders.csv' file was not downloaded.");
                //ผ่านการทดสอบ
                _testOutputHelper.WriteLine("✔ Test Passed: 'purchase_orders.csv' file was successfully downloaded.");
            }
        }
        [Fact]
        public void PurchaseOrderCheckboxApprove()
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

                //กดติ๊กเลือกใบสั่งซื้อ
                var poCheckbox = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//input[@type='checkbox']")));
                poCheckbox.Click();

                //กด Approve
                var checkboxapproveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Approve' and span[text()='Approve']]")));
                checkboxapproveButton.Click();

                bool isApproved = wait.Until(d =>
                {
                    var tag = d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//span[@class='p-tag-label']"));
                    return tag.Text == "Approved";
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Can't approve the document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully approved document.");
            }
        }
        [Fact]
        public void PurchaseOrderCheckboxReceived()
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

                //กดติ๊กเลือกใบสั่งซื้อ
                var poCheckbox = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//input[@type='checkbox']")));
                poCheckbox.Click();

                //กด Received
                var checkboxreceivedButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Received' and span[text()='Received']]")));
                checkboxreceivedButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Create Item Received
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Create Item Received')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully access item received menu.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Can't access item received menu.");
                    Assert.Fail("❌ Test Failed: Can't access item received menu.");
                }
            }
        }
        [Fact]
        public void PurchaseOrderGearEdit()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d =>d.FindElement(By.XPath("//tr[.//span[text()='PO2503067']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Edit
                var editButton = wait.Until(d =>d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Edit']")));
                editButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Create Purchase Order
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Create Purchase Order')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully access edit menu.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Can't access edit menu.");
                    Assert.Fail("❌ Test Failed: Can't access edit menu.");
                }
            }
        }
        [Fact]
        public void PurchaseOrderGearPrint()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503067']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Print
                var printButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Print']")));
                printButton.Click();

                var originalTab = driver.CurrentWindowHandle;

                //หลักจากกดปุ่ม Print รอแท็บใหม่ขึ้นมา
                wait.Until(d => d.WindowHandles.Count > 1);

                //สลับไปที่หน้าแท็บใหม่
                var newTab = driver.WindowHandles.Last();
                driver.SwitchTo().Window(newTab);

                // Wait for the blob URL to load (up to 10 seconds)
                bool pdfLoaded = false;
                var tabWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                try
                {
                    pdfLoaded = tabWait.Until(d => d.Url.StartsWith("blob:"));
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Timed out waiting for blob URL. Current URL: " + driver.Url);
                }

                //แจ้งผลการทดสอบ
                Assert.True(pdfLoaded, "❌ Test Failed: Failed to print the documents.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully printed documents.");
            }
        }
        [Fact]
        public void PurchaseOrderGearApprove()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Approve
                var approveButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Approve']")));
                approveButton.Click();

                bool isApproved = wait.Until(d =>
                {
                    var tag = d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//span[@class='p-tag-label']"));
                    return tag.Text == "Approved";
                });

                //แจ้งผลการทดสอบ
                Assert.True(isApproved, "❌ Can't approve the document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully approved document.");
            }
        }
        [Fact]
        public void PurchaseOrderGearCancel()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503072']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Cancel
                var cancelButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Cancel']")));
                cancelButton.Click();

                bool isCancelled = wait.Until(d =>
                {
                    var tag = d.FindElement(By.XPath("//tr[.//span[text()='PO2503072']]//span[@class='p-tag-label']"));
                    return tag.Text == "Cancelled";
                });

                //แจ้งผลการทดสอบ
                Assert.True(isCancelled, "❌ Can't cancel the document.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully cancelled document.");
            }
        }
        [Fact]
        public void PurchaseOrderGearReceived()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Received
                var receivedButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Received']")));
                receivedButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Create Item Received
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Create Item Received')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully access item received menu.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Can't access item received menu.");
                    Assert.Fail("❌ Test Failed: Can't access item received menu.");
                }
            }
        }
        [Fact]
        public void PurchaseOrderGearMarkAsSent()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503066']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Mark as Sent
                var markassentButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Mark as Sent']")));
                markassentButton.Click();

                bool isSent = wait.Until(d =>
                {
                    var tag = d.FindElement(By.XPath("//tr[.//span[text()='PO2503066']]//span[@class='p-tag-label']"));
                    return tag.Text == "Sent";
                });
                
                //แจ้งผลการทดสอบ
                Assert.True(isSent, "❌ Can't mark the document as sent.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully marked document as sent.");
            }
        }
        [Fact]
        public void PurchaseOrderGearCancelApproval()
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

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Cancel Approval
                var cancelapprovalButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Cancel Approval']")));
                cancelapprovalButton.Click();

                bool isDraft = wait.Until(d =>
                {
                    var tag = d.FindElement(By.XPath("//tr[.//span[text()='PO2503071']]//span[@class='p-tag-label']"));
                    return tag.Text == "Draft";
                });

                //แจ้งผลการทดสอบ
                Assert.True(isDraft, "❌ Can't cancel approval.");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully cancelled approval.");
            }
        }
    }
}