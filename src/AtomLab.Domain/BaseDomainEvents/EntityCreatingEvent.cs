using System;

namespace AtomLab.Domain
{
    public class EntityCreatingEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity EntityCreating { get; set; }
    }
}
