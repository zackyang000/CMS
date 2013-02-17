using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.IQueryServices;

namespace YangKai.BlogEngine.ServiceProxy
{
    public static class QueryFactory
    {
        public static IUserQueryServices User
        {
            get
            {
                  return InstanceLocator.Current.GetInstance<IUserQueryServices>();
            }
        }

        public static IPostQueryServices Post
        {
            get
            {
                return InstanceLocator.Current.GetInstance<IPostQueryServices>();
            }
        }

        public static IBoardQueryServices Board
        {
            get
            {
                return InstanceLocator.Current.GetInstance<IBoardQueryServices>();
            }
        }

        public static IStatQueryServices Stat
        {
            get
            {
                return InstanceLocator.Current.GetInstance<IStatQueryServices>();
            }
        }
    }
}
