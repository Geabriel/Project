using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Test_Parser.Core.flagmaUA;
using Test_Parser.Core.obyavaUA;
using Test_Parser.Core.riaCOM;

namespace Test_Parser
{
    class WebBrowser<T> where T:class
    {
        bool isActive = false;
        uint pageNumber=1;
        string pageAddres;

        IParser<T> parser;
        Test_Parser.IParserSetting settings;
        IWebDriver browser ;

        event Action<object, T> eventReturnData;
        event Action<object> eventComplited;

        #region properties
        public bool IsActiv
        { get { return isActive; } }
        public IParser<T> Parser
        { protected get { return parser; } set { parser = value; } }
        public IParserSetting Settings
        {
            protected get
            { return settings; }
            set
            {
                if (!IsActiv)
                {
                    settings = value;
                    browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(value.Sleep);
                    browser.Navigate().GoToUrl(value.Url);
                }
            }
        }
        public Action<object, T> EventReturnData
        { get { return eventReturnData; } set { eventReturnData = value; } }
        public Action<object> EventComplited
        { get { return eventComplited; } set { eventComplited = value; } }
        #endregion

        public WebBrowser()
        { browser = new OpenQA.Selenium.Chrome.ChromeDriver(); }

        public WebBrowser(IParser<T> parser):this()
        { Parser = parser; }

        public WebBrowser(IParser<T> parser,IParserSetting settings) : this(parser)
        { Settings = settings; }

        public void Stop()
        { isActive = false; }

        public async Task StartFind(string value)
        {
            isActive = true;
            await Task.Run(()=> Worker(value));
        }

        protected void Worker(string value)
        {

            if (settings.FinderOfSearchPanelClass != string.Empty)
            {
                var click = browser.FindElement(By.ClassName(settings.FinderOfSearchPanelClass));
                click.SendKeys(value + Keys.Enter);
            }
            else if (settings.FinderOfSearchPanelId != string.Empty)
            { browser.FindElement(By.Id(settings.FinderOfSearchPanelId)).SendKeys(value + Keys.Enter); }

            var href = new List<string>();
            FindePageLine();

            for (int i = 1; i <=pageNumber;i++)
            {
                if (!isActive)
                {
                    eventComplited?.Invoke(this);
                    return;
                }

                if (settings.FindOfHeadlineClass != string.Empty)
                {
                    var webElements = browser.FindElements(By.ClassName(settings.FindOfHeadlineClass));
                    foreach (var webElement in webElements)
                    {
                        try { href.Add(webElement.FindElement(By.TagName("a")).GetAttribute("href")); } catch { };
                    }
                }
                else if (settings.FindOfHeadlineId != string.Empty)
                {
                    var webElements = browser.FindElements(By.Id(settings.FindOfHeadlineId));
                    foreach (var webElement in webElements)
                    {
                        href.Add(webElement.FindElement(By.TagName("a")).GetAttribute("href"));
                    }
                }

                if (i > 1)
                { try { NextPage(pageAddres.Replace("{id}", i.ToString())); } catch { break; }; }
            }

            href = href.Where(elem => elem != "javascript:void(0);").ToList();

            foreach (var url in href)
            {
                if (!isActive)
                { return; }
                try
                {
                    NextPage(url);
                    var result = parser.Parse(browser);
                    eventReturnData?.Invoke(this, result);
                }
                catch { }
            }

            isActive = false;
            eventComplited?.Invoke(this);
        }

        void FindePageLine()
        {
            if (settings.FindOfPageInLineClass != string.Empty)
            {
                var elements = browser.FindElements(By.ClassName(settings.FindOfPageInLineClass));
                if (elements.Count > 0)
                {
                    pageAddres = settings.FindePageLine(browser.FindElement(By.ClassName(settings.FindOfPageInLineClass)),
                        ref pageNumber);
                }
            }
            else if (settings.FindOfPageInLineId != string.Empty)
            {
                var elements = browser.FindElements(By.Id(settings.FindOfPageInLineId));
                if (elements.Count > 0)
                {
                    pageAddres = settings.FindePageLine(browser.FindElement(By.Id(settings.FindOfPageInLineId)), 
                        ref pageNumber);
                }
            }
            else if (settings.FindOfPageInLineXpath != string.Empty)
            {
                var elements = browser.FindElements(By.XPath(settings.FindOfPageInLineXpath));
                if (elements.Count > 0)
                {
                    pageAddres = settings.FindePageLine(browser.FindElement(By.XPath(settings.FindOfPageInLineXpath)), ref pageNumber);
                }
            }
        }

        void NextPage(string url)
        {
            browser.Navigate().GoToUrl(url);
        }

    }
}
