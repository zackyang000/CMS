using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;
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

namespace YangKai.BlogEngine.Web.Mvc
{
    public class EntityController<T> : EntitySetController<T, Guid>
        where T : Entity
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All,PageSize = 10,MaxExpansionDepth = 5)]
        public override IQueryable<T> Get()
        {
            return Proxy.Repository<T>().GetAll();
        }

        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 10, MaxExpansionDepth = 5)]
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

        protected override T UpdateEntity(Guid key, T update)
        {
            return Proxy.Repository<T>().Update(update);
        }
    }
}