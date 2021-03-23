using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OK.ru_Serf
{
    class Serc_URL_Data
    {
        private string url;
        private string date;

        public Serc_URL_Data(string url, string date)
        {
            this.Url = url;
            this.Date = date;
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }
    }
}
