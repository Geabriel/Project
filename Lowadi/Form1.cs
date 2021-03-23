using Lowadi.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowadi
{
    public partial class Form1 : Form
    {
        CoreBrowser browser;
        public Form1()
        {
            InitializeComponent();
            browser = new CoreBrowser();
        }

        async private void Form1_Load(object sender, EventArgs e)
        {
            await Task.Run(()=>browser.StartWork());
        }
    }
}
