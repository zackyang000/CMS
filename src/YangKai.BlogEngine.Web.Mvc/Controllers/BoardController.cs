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
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public class BoardController : EntityController<Board>
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 10, MaxExpansionDepth = 5)]
        public override IQueryable<Board> Get()
        {
            return base.Get();
        }

        protected override Board CreateEntity(Board entity)
        {
            Current.User = new WebUser()
            {
                UserName = entity.Author,
                Email = entity.Email,
            };
            return base.CreateEntity(entity);
        }

//        // 删除留言
//        public object Delete(Guid id)
//        {
//            var entity = Proxy.Repository<Board>().Get(id);
//            entity.IsDeleted = true;
//            Proxy.Repository<Board>().Update(entity);
//            return true;
//        }
//
//        // 恢复留言
//        public object Renew(Guid id)
//        {
//            var entity = Proxy.Repository<Board>().Get(id);
//            entity.IsDeleted = false;
//            Proxy.Repository<Board>().Update(entity);
//            return true;
//        }
    }
}