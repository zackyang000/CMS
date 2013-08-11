using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Filters;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : EntityController<Comment>
    {
        protected override Comment CreateEntity(Comment entity)
        {
            Current.User = new WebUser()
            {
                UserName = entity.Author,
                Email = entity.Email,
            };
            return base.CreateEntity(entity);
        }

//        // É¾³ýÆÀÂÛ
//        public object Delete(Guid id)
//        {
//            var entity = Proxy.Repository<Comment>().Get(id);
//            entity.IsDeleted = true;
//            Proxy.Repository<Comment>().Update(entity);
//            return true;
//        }
//
//        // »Ö¸´ÆÀÂÛ
//        public object Renew(Guid id)
//        {
//            var entity = Proxy.Repository<Comment>().Get(id);
//            entity.IsDeleted = false;
//            Proxy.Repository<Comment>().Update(entity);
//            return true;
//        }
    }
}