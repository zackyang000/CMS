using System;

namespace AtomLab.Domain
{
    public class EntityQueryEvent<TEntity, TEntityId>
        : IEvent where TEntity : class, IEntity<TEntityId>
    {
        public TEntityId EntityId { get; set; }
        public Action<TEntity> SetReturnedEntity { get; set; }
    }
}
