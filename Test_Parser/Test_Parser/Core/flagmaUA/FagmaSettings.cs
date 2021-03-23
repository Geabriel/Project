using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Parser.Core.flagmaUA
{
    class FagmaSettings : IParserSetting
    {
        public string FinderOfSearchPanelClass { get; set; } = "ac_input";

        public string FinderOfSearchPanelId { get; set; } = "search-input-text";

        public string FindOfHeadlineClass { get; set; } = "page-list-item-header";

        public string FindOfHeadlineId { get; set; } = string.Empty;

        public string FindOfPageInLineClass { get; set; } = "yiiPager";

        public string FindOfPageInLineId { get; set; }

        public string Prefix { get; set; } = "-";

        public int Sleep { get; set; } = 5;

        public string Url
        { get; set; } = "https://{ get; set; }.ua/";
    }
}
