using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.ogoloshaUA
{
    class OgolohsaParser : IParser<ResultList>
    {
        public ResultList Parse(IWebDriver document)
        {

            string url;
            string heder;
            string discriplion;
            string supplier;
            List<string> phone =new List<string>() ;
            string category;

            List<string> priceProduct = new List<string>();

            try
            {
                url= document.Url;
            }
            catch { url = null; }
            try
            {
                heder = document.FindElement(By.ClassName("title")).Text;
            }
            catch { heder = null; }
            try
            {
                if (document.FindElements(By.ClassName("product-view__btn-text-toggler")).Count > 0)
                    document.FindElement(By.ClassName("product-view__btn-text-toggler")).Click();
                discriplion = document.FindElement(By.XPath("//*[@id='app']/section/div/div[1]/div[2]/div[2]/div")).Text;
            }
            catch { discriplion = null; }

            try
            {
                supplier = document.FindElement(By.XPath("//*[@id='app']/section/div/div[2]/div[2]/div[2]/a[1]")).Text;
            }
            catch { supplier = null; }

            try
            {
                category = document.FindElement(By.ClassName("description-data__value")).Text;
            }
            catch { category = null; }


            try
            {
                if (document.FindElements(By.Id("butShowHidePhone")).Count > 0)
                {
                    document.FindElement(By.Id("butShowHidePhone")).Click();
                    System.Threading.Thread.Sleep(500);
                }
                foreach(var element in document.FindElements(By.ClassName("phone-item")).Where(elem=> elem.FindElement(By.ClassName("phone-item")).Text!= string.Empty))
                phone.Add(element.FindElement(By.ClassName("phone-item")).Text);
            }
            catch { }

            var result = new ResultList(
                url: url,
               header: heder,
               discriplion: discriplion,
               supplier: supplier,
               category: category,
               phone: phone.ToArray(),
               city: null
                );
            return result;
        }
    }
}
