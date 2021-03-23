namespace Test_Parser
{
    interface IParserSetting
    {
        string Url { get; set; }
        string FinderOfSearchPanelClass { get; set; }
        string FinderOfSearchPanelId { get; set; }
        string Prefix { get; set; }
        string FindOfHeadlineClass { get; set; }
        string FindOfHeadlineId { get; set; }
        string FindOfPageInLineClass { get; set; }
        string FindOfPageInLineId { get; set; }
        string FindOfPageInLineXpath { get; set; }
        int Sleep { get; set; }
        string FindePageLine(OpenQA.Selenium.IWebElement element,ref uint pageCount);
    }
}
