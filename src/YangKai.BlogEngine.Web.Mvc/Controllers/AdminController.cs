using System.Web.Http;
using System.Web.Mvc;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AdminController : ApiController
    {
        public User Get()
        {
            return Current.User;
        }
    }
}