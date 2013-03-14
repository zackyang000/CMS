using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class LabsController : Controller
    {
        public FilePathResult Export()
        {
            var filepath =
                Server.MapPath(string.Format("~/excel/{0}.xls", DateTime.Now.ToString("导出数据 yyyy-MM-dd HHmmss")));
            //_labsServices.ExportDemo(filepath);
            return new FilePathResult(filepath, "application/vnd.ms-excel");
        }
    }
}