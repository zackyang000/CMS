using System;
using AtomLab.Utility;
using Newtonsoft.Json;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Service
{
    public class NeweggUserSecurity : IUserSecurity
    {
        private const string URL_AUTHENTICATION = "http://apis.newegg.org/common/v1/domain/security/authentication";
        private const string URL_GET_USER = "http://apis.newegg.org/common/v1/domain/user/{0}";
        private const string URL_GET_USER_AVATAR = "http://apis.newegg.org/common/v1/domain/user/{0}/avatar";

        public bool Login(string loginName, string password)
        {
            var postData = JsonConvert.SerializeObject(new User
            {
                UserName = loginName,
                Password = password,
            });
            return Proxy.Repository<User>().Exist(p=>p.LoginName==loginName)
                && WebRequestHelper.Request<Authentication>(URL_AUTHENTICATION, "PUT", postData).Result;
        }

        public User Get(string loginName, string password)
        {
            var neweggUser = WebRequestHelper.Request<NeweggUser>(string.Format(URL_GET_USER, loginName));
            var user=  new User
            {
                LoginName = loginName,
                Password = password,
                UserName = neweggUser.FullName,
                Email = neweggUser.Email,
            };
            user.Avatar = GetAvater(user);
            return user;
        }

        public string GetAvater(User user)
        {
            return string.Format(URL_GET_USER_AVATAR, user.LoginName ?? user.UserName);
        }

        private class Authentication
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool Result { get; set; }
        }

        public class NeweggUser
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}