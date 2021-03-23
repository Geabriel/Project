using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.ukrboardCOMUA
{
    class UkrboardSettings : IParserSetting
    {
        public string Url { get; set; } = "http://www.ukrboard.com.ua/";
        public string FinderOfSearchPanelClass { get; set; } = "ui-autocomplete-input";
        public string FinderOfSearchPanelId { get; set; } = string.Empty;
        public string Prefix { get; set; } = "&page={id}";
        public string FindOfHeadlineClass { get; set; } = "i_title";
        public string FindOfHeadlineId { get; set; } = string.Empty;
        public string FindOfPageInLineClass { get; set; } = "list";
        public string FindOfPageInLineId { get; set; } = "";
        public string FindOfPageInLineXpath { get; set; } = "";
        public int Sleep { get; set; }

        public string FindePageLine(IWebElement element, ref uint pageCount)
        {
            
            var findeAddres = element.FindElements(By.TagName("a"));
            if (findeAddres.Count > 0)
            {
                var addres = findeAddres[findeAddres.Count - 1].GetAttribute("href").Replace("&page=2", Prefix);
                pageCount = 200;
                return addres;
            }
            return string.Empty;
        }
    }
}
