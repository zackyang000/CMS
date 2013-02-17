using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace YangKai.BlogEngine.Modules.PostModule.Services.CallHandlers
{
    public class PostRssCallHandler : ICallHandler
    {
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (getNext == null) throw new ArgumentNullException("getNext");

            var result = getNext()(input, getNext);

            Rss.BuildPostRss();

            return result;
        }
    }
}
