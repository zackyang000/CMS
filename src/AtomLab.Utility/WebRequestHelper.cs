using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AtomLab.Utility
{
    public static class WebRequestHelper
    {
        public static T Request<T>(string url, string method = "GET", string postData = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json";

            if (method != "GET" && !string.IsNullOrEmpty(postData))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                var newStream = request.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }

            var webResponse = request.GetResponse();
            using (var responseStream = webResponse.GetResponseStream())
            {
                var json = new StreamReader(responseStream).ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

    }
}
