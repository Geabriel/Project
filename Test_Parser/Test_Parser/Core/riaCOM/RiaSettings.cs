using System;
using OpenQA.Selenium;

namespace Test_Parser.Core.riaCOM
{
    class RiaSettings : IParserSetting
    {
        public string FinderOfSearchPanelClass
        { get; set; } = "flex-input";

        public string FinderOfSearchPanelId
        { get; set; } = string.Empty;

        public string FindOfHeadlineClass
        { get; set; } = "head-ticket";

        public string FindOfHeadlineId
        { get; set; } = string.Empty;

        public string FindOfPageInLineClass
        { get; set; } = "pager";

        public string FindOfPageInLineId
        { get; set; } = string.Empty;

        public string FindOfPageInLineXpath
        { get; set; } = string.Empty;

        public string Prefix
        { get; set; } = "/page/{id}/";

        public int Sleep 
        { get; set; } = 5;

        public string Url
        { get; set; } = "https://www.ria.com/";

        public string FindePageLine(IWebElement element, ref uint pageCount)
        {
            var findeAddres = element.FindElements(By.TagName("span"));
            if (findeAddres.Count > 0)
            {
                var addres = findeAddres[findeAddres.Count - 1].FindElement(By.TagName("a")).GetAttribute("href").Replace("/page/2/",Prefix);
                //var findePageNumder = element.FindElements(By.ClassName("mhide"));
                pageCount = Convert.ToUInt32(findeAddres[findeAddres.Count - 3].Text);
                return addres;
            }
            return string.Empty;
        }
    }
}
