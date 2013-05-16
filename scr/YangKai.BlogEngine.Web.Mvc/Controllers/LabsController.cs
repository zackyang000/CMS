using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class LabsController : Controller
    {
        public FilePathResult Export()
        {
            var filepath =
                Server.MapPath(string.Format("/UpLoad/export/{0}.xls", DateTime.Now.ToString("导出数据 yyyy-MM-dd HHmmss")));
            QueryFactory.Instance.Lab.ExportPosts(filepath);
            return new FilePathResult(filepath, "application/vnd.ms-excel");
        }

        public ActionResult Analytics()
        {
            QueryFactory.Instance.Lab.UpdateRefStatPicture();
            return View(QueryFactory.Instance.Lab.GetRefstatInfo());
        }

        public ActionResult Probe()
        {
            return View();
        }
    }
}