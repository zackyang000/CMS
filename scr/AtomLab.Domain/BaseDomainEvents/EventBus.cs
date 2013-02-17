using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomLab.Domain
{
    public class EventBus
    {
        private static readonly EventBus _current = new EventBus();

        public static EventBus Current
        {
            get
            {
                return _current;
            }
        }

        #region Public Methods

        public void Publish(IEvent evnt)
        {
            ExecuteHandlersFromMappingForEvent(evnt);
        }

        public void Publish(IEnumerable<IEvent> evnts)
        {
            foreach (var evnt in evnts)
            {
                Publish(evnt);
            }
        }

        #endregion

        #region Private Methods

        private void ExecuteHandlersFromMappingForEvent(IEvent targetEvent)
        {
            var eventHandlerMappings = EventHandlerMappingStore.Current.EventHandlerMappings;
            var targetEventType = targetEvent.GetType();

            var isExistRegHandler = eventHandlerMappings.ContainsKey(targetEventType);
            if (isExistRegHandler)
            {
                foreach (Type handlerType in eventHandlerMappings[targetEventType])
                {
                    var registerHandlerMethod = handlerType.GetMethods().Single
                        (
                            method => method.Name == "Handle" && method.GetParameters().Count() == 1 &&
                            method.GetParameters()[0].ParameterType == targetEventType
                        );
                    var eventHandler = GetEventHandler(targetEvent, handlerType);
                    if (eventHandler != null)
                    {
                        registerHandlerMethod.Invoke(eventHandler, new object[] { targetEvent });
                    }
                }
            }
     
                 
        }

        private object GetEventHandler(IEvent evnt, Type handlerType)
        {
            //Notes: If the handler is an entity, then we will get the entity from data persistence;
            //       else create a new instance of the handler.

            Type eventType = evnt.GetType();
            if (IsHandlerTypeImplementIEntityAndIEntityEventHandler(handlerType, eventType))
            {
                //Create a new QueryEntityEvent<,> event instance.
                var entityIdType =
                    handlerType.GetInterfaces().First(
                        interfaceType =>
                        interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof (IEntity<>)).
                        GetGenericArguments()[0];
                var queryEntityEventType = typeof (EntityQueryEvent<,>).MakeGenericType(handlerType, entityIdType);
                var entityIdPropertyInfo = queryEntityEventType.GetProperty("EntityId");
                var setReturnedEntityPropertyInfo = queryEntityEventType.GetProperty("SetReturnedEntity");
                var queryEntityEvent = Activator.CreateInstance(queryEntityEventType);

                //Get the EntityId of the entity.
                object entityId =
                    handlerType.GetMethods().Single(
                        method =>
                        method.Name == "GetEntityId" && method.GetParameters().ToList()[0].ParameterType == eventType).
                        Invoke(CreateEventHandler(handlerType), new object[] {evnt});

                //Create a new Action<T> delegate instance.
                var actionWrapperType = typeof (ActionWrapper<>);
                var genericActionWrapperType = actionWrapperType.MakeGenericType(handlerType);
                var actionWrapper = Activator.CreateInstance(genericActionWrapperType);
                var setEntityAction = genericActionWrapperType.GetProperty("SetEntity").GetValue(actionWrapper, null);

                entityIdPropertyInfo.SetValue(queryEntityEvent, entityId, null);
                setReturnedEntityPropertyInfo.SetValue(queryEntityEvent, setEntityAction, null);

                //Public the QueryEntityEvent<,> event to get the entity from data persistence.
                Publish(queryEntityEvent as IEvent);

                //Get the entity from ActionWrapper.
                var entity = genericActionWrapperType.GetMethod("GetEntity").Invoke(actionWrapper, null);
                if (entity == null)
                {
                    throw new ApplicationException(string.Format("Cannot find the entity of type '{0}' with ID '{1}'.",
                                                                 handlerType, entityId));
                }
                return entity;
            }
            else
            {
                try
                {
                    return InstanceLocator.Current.GetInstance(handlerType);
                }
                catch
                {
                }
                return CreateEventHandler(handlerType);
            }
        }

        private bool IsHandlerTypeImplementIEntityAndIEntityEventHandler(Type handlerType, Type eventType)
        {
            return handlerType.IsClass &&
                   (handlerType.GetInterfaces().Any(
                       interfaceType =>
                       interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof (IEntity<>)) &&
                    handlerType.GetInterfaces().Any(
                        interfaceType =>
                        interfaceType.IsGenericType &&
                        interfaceType.GetGenericTypeDefinition() == typeof (IEntityEventHandler<>) &&
                        interfaceType.GetGenericArguments().Count() == 1 &&
                        interfaceType.GetGenericArguments().ToList()[0] == eventType));
        }

        private object CreateEventHandler(Type handlerType)
        {
            var constructor = handlerType.GetConstructors()[0];
            var parameter = constructor.GetParameters()
                .Select(parameterInfo => GetValueForType(parameterInfo.ParameterType)).ToArray();
            return constructor.Invoke(parameter);
        }

        private object GetValueForType(Type targetType)
        {
            if (targetType.IsInterface)
            {
                return InstanceLocator.Current.GetInstance(targetType);
            }
            else
            {
                return DefaultValueForType(targetType);
            }
        }

        private object DefaultValueForType(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        #endregion

        private class ActionWrapper<TEntity> where TEntity : class
        {
            private TEntity entity = null;

            public ActionWrapper()
            {
                SetEntity = new Action<TEntity>(e => entity = e);
            }

            public Action<TEntity> SetEntity { get; set; }

            public TEntity GetEntity()
            {
                return entity;
            }
        }
    }
}