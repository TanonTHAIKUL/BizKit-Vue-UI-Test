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

namespace Home
{
    [Collection("Sequential Tests")]
    public class Dashboard
    {
        private readonly ITestOutputHelper testOutputHelper;

        public Dashboard(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private const string loginUrl = "http://116.206.127.86:8003/login";

        [Fact]
        public void DashboardLogin()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();
                Thread.Sleep(5000);
            }
        }
        [Fact]
        public void DashboardLogout()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
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
                Thread.Sleep(3000);
                var logoutButton = wait.Until(d => d.FindElement(By.XPath("//a[contains(., 'Logout')]")));
                logoutButton.Click();
                Thread.Sleep(3000);
                var confirmButton = wait.Until(d => d.FindElement(By.XPath("//button[span[text()='Confirm']]")));
                confirmButton.Click();
                Thread.Sleep(3000);
            }
        }
        [Fact]
        public void DashboardScreenMode()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //����������¹�����״������ҧ
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
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //�������Ҵ��
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();
                Thread.Sleep(2000);

                //�����͡������ ������ҡ�մӷҧ����
                var noirButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='noir']")));
                noirButton.Click();
                Thread.Sleep(2000);
                //���á�
                var emeraldButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='emerald']")));
                emeraldButton.Click();
                Thread.Sleep(2000);
                //������
                var greenButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='green']")));
                greenButton.Click();
                Thread.Sleep(2000);
                //�������й��
                var limeButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='lime']")));
                limeButton.Click();
                Thread.Sleep(2000);
                //�����
                var orangeButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='orange']")));
                orangeButton.Click();
                Thread.Sleep(2000);
                //������ͧ�Ӿѹ
                var amberButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='amber']")));
                amberButton.Click();
                Thread.Sleep(2000);
                //������ͧ
                var yellowButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='yellow']")));
                yellowButton.Click();
                Thread.Sleep(2000);
                //�����Ƿ���
                var tealButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='teal']")));
                tealButton.Click();
                Thread.Sleep(2000);
                //�տ��������
                var cyanButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='cyan']")));
                cyanButton.Click();
                Thread.Sleep(2000);
                //�տ��
                var skyButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='sky']")));
                skyButton.Click();
                Thread.Sleep(2000);
                //�չ���Թ
                var blueButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='blue']")));
                blueButton.Click();
                Thread.Sleep(2000);
                //�դ���
                var indigoButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='indigo']")));
                indigoButton.Click();
                Thread.Sleep(2000);
                //����ǧ����л�ҧ
                var violetButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='violet']")));
                violetButton.Click();
                Thread.Sleep(2000);
                //����ǧ
                var purpleButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='purple']")));
                purpleButton.Click();
                Thread.Sleep(2000);
                //�պҹ���
                var fuchsiaButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='fuchsia']")));
                fuchsiaButton.Click();
                Thread.Sleep(2000);
                //�ժ���
                var pinkButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='pink']")));
                pinkButton.Click();
                Thread.Sleep(2000);
                //�ժ��١���Һ
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
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //�������Ҵ��
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();
                Thread.Sleep(2000);

                //�����͡������ ������ҡ����
                var grayButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='gray']")));
                grayButton.Click();
                Thread.Sleep(2000);
                //�� zinc
                var zincButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='zinc']")));
                zincButton.Click();
                Thread.Sleep(2000);
                //�� neutral
                var neutralButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='neutral']")));
                neutralButton.Click();
                Thread.Sleep(2000);
                //�� stone
                var stoneButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='stone']")));
                stoneButton.Click();
                Thread.Sleep(2000);
                //�� soho
                var sohoButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='soho']")));
                sohoButton.Click();
                Thread.Sleep(2000);
                //�� viva
                var vivaButton = wait.Until(d => d.FindElement(By.XPath("//button[@title='viva']")));
                vivaButton.Click();
                Thread.Sleep(2000);
                //�� ocean
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
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //�������Ҵ��
                Thread.Sleep(3000);
                var paletteButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-palette')]]")));
                paletteButton.Click();

                //����������¹ Preset
                Thread.Sleep(3000);
                var laraButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Lara']")));
                laraButton.Click();
                Thread.Sleep(3000);
                var auraButton = wait.Until(d => d.FindElement(By.XPath("//span[text()='Aura']")));
                auraButton.Click();
                Thread.Sleep(3000);

                //����������¹ Menu Mode
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
                //���������������͡ѹ�ѭ��ᶺ�ҧ�������
                driver.Manage().Window.Maximize();
                //Login
                driver.Navigate().GoToUrl(loginUrl);
                var email = wait.Until(d => d.FindElement(By.Id("email")));
                email.SendKeys("tanon.t@ku.th");
                var password = wait.Until(d => d.FindElement(By.CssSelector("input.p-password-input")));
                password.SendKeys("QuiZtaE033!");
                var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(., 'Sign In')]")));
                button.Click();

                //�������١�š
                Thread.Sleep(3000);
                var globeButton = wait.Until(d => d.FindElement(By.XPath("//button[i[contains(@class, 'pi-globe')]]")));
                globeButton.Click();

                //������¹������-�ѧ���
                Thread.Sleep(3000);
                var thaibutton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='������']")));
                thaibutton.Click();
                Thread.Sleep(3000);
                var engButton = wait.Until(d => d.FindElement(By.XPath("//li[@aria-label='English']")));
                engButton.Click();
                Thread.Sleep(3000);
            }
        }
    }
}