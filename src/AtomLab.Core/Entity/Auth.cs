using System;
using System.Linq;
using AtomLab.Utility;

namespace AtomLab.Core
{
    public static class Auth
    {
        public static string UserName
        {
            get
            {
                if (GetName != null) return GetName();
                return "unknown";
            }
        }

        public static string Ip
        {
            get
            {
                if (GetIp != null) return GetIp();
                return "unknown";
            }
        }

        public static string Address
        {
            get
            {
                return IpLocator.GetIpLocation(Ip);
            }
        }

        public static Func<string> GetName;
        public static Func<string> GetIp;
    }

}
