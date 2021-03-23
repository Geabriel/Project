using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_Parser.Core.obyavaUA;
using Test_Parser.Core.flagmaUA;
using Test_Parser.Core.ogoloshaUA;
using Test_Parser.Core.riaCOM;
using Test_Parser.Core.ukrboardCOMUA;

namespace Test_Parser
{

    public partial class FormInfo : Form
    {
        WebBrowser<ResultList> browser;
        List<ResultList> list = new List<ResultList>();

        List<RefInterfeis> listPars = new List<RefInterfeis>();
        public FormInfo()
        {
            InitializeComponent();
            listPars.Add(new RefInterfeis() { Parser = new FlagmaParser(), Settings = new FlagmaSettings() });
            listPars.Add(new RefInterfeis() { Parser = new ObyavaParser(), Settings = new ObyavaSettings() });
            listPars.Add(new RefInterfeis() { Parser = new OgolohsaParser(), Settings = new OgoloshaSettings() });
            listPars.Add(new RefInterfeis() { Parser = new RiaParser(), Settings = new RiaSettings() });
            listPars.Add(new RefInterfeis() { Parser = new UkrboardParser(), Settings = new UkrboardSettings() });
        }

        void eventComplited(object send)
        {
            //MessageBox.Show("Выполнено");
        }

        void eventReturnData(object send, ResultList result)
        {
            dataGridView1.Invoke(new Action(() =>
            {
                int rowNumber = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowNumber].Cells["url"].Value = result.Url;
                dataGridView1.Rows[rowNumber].Cells["heder"].Value = result.Header;
                dataGridView1.Rows[rowNumber].Cells["discriplion"].Value = result.Discriplion;
                dataGridView1.Rows[rowNumber].Cells["city"].Value = result.City;
                dataGridView1.Rows[rowNumber].Cells["supplier"].Value = result.Supplier;

                foreach (var text in result.Phone)
                { dataGridView1.Rows[rowNumber].Cells["phone"].Value += text; }

                dataGridView1.Rows[rowNumber].Cells["id"].Value = result.Id;
                dataGridView1.Rows[rowNumber].Cells["category"].Value = result.Category;
                dataGridView1.Rows[rowNumber].Cells["email"].Value = result.Email;
                dataGridView1.Rows[rowNumber].Cells["dateUblication"].Value = result.DateUblication;
                dataGridView1.Rows[rowNumber].Cells["DateUpdate"].Value = result.DateUpdate;

            }));

            list.Add(result);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var serch = textBox1.Text;
            browser = new WebBrowser<ResultList>();
            browser.EventComplited += eventComplited;
            browser.EventReturnData += eventReturnData;
            /*browser.Parser = listPars[3].Parser;
            browser.Settings = listPars[3].Settings;
            await browser.StartFind(serch);*/
            foreach (var parsingAndSettings in listPars)
            {
                browser.Parser = parsingAndSettings.Parser;
                browser.Settings = parsingAndSettings.Settings;
                await browser.StartFind(serch);
            }
            button1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
