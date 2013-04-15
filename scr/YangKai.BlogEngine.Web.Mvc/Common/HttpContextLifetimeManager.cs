using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
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

    public class CallContextLifeTimeManager : LifetimeManager
    {
        private string _key = string.Format(CultureInfo.InvariantCulture, "CallContextLifeTimeManager_{0}", Guid.NewGuid());

        public override object GetValue()
        {
            return CallContext.GetData(_key);
        }

        public override void SetValue(object newValue)
        {
            CallContext.SetData(_key, newValue);
        }

        public override void RemoveValue()
        {
            CallContext.FreeNamedDataSlot(_key);
        }
    }
}