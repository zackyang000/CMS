using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using Microsoft.Practices.Unity;

namespace YangKai.BlogEngine.Web.Mvc
{
    public class CallContextLifeTimeManager : LifetimeManager, IDisposable
    {
        private string _key = string.Format(CultureInfo.InvariantCulture, "CallContextLifeTimeManager_{0}", Guid.NewGuid());

        public void Dispose()
        {
            RemoveValue();
        }

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