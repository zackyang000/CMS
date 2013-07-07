using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;

namespace AtomLab.Utility
{
    public static class Function
    {
        /// <summary>
        /// 得到中文星期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetWeek(DateTime dateTime)
        {
            string strWeek = "";
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    strWeek = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    strWeek = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    strWeek = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    strWeek = "星期四";
                    break;
                case DayOfWeek.Friday:
                    strWeek = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    strWeek = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    strWeek = "星期日";
                    break;
            }
            return strWeek;
        }

        /// <summary>
        /// 得到操作系统的版本信息
        /// </summary>
        /// <returns></returns>
        public static string GetOSName(HttpContext context)
        {
            var agent = context.Request.ServerVariables.Get("HTTP_USER_AGENT");

            string osVersion = "未知";
            if (agent.IndexOf("NT 6.2", StringComparison.Ordinal) > 0)
            {
                osVersion = "Windows 8";
            }
            else if (agent.IndexOf("NT 6.1", StringComparison.Ordinal) > 0)
            {
                osVersion = "Windows 7";
            }
            else if (agent.IndexOf("NT 6.0", StringComparison.Ordinal) > 0)
            {
                osVersion = "Windows Vista";
            }
            else if (agent.IndexOf("NT 5.2", StringComparison.Ordinal) > 0)
            {
                osVersion = "Windows Server 2003";
            }
            else if (agent.IndexOf("NT 5.1", StringComparison.Ordinal) > 0)
            {
                osVersion = "Windows XP";
            }
            else if (agent.IndexOf("Mac", StringComparison.Ordinal) > 0)
            {
                osVersion = "Mac";
            }
            else if (agent.IndexOf("Unix", StringComparison.Ordinal) > 0)
            {
                osVersion = "UNIX";
            }
            else if (agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
            {
                osVersion = "Linux";
            }
            else if (agent.IndexOf("SunOS", StringComparison.Ordinal) > 0)
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }

        ///<summary>
        ///获取一串汉字的拼音声母
        ///</summary>
        ///<param name="chinese">Unicode格式的汉字字符串</param>
        ///<returns>拼音声母字符串,如:“杨凯”转换为“yk”</returns>
        public static String ConvertToLetters(String chinese)
        {
            chinese = chinese.Replace(" ", "");
            char[] buffer = new char[chinese.Length];
            for (int i = 0; i < chinese.Length; i++)
            {
                buffer[i] = ConvertToLetter(chinese[i]);
            }
            return new String(buffer);
        }

        ///<summary>
        ///获取一个汉字的拼音声母   
        ///</summary>
        ///<param name="chinese">Unicode格式的一个汉字</param>
        ///<returns>汉字的拼音首字母</returns>
        private static char ConvertToLetter(Char chinese)
        {
            if (chinese == char.MinValue)
            {
                return '\0';
            }
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;

            //   Convert   the   string   into   a   byte[].   
            byte[] unicodeBytes = unicode.GetBytes(new Char[] { chinese });
            //   Perform   the   conversion   from   one   encoding   to   the   other.   
            byte[] asciiBytes = Encoding.Convert(unicode, gb2312, unicodeBytes);

            //   计算该汉字的GB-2312编码   
            int n = (int)asciiBytes[0] << 8;
            if (asciiBytes.Length > 1)
            {
                n += (int)asciiBytes[1];
            }
            else
            {
                return '\0';
            }
            //   根据汉字区域码获取拼音声母   
            if (In(0xB0A1, 0xB0C4, n)) return 'a';
            if (In(0XB0C5, 0XB2C0, n)) return 'b';
            if (In(0xB2C1, 0xB4ED, n)) return 'c';
            if (In(0xB4EE, 0xB6E9, n)) return 'd';
            if (In(0xB6EA, 0xB7A1, n)) return 'e';
            if (In(0xB7A2, 0xB8c0, n)) return 'f';
            if (In(0xB8C1, 0xB9FD, n)) return 'g';
            if (In(0xB9FE, 0xBBF6, n)) return 'h';
            if (In(0xBBF7, 0xBFA5, n)) return 'j';
            if (In(0xBFA6, 0xC0AB, n)) return 'k';
            if (In(0xC0AC, 0xC2E7, n)) return 'l';
            if (In(0xC2E8, 0xC4C2, n)) return 'm';
            if (In(0xC4C3, 0xC5B5, n)) return 'n';
            if (In(0xC5B6, 0xC5BD, n)) return 'o';
            if (In(0xC5BE, 0xC6D9, n)) return 'p';
            if (In(0xC6DA, 0xC8BA, n)) return 'q';
            if (In(0xC8BB, 0xC8F5, n)) return 'r';
            if (In(0xC8F6, 0xCBF0, n)) return 's';
            if (In(0xCBFA, 0xCDD9, n)) return 't';
            if (In(0xCDDA, 0xCEF3, n)) return 'w';
            if (In(0xCEF4, 0xD188, n)) return 'x';
            if (In(0xD1B9, 0xD4D0, n)) return 'y';
            if (In(0xD4D1, 0xD7F9, n)) return 'z';
            return '\0';
        }

        private static bool In(int Lp, int Hp, int Value)
        {
            return ((Value <= Hp) && (Value >= Lp));
        }
    }
}