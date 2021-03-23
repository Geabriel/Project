using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.obyavaUA
{
    class ObyavaParser : IParser<ResultList>
    {
        public ResultList Parse(IWebDriver document)
        {
            string url = "Нет результата";
            string heder = "Нет результата";
            string discriplion = "Нет результата";
            string city = "Нет результата";
            string supplier = "Нет результата";
            List<string> phone = new List<string>();
            uint id = 0;
            string datepUblication = "Нет результата";
            List<string> priceProduct = new List<string>();

            url = document.Url;

            try
            {
                heder = document.FindElements(By.ClassName("name-and-time"))[0].FindElement(By.TagName("h1")).Text;
            }
            catch { url = null; }
            try
            {
                discriplion = document.FindElements(By.ClassName("text"))[0].Text;
            }
            catch { discriplion = null; }
            try
            {
                city = document.FindElements(By.XPath("//*[@id='classified-page']/div[6]/div[1]/div[2]/div[2]/div[5]"))[0].Text;
            }
            catch { city = null; }
            try
            {
                supplier = document.FindElements(By.ClassName("name"))[0].Text;
            }
            catch { supplier = null; }
            try
            {
                id = Convert.ToUInt32(document.FindElements(By.ClassName("classified-id"))[0].Text.Replace("#", string.Empty));
                datepUblication = document.FindElements(By.ClassName("time"))[0].Text;
            }
            catch { id = 0; }
            try
            {
                document.FindElements(By.ClassName("seller-number-button"))[0].Click();
                System.Threading.Thread.Sleep(500);
                if (document.FindElements(By.ClassName("seller-number")).Count > 0)
                {
                    phone.Clear();
                    foreach (var element in document.FindElements(By.ClassName("seller-number")).Where(elem=>elem.FindElement(By.TagName("span")).Text!= string.Empty))
                    {
                        phone.Add(element.FindElement(By.TagName("span")).Text);
                    }
                }
            }
            catch { phone.Clear(); }
            try
            {
                if (document.FindElements(By.ClassName("more-buttons link")).Count > 0)
                {
                    document.FindElements(By.ClassName("more-buttons link"))[0].Click();
                    priceProduct.Clear();
                    foreach (var element in document.FindElements(By.ClassName("info-block")).Where(elem => elem.Text != string.Empty))
                    {
                        priceProduct.Add(element.FindElement(By.TagName("a")).GetAttribute("href"));
                    }
                }
            }
            catch { priceProduct.Clear(); }

            var resultPars = new ResultList(
                url: url,
                header: heder,
                discriplion: discriplion,
                city:city,
                supplier:supplier,
                phone:phone.ToArray(),
                id: id,
                dateUblication: datepUblication,
                priceProduct:priceProduct.ToArray()
                );

            return resultPars;
        }
    }
}
