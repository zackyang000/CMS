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

            var sp = new MultipartMemoryStreamProvider();
            Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();

            var item = sp.Contents[0];
          
              var  filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
               var id = Guid.NewGuid();
                var path = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/temp"),
                    id + Path.GetExtension(filename));

                var ms = item.ReadAsStreamAsync().Result;
                using (var br = new BinaryReader(ms))
                {
                    var data = br.ReadBytes((int) ms.Length);
                    File.WriteAllBytes(path, data);
                }
          

            var resp = Request.CreateResponse(HttpStatusCode.OK, new {result = id + Path.GetExtension(filename)});
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return resp;
        }

        public HttpResponseMessage Photo(Guid id)
        {
            var sp = new MultipartMemoryStreamProvider();
            Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();

            var item = sp.Contents[0];

            var filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");

            var oName = Path.GetFileName(filename);
            var fileId = Guid.NewGuid();
            var name = fileId + Path.GetExtension(oName);
            var dir = HttpContext.Current.Server.MapPath("~/upload/gallery/" + id);
            var path = string.Format("{0}/photo/{1}", dir, name);

            var ms = item.ReadAsStreamAsync().Result;
            using (var br = new BinaryReader(ms))
            {
                var data = br.ReadBytes((int) ms.Length);
                File.WriteAllBytes(path, data);
            }

            var thumbnail = string.Format("{0}/thumbnail/{1}", dir, name);
            System.IO.File.Copy(path, thumbnail);
            ImageProcessing.CutForCustom(thumbnail, 100, 100, 100);

            var gallery = Proxy.Repository<Gallery>().Get(id);
            gallery.Photos.Add(new Photo()
            {
                PhotoId = fileId,
                Name = Path.GetFileNameWithoutExtension(oName),
                Path = string.Format("/upload/gallery/{0}/photo/{1}", id, name),
                Thumbnail = string.Format("/upload/gallery/{0}/thumbnail/{1}", id, name),
            });
            Proxy.Repository<Gallery>().Commit();


            var resp = Request.CreateResponse(HttpStatusCode.OK, new {result = id + Path.GetExtension(filename)});
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return resp;
        }
    }
}