using System;
using System.Collections.Generic;
using AtomLab.Domain.Infrastructure;

namespace AtomLab.Domain
{
    public abstract class EntitiesQueryEvent<TEntity> : IEvent// where TEntity : class
    {
        public Action<IEnumerable<TEntity>> SetReturnedEntities { get; set; }
    }

    public abstract class EntitiesPageQueryEvent<TEntity> : IEvent// where TEntity : class
    {
        public Action<PageList<TEntity>> SetReturnedEntities { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public abstract class EntitiesCountQueryEvent : IEvent
    {
        public Action<int> SetReturnedEntities { get; set; }
    }
}
