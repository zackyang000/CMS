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

                //更新token
                var randomBytes = new byte[16];
                new RNGCryptoServiceProvider().GetBytes(randomBytes);
                user.Token = BitConverter.ToString(randomBytes, 0).Replace("-", "");
                Proxy.Repository<User>().Update(user);

                return new User
                {
                    LoginName = user.LoginName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = GravatarHelper.GetImage(user.Email),
                    Token = user.Token
                };
            }
            return null;
        }

        public override string GetAvatar(User user)
        {
            return GravatarHelper.GetImage(user.Email);
        }
    }
}