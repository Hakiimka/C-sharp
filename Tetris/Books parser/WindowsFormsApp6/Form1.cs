using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WebRequest request;
        WebResponse response;
        List<string> link;
        List<string> name;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       private void generateBooks(string mySite)
        {
            int href = 0;
            string url = "";
            int bookinfo = 0;
            int downloadbook = 0;
            string bookname = "";
            link.Clear();
            name.Clear();
            listBox1.Items.Clear();
            while (bookinfo != -1)
            {
                bookinfo = mySite.IndexOf("<div class=\"bookinfo\">"); // poisk
                if (bookinfo != -1)
                {
                    mySite = mySite.Remove(0, bookinfo + 22);
                }
                else
                    break;

                href = mySite.IndexOf("a href=");

                if (href != -1)
                {
                    mySite = mySite.Remove(0, href + 8);
                }

                href = mySite.IndexOf("\"");

                if (href != -1)
                {
                    url = mySite.Remove(href);
                    link.Add(url);
                }

                downloadbook = mySite.IndexOf("скачать книгу");

                if (downloadbook != -1)
                {
                    mySite = mySite.Remove(0, downloadbook + 14);
                    href = mySite.IndexOf("\"");
                    bookname = mySite.Remove(href);
                    name.Add(bookname);
                }

                richTextBox1.Text = bookname;
                
            }
            for (int i = 0; i < name.Count; i++)
            {
                listBox1.Items.Add(name[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            link = new List<string>();
            name = new List<string>();
            string book = textBox1.Text;
            request = WebRequest.Create("https://www.6lib.ru/?search=" + book); // запрос
            response = request.GetResponse(); // ответ
            Stream stream = response.GetResponseStream(); // 
            StreamReader reader = new StreamReader(stream); // конвертация в текст
            string mySite = reader.ReadToEnd();
            //richTextBox1.Text = mySite;
            generateBooks(mySite);

            response.Close();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                
                string book = link[index];
                int length = book.Length;
                book = book.Remove(length - 4);
                book += "txt";
                book = book.Remove(0, 7);
                request = WebRequest.Create("https://www.6lib.ru/download/" + book); // запрос
                response = request.GetResponse(); // ответ
                Stream stream = response.GetResponseStream(); 
                StreamReader reader = new StreamReader(stream); // конвертация в текст
                string mySite = reader.ReadToEnd();
                int key = mySite.IndexOf("?key=");
                string KEY = "";
                if (key!=-1)
                {
                    mySite = mySite.Remove(0, key);
                    key = mySite.IndexOf("\"");
                    KEY = mySite.Remove(key);
                }
                WebClient wc = new WebClient();
                string url = "https://www.6lib.ru/download/" + book + KEY;
                wc.DownloadFile(url, "c:\\1\\1.txt");
                richTextBox1.Text = mySite;
            }
        }
    }
}
