using System;
using System.Security.Cryptography;
using AtomLab.Utility;
using Newtonsoft.Json;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Service
{
    public class NeweggUserSecurity : UserSecurity
    {
        private const string URL_AUTHENTICATION = "http://apis.newegg.org/common/v1/domain/security/authentication";
        private const string URL_GET_USER = "http://apis.newegg.org/common/v1/domain/user/{0}";
        private const string URL_GET_USER_AVATAR = "http://apis.newegg.org/common/v1/domain/user/{0}/avatar";

        public override User Login(string loginName, string password)
        {
            var login = Proxy.Repository<User>().Exist(p => p.LoginName == loginName
                && !p.IsDisabled
                && !p.IsDeleted);
            if (!login) return null;

            var postData = JsonConvert.SerializeObject(new User
            {
                UserName = loginName,
                Password = password,
            });
            login = Proxy.Repository<User>().Exist(p=>p.LoginName==loginName)
                && WebRequestHelper.Request<Authentication>(URL_AUTHENTICATION, "PUT", postData).Result;
            if (login)
            {
                //更新token
                var user = Proxy.Repository<User>().Get(p => p.LoginName == loginName);
                var randomBytes = new byte[16];
                new RNGCryptoServiceProvider().GetBytes(randomBytes);
                user.Token = BitConverter.ToString(randomBytes, 0).Replace("-", "");
                Proxy.Repository<User>().Update(user);

                var neweggUser = WebRequestHelper.Request<NeweggUser>(string.Format(URL_GET_USER, loginName));
                return new User
                {
                    LoginName = loginName,
                    UserName = neweggUser.FullName,
                    Email = neweggUser.Email,
                    Avatar = string.Format(URL_GET_USER_AVATAR, loginName),
                    Token = user.Token,
                };
            }
            return null;
        }

        public override string GetAvatar(User user)
        {
            return string.Format(URL_GET_USER_AVATAR, user.LoginName);
        }

        private class Authentication
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool Result { get; set; }
        }

        private class NeweggUser
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}