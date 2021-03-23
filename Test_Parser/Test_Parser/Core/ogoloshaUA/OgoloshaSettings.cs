using System;
using OpenQA.Selenium;

namespace Test_Parser.Core.ogoloshaUA
{
    class OgoloshaSettings : IParserSetting
    {
        public string Url { get; set; } = "https://ogolosha.ua/";
        public string FinderOfSearchPanelClass { get; set; } = "form-control";
        public string FinderOfSearchPanelId { get; set; } = "search_str";
        public string Prefix { get; set; } = "?page={id}";
        public string FindOfHeadlineClass { get; set; } = "prod-title";
        public string FindOfHeadlineId { get; set; } = string.Empty;
        public string FindOfPageInLineClass { get; set; } = "pagination ";
        public string FindOfPageInLineId { get; set; } = string.Empty;
        public string FindOfPageInLineXpath { get; set; } = "//*[@id='products-list']/div/div[2]";
        public int Sleep { get; set; } = 5;

        public string FindePageLine(IWebElement element, ref uint pageCount)
        {
            var findeAddres = element.FindElements(By.TagName("li"));
            if (findeAddres.Count > 0)
            {
                var addres = findeAddres[findeAddres.Count - 1].FindElement(By.TagName("a")).GetAttribute("href").Replace("?page=2", Prefix);
                pageCount = Convert.ToUInt32(findeAddres[findeAddres.Count - 3].Text);
                return addres;
            }
            return string.Empty;
        }
    }
}
