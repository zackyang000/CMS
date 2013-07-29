using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.Http.OData.Query;
using System.Web.Mvc;
using System.Linq;
using AtomLab.Core;
using Webdiyer.WebControls.Mvc;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Service;
using YangKai.BlogEngine.Web.Mvc.Extension;

namespace YangKai.BlogEngine.Web.Mvc.Controllers
{
    public abstract class RestApiController<T> : ApiController where T :Entity
    {
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public virtual IQueryable<T> Get(ODataQueryOptions options)
        {
            var data = Proxy.Repository<T>().GetAll(p => !p.IsDeleted);
            PageHelper.SetLinkHeader(data, options, Request);
            return data;
        }

        public virtual T Get(Guid id)
        {
            return Proxy.Repository<T>().Get(id);
        }

        public virtual T Post(T entity)
        {
          return Proxy.Repository<T>().Add(entity);
        }

        public virtual T Put(T entity)
        {
            throw new NotImplementedException();
            //return Proxy.Repository<T>().Update(entity);
        }
    }
}