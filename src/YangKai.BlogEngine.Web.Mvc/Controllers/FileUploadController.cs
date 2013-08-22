using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class FileManageController : Controller
    {
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(filename))
            {
                var path = string.Format("{0}/{1}", Server.MapPath("~/upload/temp"), id + Path.GetExtension(filename));
                file.SaveAs(path);
            }
            return Content(id+ Path.GetExtension(filename));
        }


        public ActionResult Delete(string id)
        {
            try
            {
                var filename = id;
                var filePath = Path.Combine(Server.MapPath("~/upload"), filename);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false, reason = "This file is not existed." });
                }
            }
            catch (Exception err)
            {
                return Json(new { result = false, reason = err.Message });
            }
        }
    }
}