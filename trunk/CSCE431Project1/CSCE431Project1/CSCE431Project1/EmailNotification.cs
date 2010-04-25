using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;

namespace CSCE431Project1
{
    class EmailNotification
    {
        private string username = "";
        private string password = "";
        private string subject = "Tiger Tracker Notification";

        private string fromName = "Tiger Bug Tracker";
        private string fromEmail = "bugtracker@henkelproductions.com";
        private string body = "";

        private string sendTime = "ASAP";
        private ArrayList toList = new ArrayList();

        private bool responseArrived = false;
        private string responseXML = "";


        public EmailNotification(string u, string p, string s, string b, string t)
        {
            username = u;
            password = p;
            subject = s;
            body = b;
            toList.Add(t);
            sendTime = "NOW";

        }

        public void setSendASAP()
        {
            sendTime = "ASAP";
        }

        public void setSendNow()
        {
            sendTime = "NOW";
        }

        public void setSendNowOrFail()
        {
            sendTime = "NOWORFAIL";
        }


        public void setUsername(string u)
        {
            username = u;
        }

        public void setPassword(string p)
        {
            password = p;
        }

        public void setSubject(string s)
        {
            subject = s;
        }

        public void setFrom(string name, string email)
        {
            fromName = name;
            fromEmail = email;
        }

        public void setBody(string b)
        {
            body = b;
        }

        public void addRecipient(string email)
        {
            toList.Add(email);
        }

        public int send()
        {
            string tolist = "";
            for (int i = 0; i < toList.Count; i++)
            {
                tolist += toList[i].ToString();
                if (i + 1 != toList.Count)
                    tolist += ",";
            }

            string postData = "username=" + HttpUtility.UrlEncode(username) +
                 "&password=" + HttpUtility.UrlEncode(password) +
                 "&to=" + HttpUtility.UrlEncode(tolist) +
                 "&from=" + HttpUtility.UrlEncode(fromName + "<" + fromEmail + ">") +
                 "&subject=" + HttpUtility.UrlEncode(subject) +
                 "&body=" + HttpUtility.UrlEncode(body) +
                 "&sendTime=" + HttpUtility.UrlEncode(sendTime);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://69.93.227.36/webservices/mailService.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] byteArray = Encoding.ASCII.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            responseXML = responseFromServer;
            responseArrived = true;

            //Parse Response
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(responseXML);
            XmlNodeList xl = xd.GetElementsByTagName("SendTime");

            if (xl.Count > 0)
            {
                String rv = xl[0].InnerText;
                if (rv == "Never")
                    return -1;
                else
                    return Int32.Parse(rv);
            }
            else
                return -1;

        }

        public string getResponseXML()
        {
            return responseXML;
        }

        public bool responseHasArrived()
        {
            return responseArrived;
        }
    }
}
