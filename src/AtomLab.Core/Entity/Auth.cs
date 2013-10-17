using System;

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

        public static Func<string> GetName;
    }

}
