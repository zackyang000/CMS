using System;
using System.Globalization;
using System.Web;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class HttpContextLifetimeManager : LifetimeManager, IDisposable
    {
        private string _key = string.Format(CultureInfo.InvariantCulture, "HttpContextLifetimeManager_{0}", Guid.NewGuid());

        private readonly HttpContextBase _context;

        public HttpContextLifetimeManager()
        {
            _context = new HttpContextWrapper(HttpContext.Current);
        }

        public void Dispose()
        {
            RemoveValue();
        }

        public override object GetValue()
        {
            return _context.Items[_key];
        }

        public override void RemoveValue()
        {
            _context.Items.Remove(_key);
        }

        public override void SetValue(object newValue)
        {
            _context.Items[_key] = newValue;
        }
    }
}