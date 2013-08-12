using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class UserController : EntityController<User>
    {
        [HttpPost]
        public void RateProduct([FromODataUri] int key, ODataActionParameters parameters)
        {
            var username = (string)parameters["Username"];
            var password = (string)parameters["Password"];
            var isRemember = (bool)parameters["IsRemember"];

            var isExist = Proxy.Repository<User>().Exist(p => p.LoginName == username && p.Password == password);

            if (isExist)
            {
                var data = Proxy.Repository<User>().Get(p => p.LoginName == username);
                Current.User = new WebUser()
                {
                    UserName = data.UserName,
                    LoginName = data.LoginName,
                    Password = data.Password,
                    Email = data.Email,
                    IsAdmin = true,
                    IsRemember = isRemember
                };
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}