using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;
using YangKai.BlogEngine.Modules.PostModule.Services;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    internal class RssBuildBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            IMethodReturn msg = getNext()(input, getNext);

            if (input.MethodBase.Name == "CreatePost")
            {
                Rss.BuildPostRss();
            }

            if (input.MethodBase.Name == "CreateComment")
            {
                Rss.BuildCommentRss();
            }

            return msg;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}