using System;
using System.Globalization;
using System.Web;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc
{
    /// <summary>
    /// 若使用多线程将造成HttpContext.Current为null,此时会直接创建新的value.
    /// 并且当该线程再次访问该Lifecycle时将再次重新创建.
    /// </summary>
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly string _key;

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
            if (HttpContext.Current == null) return null;
            var context = new HttpContextWrapper(HttpContext.Current);
            return context.Items[_key];
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current == null) return;
            new HttpContextWrapper(HttpContext.Current).Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current == null) return;
            new HttpContextWrapper(HttpContext.Current).Items[_key] = newValue;
        }
    }
}