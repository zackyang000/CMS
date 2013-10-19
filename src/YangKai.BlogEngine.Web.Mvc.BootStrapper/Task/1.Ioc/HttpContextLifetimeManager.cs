using System;
using System.Globalization;
using System.Web;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    /// <summary>
    /// 当使用多线程时,因为HttpContext.Current为null.
    /// 会直接使用singleton进行Life Cycle Manager.
    /// </summary>
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly string _key;
        private object _singletonContext;

        public HttpContextLifetimeManager()
        {
            _key = string.Format(CultureInfo.InvariantCulture, "HttpContextLifetimeManager_{0}", Guid.NewGuid());
        }

        public void Dispose()
        {
            RemoveValue();
        }

        public override object GetValue()
        {
            if (HttpContext.Current == null) return _singletonContext;
            var context = new HttpContextWrapper(HttpContext.Current);
            return context.Items[_key];
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current == null)
            {
                _singletonContext = null;
                return;
            }
            new HttpContextWrapper(HttpContext.Current).Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current == null)
            {
                _singletonContext = newValue;
                return;
            }
            new HttpContextWrapper(HttpContext.Current).Items[_key] = newValue;
        }
    }
}