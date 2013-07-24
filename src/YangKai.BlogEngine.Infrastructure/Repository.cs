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
}