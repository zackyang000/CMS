using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using System.Linq;
using AtomLab.Core;
using Microsoft.Data.Edm;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.BootStrapper;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class ArticleController : EntityController<Post>
    {
        protected override Post CreateEntity(Post entity)
        {
            entity.Group = Proxy.Repository<Group>().Get(entity.Group.GroupId);

            for (int i = 0; i < entity.Categorys.Count; i++)
            {
                entity.Categorys[i] = Proxy.Repository<Category>().Get(entity.Categorys[i].CategoryId); 
            }

            entity.PubAdmin = Proxy.Repository<User>().Get(p => p.UserName == Current.User.UserName);
            entity.PubDate = DateTime.Now;

            if (entity.Thumbnail != null)
            {
                string sourcePath = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/temp"), entity.Thumbnail.Url);
                string targetPath = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath("~/upload/thumbnail"), entity.Thumbnail.Url);
                File.Move(sourcePath, targetPath);
            }

            return Proxy.Repository<Post>().Add(entity);
        }
    }
}