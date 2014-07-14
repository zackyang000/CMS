using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Linq;
using AtomLab.Core;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Filters;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class EntityController<T> : EntitySetController<T, Guid>
        where T : Entity
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All,PageSize = 500,MaxExpansionDepth = 5)]
        public override IQueryable<T> Get()
        {
            return Proxy.Repository<T>().GetAll(p=>!p.IsDeleted);
        }

        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 5)]
        protected override T GetEntityByKey(Guid key)
        {
            return Proxy.Repository<T>().Get(key);
        }

        protected override Guid GetKey(T entity)
        {
            return new Guid();
        }

        protected override T CreateEntity(T entity)
        {
            return Proxy.Repository<T>().Add(entity);
        }

        [UserAuthorize]
        protected override T UpdateEntity(Guid key, T update)
        {
            var isEdit = Proxy.Repository<T>().Exist(update);
            if (isEdit)
            {
                return Proxy.Repository<T>().Update(update);
            }
            else
            {
                return CreateEntity(update);
            }
        }

        [HttpPost]
        [UserAuthorize]
        public virtual void Remove([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            var entity = Proxy.Repository<T>().Get(key);
            entity.IsDeleted = true;
            UpdateEntity(key, entity);
        }

        [HttpPost]
        [UserAuthorize]
        public virtual void Recover([FromODataUri] Guid key, ODataActionParameters parameters)
        {
            var entity = Proxy.Repository<T>().Get(key);
            entity.IsDeleted = false;
            UpdateEntity(key, entity);
        }
    }
}