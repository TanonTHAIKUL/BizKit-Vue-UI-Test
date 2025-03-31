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
    public class PurchaseRequest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public PurchaseRequest(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void PurchaseRequestAccess()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(5000);
            }
        }
        [Fact]
        public void PurchaseRequestCheckbox()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
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
        public void PurchaseRequestFilter()
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
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseOrderButton.Click();

                //กดปุ่ม Filter ใบสั่งซื้อ
                var filterButton_No = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Purchase Request No.')]")));
                filterButton_No.Click();
                Thread.Sleep(3000);

                var filterButton_Date = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Purchase Request Date')]")));
                filterButton_Date.Click();
                Thread.Sleep(3000);

                var filterButton_RD = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Required Date')]")));
                filterButton_RD.Click();
                Thread.Sleep(3000);

                var filterButton_Vendor = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Vendor')]")));
                filterButton_Vendor.Click();
                Thread.Sleep(3000);

                var filterButton_Project = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Project')]")));
                filterButton_Project.Click();
                Thread.Sleep(3000);

                var filterButton_Department = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Department')]")));
                filterButton_Department.Click();
                Thread.Sleep(3000);

                var filterButton_Status = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Status')]")));
                filterButton_Status.Click();
                Thread.Sleep(3000);

                var filterButton_Amount = wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Amount')]")));
                filterButton_Amount.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void PurchaseRequestList()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501002']")));
                poList.Click();
                Thread.Sleep(5000);
            }
        }

        [Fact]
        public void PurchaseRequestSearch()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดช่องใส่ข้อมูลที่ต้องการค้นหา
                var searchInput = wait.Until(d => d.FindElement(By.ClassName("p-inputtext")));
                //ใส่ข้อมูล
                searchInput.SendKeys("PR2501002");
                //กดปุ่มค้นหา
                var searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Search')]/parent::button"))); // Adjust the selector as needed
                searchButton.Click();
                
                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("Test");
                searchButton.Click();               
                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("Sep 21,2024");
                searchButton.Click();
                Thread.Sleep(2000);

                searchInput.Clear();
                searchInput.SendKeys("ร่าง");
                searchButton.Click();
                Thread.Sleep(5000);

                searchInput.Clear();
                searchInput.SendKeys("แผนกการตลาด");
                searchButton.Click();
                Thread.Sleep(5000);

                searchInput.Clear();
                searchInput.SendKeys("Unersea Co., Ltd.");
                searchButton.Click();
                Thread.Sleep(5000);


            }
        }

        [Fact]
        public void PurchaseRequestSearchDetail()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                
                //กดปุ่มค้นหาละเอียด
                var SearchDetailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-button-icon-only')]")));
                SearchDetailButton.Click();

                //ใส่ข้อมูล
                var textBoxPrno = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("prNO"))); 
                textBoxPrno.SendKeys("PR2501001");
                textBoxPrno.SendKeys(Keys.Enter);
                

                //var searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Search' and contains(@class, 'p-button')]")));
                //wait.Until(ExpectedConditions.ElementToBeClickable(searchButton)).Click();
           
                Thread.Sleep(2000);
                textBoxPrno.Clear();
                var textBoxRefno = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("refNo")));
                textBoxRefno.SendKeys("JIT001");
                textBoxRefno.SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                textBoxRefno.Clear();
                var textBoxRemark = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remark")));
                textBoxRemark.SendKeys("remark added");
                textBoxRemark.SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                textBoxRemark.Clear();
                var textBoxItemcode = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("itemCode")));
                textBoxItemcode.SendKeys("WRT-001");
                textBoxItemcode.SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                textBoxItemcode.Clear();
                var textBoxItemname = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("itemName")));
                textBoxItemname.SendKeys("TI-0204");
                textBoxItemname.SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                textBoxItemname.Clear();
                var textBoxAmountFrom = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter Amount from']")));
                textBoxAmountFrom.SendKeys("100");
                var textBoxAmountTo = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter Amount to']")));
                textBoxAmountTo.SendKeys("300");
                textBoxAmountTo.SendKeys(Keys.Enter);
                Thread.Sleep(2000);

                textBoxAmountFrom.Clear();
                textBoxAmountTo.Clear();
                var dropdown = wait.Until(d => d.FindElement(By.Id("purchaseRequestDate"))); 
                dropdown.Click(); 

                var dropdownOptions = wait.Until(d => d.FindElements(By.XPath("//div[@class='p-select-dropdown']//span"))); 

                foreach (var option in dropdownOptions)
                {
                    if (option.Text.Equals("Last Year")) 
                    {
                        option.Click();
                        break; 
                    }
                }
                var searchButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Search']"))); 
                searchButton.Click();
                Thread.Sleep(2000);


            }
        }

        [Fact]
        public void PurchaseRequestSearchDetailClear()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดปุ่มค้นหาละเอียด
                var SearchDetailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class, 'p-button-icon-only')]")));
                SearchDetailButton.Click();

                //ใส่ข้อมูล
                var textBoxPrno = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("prNO")));
                textBoxPrno.SendKeys("PR2501001");
               
                var textBoxRefno = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("refNo")));
                textBoxRefno.SendKeys("JIT001");

                var textBoxRemark = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remark")));
                textBoxRemark.SendKeys("remark added");
               
                var textBoxItemcode = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("itemCode")));
                textBoxItemcode.SendKeys("WRT-001");    

                var textBoxItemname = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("itemName")));
                textBoxItemname.SendKeys("TI-0204");
                
                var textBoxAmountFrom = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter Amount from']")));
                textBoxAmountFrom.SendKeys("100");
                var textBoxAmountTo = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Enter Amount to']")));
                textBoxAmountTo.SendKeys("300");

                var clearButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Clear']"))); // Locate the Clear button by aria-label
                clearButton.Click();

                Thread.Sleep(2000);


            }
        }


        [Fact]
        public void PurchaseRequestNew()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดปุ่มสร้างรายการใหม่
                var NewPRbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'New Purchase Request')]")));
                NewPRbutton.Click();

                //กด Vendor
                var Vendor = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a vendor']")));
                Vendor.Click();

                var VendorSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='PONGPAN']")));
                VendorSelect.Click();

                //กด PersonInCharge
                var PersonInCharge = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select sales person']")));
                PersonInCharge.Click();

                var PersonInChargeSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Tanon ']")));
                PersonInChargeSelect.Click();

                //กด Project
                var Project = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a project']")));
                Project.Click();

                var ProjectSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='P001']")));
                ProjectSelect.Click();

                //กด ReferenceNo
                var referenceNoField = wait.Until(d => d.FindElement(By.Name("referenceNo"))); 
                referenceNoField.SendKeys("TA1");

                //กด Department
                var Department = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a department']")));
                Department.Click();

                var DepartmentSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Test update department']")));
                DepartmentSelect.Click();

                //กด Shipping address
                var Shippingaddresst = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a shipping address']")));
                Shippingaddresst.Click();

                //var ShippingaddressSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Test update department']")));
                //ProjectSelect.Click();

                //กด Item
                var Item = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Selectn item']")));
                Item.Click();

                var ItemSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='TEST-004']")));
                ItemSelect.Click();


                var textArea = wait.Until(d => d.FindElement(By.ClassName("p-textarea"))); 
                textArea.SendKeys("TA11");


                var UOM = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a UOM']")));
                UOM.Click();


                var Vat = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a Vat']")));
                Vat.Click();

                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Save']")));
                saveButton.Click();

                var saveSaveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='p-button p-component p-confirmdialog-accept-button' and @aria-label='Save']")));
                saveSaveButton.Click();

                Thread.Sleep(2000);


            }
        }

        [Fact]
        public void PurchaseRequestNewApprove()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดปุ่มสร้างรายการใหม่
                var NewPRbutton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'New Purchase Request')]")));
                NewPRbutton.Click();

                //กด Vendor
                var Vendor = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a vendor']")));
                Vendor.Click();

                var VendorSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='PONGPAN']")));
                VendorSelect.Click();

                //กด PersonInCharge
                var PersonInCharge = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select sales person']")));
                PersonInCharge.Click();

                var PersonInChargeSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Tanon ']")));
                PersonInChargeSelect.Click();

                //กด Project
                var Project = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a project']")));
                Project.Click();

                var ProjectSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='P001']")));
                ProjectSelect.Click();

                //กด ReferenceNo
                var referenceNoField = wait.Until(d => d.FindElement(By.Name("referenceNo")));
                referenceNoField.SendKeys("TA1");

                //กด Department
                var Department = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a department']")));
                Department.Click();

                var DepartmentSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Test update department']")));
                DepartmentSelect.Click();

                //กด Shipping address
                var Shippingaddresst = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a shipping address']")));
                Shippingaddresst.Click();

                //var ShippingaddressSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Test update department']")));
                //ProjectSelect.Click();

                //กด Item
                var Item = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Selectn item']")));
                Item.Click();

                var ItemSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='TEST-004']")));
                ItemSelect.Click();


                var textArea = wait.Until(d => d.FindElement(By.ClassName("p-textarea")));
                textArea.SendKeys("TA11");


                var UOM = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a UOM']")));
                UOM.Click();


                var Vat = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select a Vat']")));
                Vat.Click();

                var saveApproveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Save and Approve']")));
                saveApproveButton.Click();

                var saveSaveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='p-button p-component p-confirmdialog-accept-button' and @aria-label='Save']")));
                saveSaveButton.Click();

                Thread.Sleep(2000);


            }
        }
        [Fact]
        public void PurchaseRequestDetailConvertedToPO()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501002']")));
                poList.Click();

                //กด Print
                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                Thread.Sleep(2000);

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();

                Thread.Sleep(5000);
            }
        }

        [Fact]
        public void PurchaseRequestDetailDraft()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501004']")));
                poList.Click();

                //กด Edit
                var EditButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Edit']")));
                EditButton.Click();
                Thread.Sleep(2000);

                driver.Navigate().Back();

                //กด Approve
                var ApproveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Approve']")));
                ApproveButton.Click();

                Thread.Sleep(2000);

                var CancelApproveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Approve']")));
                CancelApproveButton.Click();

                //กด Print
                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                Thread.Sleep(2000);

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();

                var Reject = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Reject']")));
                Reject.Click();

                var CancelApproveMAButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Approve']")));
                CancelApproveMAButton.Click();

                var Cancel = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Cancel']")));
                Cancel.Click();

                Thread.Sleep(5000);
            }
        }

        [Fact]
        public void PurchaseRequestDetailApprove()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2409045']"))); //เลือกรหัส PR ที่ต้องการ
                poList.Click();

                //กด Cancel Approval
                var CancelApprovalButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Approval']")));
                CancelApprovalButton.Click();
                Thread.Sleep(2000);

                var ApproveButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Approve']")));
                ApproveButton.Click();

                Thread.Sleep(2000);

                

                //กด Print
                var PrintButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Print']")));
                PrintButton.Click();

                Thread.Sleep(2000);

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();

                //กด Convert to Purchase Order
                var ConverttoPurchaseOrderButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='Cancel Approve']")));
                ConverttoPurchaseOrderButton.Click();

                Thread.Sleep(5000);
            }
        }

        [Fact]
        public void PurchaseRequestDetailCancelled()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501002']"))); //เลือกรหัส PR ที่ต้องการ
                poList.Click();

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();

                Thread.Sleep(5000);

                driver.Navigate().Back();
            }
        }

        [Fact]
        public void PurchaseRequestDetailCopy()
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

                //กดเข้าไปที่หน้า Purchase Request
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501002']"))); //เลือกรหัส PR ที่ต้องการ
                poList.Click();

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();
                Thread.Sleep(5000);



                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Save']")));
                saveButton.Click();

                var saveSaveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='p-button p-component p-confirmdialog-accept-button' and @aria-label='Save']")));
                saveSaveButton.Click();

                Thread.Sleep(5000);

               
            }
        }

        [Fact]
        public void PurchaseRequestDetailCopyAndEdit()
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

                //กดเข้าไปที่หน้า Purchase Request
                var PurchasesButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-panelmenu-header' and @aria-label='Purchases']")));
                PurchasesButton.Click();
                Thread.Sleep(2000);
                var purchaseRequestButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/purchaseRequests/list']")));
                purchaseRequestButton.Click();
                Thread.Sleep(2000);

                //กดเลือกดูรายการ Purchase Request หนึ่งรายการ 
                var poList = wait.Until(d => d.FindElement(By.XPath("//span[text()='PR2501002']"))); //เลือกรหัส PR ที่ต้องการ
                poList.Click();

                //กด More Action
                var moreActionsButton = wait.Until(d => d.FindElement(By.XPath("//button[@aria-label='More Actions']")));
                moreActionsButton.Click();

                var copy = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Copy']")));
                copy.Click();
                Thread.Sleep(5000);

                //เพิ่มข้อมูล
                var PersonInCharge = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class='p-select-label p-placeholder' and @aria-label='Select sales person']")));
                PersonInCharge.Click();

                var PersonInChargeSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[@aria-label='Tanon ']")));
                PersonInChargeSelect.Click();

                //แก้ไขข้อมูล
                var referenceNoField = wait.Until(d => d.FindElement(By.Name("referenceNo")));
                referenceNoField.SendKeys("TA1");


                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Save']")));
                saveButton.Click();

                var saveSaveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@class='p-button p-component p-confirmdialog-accept-button' and @aria-label='Save']")));
                saveSaveButton.Click();

                Thread.Sleep(5000);
            }
        }
            }
        }