using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AtomLab.Domain.Infrastructure
{
    public class GuidRepository<TEntity> : Repository<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        protected GuidRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}