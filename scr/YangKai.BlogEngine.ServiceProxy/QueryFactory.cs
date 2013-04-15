using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.IQueryServices;

namespace YangKai.BlogEngine.ServiceProxy
{
    public class QueryFactory
    {
        public static QueryFactory Instance
        {
            get { return new QueryFactory(); }
        }

        private QueryFactory()
        {

        }

        public IUserQueryServices User
        {
            get { return InstanceLocator.Current.GetInstance<IUserQueryServices>(); }
        }

        public IPostQueryServices Post
        {
            get { return InstanceLocator.Current.GetInstance<IPostQueryServices>(); }
        }

        public IBoardQueryServices Board
        {
            get { return InstanceLocator.Current.GetInstance<IBoardQueryServices>(); }
        }

        public ILabQueryServices Lab
        {
            get { return InstanceLocator.Current.GetInstance<ILabQueryServices>(); }
        }
    }
}