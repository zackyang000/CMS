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
using YangKai.BlogEngine.Web.Mvc.Extension;
using YangKai.BlogEngine.Web.Mvc.Filters;


namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class CommentController : ApiController
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Comment> Get(ODataQueryOptions options)
        {
            var data = Proxy.Repository<Comment>().GetAll(p => !p.IsDeleted);
            PageHelper.SetLinkHeader(data, options, Request);
            return data;
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
            entity.IsAdmin = Current.IsLogin;

            Proxy.Repository<Comment>().Add(entity);

            Current.User = new WebUser()
                {
                    UserName = entity.Author,
                    Email = entity.Email,
                    Avatar = entity.Url,
                };

            return entity;
        }

        // É¾³ýÆÀÂÛ
        public object Delete(Guid id)
        {
            var entity = Proxy.Repository<Comment>().Get(id);
            entity.IsDeleted = true;
            Proxy.Repository<Comment>().Update(entity);
            return true;
        }

        // »Ö¸´ÆÀÂÛ
        public object Renew(Guid id)
        {
            var entity = Proxy.Repository<Comment>().Get(id);
            entity.IsDeleted = false;
            Proxy.Repository<Comment>().Update(entity);
            return true;
        }
    }
}