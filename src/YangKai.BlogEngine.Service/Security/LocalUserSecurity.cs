using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AtomLab.Core;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Service
{
    public class LocalUserSecurity : UserSecurity
    {
        public override User Login(string loginName, string password)
        {
            var login = Proxy.Repository<User>().Exist(p => p.LoginName == loginName && p.Password == password);
            if (login)
            {
                var user = Proxy.Repository<User>().Get(p => p.LoginName == loginName);
                user.Avatar = GravatarHelper.GetImage(user.Email);
                return user;
            }
            return null;
        }
    }
}