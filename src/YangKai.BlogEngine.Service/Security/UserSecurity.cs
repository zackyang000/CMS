using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AtomLab.Core;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Service
{
    public abstract class UserSecurity
    {
        public virtual User Login(string loginName, string password)
        {
            throw new NotImplementedException();
        }

        public User AutoLogin()
        {
            throw new NotImplementedException();
        }

        public  void Logoff()
        {
            throw new NotImplementedException();
        }
    }
}