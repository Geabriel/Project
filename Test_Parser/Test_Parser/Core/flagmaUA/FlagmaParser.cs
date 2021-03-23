using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.flagmaUA
{
    class FlagmaParser : IParser<ResultList>
    {
        public ResultList Parse(IWebDriver document)
        {
            string url = "Нет результата";
            string heder = "Нет результата";
            string discriplion = "Нет результата";
            string city = "Нет результата";
            string supplier = "Нет результата";
            List<string> phone = new List<string>() { "Нет результата" };
            string datepUblication = "Нет результата";


            url = document.Url;
            try
            {
                heder = document.FindElement(By.ClassName("header-container")).Text;
            }
            catch { };
            try
            {
                discriplion = document.FindElement(By.Id("description")).Text;
            }
            catch { };
            try
            {
                city = document.FindElement(By.ClassName("terr")).Text;
            }
            catch { };
            try
            {
                supplier = document.FindElement(By.XPath("//*[@id='company-link']/div/a/span")).Text;
            }
            catch { };
            try
            {
                if (document.FindElements(By.XPath("//*[@id='contacts']/div/a")).Count > 0)
                {
                    document.FindElement(By.XPath("//*[@id='contacts']/div/a")).Click();
                    System.Threading.Thread.Sleep(500);
                    phone.Clear();
                    foreach (var element in document.FindElements(By.ClassName("tel")).Where(elem => elem.Text != string.Empty))
                    {
                        phone.Add(element.Text);
                    }
                }
            }
            catch { };
            try
            {
                datepUblication = document.FindElement(By.ClassName("reg-date")).Text;
            }
            catch { };

            var resultPars = new ResultList(
               url: url,
               header: heder,
               discriplion: discriplion,
               city: city,
               supplier: supplier,
               phone: phone.ToArray(),
               dateUblication: datepUblication
               );

            return resultPars;
        }
    }
}
