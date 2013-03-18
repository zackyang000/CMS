//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/1/2011 10:29:38 PM
//===========================================================

using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Text.RegularExpressions;

namespace AtomLab.Utility
{
    public class RequestHelper
    {
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !IsIP(result))
            {
                return "127.0.0.1";
            }

            return result;
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #region 获取String方式传入的值

        /// <summary>
        /// 获得传入对象的值 默认过滤SQL注入
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <returns></returns>
        public static string GetInputString(string _value)
        {
            return GetInputString(_value, "", true, false);
        }

        /// <summary>
        /// 获得传入对象的值(返回字符串类型)
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <param name="filterSQL">是否过滤SQL注入</param>
        /// <param name="filterHtml">是否过滤Html</param>
        /// <returns></returns>
        public static string GetInputString(string _value, string defaultValue, bool filterSQL, bool filterHtml)
        {

            if (string.IsNullOrEmpty(_value))
            {
                return defaultValue;
            }

            if (filterSQL)
            {
                //过滤SQL注入
                _value = StringHelper.SqlFilter(_value);
            }

            if (filterHtml)
            {
                //过滤Html
                _value = StringHelper.SqlFilter(_value);
            }

            return _value;

        }

        /// <summary>
        /// 获得传入对象的值(返回整型)
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns>返回整型</returns>
        public static int GetInputInt32(string _value, int defaultValue)
        {

            int num;
            if (!int.TryParse(_value, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 获得传入对象的值(返回双浮点型)
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns></returns>
        public static double GetInputSingle(string _value, double defaultValue)
        {
            double num;
            if (!double.TryParse(_value, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 获得传入对象的值(返回布尔型)
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns></returns>
        public static bool GetInputBoolean(string _value, bool defaultValue)
        {
            bool result;
            if (!bool.TryParse(_value, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 获得传入对象的值(返回日期型)
        /// </summary>
        /// <param name="_value">传入的值</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns></returns>
        public static DateTime GetInputDateTime(string _value, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(_value, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        #endregion

        #region 获取GET方式传入的值

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <returns></returns>
        public static string GetQueryString(string queryItem)
        {
            return GetQueryString(queryItem, "", true);
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回字符串类型)
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <param name="filterSQL">是否检测SQL注入</param>
        /// <returns></returns>
        public static string GetQueryString(string queryItem, string defaultValue, bool filterSQL)
        {
            string _str = HttpContext.Current.Request.QueryString[queryItem];
            if (string.IsNullOrEmpty(_str))
            {
                return defaultValue;
            }

            if (filterSQL)
            {
                //过滤SQL注入
                return StringHelper.SqlFilter(_str);
            }
            else
            {
                return _str;
            }
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回整型)
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns>返回整型</returns>
        public static int GetQueryInt32(string queryItem, int defaultValue)
        {
            queryItem = HttpContext.Current.Request.QueryString[queryItem];
            int num;
            if (!int.TryParse(queryItem, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回双浮点型)
        /// </summary>
        /// <param name="queryItem"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetQuerySingle(string queryItem, double defaultValue)
        {
            queryItem = HttpContext.Current.Request.QueryString[queryItem];
            double num;
            if (!double.TryParse(queryItem, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        #endregion

        #region 获取POST方式传入的值

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <returns></returns>
        public static string GetFormString(string queryItem)
        {
            return GetFormString(queryItem, "", true);
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回字符串类型)
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <param name="filterSQL">是否检测SQL注入</param>
        /// <returns></returns>
        public static string GetFormString(string queryItem, string defaultValue, bool filterSQL)
        {
            string _str = HttpContext.Current.Request.Form[queryItem];
            if (string.IsNullOrEmpty(_str))
            {
                return defaultValue;
            }

            if (filterSQL)
            {
                //过滤SQL注入
                return StringHelper.SqlFilter(_str);
            }
            else
            {
                return _str;
            }
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回整型)
        /// </summary>
        /// <param name="queryItem">页面请求的参数名称</param>
        /// <param name="defaultValue">如果没有获取到 返回该默认值</param>
        /// <returns>返回整型</returns>
        public static int GetFormInt32(string queryItem, int defaultValue)
        {
            queryItem = HttpContext.Current.Request.Form[queryItem];
            int num;
            if (!int.TryParse(queryItem, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 获取WEB页面传入的值(返回双浮点型)
        /// </summary>
        /// <param name="queryItem"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetFormSingle(string queryItem, double defaultValue)
        {
            queryItem = HttpContext.Current.Request.Form[queryItem];
            double num;
            if (!double.TryParse(queryItem, out num))
            {
                num = defaultValue;
            }
            return num;
        }

        #endregion

    }
}
