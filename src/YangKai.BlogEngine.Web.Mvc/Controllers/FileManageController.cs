using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;

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

        public ActionResult Photo(Guid id, HttpPostedFileBase file)
        {
            var fileNo = Guid.NewGuid();
            var oName = Path.GetFileName(file.FileName);
            var name = fileNo + Path.GetExtension(oName);
            var dir = Server.MapPath("~/upload/gallery/" + id);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var path = string.Format("{0}/photo/{1}", dir, name);
            file.SaveAs(path);
            var thumbnail = string.Format("{0}/thumbnail/{1}", dir, name);
            System.IO.File.Copy(path, thumbnail);
            ImageProcessing.CutForCustom(thumbnail, 100, 100, 100);

            var gallery = Proxy.Repository<Gallery>().Get(id);
            gallery.Photos.Add(new Photo()
            {
                PhotoId = fileNo,
                Name = Path.GetFileNameWithoutExtension(oName),
                Path = string.Format("/upload/gallery/{0}/photo/{1}", id,name),
                Thumbnail = string.Format("/upload/gallery/{0}/thumbnail/{1}", id, name),
            });
            Proxy.Repository<Gallery>().Commit();
            return Json(fileNo, JsonRequestBehavior.AllowGet);
        }
    }
}