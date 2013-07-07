using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain.Infrastructure;

namespace AtomLab.Domain
{
    public sealed class EventProcessor
    {
        /// <summary>
        /// 触发事件.
        /// </summary>
        /// <param name="evnt">事件</param>
        public static void ExecuteEvent(IEvent evnt)
        {
            try
            {
                EventBus.Current.Publish(evnt);
            }
            catch (Exception e)
            {
                if (e.InnerException is DomainException)
                {
                    throw e.InnerException;
                }
                throw;
            }
        }

        public static List<TEntity> QueryEntities<TEntity, TQueryEntitiesEvent>(
            Action<TQueryEntitiesEvent> setQueryConditionAction)
            where TQueryEntitiesEvent : EntitiesQueryEvent<TEntity>, new()
            //where TEntity : class
        {
            var evnt = new TQueryEntitiesEvent();
            List<TEntity> entities = null;

            setQueryConditionAction(evnt);

            evnt.SetReturnedEntities = returnedEntities => entities = returnedEntities.ToList();

            ExecuteEvent(evnt);

            return entities;
        }

        public static PageList<TEntity> QueryEntitiesByPage<TEntity, TQueryEntitiesEvent>(
       Action<TQueryEntitiesEvent> setQueryConditionAction)
       where TQueryEntitiesEvent : EntitiesPageQueryEvent<TEntity>, new()
        //where TEntity : class
        {
            var evnt = new TQueryEntitiesEvent();
            PageList<TEntity> entities = null;

            setQueryConditionAction(evnt);

            evnt.SetReturnedEntities = returnedEntities => entities = returnedEntities;

            ExecuteEvent(evnt);

            return entities;
        }

        public static int QueryCount<TQueryEntitiesEvent>(
    Action<TQueryEntitiesEvent> setQueryConditionAction)
    where TQueryEntitiesEvent : EntitiesCountQueryEvent, new()
        {
            var evnt = new TQueryEntitiesEvent();
            int entitiesCount = -1;

            setQueryConditionAction(evnt);

            evnt.SetReturnedEntities = returnedEntities => entitiesCount = returnedEntities;

            ExecuteEvent(evnt);

            return entitiesCount;
        }

        public static TEntity CreateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            ExecuteEvent(new EntityCreatingEvent<TEntity> { EntityCreating = entity });
            ExecuteEvent(new EntityCreatedEvent<TEntity> { EntityCreated = entity });
            return entity;
        }

        public static TEntity GetEntity<TEntity, TEntityId>(TEntityId id) where TEntity : class, IEntity<TEntityId>
        {
            TEntity entity = null;
            ExecuteEvent(new EntityQueryEvent<TEntity, TEntityId> { EntityId = id, SetReturnedEntity = e => entity = e });
            return entity;
        }

        public static bool DeleteEntity<TEntity, TEntityId>(TEntityId id) where TEntity : class, IEntity<TEntityId>
        {
            //Query the entity.
            TEntity entity = null;
            ExecuteEvent(new EntityQueryEvent<TEntity, TEntityId> { EntityId = id, SetReturnedEntity = e => entity = e });
            if (entity == null)
            {
                return false;
            }

            //Try to delete the entity.
            bool status = false;
            ExecuteEvent(new EntityDeleteEvent<TEntity> { EntityToDelete = entity, SetStatus = s => status = s });
            if (!status)
            {
                return false;
            }

            //Raise the entity deleted event.
            ExecuteEvent(new EntityDeletedEvent<TEntity> { DeletedEntity = entity });

            return true;
        }

        public static bool DeleteEntity<TEntity>(TEntity entity) where TEntity : class
        {
            //Try to delete the entity.
            bool status = false;
            ExecuteEvent(new EntityDeleteEvent<TEntity> { EntityToDelete = entity, SetStatus = s => status = s });
            if (!status)
            {
                return false;
            }

            //Raise the entity deleted event.
            ExecuteEvent(new EntityDeletedEvent<TEntity> { DeletedEntity = entity });

            return true;
        }
    }
}