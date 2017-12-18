using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChatBot_FR_ver1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();


            AutorisVK vk = new AutorisVK(); // запуск формы авторизации
            vk.ShowDialog();
            vkA = new VkAPI("C:\\Users\\Рашад\\Desktop\\1.txt");
            bot.GetStr += bot_GetStr;
            bot2.GetStr += HandleAction;
            timer1.Enabled = true;
            timer2.Enabled = true;
        }



        VkAPI vkA;
        string myQues = string.Empty;
        string old = string.Empty;
        string myQues2 = string.Empty;
        string old2 = string.Empty;

        ChatBot bot = new ChatBot("C:\\Users\\Рашад\\Desktop\\BD.txt");
        ChatBot bot2 = new ChatBot("C:\\Users\\Рашад\\Desktop\\BD.txt");
        

        void HandleAction(string obj)
        {
            myQues2 = obj + "\n";
            vkA.SendInDialog(obj, Convert.ToInt32(textBox2.Text));
        }

        void bot_GetStr(string obj)
        {
            myQues = obj + "\n";
            vkA.SendInDialog(obj, Convert.ToInt32(textBox1.Text));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string str = vkA.GetForDialog(Convert.ToInt32(textBox1.Text));
            try
            {
                str = str.Split(new string[] { "body:" }, StringSplitOptions.None)[1];
                str = str.Split(new string[] { "user_id:" }, StringSplitOptions.None)[0];

                if ((str != myQues) && (str != old) && (str != " ") && (str != "\t") && (str != "\n") && (str != "введите ответ!\n") && (str != string.Empty))
                {
                    bot.Answ(str.Trim('\n'));
                    old = str;
                }
            }
            catch { }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string str = vkA.GetForDialog(Convert.ToInt32(textBox2.Text));
            try
            {
                str = str.Split(new string[] { "body:" }, StringSplitOptions.None)[1];
                str = str.Split(new string[] { "user_id:" }, StringSplitOptions.None)[0];

                if ((str != myQues2) && (str != old2) && (str != " ") && (str != "\t") && (str != "\n") && (str != "введите ответ!\n") && (str != string.Empty))
                {
                    bot2.Answ(str.Trim('\n'));
                    old2 = str;
                }
            }
            catch { }
        }

    }
}
