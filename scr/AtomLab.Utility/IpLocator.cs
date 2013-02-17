/************************************************************
IP数据库、手机归属地查询软件完整源代码（C#）
  Author: rssn
  Email : rssn@163.com
  QQ    : 126027268
  Blog  : http://blog.csdn.net/rssn_net/
 ************************************************************/
using System;
using System.IO;


namespace AtomLab.Utility
{
    /// <summary>
    /// IpLocator类
    /// </summary>
    public class IpLocator
    {
        // 核心方法：IP搜索
        public static IpLocation GetIpLocation(string fn, string ips)
        {
            if (!File.Exists(fn))
            {
                throw new Exception("文件不存在!");
            }
            FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader fp = new BinaryReader(fs);
            //读文件头,获取首末记录偏移量
            int fo = fp.ReadInt32();
            int lo = fp.ReadInt32();
            //IP值
            uint ipv = IpStringToInt(ips);
            // 获取IP索引记录偏移值
            int rcOffset = getIndexOffset(fs, fp, fo, lo, ipv);
            fs.Seek(rcOffset, System.IO.SeekOrigin.Begin);

            IpLocation ipl;
            if (rcOffset >= 0)
            {
                fs.Seek(rcOffset, System.IO.SeekOrigin.Begin);
                //读取开头IP值
                ipl.IpStart = fp.ReadUInt32();
                //转到记录体
                fs.Seek(ReadInt24(fp), System.IO.SeekOrigin.Begin);
                //读取结尾IP值
                ipl.IpEnd = fp.ReadUInt32();
                ipl.Country = GetString(fs, fp);
                ipl.City = GetString(fs, fp);
            }
            else
            {
                //没找到
                ipl.IpStart = 0;
                ipl.IpEnd = 0;
                ipl.Country = "未知国家";
                ipl.City = "未知地址";
            }
            ipl.All = ipl.Country + " " + ipl.City;
            fp.Close();
            fs.Close();
            return ipl;
        }

        // 函数功能: 采用“二分法”搜索索引区, 定位IP索引记录位置
        private static int getIndexOffset(FileStream fs, BinaryReader fp, int _fo, int _lo, uint ipv)
        {
            int fo = _fo, lo = _lo;
            int mo;    //中间偏移量
            uint mv;    //中间值
            uint fv, lv; //边界值
            uint llv;   //边界末末值
            fs.Seek(fo, System.IO.SeekOrigin.Begin);
            fv = fp.ReadUInt32();
            fs.Seek(lo, System.IO.SeekOrigin.Begin);
            lv = fp.ReadUInt32();
            //临时作它用,末记录体偏移量
            mo = ReadInt24(fp);
            fs.Seek(mo, System.IO.SeekOrigin.Begin);
            llv = fp.ReadUInt32();
            //边界检测处理
            if (ipv < fv)
                return -1;
            else if (ipv > llv)
                return -1;
            //使用"二分法"确定记录偏移量
            do
            {
                mo = fo + (lo - fo) / 7 / 2 * 7;
                fs.Seek(mo, System.IO.SeekOrigin.Begin);
                mv = fp.ReadUInt32();
                if (ipv >= mv)
                    fo = mo;
                else
                    lo = mo;
                if (lo - fo == 7)
                    mo = lo = fo;
            } while (fo != lo);
            return mo;
        }

        // 字符串数值型判断
        public static bool IsNumeric(string s)
        {
            if (s != null && System.Text.RegularExpressions.Regex.IsMatch(s, @"^-?\d+$"))
                return true;
            else
                return false;
        }
        // IP字符串->长整型值
        public static uint IpStringToInt(string IpString)
        {
            uint Ipv = 0;
            string[] IpStringArray = IpString.Split('.');
            int i;
            uint Ipi;
            for (i = 0; i < 4 && i < IpStringArray.Length; i++)
            {
                if (IsNumeric(IpStringArray[i]))
                {
                    Ipi = (uint)Math.Abs(Convert.ToInt32(IpStringArray[i]));
                    if (Ipi > 255) Ipi = 255;
                    Ipv += Ipi << (3 - i) * 8;
                }
            }
            return Ipv;
        }
        // 长整型值->IP字符串
        public static string IntToIpString(uint Ipv)
        {
            string IpString = "";
            IpString += (Ipv >> 24) + "." + ((Ipv & 0x00FF0000) >> 16) + "." + ((Ipv & 0x0000FF00) >> 8) + "." + (Ipv & 0x000000FF);
            return IpString;
        }
        // 读取字符串
        private static string ReadString(BinaryReader fp)
        {
            byte[] TempByteArray = new byte[128];
            int i = 0;
            do
            {
                TempByteArray[i] = fp.ReadByte();
            } while (TempByteArray[i++] != '\0' && i < 128);
            return System.Text.Encoding.Default.GetString(TempByteArray).TrimEnd('\0');
        }
        // 读取三字节的整数
        private static int ReadInt24(BinaryReader fp)
        {
            if (fp == null) return -1;
            int ret = 0;
            ret |= (int)fp.ReadByte();
            ret |= (int)fp.ReadByte() << 8 & 0xFF00;
            ret |= (int)fp.ReadByte() << 16 & 0xFF0000;
            return ret;
        }
        // 读取IP所在地字符串
        private static string GetString(FileStream fs, BinaryReader fp)
        {
            byte Tag;
            int Offset;
            Tag = fp.ReadByte();
            if (Tag == 0x01)		// 重定向模式1: 城市信息随国家信息定向
            {
                Offset = ReadInt24(fp);
                fs.Seek(Offset, System.IO.SeekOrigin.Begin);
                return GetString(fs, fp);
            }
            else if (Tag == 0x02)	// 重定向模式2: 城市信息没有随国家信息定向
            {
                Offset = ReadInt24(fp);
                int TmpOffset = (int)fs.Position;
                fs.Seek(Offset, System.IO.SeekOrigin.Begin);
                string TmpString = GetString(fs, fp);
                fs.Seek(TmpOffset, System.IO.SeekOrigin.Begin);
                return TmpString;
            }
            else	// 无重定向: 最简单模式
            {
                fs.Seek(-1, System.IO.SeekOrigin.Current);
                return ReadString(fp);
            }
        }

        public static string GetIpLocation(string ip)
        {
            return GetIpLocation(AppDomain.CurrentDomain.BaseDirectory + "App_data\\QQWry.Dat", ip).All;
        }
    }

    // IP查询结果结构
    public struct IpLocation
    {
        public uint IpStart;
        public uint IpEnd;
        public string Country;
        public string City;
        public string All;
    }
}