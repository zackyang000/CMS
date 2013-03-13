//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/16/2011 9:42:39 PM
//===========================================================

namespace AtomLab.Utility
{
    public class EncryptionCookieHelper
    {
        #region [Private Methods]

        public static void Add(string key, string value)
        {
            Add(key, value, -1);
        }

        public static void Add(string key, string value, int day)
        {
            key = EncryptKey(key);
            value = EncryptValue(value);
            CookieHelper.Add(key, value, day);
        }

        public static void Remove(string key)
        {
            key = EncryptKey(key);
            CookieHelper.Remove(key);
        }

        public static string Load(string key)
        {
            key = EncryptKey(key);
            var value = CookieHelper.Load(key);
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else
            {
                return EncryptStringHelper.Decrypt(value);
            }
        }

        private static string EncryptKey(string o)
        {
            return EncryptStringHelper.Encrypt(o).Replace("=", string.Empty);
        }

        private static string EncryptValue(string o)
        {
            return EncryptStringHelper.Encrypt(o);
        }

        #endregion
    }
}
