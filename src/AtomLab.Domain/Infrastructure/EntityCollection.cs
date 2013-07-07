using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AtomLab.Domain;

namespace AtomLab.Domain.Infrastructure
{
    public abstract class EntityCollection<TEntity, TEntityId> :
        IEventHandler<EntityQueryEvent<TEntity, TEntityId>>,
        IEventHandler<EntityCreatedEvent<TEntity>>,
        IEventHandler<EntityDeleteEvent<TEntity>>
        where TEntity : class, IEntity<TEntityId>
    {
        protected readonly DbContext _context;

        protected EntityCollection(IUnitOfWork context)
        {
            _context = context as DbContext;
        }

        #region private members

        protected  TEntity Get(TEntityId id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        protected IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        protected void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        protected void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        #endregion

        #region IEventHandler<EntityQueryEvent<TEntity,TEntityId>> Members

        public void Handle(EntityQueryEvent<TEntity, TEntityId> evnt)
        {
            evnt.SetReturnedEntity(Get(evnt.EntityId));
        }

        #endregion

        #region IEventHandler<EntityCreatedEvent<TEntity>> Members

        public void Handle(EntityCreatedEvent<TEntity> evnt)
        {
            Add(evnt.EntityCreated);
        }

        #endregion

        #region IEventHandler<EntityDeleteEvent<TEntity>> Members

        public void Handle(EntityDeleteEvent<TEntity> evnt)
        {
            Remove(evnt.EntityToDelete);
            evnt.SetStatus(true);
        }

        #endregion
    }
}