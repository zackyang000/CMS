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
using QrCode = YangKai.BlogEngine.Domain.QrCode;

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

            if (Proxy.Repository<Post>().Exist(p => p.Url == entity.Url))
            {
                throw new Exception("URL already exists.");
            }

            SaveRemoteImg(entity);

            entity.Group = Proxy.Repository<Group>().Get(entity.Group.GroupId);

            for (int i = 0; i < entity.Categorys.Count; i++)
            {
                entity.Categorys[i] = Proxy.Repository<Category>().Get(entity.Categorys[i].CategoryId);
            }

            entity.PubAdmin = Proxy.Repository<User>().Get(p => p.LoginName == Current.User.LoginName);
            entity.PubDate = DateTime.Now;

            SaveThumbnail(entity);

            entity.QrCode = new QrCode
            {
                QrCodeId = Guid.NewGuid(),
                Content = entity.Title,
                Url = entity.Url + ".png"
            };
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

            update = Proxy.Repository<Post>().Update(update);

            var entity = Proxy.Repository<Post>().Get(update.PostId);
            entity.Categorys.Clear();
            foreach (var item in update.Categorys.Select(p => p.CategoryId))
            {
                entity.Categorys.Add(Proxy.Repository<Category>().Get(item));
            }

            if (entity.Source != null)
            {
                Proxy.Repository<Source>().Remove(entity.Source);
            }
            entity.Source = update.Source;

            if (entity.Thumbnail != null)
            {
                Proxy.Repository<Thumbnail>().Remove(entity.Thumbnail);
            }
            entity.Thumbnail = update.Thumbnail;
            SaveThumbnail(entity);

            if (entity.QrCode != null)
            {
                Proxy.Repository<QrCode>().Remove(entity.QrCode);
            }
            entity.QrCode = new QrCode
            {
                QrCodeId = Guid.NewGuid(),
                Content = entity.Title,
                Url = entity.Url + ".png"
            };
            SaveQrCode(entity);

            if (entity.Tags != null)
            {
                Proxy.Repository<Tag>().Remove(entity.Tags);
            }
            entity.Tags = update.Tags;

            entity.EditAdmin = Proxy.Repository<User>().Get(p => p.LoginName == Current.User.LoginName);
            entity.EditDate = DateTime.Now;

            entity.Group = Proxy.Repository<Group>().Get(update.Group.GroupId);

            Proxy.Repository<Post>().Commit();

            Rss.Current.BuildPost();
            return update;
        }

        private Post PreHandle(Post entity)
        {
            entity.Title = entity.Title.Trim();
            entity.Url = entity.Url.Trim().ToLower().Replace(" ", "-");
            entity.Description = entity.Description ?? string.Empty;

            return entity;
        }

        private void SaveRemoteImg(Post entity)
        {
            entity.Content = SaveRemoteFile.SaveContentPic(entity.Content, entity.Url);
            entity.Description = SaveRemoteFile.SaveContentPic(entity.Description, entity.Url);
        }

        private static void SaveThumbnail(Post entity)
        {
            if (entity.Thumbnail != null)
            {
                var filename = entity.PubDate.ToString("yyyy.MM.dd.") + entity.Url +
                               Path.GetExtension(entity.Thumbnail.Url);
                var source = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/temp"),
                    entity.Thumbnail.Url);
                var target = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/thumbnail"), filename);
                if (File.Exists(source))
                {
                    if (File.Exists(target))
                    {
                        File.Delete(target);
                    }
                    File.Move(source, target);
                    ImageProcessing.CutForCustom(target, 160, 100, 100);
                    entity.Thumbnail.Url = filename;
                }
            }
        }

        private static void SaveQrCode(Post entity)
        {
            var gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
            var fullUrl = Config.URL.Domain + "/post/" + entity.Url;
            BitMatrix matrix = new QrEncoder().Encode(entity.QrCode.Content + " | " + fullUrl).Matrix;
            using (var stream = new FileStream(HttpContext.Current.Server.MapPath("/upload/qrcode/" + entity.QrCode.Url), FileMode.Create))
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