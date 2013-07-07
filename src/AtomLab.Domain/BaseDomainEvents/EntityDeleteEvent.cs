using System;

namespace AtomLab.Domain
{
    public class EntityDeleteEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity EntityToDelete { get; set; }
        public Action<bool> SetStatus { get; set; }
    }
}
