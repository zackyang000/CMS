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
            var token = HttpContext.Current.Request.Headers.Get("authorization");
            if (token == null) return null;

            var data=Proxy.Repository<User>().GetAll(p => p.Token == token);

            if (data.Count() == 1)
            {
                return data.First();
            }

            return null;
        }

        public  void Logoff()
        {
            throw new NotImplementedException();
        }
    }
}