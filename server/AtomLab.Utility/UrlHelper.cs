//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 6/28/2011 11:32:15 PM
//===========================================================

using System;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace AtomLab.Utility
{
    public static class UrlHelper
    {
        #region public method

        // 获取网页的HTML内容，根据网页的charset自动判断Encoding
        public static string GetHtml(string url, Encoding encoding = null)
        {
            byte[] buf = new WebClient().DownloadData(url);
            if (encoding != null) return encoding.GetString(buf);
            string html = Encoding.UTF8.GetString(buf);
            encoding = GetEncodingFromHtml(html);
            if (encoding == null || encoding == Encoding.UTF8) return html;
            return encoding.GetString(buf);
        }

        // 根据URL得到网页的Title
        public static string GetTitleFromUrl(string url)
        {
            var html = GetHtml(url);
            return GetTitleFromHtml(html);
        }

        #endregion

        #region private method

        /// <summary>
        /// 根据网页的HTML内容提取网页的Encoding
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static Encoding GetEncodingFromHtml(string html)
        {
            const string pattern = @"(?i)\bcharset=(?<charset>[-a-zA-Z_0-9]+)";
            string charset = Regex.Match(html, pattern).Groups["charset"].Value;
            try { return Encoding.GetEncoding(charset); }
            catch (ArgumentException) { return null; }
        }

        /// <summary>
        /// 根据网页的HTML内容提取网页的Title
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string GetTitleFromHtml(string html)
        {
            const string pattern = @"(?si)<title(?:\s+(?:""[^""]*""|'[^']*'|[^""'>])*)?>(?<title>.*?)</title>";
            return Regex.Match(html, pattern).Groups["title"].Value.Trim();
        }

        #endregion
    }
}