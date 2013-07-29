using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Web.Mvc.Filters;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : ApiController
    {
        public IList<Comment> Get(Guid PostId)
        {
            var comments = Proxy.Repository.Comment.GetAll(p => p.PostId == PostId);
            if (!WebMasterCookie.IsLogin)
            {
                comments = comments.Where(p => !p.IsDeleted);
            }
            return comments.OrderBy(p=>p.CreateDate).ToList();
        }

        [UserAuthorize]
        public object Post(Guid id,Comment entity, string action)
        {
            switch (action)
            {
                case "delete":
                    return Delete(id);
                case "renew":
                    return Renew(id);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public Comment Put(Comment viewModel)
        {
            var entity = viewModel;
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);
            entity.IsAdmin = WebMasterCookie.IsLogin;

            Proxy.Repository.Comment.Add(entity);

            WebGuestCookie.Save(entity.Author, entity.Email, entity.Url, true);

            return entity;
        }

        // É¾³ýÆÀÂÛ
        public object Delete(Guid id)
        {
            var entity=Proxy.Repository.Comment.Get(id);
            entity.IsDeleted = true;
            Proxy.Repository.Comment.Update(entity);
            return true;
        }

        // »Ö¸´ÆÀÂÛ
        public object Renew(Guid id)
        {
            var entity = Proxy.Repository.Comment.Get(id);
            entity.IsDeleted = false;
            Proxy.Repository.Comment.Update(entity);
            return true;
        }
    }
}