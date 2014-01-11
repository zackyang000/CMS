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
    public class LocalUserSecurity : IUserSecurity
    {
        public bool Login(string loginName, string password)
        {
            return Proxy.Repository<User>().Exist(p => p.LoginName == loginName && p.Password == password);
        }

        public User Get(string loginName, string password)
        {
            var user = Proxy.Repository<User>().Get(p => p.LoginName == loginName);
            user.Avatar = GetAvater(user);
            return user;
        }

        public string GetAvater(User user)
        {
            return GravatarHelper.GetImage(user.Email);
        }
    }
}