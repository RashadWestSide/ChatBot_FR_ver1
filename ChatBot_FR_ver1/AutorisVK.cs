using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChatBot_FR_ver1
{
    public partial class AutorisVK : Form
    {
        public AutorisVK()
        {
            InitializeComponent();

            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=6303531&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&scope=messages&response_type=token&v=5.62%22);");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = webBrowser1.Url.ToString();
            string l = url.Split('#')[1];

            if (l[0] == 'a')
            {
                string[] strs = new string[2];
                strs[0] = l.Split('&')[0].Split('=')[1];
                strs[1] = l.Split('=')[3];
                File.WriteAllLines("C:\\Users\\Рашад\\Desktop\\1.txt", strs);
                this.Close();
            }
        }
    }
}
