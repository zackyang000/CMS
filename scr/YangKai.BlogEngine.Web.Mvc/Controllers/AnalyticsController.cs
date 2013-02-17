using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class AnalyticsController : Controller
    {
        //
        // GET: /analytics/
        [ActionName("index")]
        public ActionResult Index()
        {
            QueryFactory.Stat.UpdateRefStatPicture();
            return View(QueryFactory.Stat.GetRefstatInfo());
        }
    }
}