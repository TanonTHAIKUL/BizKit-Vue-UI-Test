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
        private readonly ITestOutputHelper testOutputHelper;

        public Vendor(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();
                Thread.Sleep(2000);
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

                //กดเข้าไปที่หน้า Vendor
                var VendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                VendorButton.Click();
                Thread.Sleep(1000);

                //เลือกมาหนึ่งรายการ (ในที่นี้จะเลือก VD-003)
                var vendorListButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='VD-003']")));
                vendorListButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void VendorCheckbox()
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();
                Thread.Sleep(2000);

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
                Thread.Sleep(3000);
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();
                Thread.Sleep(2000);

                //ใส่ข้อมูลและกดค้นหา
                var vendorSearchButton = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Search vendor']")));
                vendorSearchButton.SendKeys("VD-003");
                Thread.Sleep(1000);
                var defaultSearchButton = wait.Until(d => d.FindElement(By.XPath("//button[@type='button' and @aria-label='Search']")));
                defaultSearchButton.Click();
                Thread.Sleep(3000);
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

                //กดเข้าไปที่หน้า Vendor
                var vendorButton = wait.Until(d => d.FindElement(By.XPath("//a[@href='/vendor/list']")));
                vendorButton.Click();
                Thread.Sleep(1000);

                //กดปุ่ม New Vendor
                var newVendorButton = wait.Until(d => d.FindElement(By.XPath("//button[contains(., 'New Vendor')]")));
                newVendorButton.Click();
                Thread.Sleep(1000);

                //General Information

                var vendorCode = wait.Until(d => d.FindElement(By.Id("vendorCode")));
                vendorCode.SendKeys("VD-TTT");
                Thread.Sleep(1000);
                var vendorCompanyName = wait.Until(d => d.FindElement(By.Id("vendorName")));
                vendorCompanyName.SendKeys("Test Company");
                Thread.Sleep(1000);
                var vendorTaxID = wait.Until(d => d.FindElement(By.Id("taxId")));
                vendorTaxID.SendKeys("01013395");
                Thread.Sleep(1000);
                var vendorTypeDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and @aria-label='Company']")));
                vendorTypeDropdown.Click();
                var vendorTypeOption = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'Company')]")));
                vendorTypeOption.Click();
                Thread.Sleep(1000);
                var vendorCurrencyDropdown = wait.Until(d => d.FindElement(By.XPath("//span[@role='combobox' and @aria-controls='currencyCode_list']")));
                vendorCurrencyDropdown.Click();
                var vendorCurrencyOption = wait.Until(d => d.FindElement(By.Id("currencyCode_7")));
                vendorCurrencyOption.Click();
                Thread.Sleep(1000);
                var vendorTelephone = wait.Until(d => d.FindElement(By.Id("telephone1")));
                vendorTelephone.SendKeys("0949671295");
                Thread.Sleep(1000);
                var vendorFax = wait.Until(d => d.FindElement(By.Id("fax")));
                vendorFax.SendKeys("5912769490");
                Thread.Sleep(1000);
                var vendorLeadTime = wait.Until(d => d.FindElement(By.Id("leadTime")));
                vendorLeadTime.SendKeys("4");
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
    }
}