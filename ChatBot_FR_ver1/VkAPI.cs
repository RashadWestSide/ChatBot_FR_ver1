using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Net;

namespace ChatBot_FR_ver1
{
    class VkAPI
    {
        string _tocken;
        string _id; //  id приложения
        HttpWebRequest _hwReq;
        HttpWebResponse _hwRes;

        public VkAPI(string pathParam)
        {
            string[] strs = File.ReadAllLines(pathParam);
            _tocken = strs[0];
            _id = strs[1];
        }
        public string SendInDialog(string msg, int userID)
        {
            string url = "https://api.vk.com/method/messages.send?user_id=" + userID + "&message=" + msg + "&access_token=" + _tocken + "&v=5.63";
            return Request(url);
        }

        public string GetForDialog(int userID)
        {
            string url = "https://api.vk.com/method/messages.getHistory?user_id=" + userID + "&count=1&access_token=" + _tocken + "&v=5.63";
            return Request(url);
        }


        // запрос
        string Request(string url)
        {
            _hwReq = (HttpWebRequest)HttpWebRequest.Create(url);
            _hwRes = (HttpWebResponse)_hwReq.GetResponse();

            string output = string.Empty;

            using (StreamReader stream = new StreamReader(_hwRes.GetResponseStream(), Encoding.UTF8))
            {
                output = stream.ReadToEnd();
            }


            output = HttpUtility.UrlDecode(output);
            output = output.Replace("{", "\n").Replace("}", "").Replace(",\"", "\n").Replace("\"", "");

            return output;
        }
    }
}
