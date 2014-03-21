using System;
using System.Web.Http;
using System.Web.Http.OData;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class UserController : EntityController<User>
    {
        [HttpPost]
        public object Signin([FromODataUri] int key, ODataActionParameters parameters)
        {
            var username = (string)parameters["Username"];
            var password = (string)parameters["Password"];
            var isRemember = (bool)parameters["IsRemember"];

            var security = Proxy.Security();

            var login = security.Login(username, password);
            if (login)
            {
                var data = security.Get(username, password);
                return new WebUser
                {
                    UserName = data.UserName,
                    LoginName = data.LoginName,
                    Password = password,
                    Email = data.Email,
                    Avatar = data.Avatar,
                    IsAdmin = true,
                    IsRemember = isRemember
                };
            }
            else
            {
                throw new Exception("Username or password error.");
            }
        }

        [HttpPost]
        public void Signout([FromODataUri] int key, ODataActionParameters parameters)
        {
            Current.User = null;
        }
    }
}