//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using AtomLab.Core;
//using AtomLab.Utility;
//using Gma.QrCodeNet.Encoding;
//using Gma.QrCodeNet.Encoding.Windows.Render;
//using Webdiyer.WebControls.Mvc;
//using YangKai.BlogEngine.Common;
//using YangKai.BlogEngine.Domain;
//using YangKai.BlogEngine.ProxyService;
//using YangKai.BlogEngine.Web.Mvc.Filters;
//using YangKai.BlogEngine.Web.Mvc.Areas.Admin.Common;
//using QrCode = YangKai.BlogEngine.Domain.QrCode;
//
//
//namespace YangKai.BlogEngine.Web.Mvc.Areas.Admin.Controllers
//{
//    [UserAuthorizeForMVC]
//    public class PostManageController : Controller
//    {
//        [ActionName("index")]
//        public ActionResult Index(int? page, string status)
//        {
//            PageList<Post> data = RepositoryProxy.Post.GetPage(page ?? 1, 50, new OrderByExpression<Post, DateTime>(p => p.CreateDate, OrderMode.DESC));
//            var pagedList = new PagedList<Post>(data.DataList, page ?? 1, 50, data.TotalCount);
//            return View(pagedList);
//        }
//
//        [ActionName("new-channel-and-group")]
//        public PartialViewResult ChannelBox()
//        {
//            IList<Channel> entities = RepositoryProxy.Channel.GetAll(p => !p.IsDeleted).ToList();
//            return PartialView(entities);
//        }
//
//        [ActionName("new")]
//        public ActionResult New()
//        {
//            return View();
//        }
//
//        [ActionName("new")]
//        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
//        public ActionResult New(FormCollection collection, HttpPostedFileBase uploadpic)
//        {
//            Post data = GetPost(collection, uploadpic);
//            RepositoryProxy.Post.Add(data);
//            return PostAction(collection, data);
//        }
//
//        [ActionName("edit")]
//        public ActionResult Edit(string id)
//        {
//            Post data = RepositoryProxy.Post.Get(p => p.Url == id);
//            return View("New", data);
//        }
//
//        [ActionName("edit")]
//        [AcceptVerbs(HttpVerbs.Post), ValidateInput(false)]
//        public ActionResult Edit(FormCollection collection, HttpPostedFileBase uploadpic, string id)
//        {
//            string postUrl = id; //id参数实际为Post.Url
//            Post data = GetPost(collection, uploadpic);
//
//            data.LastEditUser = WebMasterCookie.Load().Name;
//            data.EditIp = data.PubIp;
//            data.EditAddress = data.PubAddress;
//            data.EditDate = data.CreateDate;
//
//            var existThumbnail = Convert.ToBoolean(collection["exist-thumbnail"]);
//            throw new Exception("编辑文章");
//            //Command.Instance.UpdatePost(postUrl, data, existThumbnail);
//            return PostAction(collection, data);
//        }
//
//        [AcceptVerbs(HttpVerbs.Get)]
//        public JsonResult GetChannel()
//        {
//            var entities = RepositoryProxy.Channel.GetAll(p => !p.IsDeleted)
//                .Select(p => new { Value = p.Url, Text = p.Name });
//            return Json(entities, JsonRequestBehavior.AllowGet);
//        }
//
//        [AcceptVerbs(HttpVerbs.Get)]
//        public JsonResult GetGroup(string channelUrl)
//        {
//            var entities = RepositoryProxy.Group.GetAll(p => p.Channel.Url == channelUrl && !p.IsDeleted).ToList()
//                .Select(p => new { Value = p.Url, Text = p.Name });
//            return Json(entities, JsonRequestBehavior.AllowGet);
//        }
//
//        [AcceptVerbs(HttpVerbs.Get)]
//        public JsonResult GetCategory(string groupUrl)
//        {
//            var entities = RepositoryProxy.Category.GetAll(p => p.Group.Url == groupUrl && !p.IsDeleted)
//                .Select(p => new { Value = p.CategoryId, Text = p.Name });
//            return Json(entities, JsonRequestBehavior.AllowGet);
//        }
//
//        [AcceptVerbs(HttpVerbs.Get)]
//        public JsonResult GetSourceTitle(string url)
//        {
//            try
//            {
//                return Json(new { result = true, title =AtomLab.Utility.UrlHelper.GetTitleFromUrl(url) },
//                            JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception e)
//            {
//                return Json(new { result = false, reason = e.Message },
//                            JsonRequestBehavior.AllowGet);
//            }
//        }
//
//        [AcceptVerbs(HttpVerbs.Post)]
//        public JsonResult AddCategory(string name, string url, string groupUrl)
//        {
//            try
//            {
//                var g = RepositoryProxy.Group.Get(p => p.Url == groupUrl);
//                var entity = new Category
//                {
//                    Name = name,
//                    Url = url,
//                    Group = g,
//                    OrderId = 1,
//                };
//                RepositoryProxy.Category.Add(entity);
//                return Json(new { result = true },
//                            JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception e)
//            {
//                return Json(new { result = false, reason = e.Message },
//                            JsonRequestBehavior.AllowGet);
//            }
//        }
//
//        [AcceptVerbs(HttpVerbs.Post)]
//        public JsonResult Delete(Guid id)
//        {
//            try
//            {
//                var entity = RepositoryProxy.Post.Get(id);
//                entity.IsDeleted = true;
//                RepositoryProxy.Post.Update(entity);
//                return Json(new { result = true });
//            }
//            catch (Exception e)
//            {
//                return Json(new { result = false, reason = e.Message });
//            }
//        }
//
//        [AcceptVerbs(HttpVerbs.Post)]
//        public JsonResult Renew(Guid id)
//        {
//            try
//            {
//                var entity = RepositoryProxy.Post.Get(id);
//                entity.IsDeleted = false;
//                RepositoryProxy.Post.Update(entity);
//                return Json(new { result = true });
//            }
//            catch (Exception e)
//            {
//                return Json(new { result = false, reason = e.Message });
//            }
//        }
//
//        #region private method
//
//        private ActionResult PostAction(FormCollection collection, Post data)
//        {
//            return Redirect(string.Format("/#!/post/{0}", data.Url));
//        }
//
//        private Post GetPost(FormCollection collection, HttpPostedFileBase uploadpic)
//        {
//            var groupname = collection["groupRadio"];
//            var data = new Post
//                {
//                    GroupId = RepositoryProxy.Group.Get(p => p.Url == groupname).GroupId,
//                    Url = collection["post_url"].ToLower(),
//                    Title = collection["post_title"],
//                    Content = collection["post_content"],
//                    Description = collection["post_description"],
//                    CreateUser = WebMasterCookie.Load().Name,
//                    PubIp = Request.UserHostAddress,
//                    PubAddress = IpLocator.GetIpLocation(Request.UserHostAddress),
//                    CreateDate = DateTime.Now,
//                    ReplyCount = 0,
//                    ViewCount = 0,
//                    PubDate = Convert.ToDateTime(string.Format("{0} {1}:{2}:00",
//                                                               collection["pubdate"],
//                                                               collection["hh"],
//                                                               collection["mm"])),
//                    PostStatus = (int) PostStatusEnum.Publish
//                };
//
//            //添加Post.Categorys
//            if (!string.IsNullOrEmpty(collection["category"]))
//             {
//                 IList<string> categorysId = collection["category"].Split(',');
//                 foreach (var item in categorysId)
//                 {
//                     var category = RepositoryProxy.Category.Get(new Guid(item));
//                     data.Categorys.Add(category);
//                 }
//             }
//
//            //添加Post.Tags
//            if (!string.IsNullOrEmpty(collection["tag"]))
//            {
//                IList<string> tagsName = collection["tag"].Split(',');
//                foreach (var item in tagsName)
//                {
//                    var tag = new Tag { Name = item };
//                    data.Tags.Add(tag);
//                }
//            }
//
//            //添加Post.Source
//            if (!string.IsNullOrEmpty(collection["SourceTitle"]))
//            {
//                data.Source = new Source
//                {
//                    Title = collection["SourceTitle"],
//                    Url = collection["SourceURL"]
//                };
//            }
//
//            //添加Post.Thumbnail
//            var picUrl = SaveThumbnailPic(uploadpic,
//                                          DateTime.Now.ToString("yyyy.MM.dd.") +
//                                          data.Url.Trim().ToLower().Replace(" ", "-"));
//            if (!string.IsNullOrEmpty(picUrl))
//            {
//                data.Thumbnail = new Thumbnail
//                {
//                    Title = data.Title,
//                    Url = picUrl
//                };
//            }
//
//            //添加Post.Qrcode
//            var g = RepositoryProxy.Group.Get(data.GroupId);
//              data.QrCode = new QrCode
//            {
//                Content = data.Title,
//                Url = data.Url.Replace(" ", "-") + ".png"
//            };
//
//            var gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
//            var fullUrl = Config.URL.Domain + "/#!/post/" + data.Url.Replace(" ", "-");
//            BitMatrix matrix = new QrEncoder().Encode(data.QrCode.Content + " | " + fullUrl).Matrix;
//            using (var stream = new FileStream(Server.MapPath("/upload/qrcode/" + data.QrCode.Url.Replace(" ", "-")), FileMode.Create))
//            {
//                gRender.WriteToStream(matrix, ImageFormat.Png, stream, new Point(1000, 1000));
//            }
//
//          
//
//            data.Content = SaveRemoteFile.SaveContentPic(data.Content, data.Url.Trim().ToLower().Replace(" ", "-"));
//
//            return data;
//        }
//
//        private string SaveThumbnailPic(HttpPostedFileBase files, string filename)
//        {
//            if (files == null) return string.Empty;
//
//            if (!ImageProcessing.IsWebImage(files.ContentType)) throw new Exception("图片格式错误!");
//
//            string fileType = files.FileName.Substring(files.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
//
//            IList<string> allowType = new List<string> {"gif", "GIF", "jpg", "JPG", "png", "PNG"};
//            //删除原始上传文件
//            allowType.ToList().ForEach
//                (p => System.IO.File.Delete(Server.MapPath("/upload/thumbnail/" + filename + "." + p)));
//            string picName = filename + "." + fileType;
//            ImageProcessing.CutForCustom(files, Server.MapPath("/upload/thumbnail/" + picName), 160, 100, 100);
//
//            return picName;
//        }
//
//        #endregion
//    }
//}