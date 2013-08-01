using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Infrastructure
{
    public class Repository<TEntity> : Repository<TEntity, Guid> where TEntity : class, IEntity
    {
        public Repository(BlogEngineContext context)
            : base(context)
        {
        }
    }
        public class ODataRepository<TEntity> :Repository<TEntity> where TEntity : class, IEntity
    {
            public ODataRepository(BlogEngineContext context)
            : base(context)
            {
                context.Configuration.ProxyCreationEnabled = false;
            }
    }
}