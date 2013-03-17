//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 3.5
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 8/1/2010 1:04:32 PM
//===========================================================

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AtomLab.Utility
{
    public class IniHelper
    {
        private readonly string path;

        public IniHelper(string iniPath)
        {
            path = iniPath;
        }

        public string IniReadValue(string section, string key)
        {
            var retVal = new StringBuilder(0xff);
            GetPrivateProfileString(section, key, "", retVal, 0xff, path);
            return retVal.ToString();
        }

        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.path);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }

}