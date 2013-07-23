using System;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;

namespace YangKai.BlogEngine.Infrastructure
{
    public class Repository<TEntity> : Repository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
        public Repository(BlogEngineContext context)
            : base(context)
        {
        }
    }
}