using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Parser
{
    class RefInterfeis
    {
        public IParser<ResultList> Parser { get; set; }
        public IParserSetting Settings { get; set; }
    }
}
