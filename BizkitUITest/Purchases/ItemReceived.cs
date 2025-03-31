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
    public class ItemReceived
    {
        private readonly ITestOutputHelper testOutputHelper;

        public ItemReceived(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void ItemReceivedAccess()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedList()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Item Received หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='IRC2503001']")));
                poList.Click();
                Thread.Sleep(5000);
            }
        }

        [Fact]
        public void ItemReceivedFilter()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000); ;

                //กดปุ่ม Filter รายการรับสินค้า
                var filterButton_IRNo = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Item Receive No.')]")));
                filterButton_IRNo.Click();
                Thread.Sleep(3000);

                var filterButton_RDate = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Received Date')]")));
                filterButton_RDate.Click();
                Thread.Sleep(3000);

                var filterButton_Vendor = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Vendor')]")));
                filterButton_Vendor.Click();
                Thread.Sleep(3000);

                var filterButton_Status = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Status')]")));
                filterButton_Status.Click();
                Thread.Sleep(3000);

                var filterButton_PINo = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Purchase Invoice No.')]")));
                filterButton_PINo.Click();
                Thread.Sleep(3000);

                var filterButton_RQ = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Received Quantity')]")));
                filterButton_RQ.Click();
                Thread.Sleep(3000);

                var filterButton_TRA = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Total Received Amount')]")));
                filterButton_TRA.Click();
                Thread.Sleep(3000);

                var filterButton_Currency = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Currency')]")));
                filterButton_Currency.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void ItemReceivedCheckbox()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
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
        public void ItemReceivedSearch()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                //กดช่องใส่ข้อมูลที่ต้องการค้นหา
                var searchInput = wait.Until(d => d.FindElement(By.ClassName("p-inputtext")));
                //ใส่ข้อมูล
                searchInput.SendKeys("IRC2503001");
                //กดปุ่มค้นหา
                var searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Search')]/parent::button"))); 
                searchButton.Click();

                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("10/Mar/2025");
                searchButton.Click();
                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("Vendor 1");
                searchButton.Click();
                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("ร่าง");
                searchButton.Click();
                Thread.Sleep(5000);

            }
        }
        [Fact]
        public void ItemReceivedSearchDetail()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(1000);
                var purchaseItemReceived = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                purchaseItemReceived.Click();
                Thread.Sleep(1000);

                //กดปุ่มค้นหาละเอียด
                var SearchDetailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-button') and contains(@class, 'p-component') and contains(@class, 'p-button-icon-only') and contains(@class, 'p-button-info') and contains(@class, 'p-button-text')]")));
                SearchDetailButton.Click();
                
                //ใส่ข้อมูล
                var textBoxReceiedNo = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("itemCode")));
                textBoxReceiedNo.SendKeys("IRC2503001");
                var searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-button') and contains(@class, 'p-component') and contains(@class, 'col-span-1')]")));
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                var clearButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Clear']")));
                clearButton.Click();
                textBoxReceiedNo.Clear();
                var VendorDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select vendor']")));
                VendorDropdown.Click();
                var VendorOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='string testtest']")));
                VendorOption.Click();
                searchButton.Click();
                Thread.Sleep(2000);
            
                SearchDetailButton.Click();
                clearButton.Click();
                var StatusDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select Status']")));
                StatusDropdown.Click();
                var StatusOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Draft']")));
                StatusOption.Click();
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                clearButton.Click();
                var textBoxPONo = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter PO no']")));
                textBoxPONo.SendKeys("PO2503028");
                searchButton.Click();
                Thread.Sleep(2000);


                SearchDetailButton.Click();
                textBoxPONo.Clear();
                var textBoxNote = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter note']")));
                textBoxNote.SendKeys("Test");
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                textBoxNote.Clear();
                var textBoxItemCode = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter item code']")));
                textBoxItemCode.SendKeys("WRT-001");
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                textBoxItemCode.Clear();
                var textBoxAmountFrom = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter amount from']")));
                textBoxAmountFrom.SendKeys("100");
                var textBoxAmountTo = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter a" +
                    "mount to']")));
                textBoxAmountTo.SendKeys("300");
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                clearButton.Click();
                var ReceivedDateDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='purchaseOrderDate']")));
                ReceivedDateDropdown.Click();
                var ReceivedDateOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Last Year']")));
                ReceivedDateOption.Click();
                searchButton.Click();
                Thread.Sleep(2000);

                //เลือกวันที่
                SearchDetailButton.Click();
                var DateRangePicker = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("dateRange")));
                DateRangePicker.Click();
                Thread.Sleep(2000);
                //ใส่วันที่
                var dateToSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td[@aria-label='19']//span[text()='19']")));
                dateToSelect.Click();
                searchButton.Click();
                Thread.Sleep(2000);

                SearchDetailButton.Click();
                clearButton.Click();
                var textBoxLotSerial = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter lot/serial no']")));
                textBoxLotSerial.SendKeys("IRC2503001");
                searchButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedExport()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var ExportButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Export']")));
                ExportButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedImport()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var ImportButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Import']")));
                ImportButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedSaveWithPO()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var NewIRPOButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='New Item Received With PO']")));
                NewIRPOButton.Click();
                Thread.Sleep(2000);

                var VendorDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select vendor']")));
                VendorDropdown.Click();
                var VendorOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='string testtest']")));
                VendorOption.Click();

                var PONo = wait.Until(d => d.FindElement(By.ClassName("p-inputtext")));
                PONo.SendKeys("PO2503071");

                var PODateDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='-- As specified --']")));
                PODateDropdown.Click();
                var PODateOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='This Month']")));
                PODateOption.Click();

                /*
                var datePickerSpan = wait.Until(d => d.FindElement(By.XPath("//span[contains(@class, 'p-datepicker') and @data-pc-name='datepicker']")));
                datePickerSpan.Click();
                var dateToSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td[@aria-label='19']//span[text()='19']")));
                dateToSelect.Click();
                */

                var SearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Search']")));
                SearchButton.Click();

                var ReceivedBy = wait.Until(d => d.FindElement(By.XPath("//div[@id='pic']")));
                ReceivedBy.Click();
                var ReceivedByList = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Tanon ']")));
                ReceivedByList.Click();

                var referenceNO = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Enter Reference No.']")));
                referenceNO.SendKeys("TIR1");

                var SaveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Save']")));
                SaveButton.Click();
                Thread.Sleep(2000);
            }
        }

        [Fact]
        public void ItemReceivedSaveWithPOApprove()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var NewIRPOButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='New Item Received With PO']")));
                NewIRPOButton.Click();
                Thread.Sleep(2000);

                var VendorDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select vendor']")));
                VendorDropdown.Click();
                var VendorOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='string testtest']")));
                VendorOption.Click();

                var PONo = wait.Until(d => d.FindElement(By.ClassName("p-inputtext")));
                PONo.SendKeys("PO2503071");

                var PODateDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='-- As specified --']")));
                PODateDropdown.Click();
                var PODateOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='This Month']")));
                PODateOption.Click();     

                var SearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Search']")));
                SearchButton.Click();

                var ReceivedBy = wait.Until(d => d.FindElement(By.XPath("//div[@id='pic']")));
                ReceivedBy.Click();
                var ReceivedByList = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Tanon ']")));
                ReceivedByList.Click();

                var referenceNO = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Enter Reference No.']")));
                referenceNO.SendKeys("TIR1");

                var SaveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Save and Approve']")));
                SaveButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedSaveWithOutPO()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var NewIRWithOutPOButton = wait.Until(d => d.FindElement(By.XPath("//button[@class='p-button p-component p-button-icon-only p-splitbutton-dropdown']")));
                NewIRWithOutPOButton.Click();
                var NewIRNOPOButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='New Item Received With Out PO']")));
                NewIRNOPOButton.Click();
             
                Thread.Sleep(2000);

                var VendorDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select vendor']")));
                VendorDropdown.Click();
                var VendorOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='string testtest']")));
                VendorOption.Click();

                var referenceNO = wait.Until(d => d.FindElement(By.XPath("//input[@name='referenceNo']")));
                referenceNO.SendKeys("TIRNOPO1");

                var ReceivedBYDropdown = wait.Until(d => d.FindElement(By.XPath("//div[@id='pic']")));
                ReceivedBYDropdown.Click();
                var ReceivedBYOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='Tanon ']")));
                ReceivedBYOption.Click();

                //บางครั้งจะออกจากหน้า
                var LocationDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select Warehouse']")));
                LocationDropdown.Click();
                Thread.Sleep(2000);
                var LocationOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-posinset='4' and contains(@class, 'p-select-option')]")));
                LocationOption.Click();
                Thread.Sleep(2000);
                
                var ItemCodeDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select item']")));
                ItemCodeDropdown.Click();
                var ItemCodeOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='WRT-001']")));
                ItemCodeOption.Click();

                var UnitDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select unit']")));
                UnitDropdown.Click();

                var UnitCost = wait.Until(d => d.FindElement(By.XPath("//input[@class='p-inputtext p-component p-inputtext-fluid p-inputnumber-input text-right']")));
                UnitCost.SendKeys("100");

                var SaveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Save']")));
                SaveButton.Click();

            }
        }
        [Fact]
        public void ItemReceivedSaveWithOutPOApprove()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                var NewIRWithOutPOButton = wait.Until(d => d.FindElement(By.XPath("//button[@class='p-button p-component p-button-icon-only p-splitbutton-dropdown']")));
                NewIRWithOutPOButton.Click();
                var NewIRNOPOButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='New Item Received With Out PO']")));
                NewIRNOPOButton.Click();

                Thread.Sleep(2000);

                var VendorDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@aria-label='Select vendor']")));
                VendorDropdown.Click();
                var VendorOption = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='string testtest']")));
                VendorOption.Click();

                var referenceNO = wait.Until(d => d.FindElement(By.XPath("//input[@name='referenceNo']")));
                referenceNO.SendKeys("TIRNOPO1");

                var ReceivedBYDropdown = wait.Until(d => d.FindElement(By.XPath("//div[@id='pic']")));
                ReceivedBYDropdown.Click();
                var ReceivedBYOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='Tanon ']")));
                ReceivedBYOption.Click();

                //บางครั้งจะออกจากหน้า
                var LocationDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select Warehouse']")));
                LocationDropdown.Click();
                Thread.Sleep(2000);
                var LocationOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-posinset='4' and contains(@class, 'p-select-option')]")));
                LocationOption.Click();
                Thread.Sleep(2000);

                var ItemCodeDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select item']")));
                ItemCodeDropdown.Click();
                var ItemCodeOption = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='WRT-001']")));
                ItemCodeOption.Click();

                var UnitDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@aria-label='Select unit']")));
                UnitDropdown.Click();

                var UnitCost = wait.Until(d => d.FindElement(By.XPath("//input[@class='p-inputtext p-component p-inputtext-fluid p-inputnumber-input text-right']")));
                UnitCost.SendKeys("100");

                var SaveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Save and Approve']")));
                SaveButton.Click();

            }
        }
        [Fact]
        public void ItemReceivedDraft()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Item Received หนึ่งรายการ 
                var IRList = wait.Until(d => d.FindElement(By.XPath("//span[text()='IRC2503005']")));
                IRList.Click();
                Thread.Sleep(2000);

                var EditButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Edit']")));
                EditButton.Click();
                driver.Navigate().Back();
                var AcceptButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Accept']")));
                AcceptButton.Click();

                var ApproveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Approve']")));
                ApproveButton.Click();
                AcceptButton.Click();
                var CancelReceivedButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Received']")));
                CancelReceivedButton.Click();
                AcceptButton.Click();

                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                var MoreActionButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                MoreActionButton.Click();
                var CancelButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='Cancel']")));
                CancelButton.Click();
            }
        }
        [Fact]
        public void ItemReceivedReceived()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Item Received หนึ่งรายการ 
                var IRList = wait.Until(d => d.FindElement(By.XPath("//span[text()='IRC2503003']")));
                IRList.Click();
                Thread.Sleep(2000);

                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                var CancelReceivedButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Received']")));
                CancelReceivedButton.Click();
                Thread.Sleep(2000);
                var AcceptButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Accept']")));
                AcceptButton.Click();
                
                var ApproveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Approve']")));
                ApproveButton.Click();
                Thread.Sleep(2000);

                AcceptButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void ItemReceivedBilled()
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

                //กดเข้าไปที่หน้า Item Received
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var ItemReceivedButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/itemReceived/list']")));
                ItemReceivedButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Item Received หนึ่งรายการ 
                var IRList = wait.Until(d => d.FindElement(By.XPath("//span[text()='IRC2409006']")));
                IRList.Click();
                Thread.Sleep(2000);

                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                Thread.Sleep(2000);

            }
        }
    }
}