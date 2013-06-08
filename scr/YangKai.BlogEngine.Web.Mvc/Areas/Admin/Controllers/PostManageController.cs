using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtomLab.Domain.Infrastructure;
using AtomLab.Utility;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Model;
using YangKai.BlogEngine.Modules.PostModule.Commands;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.ServiceProxy;
using YangKai.BlogEngine.Web.Mvc.Filters;


namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class PostManageController : Controller
    {
        [ActionName("index")]
        public ActionResult Index(int? page, string status)
        {
            PageList<Post> data = QueryFactory.Instance.Post.FindAll(page ?? 1, 50, null, null, null, null, null, null);
            var pagedList = new PagedList<Post>(data.DataList, page ?? 1, 50, data.TotalCount);
            return View(pagedList);
        }

        [ActionName("new-channel-and-group")]
        public PartialViewResult ChannelBox()
        {
            IList<Channel> entities = QueryFactory.Instance.Post.FindAllByNotDeletion();
            return PartialView(entities);
        }

        [ActionName("new")]
        public ActionResult New()
        {
            return View();
        }

        [ActionName("new")]
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public ActionResult New(FormCollection collection, HttpPostedFileBase uploadpic)
        {
            Post data = GetPost(collection, uploadpic);
            CommandFactory.Instance.Create(data);
            return PostAction(collection, data);
        }

        [ActionName("edit")]
        public ActionResult Edit(string id)
        {
            Post data = QueryFactory.Instance.Post.Find(id);
            return View("New", data);
        }

        [ActionName("edit")]
        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
        public ActionResult Edit(FormCollection collection, HttpPostedFileBase uploadpic, string id)
        {
            string postUrl = id; //id参数实际为Post.Url
            Post data = GetPost(collection, uploadpic);

            data.EditAdminId = data.PubAdminId;
            data.EditIp = data.PubIp;
            data.EditAddress = data.PubAddress;
            data.EditDate = data.CreateDate;

            var existThumbnail = Convert.ToBoolean(collection["exist-thumbnail"]);
            CommandFactory.Instance.UpdatePost(postUrl, data, existThumbnail);
            return PostAction(collection, data);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetChannel()
        {
            var entities = QueryFactory.Instance.Post.FindAllByNotDeletion()
                .Select(p => new { Value = p.Url, Text = p.Name });
            return Json(entities, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetGroup(string channelUrl)
        {
            var entities = QueryFactory.Instance.Post.GetGroupsByChannelUrl(channelUrl)
                .Select(p => new { Value = p.Url, Text = p.Name });
            return Json(entities, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetCategory(string groupUrl)
        {
            var entities = QueryFactory.Instance.Post.GetCategories(groupUrl)
                .Select(p => new { Value = p.CategoryId, Text = p.Name });
            return Json(entities, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetTag(string groupUrl)
        {
            var entities = QueryFactory.Instance.Post.GetTagsList(groupUrl).Data.Take(30)
                .Select(p => new { Value = p.Key, Text = p.Value });
            return Json(entities, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSourceTitle(string url)
        {
            try
            {
                return Json(new { result = true, title =AtomLab.Utility.UrlHelper.GetTitleFromUrl(url) },
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message },
                            JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddCategory(string name, string url, string groupUrl)
        {
            try
            {
                var g = QueryFactory.Instance.Post.GetGroup(groupUrl);
                var entity = new Category
                {
                    Name = name,
                    Url = url,
                    Group = g,
                    OrderId = 1,
                };
                CommandFactory.Instance.Create(entity);
                return Json(new { result = true },
                            JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message },
                            JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Guid id)
        {
            try
            {
                CommandFactory.Instance.Run(new PostDeleteEvent() { PostId = id });
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Renew(Guid id)
        {
            try
            {
                CommandFactory.Instance.Run(new PostRenewEvent() { PostId = id });
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                return Json(new { result = false, reason = e.Message });
            }
        }

        #region private method

        private ActionResult PostAction(FormCollection collection, Post data)
        {
            return Redirect(string.Format("/#!/post/{0}", data.Url));
        }

        private Post GetPost(FormCollection collection, HttpPostedFileBase uploadpic)
        {
            var data = new Post
                {
                    GroupId = QueryFactory.Instance.Post.GetGroup(collection["groupRadio"]).GroupId,
                    Url = collection["post_url"].ToLower(),
                    Title = collection["post_title"],
                    Content = collection["post_content"],
                    Description = collection["post_description"],
                    PubAdminId = QueryFactory.Instance.User.UserId(),
                    PubIp = Request.UserHostAddress,
                    PubAddress = IpLocator.GetIpLocation(Request.UserHostAddress),
                    CreateDate = DateTime.Now,
                    ReplyCount = 0,
                    ViewCount = 0,
                    PubDate = Convert.ToDateTime(string.Format("{0} {1}:{2}:00",
                                                               collection["pubdate"],
                                                               collection["hh"],
                                                               collection["mm"])),
                    PostStatus = (int) PostStatusEnum.Publish
                };

            //添加Post.Categorys
            if (!string.IsNullOrEmpty(collection["category"]))
             {
                 IList<string> categorysId = collection["category"].Split(',');
                 foreach (var item in categorysId)
                 {
                     var category = QueryFactory.Instance.Post.GetCategory(new Guid(item));
                     data.Categorys.Add(category);
                 }
             }

            //添加Post.Tags
            if (!string.IsNullOrEmpty(collection["tag"]))
            {
                IList<string> tagsName = collection["tag"].Split(',');
                foreach (var item in tagsName)
                {
                    var tag = new Tag { Name = item };
                    data.Tags.Add(tag);
                }
            }

            //添加Post.Source
            if (!string.IsNullOrEmpty(collection["SourceTitle"]))
            {
                data.Source = new Source
                {
                    Title = collection["SourceTitle"],
                    Url = collection["SourceURL"]
                };
            }

            //添加Post.Thumbnail
            var picUrl = SaveThumbnailPic(uploadpic,
                                          DateTime.Now.ToString("yyyy.MM.dd.") +
                                          data.Url.Trim().ToLower().Replace(" ", "-"));
            if (!string.IsNullOrEmpty(picUrl))
            {
                data.Thumbnail = new Thumbnail
                {
                    Title = data.Title,
                    Url = picUrl
                };
            }

            //添加Post.Qrcode

            var g = QueryFactory.Instance.Post.GetGroup(data.GroupId);
              data.QrCode = new Modules.PostModule.Objects.QrCode
            {
                Content = data.Title,
                Url = data.Url.Replace(" ", "-") + ".png"
            };

            var gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
            var fullUrl = Config.URL.Domain + "/" + g.Url + "/" + data.Url.Replace(" ", "-") + ".png";
            BitMatrix matrix = new QrEncoder().Encode(data.QrCode.Content + " | " + fullUrl).Matrix;
            using (var stream = new FileStream(Server.MapPath("/upload/qrcode/" + data.QrCode.Url.Replace(" ", "-")), FileMode.Create))
            {
                gRender.WriteToStream(matrix, ImageFormat.Png, stream, new Point(1000, 1000));
            }

          

            data.Content = SaveRemoteFile.SaveContentPic(data.Content, data.Url.Trim().ToLower().Replace(" ", "-"));

            return data;
        }

        private string SaveThumbnailPic(HttpPostedFileBase files, string filename)
        {
            if (files == null) return string.Empty;

            if (!ImageProcessing.IsWebImage(files.ContentType)) throw new Exception("图片格式错误!");

            string fileType = files.FileName.Substring(files.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1);

            IList<string> allowType = new List<string> {"gif", "GIF", "jpg", "JPG", "png", "PNG"};
            //删除原始上传文件
            allowType.ToList().ForEach
                (p => System.IO.File.Delete(Server.MapPath("/upload/thumbnail/" + filename + "." + p)));
            string picName = filename + "." + fileType;
            ImageProcessing.CutForCustom(files, Server.MapPath("/upload/thumbnail/" + picName), 160, 100, 100);

            return picName;
        }

        #endregion
    }
}