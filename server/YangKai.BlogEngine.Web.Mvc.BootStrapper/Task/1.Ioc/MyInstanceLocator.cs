using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Core;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    internal class MyInstanceLocator : IInstanceLocator
    {
        private readonly IUnityContainer _container;

        public MyInstanceLocator(IUnityContainer container)
        {
            _container = container;
        }

        #region IInstanceLocator Members

        public T GetInstance<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public object GetInstance(Type instanceType)
        {
            return _container.Resolve(instanceType);
        }

        public bool IsTypeRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }

        public bool IsTypeRegistered(Type type)
        {
            return _container.IsRegistered(type);
        }

        public void RegisterType<T>()
        {
            _container.RegisterType<T>();
        }

        public void RegisterType(Type type)
        {
            _container.RegisterType(type);
        }

        #endregion
    }
}