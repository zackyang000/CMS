using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Models;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class LabsController : Controller
    {
        public ActionResult Export()
        {
            var data = QueryFactory.Instance.Post.FindAllByNormal(1, 50, null, null, null, null, null, null);
            return View(data.DataList.ToViewModels());
        }

        public FilePathResult ExportExcel()
        {
            var filepath =
                Server.MapPath(string.Format("/UpLoad/export/{0}.xls", DateTime.Now.ToString("导出数据 yyyy-MM-dd HHmmss")));
            var data = QueryFactory.Instance.Post.FindAllByNormal(1, 50, null, null, null, null, null, null);
            QueryFactory.Instance.Lab.ExportPosts(filepath,data.DataList);
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