using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class LabsController : Controller
    {
        public ActionResult Export()
        {
            var data = Proxy.Repository<Post>().GetAll(50, p => p.PostStatus == (int)PostStatusEnum.Publish);
            return View(data.ToList());
        }

        public FilePathResult ExportExcel()
        {
            var filepath =
                Server.MapPath(string.Format("/UpLoad/export/{0}.xls", DateTime.Now.ToString("导出数据 yyyy-MM-dd HHmmss")));
            var data = Proxy.Repository<Post>().GetAll(50, p => p.PostStatus == (int)PostStatusEnum.Publish);
            //TODO
            throw new Exception("TODO");
            //QueryFactory.Instance.Lab.ExportPosts(filepath,data.DataList);
            return new FilePathResult(filepath, "application/vnd.ms-excel");
        }

        public ActionResult Analytics()
        {
            //TODO
            throw new Exception("TODO");
            //QueryFactory.Instance.Lab.UpdateRefStatPicture();
            //return View(QueryFactory.Instance.Lab.GetRefstatInfo());
        }

        public ActionResult Probe()
        {
            return View();
        }
    }
}