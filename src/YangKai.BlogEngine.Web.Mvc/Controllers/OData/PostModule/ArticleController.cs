using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AtomLab.Utility;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Areas.Admin.Common;

namespace YangKai.BlogEngine.Web.Mvc.Controllers.OData
{
    public class ArticleController : EntityController<Post>
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 10, MaxExpansionDepth = 5)]
        public override IQueryable<Post> Get()
        {
            return Proxy.Repository<Post>().GetAll();
        }

        protected override Post CreateEntity(Post entity)
        {
            entity = PreHandle(entity);

            if (Proxy.Repository<Post>().Exist(p => p.Url == entity.Url && p.PostStatus==(int)PostStatusEnum.Publish))
            {
                throw new Exception("URL already exists.");
            }


            //如果没有Group,则自动保存为草稿(用于ChromeExtension)
            if (entity.Group != null)
            {
                entity.Group = Proxy.Repository<Group>().Get(entity.Group.GroupId);
            }
            else
            {
                entity.PostStatus = (int) PostStatusEnum.Draft;
            }

            if (entity.PostId == default(Guid))
            {
                entity.PostId=Guid.NewGuid();
            }

            entity.PubDate = DateTime.Now;

            SaveRemoteImg(entity);
            SaveThumbnail(entity);
            SaveQrCode(entity);

            entity = Proxy.Repository<Post>().Add(entity);

            Rss.Current.BuildPost();

            return entity;
        }

        protected override Post UpdateEntity(Guid key, Post update)
        {
            update = PreHandle(update);

            if (Proxy.Repository<Post>().Exist(p => p.Url == update.Url && p.PostId!=update.PostId))
            {
                throw new Exception("URL already exists.");
            }

            SaveRemoteImg(update);
            SaveThumbnail(update);
            SaveQrCode(update);

            update = Proxy.Repository<Post>().Update(update);

            var entity = Proxy.Repository<Post>().Get(update.PostId);
            if (entity.Tags != null)
            {
                Proxy.Repository<Tag>().Remove(entity.Tags);
            }
            entity.Tags = update.Tags;

            entity.Group = Proxy.Repository<Group>().Get(update.Group.GroupId);

            entity.PostStatus = (int)PostStatusEnum.Publish;

            Proxy.Repository<Post>().Commit();

            Rss.Current.BuildPost();
            return update;
        }

        private Post PreHandle(Post entity)
        {
            entity.Title = entity.Title.Trim();
            entity.Url = entity.Url != null ? entity.Url.Trim().ToLower().Replace(" ", "-") : string.Empty;
            entity.Description = entity.Description ?? string.Empty;

            return entity;
        }

        private void SaveRemoteImg(Post entity)
        {
            entity.Content = SaveRemoteFile.SaveContentPic(entity.Content, entity.PostId.ToString());
            entity.Description = SaveRemoteFile.SaveContentPic(entity.Description, entity.PostId.ToString());
        }

        private static void SaveThumbnail(Post entity)
        {
            if (entity.Thumbnail != null)
            {
                var sourceName = entity.Thumbnail;
                entity.Thumbnail = Config.Path.THUMBNAIL_FOLDER + entity.PubDate.ToString("yyyy.MM.dd.") + entity.Url +
                               Path.GetExtension(entity.Thumbnail);

                var source = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/temp"), sourceName);
                var target = string.Format("{0}{1}", HttpContext.Current.Server.MapPath("~"), entity.Thumbnail);
               
                if (File.Exists(source))
                {
                    if (File.Exists(target))
                    {
                        File.Delete(target);
                    }
                    File.Move(source, target);
                    ImageProcessing.CutForCustom(target, 160, 100, 100);
                }
            }
        }

        private static void SaveQrCode(Post entity)
        {
            var gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
            var url = Config.URL.Domain + "/post/" + entity.Url;
            var matrix = new QrEncoder().Encode(entity.Title + " | " + url).Matrix;
            using (var stream = new FileStream(HttpContext.Current.Server.MapPath(Config.Path.QRCODE_FOLDER + entity.Url+".png"), FileMode.Create))
            {
                gRender.WriteToStream(matrix, ImageFormat.Png, stream, new Point(1000, 1000));
            }
        }

        [HttpPost]
        public void Browsed([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            var entity = Proxy.Repository<Post>().Get(key);
            entity.ViewCount++;
            Proxy.Repository<Post>().Update(entity);
        }

        [HttpPost]
        public void Commented([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            var entity = Proxy.Repository<Post>().Get(key);
            entity.ReplyCount = entity.Comments.Count(p=>!p.IsDeleted);
            Proxy.Repository<Post>().Update(entity);
        }
    }
}