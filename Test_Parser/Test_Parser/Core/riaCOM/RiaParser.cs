using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.riaCOM
{
    class RiaParser : IParser<ResultList>
    {
        public ResultList Parse(IWebDriver document)
        {
            string url;
            string heder;
            string discriplion;
            string city;
            string supplier;
            List<string> phone = new List<string>();
            uint id;
            string category;
            string dateUblication;
            string dateUpdate;

            url = document.Url;

            try{ heder = document.FindElement(By.ClassName("break-all")).Text;}
            catch { heder = null; }

            try
            {
                try
                {
                    document.FindElement(By.ClassName("more-description")).Click();
                }
                catch { }
                discriplion = document.FindElement(By.ClassName("v-desc")).Text;
            }
            catch { discriplion = null; }

            try { city = document.FindElement(By.ClassName("city-name")).Text;}
            catch { city = null; }

            try{supplier = document.FindElement(By.ClassName("user-name")).Text;}
            catch { supplier = null; }

            try { category = document.FindElement(By.ClassName("indent")).Text;}
            catch { category = null; }

            try
            {
                dateUblication = document.FindElements(By.XPath("/html/body/div[1]/div[2]/div[4]/div[1]/div[5]/div[1]/div"))[0].Text;

            }
            catch { dateUblication = null; }

            try
            {
                dateUpdate = document.FindElements(By.XPath("/html/body/div[1]/div[2]/div[4]/div[1]/div[5]/div[1]/div"))[1].Text;
            }
            catch { dateUpdate = null; }

            try
            {
                var elem = document.FindElements(By.XPath("/html/body/div[1]/div[2]/div[4]/div[1]/div[5]/div[1]/div"));
                var idFind = elem[elem.Count - 1].Text.Split().ToList();
                id = Convert.ToUInt32(idFind[idFind.Count-1]);
            }
            catch { id = 0; }

            try
            {

                if (document.FindElements(By.ClassName("show-data")).Count > 0)
                {
                    document.FindElement(By.ClassName("show-data")).Click();
                    try { document.FindElement(By.ClassName("close")).Click(); } catch { }
                    System.Threading.Thread.Sleep(500);
                    phone.Clear();
                    foreach (var element in document.FindElements(By.ClassName("show-phone-data")))
                    { phone.Add(element.Text);}
                }
            }
            catch { phone.Clear(); }


            var resultPars = new ResultList(
               url: url,
               header: heder,
               discriplion: discriplion,
               city: city,
               id: id,
               supplier: supplier,
               category: category,
               phone: phone.ToArray(),
               dateUblication: dateUblication,
               dateUpdate: dateUpdate
               );

            return resultPars;

        }
    }
}
