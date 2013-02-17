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
        private string path;

        public IniHelper(string INIPath)
        {
            this.path = INIPath;
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            int num = GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.path);
            return retVal.ToString();
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }

}