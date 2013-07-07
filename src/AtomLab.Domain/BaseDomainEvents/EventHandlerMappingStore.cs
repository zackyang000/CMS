using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AtomLab.Domain
{
    public class EventHandlerMappingStore
    {
        private static readonly EventHandlerMappingStore _current = new EventHandlerMappingStore();
        private readonly Dictionary<Type, List<Type>> _eventHandlerMappings = new Dictionary<Type, List<Type>>();

        private EventHandlerMappingStore()
        {
            
        }

		public static EventHandlerMappingStore Current
		{
			get
			{
                return _current;
			}
		}

        public Dictionary<Type, List<Type>> EventHandlerMappings
        {
            get
            {
                return _eventHandlerMappings;
            }
        }

        public void RegisterAllHandlerTypesFromAssembly(Assembly assembly)
        {
            foreach (Type t in
                from type in assembly.GetTypes()
                where type.IsClass && !type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any()
                select type)
            {
                IEnumerable<MethodInfo> enumerable = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(isEventHandler);
                foreach (MethodInfo methodInfo in enumerable)
                {
                    Type parameterType = methodInfo.GetParameters()[0].ParameterType;
                    var isExistMapping = this.EventHandlerMappings.ContainsKey(parameterType);
                    if (!isExistMapping)
                    {
                        var list = new List<Type>();
                        EventHandlerMappings.Add(parameterType, list);
                    }
                    addSubscriberType(EventHandlerMappings[parameterType], t);
                }
            }
        }
        private bool isEventHandler(MethodInfo methodInfo)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();
            return parameters.Count() == 1 && typeof(IEvent).IsAssignableFrom(parameters[0].ParameterType);
        }

        private void addSubscriberType(List<Type> existingSubscriberTypes, Type subscriberType)
        {
            if (!existingSubscriberTypes.Exists(existingSubscriberType => existingSubscriberType == subscriberType))
            {
                var list = existingSubscriberTypes.FindAll(subscriberType.IsAssignableFrom);
                list.ForEach(subSubscriberType => existingSubscriberTypes.Remove(subSubscriberType));
                if (
                    !existingSubscriberTypes.Exists(
                        existingSubscriberType => existingSubscriberType.IsAssignableFrom(subscriberType)))
                {
                    existingSubscriberTypes.Add(subscriberType);
                }
            }
        }


        #region Private Methods

        private bool ImplementsAtLeastOneIEventHandlerInterface(Type type)
        {
            return type.IsClass &&
                   type.GetInterfaces().Any(IsIEventHandlerInterface);
        }
        private bool IsIEventHandlerInterface(Type type)
        {
            return type.IsInterface &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IEventHandler<>);
        }

        #endregion
    }
}
