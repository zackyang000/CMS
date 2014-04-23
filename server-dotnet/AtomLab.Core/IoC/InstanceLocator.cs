using System;

namespace AtomLab.Core
{
    public class InstanceLocator : IInstanceLocator
    {
        private static IInstanceLocator _currentLocator;

        private InstanceLocator()
        { }

        public static IInstanceLocator Current
        {
            get
            {
                return _currentLocator;
            }
        }

        public static void SetLocator(IInstanceLocator locator)
        {
            _currentLocator = locator;
        }

        public T GetInstance<T>() where T : class
        {
            return Current.GetInstance<T>();
        }

        public object GetInstance(Type instanceType)
        {
            return Current.GetInstance(instanceType);
        }

        public void RegisterType<T>()
        {
            _currentLocator.RegisterType<T>();
        }

        public void RegisterType(Type type)
        {
            _currentLocator.RegisterType(type);
        }

        public bool IsTypeRegistered<T>()
        {
            return _currentLocator.IsTypeRegistered<T>();
        }

        public bool IsTypeRegistered(Type type)
        {
            return _currentLocator.IsTypeRegistered(type);
        }
    }
}
