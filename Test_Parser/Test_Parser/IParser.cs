using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace Test_Parser
{
    interface IParser<T> where T : class
    {
        
        T Parse(IWebDriver document);
    }
}
