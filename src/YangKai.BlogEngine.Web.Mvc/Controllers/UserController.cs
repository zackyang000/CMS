using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class UserController : ApiController
    {
        public object Get()
        {
            return Current.User;
        }
    }
}