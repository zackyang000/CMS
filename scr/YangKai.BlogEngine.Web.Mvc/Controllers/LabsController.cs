using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class LabsController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }

        public FilePathResult ExportDemo()
        {
            var filepath = Server.MapPath(string.Format("~/excel/{0}.xls", DateTime.Now.ToString("导出数据 yyyy年MM月dd日HH时mm分ss秒")));
           // _labsServices.ExportDemo(filepath);
            return new FilePathResult(filepath, "application/vnd.ms-excel");
        }
    }
}