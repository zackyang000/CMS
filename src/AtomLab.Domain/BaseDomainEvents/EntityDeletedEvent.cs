using System;

namespace AtomLab.Domain
{
    public class EntityDeletedEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity DeletedEntity { get; set; }
    }
}
