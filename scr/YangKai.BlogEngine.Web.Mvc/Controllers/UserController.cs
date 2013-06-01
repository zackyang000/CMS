using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class UserController : ApiController
    {
        public object Get()
        {
            if (WebMasterCookie.IsLogin)
            {
                var user = WebMasterCookie.Load();
                return new
                {
                    isAdmin = true,
                    Name = user.Name,
                };
            }

            WebGuestCookie cookie = WebGuestCookie.Load();
            return new
            {
                isAdmin = false,
                Name = cookie.Name,
                Email = cookie.Email,
                Url = cookie.Url,
            };
        }
    }
}