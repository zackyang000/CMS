using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace AtomLab.Utility
{
    public class StringHelper
    {

        /// <summary>
        /// 把指定字符串截取成指定长度的子串(一中文按两长度)。
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns>指定长度的子串</returns>
        public static string CutString(string str, int length)
        {
            return CutString(str, length, "");

        }

        /// <summary>
        /// 把指定字符串截取成指定长度的子串(一中文按两长度)。
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns>指定长度的子串</returns>
        public static string CutString(string _FullStr, int _Length, string _EndStr)
        {
            if (Encoding.Default.GetBytes(_FullStr).Length <= _Length)
            {
                return _FullStr;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            _Length -= Encoding.Default.GetBytes(_EndStr).Length;
            int num = 0;
            StringBuilder builder = new StringBuilder();
            byte[] bytes = encoding.GetBytes(_FullStr);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > _Length)
                {
                    break;
                }
                builder.Append(_FullStr.Substring(i, 1));
            }

            if (!string.IsNullOrEmpty(_EndStr))
            {
                builder.Append(_EndStr);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 过滤'-.\\;:\%<>《》 *@
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string getInputTxt(string txt)
        {
            txt = FilterHtml(txt);
            txt = txt.Replace("'", "").Replace("-", "").Replace(".", "").Replace("\\", "").Replace(";", "").Replace(":", "").Replace("\"", "").Replace("%", "").Replace("<", "").Replace(">", "").Replace("《", "").Replace("》", "").Replace(" ", "").Replace("*", "").Replace("@", "").Trim();
            return txt;
        }

        /// <summary>
        /// 过滤'-\\;\<>《》 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string getInputUrl(string txt)
        {
            return txt.Replace("'", "").Replace("-", "").Replace("\\", "").Replace(";", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("《", "").Replace("》", "").Replace(" ", "").Trim();
        }

        public static string FilterHtml(string htmlCode)
        {
            string returnStr = htmlCode;
            foreach (Match m in Regex.Matches(htmlCode, "<(.|\n)+?>"))
            {
                returnStr = returnStr.Replace(m.Value, "");
            }
            return returnStr;
        }

        /// <summary>
        /// 给字符串前面加0
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string AddZeroToStr(int str, int length)
        {
            string restr = str.ToString();

            for (int i = 0; i < length - str.ToString().Length; i++)
            {
                restr = "0" + restr;
            }

            return restr;
        }

        #region 数据安全

        /// <summary>
        /// SQL 特殊字符过滤,防SQL注入
        /// </summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        public static string SqlFilter(string Contents)
        {
            string _pattern = "exec|insert|select|delete|'|update|chr|mid|master|truncate|char|declare|and|--";
            if (Regex.IsMatch(Contents.ToLower(), _pattern, RegexOptions.IgnoreCase))
            {
                Contents = Regex.Replace(Contents.ToLower(), _pattern, " ", RegexOptions.IgnoreCase);
            }
            return Contents;
        }

        /// <summary>
        /// SQL 特殊字符(%,-,')替换处理,防SQL注入
        /// </summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        public static string SqlReplace(string Contents)
        {
            Contents = Contents.Replace("'", "''").Replace("%", "[%]").Replace("-", "[-]");
            return Contents;
        }

        /// <summary>
        /// 转化XML的特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string XmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("&", "&amp;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("'", "&apos;");
                str = str.Replace("\"", "&quot;");
            }
            return str;
        }

        /// <summary>
        /// 转化JS的特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToJavaScript(string str)
        {
            str = str.Replace(@"\", @"\\");
            str = str.Replace("\n", @"\n");
            str = str.Replace("\r", @"\r");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        #endregion

        #region 过滤方法
        /// <summary>
        /// 过滤A标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterA(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(returnStr, "<.?a(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤DIV标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterDiv(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(htmlCode, "<.?div(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤FONT标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterFont(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(returnStr, "<.?font(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤IMG标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterImg(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(returnStr, "<img(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤OBJECT标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterObject(string htmlCode)
        {
            string pattern = @"<object((?:.|\n)*?)</object>";
            string objStr = string.Empty;
            Match m = new Regex(pattern, RegexOptions.IgnoreCase).Match(htmlCode);
            if (m.Success)
            {
                objStr = m.Value;
                htmlCode = htmlCode.Replace(objStr, "");
            }
            return htmlCode;
        }

        /// <summary>
        /// 过滤JavaScript标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterScript(string htmlCode)
        {
            string pattern = @"<script((?:.|\n)*?)</script>";

            MatchCollection matches = Regex.Matches(htmlCode, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                htmlCode = htmlCode.Replace(m.Value, "");
            }
            return htmlCode;
        }

        /// <summary>
        /// 过滤IFRAME标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterIFrame(string htmlCode)
        {
            string pattern = @"<iframe((?:.|\n)*?)</iframe>";

            MatchCollection matches = Regex.Matches(htmlCode, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                htmlCode = htmlCode.Replace(m.Value, "");
            }
            return htmlCode;
        }

        /// <summary>
        /// 过滤SPAN标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterSpan(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(htmlCode, "<.?span(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤STYLE样式标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterStyle(string htmlCode)
        {
            string pattern = @"<style((?:.|\n)*?)</style>";
            string styleStr = string.Empty;
            Match m = new Regex(pattern, RegexOptions.IgnoreCase).Match(htmlCode);
            if (m.Success)
            {
                styleStr = m.Value;
                htmlCode = htmlCode.Replace(styleStr, "");
            }
            return htmlCode;
        }

        /// <summary>
        /// 过滤TABLE、TR、TD
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FilterTableProtery(string htmlCode)
        {
            string returnStr = htmlCode;
            return Regex.Replace(Regex.Replace(Regex.Replace(returnStr, "<.?table(.|\n)*?>", "", RegexOptions.IgnoreCase), "<.?tr(.|\n)*?>", "", RegexOptions.IgnoreCase), "<.?td(.|\n)*?>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 根据传入的正则表达式进行过滤
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string SuperiorHtml(string htmlCode, string pattern)
        {
            return Regex.Replace(htmlCode, pattern, "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤所有HTML标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string RemoveHtml(string htmlCode)
        {
            string returnStr = htmlCode;
            foreach (Match m in Regex.Matches(htmlCode, "<.+?>"))
            {
                returnStr = returnStr.Replace(m.Value, "");
            }
            return returnStr;
        }

        /// <summary>
        /// 过滤&...;的HTML标签
        /// </summary>
        /// <param name="htmlCode"></param>
        /// <returns></returns>
        public static string FileterSpec(string htmlCode)
        {
            string returnStr = htmlCode;
            foreach (Match m in Regex.Matches(htmlCode, "&.+?;"))
            {
                returnStr = returnStr.Replace(m.Value, "");
            }
            return returnStr;
        }

        /// <summary>
        /// 过滤所有HTML标签
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripTags(string input)
        {
            Regex regex = new Regex("<([^<]|\n)+?>");
            return regex.Replace(input, "");
        }

        #endregion
        
        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="InputStr">待分割的字符串内容</param>
        /// <param name="SplitStr">以该字符进行分割</param>
        /// <returns></returns>
        public static string[] StringSplit(string InputStr, string SplitStr)
        {
            return InputStr.Split(new string[] { SplitStr }, StringSplitOptions.None);
        }
        
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 冒泡排序法
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string[] BubbleSort(string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {//交换条件
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return r;
        }

        /// <summary>
        /// 模拟虚拟数据的方法 可以到小时 返回一个模拟后的结果整数  By Taven
        /// </summary>
        /// <param name="_RealInt">原数值</param>
        /// <param name="_StartTime">开始模拟的时间</param>
        /// <param name="_StopTime">截至日期  可以是当前时间</param>
        /// <param name="K">定义K值   2~20</param>
        /// <param name="Seed">种子数  可以是一个对象的ID</param>
        /// <returns></returns>
        public static int MockInt(int _RealInt, DateTime _StartTime, DateTime _StopTime, int _K, int Seed)
        {
            //固定随机数
            int _seed = Seed;
            double _RandK = 0.314;
            int _Rand = (int)Math.Floor(((_seed * _RandK) - Math.Floor(_seed * _RandK)) * 10);
            _Rand = _Rand < 0 ? 3 : _Rand;


            System.TimeSpan tsDiffer = _StopTime.Date - _StartTime.Date;
            int _Days = tsDiffer.Days < 0 ? 0 : tsDiffer.Days;

            //计算每天需要模拟的数
            int _MockInt = _Rand * _K;

            double _Seed_K = 8.6;
            //今天的附加K值
            int _TodayK = (int)Math.Floor(_StopTime.Day * _Seed_K);

            //计算总数的附加K值
            int _TotalK = 0;
            DateTime _TDate = _StartTime.AddDays(-1);
            while (_TDate.AddDays(1) <= _StopTime)
            {
                _TotalK += (int)Math.Floor(_TDate.Day * _Seed_K);
                _TDate = _TDate.AddDays(1);
            }

            //计算当前阶段模拟展现的数
            int CurrentTimeInt = (int)Math.Floor((double)((_MockInt + _TodayK) / 24) * DateTime.Now.Hour);
            int _TMockTodayInt = _MockInt + _TodayK + _RealInt - CurrentTimeInt;

            //计算总模拟数
            int _MockTotalInt = _MockInt * _Days + _RealInt + _TotalK - _TMockTodayInt;

            return _MockTotalInt;

        }

    }
}
