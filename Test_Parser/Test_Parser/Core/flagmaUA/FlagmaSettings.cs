using System;
using OpenQA.Selenium;

namespace Test_Parser.Core.flagmaUA
{
    class FlagmaSettings : IParserSetting
    {
        public string FinderOfSearchPanelClass { get; set; } = "ac_input";

        public string FinderOfSearchPanelId { get; set; } = "search-input-text";

        public string FindOfHeadlineClass { get; set; } = "page-list-item-header";

        public string FindOfHeadlineId { get; set; } = string.Empty;

        public string FindOfPageInLineClass { get; set; } = "yiiPager";

        public string FindOfPageInLineId { get; set; } = string.Empty;
        public string FindOfPageInLineXpath { get; set; } = string.Empty;

        public string Prefix { get; set; } = "-{id}.html";

        public int Sleep { get; set; } = 5;

        public string Url
        { get; set; } = "https://flagma.ua/";

        public string FindePageLine(IWebElement element, ref uint pageCount)
        {
            var findeAddres = element.FindElements(By.TagName("li"));
            if (findeAddres.Count > 0)
            {
                var addres = findeAddres[findeAddres.Count - 1].FindElement(By.TagName("a")).GetAttribute("href").Replace("-2.html", Prefix);
                pageCount = Convert.ToUInt32(findeAddres[findeAddres.Count - 2].Text);
                return addres;
            }
            return string.Empty;
        }
    }
}
