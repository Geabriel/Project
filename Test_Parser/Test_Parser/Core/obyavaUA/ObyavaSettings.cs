using System;
using OpenQA.Selenium;

namespace Test_Parser.Core.obyavaUA
{
    class ObyavaSettings : IParserSetting
    {
        public string Url { get; set; } = "https://obyava.ua/";
        public string FinderOfSearchPanelClass { get; set; } = "input-text";

        public string FinderOfSearchPanelId { get; set; } = "search-query";

        public string Prefix { get; set; } = "?page={id}";

        public string FindOfHeadlineClass { get; set; } = "info-block";

        public string FindOfHeadlineId { get; set; } = String.Empty;

        public string FindOfPageInLineClass { get; set; } = "paginate";

        public string FindOfPageInLineId { get; set; } = String.Empty;

        public int Sleep { get; set; } = 5;

        public string FindOfPageInLineXpath { get; set; } = string.Empty;

        public string FindePageLine(IWebElement element, ref uint pageCount)
        {
            var findeAddres = element.FindElements(By.TagName("li"));
            if (findeAddres.Count > 0)
            {
                var addres = findeAddres[findeAddres.Count - 1].FindElement(By.TagName("a")).GetAttribute("href").Replace("?page=2", Prefix);
                pageCount = Convert.ToUInt32(findeAddres[findeAddres.Count - 2].Text);
                return addres;
            }
            return string.Empty;
        }
    }
}
