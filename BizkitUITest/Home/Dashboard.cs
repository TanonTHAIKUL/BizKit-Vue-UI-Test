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
using Xunit.Sdk;

namespace Home
{
    [Collection("Sequential Tests")]
    public class Dashboard
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Dashboard(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void DashboardLogin()
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

                //ตรวจสอบผลการทดสอบด้วยการค้นหาแถบ Breadcrumb ด้านบนที่มีคำว่า "Dashboard" ถ้ามีคือผ่านการทดสอบ
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//span[contains(text(), 'Dashboard')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Dashboard page is accessible.");
                }
                //เว็บไซต์ใช้เวลาโหลดนานเกินไปจึงรอตรวจสอบอีกครั้ง
                catch (WebDriverTimeoutException)
                {
                    //รออีก 5 วินาทีจึงเริ่มตรวจสอบผลการทดสอบอีกครั้ง
                    Thread.Sleep(5000);
                    try
                    {
                        driver.FindElement(By.XPath("//span[contains(text(), 'Dashboard')]"));
                        _testOutputHelper.WriteLine("✔ Test Passed (after delay): Dashboard page is accessible.");
                    }
                    //ถ้าไม่เป็นไปตามเงื่อนไขก็จะไม่ผ่านการทดสอบ (เช่น ไม่พบปุ่มที่ต้องการกด ไม่พบข้อความที่ค้นหา)
                    catch (NoSuchElementException)
                    {
                        Assert.Fail("❌ Test Failed: Dashboard page not accessible.");
                    }
                }
            }
        }
        [Fact]
        public void DashboardLogout()
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

                //Logout
                var profileButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Profile']]")));
                profileButton.Click();
                var logoutButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Logout')]")));
                logoutButton.Click();
                var confirmButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Confirm']]")));
                confirmButton.Click();

                //ตรวจสอบผลการทดสอบด้วยการค้นหาคำว่า "Welcome to BizKit!" ซึ่งจะแสดงในหน้าการเข้าสู่ระบบ ถ้าพบเจอจะผ่านการทดสอบ
                try
                {
                    wait.Until(d => d.FindElement(By.XPath("//div[contains(text(), 'Welcome to BizKit!')]")));
                    _testOutputHelper.WriteLine("✔ Test Passed: Successfully logged out.");
                }
                //เว็บไซต์ใช้เวลาโหลดนานเกินไปจึงรอตรวจสอบอีกครั้ง
                catch (WebDriverTimeoutException)
                {
                    //รออีก 5 วินาทีจึงเริ่มตรวจสอบผลการทดสอบอีกครั้ง
                    Thread.Sleep(5000);
                    try
                    {
                        driver.FindElement(By.XPath("//div[contains(text(), 'Welcome to BizKit!')]"));
                        _testOutputHelper.WriteLine("✔ Test Passed (after delay): Successfully logged out.");
                    }
                    //ถ้าไม่เป็นไปตามเงื่อนไขก็จะไม่ผ่านการทดสอบ (เช่น ไม่พบปุ่มที่ต้องการกด ไม่พบข้อความที่ค้นหา)
                    catch (NoSuchElementException)
                    {
                        Assert.Fail("❌ Test Failed: Logging out failed.");
                    }
                }
            }
        }
        [Fact]
        public void DashboardScreenMode()
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

                //กดปุ่มเปลี่ยนโหมดมืดและสว่าง
                Thread.Sleep(3000);
                var sunButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-sun')]]")));
                sunButton.Click();
                Thread.Sleep(3000);
                var moonButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-moon')]]")));
                moonButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void DashboardPrimaryColor()
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

                //กดปุ่มถาดสี
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();
                Thread.Sleep(2000);

                //กดเลือกทีละสี เริ่มจากสีดำทางซ้าย
                var noirButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='noir']")));
                noirButton.Click();
                Thread.Sleep(2000);
                //สีมรกต
                var emeraldButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='emerald']")));
                emeraldButton.Click();
                Thread.Sleep(2000);
                //สีเขียว
                var greenButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='green']")));
                greenButton.Click();
                Thread.Sleep(2000);
                //สีเขียวมะนาว
                var limeButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='lime']")));
                limeButton.Click();
                Thread.Sleep(2000);
                //สีส้ม
                var orangeButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='orange']")));
                orangeButton.Click();
                Thread.Sleep(2000);
                //สีเหลืองอำพัน
                var amberButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='amber']")));
                amberButton.Click();
                Thread.Sleep(2000);
                //สีเหลือง
                var yellowButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='yellow']")));
                yellowButton.Click();
                Thread.Sleep(2000);
                //สีเขียวทะเล
                var tealButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='teal']")));
                tealButton.Click();
                Thread.Sleep(2000);
                //สีฟ้าอมเขียว
                var cyanButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='cyan']")));
                cyanButton.Click();
                Thread.Sleep(2000);
                //สีฟ้า
                var skyButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='sky']")));
                skyButton.Click();
                Thread.Sleep(2000);
                //สีน้ำเงิน
                var blueButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='blue']")));
                blueButton.Click();
                Thread.Sleep(2000);
                //สีคราม
                var indigoButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='indigo']")));
                indigoButton.Click();
                Thread.Sleep(2000);
                //สีม่วงเม็ดมะปราง
                var violetButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='violet']")));
                violetButton.Click();
                Thread.Sleep(2000);
                //สีม่วง
                var purpleButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='purple']")));
                purpleButton.Click();
                Thread.Sleep(2000);
                //สีบานเย็น
                var fuchsiaButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='fuchsia']")));
                fuchsiaButton.Click();
                Thread.Sleep(2000);
                //สีชมพู
                var pinkButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='pink']")));
                pinkButton.Click();
                Thread.Sleep(2000);
                //สีชมพูกุหลาบ
                var roseButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='rose']")));
                roseButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void DashboardSurfaceColor()
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

                //กดปุ่มถาดสี
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();
                Thread.Sleep(2000);

                //กดเลือกทีละสี เริ่มจากสีเทา
                var grayButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='gray']")));
                grayButton.Click();
                Thread.Sleep(2000);
                //สี zinc
                var zincButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='zinc']")));
                zincButton.Click();
                Thread.Sleep(2000);
                //สี neutral
                var neutralButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='neutral']")));
                neutralButton.Click();
                Thread.Sleep(2000);
                //สี stone
                var stoneButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='stone']")));
                stoneButton.Click();
                Thread.Sleep(2000);
                //สี soho
                var sohoButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='soho']")));
                sohoButton.Click();
                Thread.Sleep(2000);
                //สี viva
                var vivaButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='viva']")));
                vivaButton.Click();
                Thread.Sleep(2000);
                //สี ocean
                var oceanButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='ocean']")));
                oceanButton.Click();
                Thread.Sleep(2000);
            }
        }
        [Fact]
        public void DashboardPresetMenuMode()
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

                //กดปุ่มถาดสี
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();

                //กดปุ่มเปลี่ยน Preset
                Thread.Sleep(3000);
                var laraButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Lara']")));
                laraButton.Click();
                Thread.Sleep(3000);
                var auraButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Aura']")));
                auraButton.Click();
                Thread.Sleep(3000);

                //กดปุ่มเปลี่ยน Menu Mode
                var overlayButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Overlay']")));
                overlayButton.Click();
                Thread.Sleep(3000);
                var staticButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Static']")));
                staticButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void DashboardLanguage()
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

                //กดปุ่มลูกโลก
                Thread.Sleep(3000);
                var globeButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-globe')]]")));
                globeButton.Click();

                //กดเปลี่ยนภาษาไทย-อังกฤษ
                Thread.Sleep(3000);
                var thaibutton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='ภาษาไทย']")));
                thaibutton.Click();
                Thread.Sleep(3000);
                var engButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='English']")));
                engButton.Click();
                Thread.Sleep(3000);
            }
        }
    }
}