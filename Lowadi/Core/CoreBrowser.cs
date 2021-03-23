using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using Lowadi.Core.Resource;
using OpenQA.Selenium.Interactions;

namespace Lowadi.Core
{
    class CoreBrowser
    {
        IWebDriver brouser;
        UsersLoad users;
        public CoreBrowser()
        {
            users = new UsersLoad($@"{AppDomain.CurrentDomain.BaseDirectory}UserList\Users.xml");
            OpenQA.Selenium.Chrome.ChromeOptions brouserOptions = new OpenQA.Selenium.Chrome.ChromeOptions();
            OpenQA.Selenium.Chrome.ChromeDriver chrom = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverDirectory: $@"{AppDomain.CurrentDomain.BaseDirectory}Browser",options: brouserOptions);
            brouser = chrom;
            brouser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            brouser.Url = @"https://www.lowadi.com/";
        }

        public void login(string login,string password)
        {
            By byButton = By.ClassName("header-login-button");
            By byLogin = By.Id("login");
            By byPas = By.Id("password");
            brouser.FindElement(byButton).Click();
            brouser.FindElement(byLogin).SendKeys(login);
            brouser.FindElement(byPas).SendKeys(password);
            byButton = By.Id("authentificationSubmit");
            brouser.FindElement(byButton).Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void StartWork()
        {
            login(((List<User>)users.Users)[2].Login, ((List<User>)users.Users)[2].Password);

            //FindeQuastions();
            Task.Run(() => ShopBy(0, "Фураж"));
        }
        public void FindeQuastions()
        {
            brouser.Url = @"https://www.lowadi.com/daily/#header-anchor";
            By byClass = By.ClassName("module-ojectifs-container");
            var elements = brouser.FindElements(byClass);
            foreach (var element in elements)
            {
                byClass = By.ClassName("objective-title");
                var text1 = element.FindElement(byClass).Text;
                byClass = By.ClassName("objective-section");
                var text2 = element.FindElement(byClass).Text;
            }
        }

        public void Office(int index)
        {
            brouser.Url = @"https://www.lowadi.com/elevage/bureau/";
            By page = By.ClassName("tab-action");
            var pageElements = brouser.FindElements(page);
            pageElements[index].Click();
        }
        
        public void ShopBy(int index,string name)
        {
            brouser.Url = @"https://www.lowadi.com/marche/boutique";
            List<WebElementTable> table = new List<WebElementTable>();
            By page = By.ClassName("tab-action");
            var pageElements = brouser.FindElements(page);
            System.Threading.Thread.Sleep(2000);
            pageElements[index].Click();
            By byXPath = By.XPath(@"//*[@id='table-0']/tbody/tr/td/div/div/div/a/strong");
            var elements = brouser.FindElements(byXPath);
            foreach (var element in elements)
            {
                table.Add(new WebElementTable { Name = element.Text});
            }

            
            //*[@id='table-0']/tbody/tr/td/button
            elements = brouser.FindElements(By.XPath(@"//*[@id='table-0']/tbody/tr/td/button"));
            int i = 0;
            foreach (var element in elements)
            {
                if (table[i].Name == name)
                { element.Click(); break; }
                i++;
            }

            var select =  brouser.FindElement(By.XPath(@"//*/body/div/div/div/table/tbody/tr/td/select"));
            /*var action = new Actions(brouser);
            action.MoveToElement(select).Build().Perform();
            action.Click();*/
            SelectElement selectelement = new SelectElement(select);
            selectelement.SelectByValue("5");
            elements = brouser.FindElements(By.XPath(@"//*/body/div/div/div/table/tbody/tr/td/select/option"));
            
            foreach (var element in elements)
            {
                if (element.Text == "5")
                {
                    
                    //*[@id="produit109Content"]/table/tbody
                    //*div[@class='box']/*button
                    var button = brouser.FindElement(By.XPath("//*/body/div/div/div/table/tbody/tr/td/button"));
                    break;
                }
            }
            

        }

        public void mouseOver(IWebElement element)
        {
            String code = "var fireOnThis = arguments[0];"
                        + "var evObj = document.createEvent('MouseEvents');"
                        + "evObj.initEvent( 'mouseover', true, true );"
                        + "fireOnThis.dispatchEvent(evObj);";
            ((IJavaScriptExecutor)brouser).ExecuteScript(code, element);
        }

        void Alert()
        {
            brouser.SwitchTo().Alert().Accept();
        }

        
    }
}
