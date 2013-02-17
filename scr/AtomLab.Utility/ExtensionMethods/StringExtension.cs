using System;
using System.Text.RegularExpressions;

   public static class StringExtension
    {
        #region 截取字符串,中文占2字符
        /// <summary>
        /// 截取字符串,中文占2字符.
        /// </summary>
        /// <param name="original">原字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns>已截取字符串</returns>
        public static string SubstringChinese(this String original, int length)
        {
            //删除前后空格
            original = original.Trim();
            //替换空格&换行,避免如因HTML编码空格占6个字符造成的字符串截取不准确的问题
            original = original.Replace("&nbsp;", " ").Replace("<br>", " ");
            //得到字符串总长度
            int len = original.Length;

            int i = 0;
            for (; i < length && i < len; ++i)
            {
                if (original[i] > 0xFF)
                    --length;
            }
            length = length <= i ? i : len;
            return original.Substring(0, length);
        }
        #endregion

        #region 删除HTML标记
        /// <summary>
        /// 删除HTML标记.
        /// </summary>
        /// <param name="html">HTML字符串</param>
        /// <returns>纯文本字符串</returns>
        public static string ReplaceHTMLCode(this String html)
        {
            var regex = new Regex("<[^>]*>");
            return regex.Replace(html, string.Empty);
        }
        #endregion

       /// <summary>
       /// 判断字符串是否有值.
       /// </summary>
       /// <param name="source">源字符串</param>
       /// <returns>是否有值</returns>
        public static bool HasValue(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        #region 长度检查
        public static bool LengthValidate(this string source, int min, int max)
        {
            return source.Length > min && source.Length < max;
        }

       #endregion
    }

