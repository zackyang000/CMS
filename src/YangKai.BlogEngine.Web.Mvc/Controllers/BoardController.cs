using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;
using AtomLab.Core;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Extension;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class BoardController : ApiController
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Board> Get(ODataQueryOptions options)
        {
            var data = Proxy.Repository<Board>().GetAll();
            PageHelper.SetLinkHeader(data, options, Request);
            return data;
        }

        public Board Post(Board viewModel)
        {
            var entity = viewModel;
            entity.Ip = HttpContext.Current.Request.UserHostAddress;
            entity.Address = IpLocator.GetIpLocation(entity.Ip);

            Proxy.Repository<Board>().Add(entity);

            Current.User = new WebUser()
            {
                UserName = entity.Author,
                Email = entity.Email,
            };

            return entity;
        }

        [UserAuthorize]
        public object Put(Guid id, Board entity, string action)
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

        // 删除留言
        public object Delete(Guid id)
        {
            var entity = Proxy.Repository<Board>().Get(id);
            entity.IsDeleted = true;
            Proxy.Repository<Board>().Update(entity);
            return true;
        }

        // 恢复留言
        public object Renew(Guid id)
        {
            var entity = Proxy.Repository<Board>().Get(id);
            entity.IsDeleted = false;
            Proxy.Repository<Board>().Update(entity);
            return true;
        }
    }
}