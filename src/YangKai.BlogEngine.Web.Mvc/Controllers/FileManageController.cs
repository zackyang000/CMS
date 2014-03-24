using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class FileManageController : ApiController
    {
        public HttpResponseMessage Upload()
        {
            var id = new Guid();
            var filename = "";

            var sp = new MultipartMemoryStreamProvider();
            Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();

            foreach (var item in sp.Contents)
            {
                if (item.Headers.ContentDisposition.FileName != null)
                {
                    filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                    id = Guid.NewGuid();
                    var path = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/temp"),
                        id + Path.GetExtension(filename));

                    var ms = item.ReadAsStreamAsync().Result;
                    using (var br = new BinaryReader(ms))
                    {
                        var data = br.ReadBytes((int) ms.Length);
                        File.WriteAllBytes(path, data);
                    }
                }
            }

            var resp = Request.CreateResponse(HttpStatusCode.OK, new {result=id + Path.GetExtension(filename)});
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return resp;
        }

        public object Delete(string id)
        {
            try
            {
                var filename = id;
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/upload"), filename);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return new { result = true };
                }
                else
                {
                    return new { result = false, reason = "This file is not existed." };
                }
            }
            catch (Exception err)
            {
                return new { result = false, reason = err.Message };
            }
        }

        public object Photo(Guid id, HttpPostedFileBase file)
        {
            var fileNo = Guid.NewGuid();
            var oName = Path.GetFileName(file.FileName);
            var name = fileNo + Path.GetExtension(oName);
            var dir = HttpContext.Current.Server.MapPath("~/upload/gallery/" + id);
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
            return fileNo;
        }
    }
}