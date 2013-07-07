using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AtomLab.Domain
{
    public class Entity<TEntityId> : IEntity<TEntityId>
    {
        #region Constructors

        public Entity(TEntityId id)
        {
            this.Id = id;
        }

        #endregion

        #region IEntity<TEntityId> Members

        [IgnoreDataMember]
        public TEntityId Id { get; private set; }

        #endregion

        #region Protected Methods

        protected void RaiseEvent(IEvent evnt)
        {
            EventProcessor.ExecuteEvent(evnt);
        }

        protected TEntity GetEntity<TEntity, TEntityIdType>(TEntityIdType id)
            where TEntity : class, IEntity<TEntityIdType>
        {
            return EventProcessor.GetEntity<TEntity, TEntityIdType>(id);
        }

        protected List<TEntity> GetEntities<TEntity, TQueryEntitiesEvent>(
            Action<TQueryEntitiesEvent> setQueryConditionAction)
            where TEntity : class
            where TQueryEntitiesEvent : EntitiesQueryEvent<TEntity>, new()
        {
            return EventProcessor.QueryEntities<TEntity, TQueryEntitiesEvent>(setQueryConditionAction);
        }

        protected bool DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            return EventProcessor.DeleteEntity(entity);
        }

        protected bool DeleteEntity<TEntity, TEntityIdType>(TEntityIdType id)
            where TEntity : class, IEntity<TEntityIdType>
        {
            return EventProcessor.DeleteEntity<TEntity, TEntityIdType>(id);
        }

        #endregion
    }
}
