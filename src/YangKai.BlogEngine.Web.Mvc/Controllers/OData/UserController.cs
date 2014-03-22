using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
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

            var security = Proxy.Security();

            var data = security.Login(username, password);
            if (data!=null)
            {
                HttpContext.Current.Response.Headers.Set("x-security-token", data.Token);
                return new User
                {
                    UserName = data.UserName,
                    LoginName = data.LoginName,
                    Email = data.Email,
                    Avatar = data.Avatar,
                };
            }
            else
            {
                throw new Exception("Username or password error.");
            }
        }

        [HttpPost]
        public object AutoSignin([FromODataUri] int key, ODataActionParameters parameters)
        {
            var data = Proxy.Security().AutoLogin();
            if (data!=null)
            {
                return new User
                {
                    UserName = data.UserName,
                    LoginName = data.LoginName,
                    Email = data.Email,
                    Avatar = data.Avatar,
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
            Proxy.Security().Logoff();
        }
    }
}