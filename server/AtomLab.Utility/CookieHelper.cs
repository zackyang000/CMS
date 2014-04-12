//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/16/2011 5:35:36 PM
//===========================================================

using System;
using System.Web;

namespace AtomLab.Utility
{
    public class CookieHelper
    {
        public static void Add(string key, string value)
        {
            Add(key, value, -1);
        }

        public static void Add(string key, string value, int day)
        {
            HttpContext.Current.Response.Cookies[key].Value = value;
            if (day > 0)
            {
                HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(day);
            }
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Response.Cookies[key].Value = string.Empty;
            HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(-1);
        }

        public static string Load(string key)
        {
            try
            {
                return HttpContext.Current.Request.Cookies[key].Value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
