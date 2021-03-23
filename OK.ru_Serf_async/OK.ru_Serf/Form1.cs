using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;

namespace OK.ru_Serf
{
    public partial class Form1 : Form
    {
        

        IWebDriver Browser;
		XmlDocument doc;

		public Form1()
        {
            InitializeComponent();
            doc = new XmlDocument();
        }

        void funcComplite()
        {
            string Pach = Application.StartupPath + @"\complite\";
            string fileName = Pach + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            List<string> result = new List<string>();
            IWebElement SerchInput;
            IReadOnlyCollection<IWebElement> SerchsInput;

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            XmlElement root = doc.DocumentElement;

            Browser.Navigate().GoToUrl(root["URL"].Attributes.GetNamedItem("url").Value);
            try
            {

                #region login
                SerchInput = Browser.FindElement(By.Id(root["URL"].ChildNodes[0].InnerText));
                SerchInput.SendKeys(root["URL"].ChildNodes[0].Attributes[0].Value);

                SerchInput = Browser.FindElement(By.Id(root["URL"].ChildNodes[1].InnerText));
                SerchInput.SendKeys(root["URL"].ChildNodes[1].Attributes[0].Value);

                SerchInput = Browser.FindElement(By.XPath($".//*/input[@value=\"Войти\"]"));
                SerchInput.Click();
                //Browser.Navigate().GoToUrl(root["URL"].ChildNodes[2].Attributes[0].Value);
                #endregion

                searchTextBox.Invoke(new Action(() => { }));

                string[] SEARCHLIST = File.ReadAllLines(Application.StartupPath + @"\" + root["URL"].ChildNodes[3].Attributes[0].Value, Encoding.Default);
                string[] IGNORLIST = File.ReadAllLines(Application.StartupPath + @"\" + root["URL"].ChildNodes[4].Attributes[0].Value, Encoding.Default);

                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText("Find sentence:\n"); }));
                foreach (var tmp in SEARCHLIST)
                    searchTextBox.Invoke(new Action(() =>
                    {
                        searchTextBox.AppendText(tmp + "\n");
                    }));
                searchTextBox.Invoke(new Action(() =>
                {
                    searchTextBox.AppendText("Ignore sentence:\n");
                }));
                foreach (var tmp in IGNORLIST)
                    searchTextBox.Invoke(new Action(() =>
                    {
                        searchTextBox.AppendText(tmp + "\n");
                    }));

                #region поиск групп
                foreach (var saertext in SEARCHLIST)
                {
                    Browser.Navigate().GoToUrl(root["URL"].ChildNodes[2].Attributes[0].Value);
                    SerchInput = Browser.FindElement(By.Id("query_userAltGroupSearch"));
                    SerchInput.SendKeys(saertext + OpenQA.Selenium.Keys.Enter);


                    for (int i = 0; i < 5; i++)
                    {
                        IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        System.Threading.Thread.Sleep(500);

                    }
                    System.Threading.Thread.Sleep(2000);
                    SerchsInput = Browser.FindElements(By.XPath("//*/div/div[@class=\"caption\"]")).ToList();
                    System.Threading.Thread.Sleep(500);
                    searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText(SerchsInput.Count + "\n"); }));

                    List<Serc_URL_Data> url_list = new List<Serc_URL_Data>();
                    foreach (var href in SerchsInput)
                    {
                        bool check = true;
                        foreach (var ignor in IGNORLIST)
                        {
                            string tmp = href.FindElement(By.ClassName("o")).Text;
                            if (tmp.ToLower().Contains(ignor.ToLower()))
                            {
                                check = false;
                                break;
                            }
                        }

                        searchTextBox.Invoke(new Action(() =>
                        {
                            searchTextBox.AppendText("URL " + href.FindElement(By.ClassName("o")).GetAttribute("href") + " Title " + href.FindElement(By.ClassName("o")).Text + "\n");
                        }));
                        if (check)
                        {
                            url_list.Add(new Serc_URL_Data(href.FindElement(By.ClassName("o")).GetAttribute("href"), ""));
                        }
                        else
                        {
                            url_list.Add(new Serc_URL_Data(href.FindElement(By.ClassName("o")).GetAttribute("href"), "block"));
                        }
                    }

                    for (int i = 0; i < url_list.Count; i++)
                    {
                        if (url_list[i].Date != "block")
                        {
                            Browser.Url = url_list[i].Url;
                            if (Browser.FindElements(By.ClassName("feed_date")).Count > 0)
                            {
                                string Date = Browser.FindElements(By.ClassName("feed_date"))[0].Text;
                                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText(url_list[i].Url + "   " + Date + " "); }));

                                bool check = Date.ToLower().Contains("вчера");
                                if (check)
                                {
                                    Date = (DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0))).ToString();
                                }

                                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText("\n"); }));

                                if ((DateTime.Now - Convert.ToDateTime(Date)).Days <= 7)
                                {
                                    url_list[i].Date = Date;
                                }
                                else { url_list[i].Date = "block"; }
                                string count="";
                                foreach (var tmp in Browser.FindElements(By.Id("membersCountEl"))[0].Text.Split())
                                    count += tmp;
                                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText("Подписчики: "+count + "\n"); }));
                                if (countPiple.Text != "")
                                if (Convert.ToInt32(count) > Convert.ToInt32(countPiple.Text))
                                { url_list[i].Date = "block"; }
                                //searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText("Подписчики: " + count +" "+url_list[i].Date+ "\n"); }));
                            }
                            else { url_list[i].Date = "block"; }
                        }
                    }

                    foreach (var select in url_list)
                    {
                        if (select.Date != "block")
                        {
                            result.Add(select.Url);
                        }
                        else
                        {
                            searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText($"{select.Url} {select.Date}\n"); }));
                        }
                    }
                    #endregion
                    foreach (var select in url_list)
                    {
                        if (select.Date != "block")
                        {
                            result.Add(select.Url);
                        }
                        else
                        {
                            searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText($"{select.Url} {select.Date}\n"); }));
                        }
                    }
                    #region save file
                    if (File.Exists(fileName))
                    {
                        using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Open, FileAccess.Write)))
                        {
                            foreach (string tmp in result)
                            { sw.WriteLine(tmp); }
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                        {
                            foreach (string tmp in result)
                            { sw.WriteLine(tmp); }
                            sw.Close();
                        }
                    }
                    #endregion
                    result.Clear();
                }
            }
            catch (Exception e)
            {
                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText("PROGRAM STOP:\n"); searchTextBox.AppendText($"{e.Message}\n"); }));
            }

            if (File.Exists(fileName))
            {
                SendMail(
                 root["URL"].ChildNodes[5].Attributes["host"].Value,
                 Convert.ToInt32(root["URL"].ChildNodes[5].Attributes["port"].Value),
                 root["URL"].ChildNodes[5].Attributes["addres"].Value,
                 root["URL"].ChildNodes[5].Attributes["password"].Value,
                 root["URL"].ChildNodes[6].Attributes["addres"].Value, "Отчет", "Парсинг выполнен", fileName);
                File.Delete(fileName);
            }
            startSearchButton.Invoke(new Action(() => { startSearchButton.Enabled = true; }));
            countPiple.Invoke(new Action(() => { countPiple.Enabled = true; }));
        }

		public void SendMail(string smtpServer,int port, string from, string password,
			string mailto, string caption, string message, string attachFile = null)
		{
			try
			{
				MailMessage mail = new MailMessage();
				mail.From = new MailAddress(from);
				mail.To.Add(new MailAddress(mailto));
				mail.Subject = caption;
				mail.Body = message;
				if (!string.IsNullOrEmpty(attachFile))
					mail.Attachments.Add(new Attachment(attachFile));
				SmtpClient client = new SmtpClient();
				client.UseDefaultCredentials = false;
				client.Host = smtpServer;
				client.Port = port;
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential(from, password);
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.Send(mail);
				mail.Dispose();
                searchTextBox.Invoke(new Action(() => { searchTextBox.AppendText($"Message to email {mailto} \n"); }));
			}
			catch (Exception e)
			{
                searchTextBox.Invoke(new Action(() =>
                {
                    searchTextBox.AppendText("Mail.Send: " + e.Message + "\n");
                }));
                throw e;
            }
		}

		private async void startSearchButton_Click(object sender, EventArgs e)
		{
			searchTextBox.Text = "";
            startSearchButton.Enabled = false;
            countPiple.Enabled = false;
			doc.Load(Application.StartupPath + @"\config\OK_RU.xml");
            //funcComplite();
            await Task.Factory.StartNew(funcComplite);
		}

        private void stoptSearchButton_Click(object sender, EventArgs e)
        {
            startSearchButton.Enabled = true;
            if (Browser != null)
			Browser.Quit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Browser != null)
                Browser.Quit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doc.Load(Application.StartupPath + @"\config\OK_RU.xml");
            XmlElement root = doc.DocumentElement;
            try
            {
                button1.Enabled = false;
                SendMail(
                    root["URL"].ChildNodes[5].Attributes["host"].Value,
                    Convert.ToInt32(root["URL"].ChildNodes[5].Attributes["port"].Value),
                root["URL"].ChildNodes[5].Attributes["addres"].Value,
                root["URL"].ChildNodes[5].Attributes["password"].Value,
                root["URL"].ChildNodes[6].Attributes["addres"].Value, "Тест отправки", "тест пройден");
            }
            catch (Exception ex) { searchTextBox.AppendText(ex.Message + "\n"); button1.Enabled = true; };
            button1.Enabled = true;
        }

        private void countPiple_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) ) return;
            else
                e.Handled = true;
        }
    }
}
