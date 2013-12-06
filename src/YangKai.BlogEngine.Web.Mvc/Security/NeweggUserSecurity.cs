using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AtomLab.Core;
using AtomLab.Utility;
using Newtonsoft.Json;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class NeweggUserSecurity : IUserSecurity
    {
        private const string URL_AUTHENTICATION = "http://apis.newegg.org/common/v1/domain/security/authentication";
        private const string URL_GET_USER = "http://apis.newegg.org/common/v1/domain/user/{0}";

        public bool Login(string loginName, string password)
        {
            var postData = JsonConvert.SerializeObject(new User
            {
                UserName = loginName,
                Password = password,
            });
            return Proxy.Repository<User>().Exist(p=>p.LoginName==loginName)
                && WebRequestHelper.Request<DomainUserAuthenticationInfo>(URL_AUTHENTICATION, "PUT", postData).Result;
        }

        public User Get(string loginName, string password)
        {
            var neweggUser = WebRequestHelper.Request<NeweggUser>(string.Format(URL_GET_USER, loginName));
            var user=  new User()
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
            var name = string.IsNullOrEmpty(user.LoginName) ? user.UserName : user.LoginName;
            return string.Format("http://apis.newegg.org/common/v1/domain/user/{0}/avatar", name);
        }

        private class DomainUserAuthenticationInfo
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