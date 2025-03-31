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
    public class Vendor
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Vendor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void VendorAccess()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Vendor
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Vendor')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Vendor page is accessible.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Vendor page is not accessible.");
                    Assert.Fail("❌ Test Failed: Vendor page is not accessible.");
                }
            }
        }
        [Fact]
        public void VendorList()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //เลือกมาหนึ่งรายการ (ในที่นี้จะเลือก VD-003)
                var vendorListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='VD-003']")));
                vendorListButton.Click();

                //ตรวจสอบชื่อบริษัทของลูกค้าว่าตรงกันไหม
                var companyNameElement = wait.Until(d => d.FindElement(By.XPath("//label[text()='Company Name']/following-sibling::span")));
                string companyName = companyNameElement.Text;

                //แสดงผลการทดสอบ
                Assert.True(companyName == "Vendor 1", "❌ Test Failed: Can't view vendor detail");
                _testOutputHelper.WriteLine("✔ Test Passed: Successfully accessed vendor detail");
            }
        }
        [Fact]
        public void VendorFilter()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดปุ่ม Filter ผู้ขาย
                var filterButton = wait.Until(d => d.FindElement(By.XPath("//button[span[contains(@class, 'pi-objects-column')]]")));
                filterButton.Click();
                var vendorFilterButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='Vendor Code']")));
                vendorFilterButton.Click();
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
        public void VendorAscendDescend()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดหัวข้อตารางเพื่อเรียงลำดับรายการ
                var vendorCodeHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Vendor Code')]]")));
                vendorCodeHeaderButton.Click();
                Thread.Sleep(1000);
                vendorCodeHeaderButton.Click();
                Thread.Sleep(1000);
                var vendorNameHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Vendor Name')]]")));
                vendorNameHeaderButton.Click();
                Thread.Sleep(1000);
                vendorNameHeaderButton.Click();
                Thread.Sleep(1000);
                var vendorNameENHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Vendor Name(EN)')]]")));
                vendorNameENHeaderButton.Click();
                Thread.Sleep(1000);
                vendorNameENHeaderButton.Click();
                Thread.Sleep(1000);
                var vendorTeleHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Telephone No.')]]")));
                vendorTeleHeaderButton.Click();
                Thread.Sleep(1000);
                vendorTeleHeaderButton.Click();
                Thread.Sleep(1000);
                var vendorEmailHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Email')]]")));
                vendorEmailHeaderButton.Click();
                Thread.Sleep(1000);
                vendorEmailHeaderButton.Click();
                Thread.Sleep(1000);
                var vendorBalanceHeaderButton = wait.Until(d => d.FindElement(By.XPath("//div[@class='p-datatable-column-header-content'][div[contains(text(), 'Balance')]]")));
                vendorBalanceHeaderButton.Click();
                Thread.Sleep(1000);
                vendorBalanceHeaderButton.Click();
                Thread.Sleep(1000);
            }
        }
        [Fact]
        public void VendorSearch()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //ใส่ข้อมูลและกดค้นหา
                string targetVendor = "VD-003";

                var searchInput = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search vendor']")));
                searchInput.Clear();
                searchInput.SendKeys(targetVendor);

                var searchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Search']")));
                searchButton.Click();
                Thread.Sleep(3000);

                var resultVendor = wait.Until(d => d.FindElement(By.XPath($"//span[contains(@class, 'p-button-label') and text()='{targetVendor}']")));

                //แสดงผลการทดสอบ
                Assert.True(resultVendor.Displayed, $"❌ Test Failed: {targetVendor} was not displayed in the search results.");
                _testOutputHelper.WriteLine($"✔ Test Passed: {targetVendor} was successfully found in the search results.");
            }
        }
        [Fact]
        public void VendorSave()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดปุ่ม New Vendor
                var newVendorButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Vendor')]")));
                newVendorButton.Click();
                Thread.Sleep(1000);

                //กรอกข้อมูลและ Save
                var vendorCode = wait.Until(d => d.FindElement(By.Id("vendorCode")));
                vendorCode.Clear();
                vendorCode.SendKeys("SKYNET");
                Thread.Sleep(1000);
                var companyName = wait.Until(d => d.FindElement(By.Id("vendorName")));
                companyName.Clear();
                companyName.SendKeys("Thailandese");
                Thread.Sleep(1000);
                var taxID = wait.Until(d => d.FindElement(By.Id("taxId")));
                taxID.Clear();
                taxID.SendKeys("6421106421103");
                Thread.Sleep(1000);
                var Address = wait.Until(d => d.FindElement(By.Id("address1")));
                Address.Clear();
                Address.SendKeys("90 Ratchadaphisek Rd, Khwaeng Huai Khwang, Khet Huai Khwang, Bangkok");
                Thread.Sleep(1000);
                var Amphur = wait.Until(d => d.FindElement(By.Id("city")));
                Amphur.Clear();
                Amphur.SendKeys("Huai Khwang");
                Thread.Sleep(1000);
                var zipCode = wait.Until(d => d.FindElement(By.Id("zipCode")));
                zipCode.Clear();
                zipCode.SendKeys("10310");
                Thread.Sleep(1000);
                var Province = wait.Until(d => d.FindElement(By.Id("state")));
                Province.Clear();
                Province.SendKeys("Bangkok");
                Thread.Sleep(1000);

                var saveButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'Save')]")));
                saveButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void VendorGearEdit()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='VD-123']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Edit
                var editButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Edit']")));
                editButton.Click();

                //รอหน้าเว็บโหลดและตรวจสอบว่าเป็นหน้าที่ถูกหรือไม่จากการตรวจสอบหัวข้อใหญ่ Add/Edit Vendor
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//h1[contains(text(), 'Add/Edit Vendor')]")));
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
        public void VendorGearDelete()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดปุ่มฟันเฟือง
                var gearButton = wait.Until(d => d.FindElement(By.XPath("//tr[.//span[text()='VD-123']]//button[.//span[contains(@class, 'pi-cog')]]")));
                gearButton.Click();

                //กดปุ่ม Delete
                var editButton = wait.Until(d => d.FindElement(By.XPath("//li[@role='menuitem' and @aria-label='Delete']")));
                editButton.Click();
            }
        }
        [Fact]
        public void VendorExport()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //ตรวจสอบชื่อไฟล์ใน Folder ก่อนจะกด Export
                string[] filesBefore = Directory.GetFiles(downloadPath, "Vendors*.csv");

                //กดปุ่ม Export
                var exportButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Export']")));
                exportButton.Click();

                //ตั้งค่าตัวแปรว่าเป็น false เพราะยังไม่ได้ดาวน์โหลดไฟล์
                bool fileDownloaded = false;
                //รอดาวน์โหลดไฟล์สูงสุด 10 วินาที
                DateTime startTime = DateTime.Now;

                while ((DateTime.Now - startTime).TotalSeconds < 10)
                {
                    string[] filesAfter = Directory.GetFiles(downloadPath, "Vendors*.csv");
                    if (filesAfter.Length > filesBefore.Length) //ตรวจสอบว่ามีไฟล์ใหม่ใน Folder ที่ชื่อว่า "Vendors(ไฟล์ซ้ำ).csv" หรือไม่
                    {
                        fileDownloaded = true;
                        break; //จะหยุดการตรวจสอบหากพบไฟล์ใหม่
                    }
                    Thread.Sleep(1000); //รอ 1 วินาทีก่อนตรวจสอบไฟล์อีกครั้ง
                }

                //ไม่ผ่านการทดสอบ เพราะว่าผ่านไปแล้ว 10 วินาทียังไม่มีไฟล์ใหม่เพิ่มเข้ามา ตัวแปร fileDownloaded จึงยังเป็น false
                Assert.True(fileDownloaded, "❌ Test Failed: 'Vendors.csv' file was not downloaded.");
                //ผ่านการทดสอบ
                _testOutputHelper.WriteLine("✔ Test Passed: 'Vendors.csv' file was successfully downloaded.");
            }
        }
        [Fact]
        public void VendorImport()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();

                //กดปุ่ม Import
                var importButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Import Vendor']")));
                importButton.Click();

                //ตรวจสอบการเปิดหน้าต่าง Import
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//span[text()='Import Vendor']")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully opened import vendor window.");
                }
                catch (WebDriverTimeoutException)
                {
                    _testOutputHelper.WriteLine("❌ Test Failed: Can't open import vendor window.");
                    Assert.Fail("❌ Test Failed: Can't open import vendor window.");
                }
            }
        }
    }
}