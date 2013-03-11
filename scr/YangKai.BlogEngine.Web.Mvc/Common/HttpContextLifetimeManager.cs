using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly HttpContextBase _context;

        public HttpContextLifetimeManager()
        {
            _context = new HttpContextWrapper(HttpContext.Current);
        }

        public HttpContextLifetimeManager(HttpContextBase context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }

        public void Dispose()
        {
            RemoveValue();
        }

        public override object GetValue()
        {
            return _context.Items["__entityframework_context__"];
        }

        public override void RemoveValue()
        {
            _context.Items.Remove("__entityframework_context__");
        }

        public override void SetValue(object newValue)
        {
            _context.Items["__entityframework_context__"] = newValue;
        }
    }

    public class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        private readonly HttpContextBase _context;

        public HttpContextLifetimeManager()
        {
            _context = new HttpContextWrapper(HttpContext.Current);
        }

        public HttpContextLifetimeManager(HttpContextBase context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }

        public void Dispose()
        {
            RemoveValue();
        }

        public override object GetValue()
        {
            return _context.Items[typeof (T)];
        }

        public override void RemoveValue()
        {
            _context.Items.Remove(typeof (T));
        }

        public override void SetValue(object newValue)
        {
            _context.Items[typeof (T)] = newValue;
        }
    }
}