//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/16/2011 9:42:39 PM
//===========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Utility;

namespace YangKai.BlogEngine.Common
{
    public class Cookie
    {
        #region [Private Methods]

        public static void Add(string key, string value)
        {
            Add(key, value, -1);
        }

        public static void Add(string key, string value, int day)
        {
            key = EncryptStringHelper.Encrypt(key).Replace("=", string.Empty);
            value = EncryptStringHelper.Encrypt(value);
            CookieHelper.Add(key, value, day);
        }

        public static void Remove(string key)
        {
            key = EncryptStringHelper.Encrypt(key).Replace("=", string.Empty);
            CookieHelper.Remove(key);
        }

        public static string Load(string key)
        {
            key = EncryptStringHelper.Encrypt(key).Replace("=", string.Empty);
            string value = CookieHelper.Load(key);
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else
            {
                return EncryptStringHelper.Decrypt(value);
            }
        }

        #endregion
    }
}
