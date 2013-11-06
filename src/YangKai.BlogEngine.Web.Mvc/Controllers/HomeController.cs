using System.Linq;
using System.Text;
using System.Web.Mvc;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string _escaped_fragment_)
        {
            var channels = Proxy.Repository<Channel>().GetAll(p => !p.IsDeleted).ToList();
            return View(channels);
        }

        public ActionResult SEO()
        {
            var data = Proxy.Repository<Post>().GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).ToList();
            return View("google-list",data);
        }

        public ActionResult Config()
        {
            return Json(new {a = "1"});
        }
    }
}