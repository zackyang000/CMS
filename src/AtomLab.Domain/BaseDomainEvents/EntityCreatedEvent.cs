using System;

namespace AtomLab.Domain
{
    public class EntityCreatedEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity EntityCreated { get; set; }
    }
}
