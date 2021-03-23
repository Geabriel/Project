using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test_Parser.Core.ukrboardCOMUA
{
    class UkrboardParser : IParser<ResultList>
    {
        public ResultList Parse(IWebDriver document)
        {
            string url;
            string heder;
            string discriplion;
            string city;
            string supplier;
            List<string> phone = new List<string>();
            string dateUpdate;

            url = document.Url;

            try
            {
                heder = document.FindElement(By.ClassName("h1_desktop_c")).Text;
            }
            catch { heder = string.Empty; }
            try
            {
                discriplion = document.FindElement(By.ClassName("i_text")).Text;
            }
            catch { discriplion = null; }
            try
            {
                city = document.FindElements(By.ClassName("t85"))[0].Text;
            }
            catch { city = null; }
            try
            {
                supplier = document.FindElement(By.ClassName("ct_user_box_7_1")).Text;
            }
            catch { supplier = null; }
            try
            {
                dateUpdate= document.FindElements(By.ClassName("t85"))[1].Text;
            }
            catch { dateUpdate = null; }


            var result = new ResultList(url: url,
                header: heder, 
                discriplion:discriplion,
                city:city,
                supplier:supplier,
                phone:phone.ToArray(),
                dateUpdate: dateUpdate
                );
            return result;
        }
    }
}
