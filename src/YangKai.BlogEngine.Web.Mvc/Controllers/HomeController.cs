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
            //SEO
            if (!string.IsNullOrEmpty(_escaped_fragment_))
            {
                var query = _escaped_fragment_.Split('/');
                if (query[1] == "post")
                {
                    var url = query[2];
                    var post = Proxy.Repository<Post>().Get(p => p.Url == url);
                    return Content(post.Content);
                }
            }

            var channels = Proxy.Repository<Channel>().GetAll(p => !p.IsDeleted).ToList();
            return View(channels);
        }

        public ActionResult SEO()
        {
            var data = Proxy.Repository<Post>().GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).ToList();
            return View(data);
        }

        public ActionResult Config()
        {
            return Json(new {a = "1"});
        }
    }
}