using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.ServiceProxy
{
    public static class Query
    {
        public static Repository<User, Guid> User
        {
            get { return InstanceLocator.Current.GetInstance<Repository<User, Guid>>(); }
        }

        public static Repository<Post, Guid> Post
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Post, Guid>>(); }
        }

        public static Repository<Channel, Guid> Channel
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Channel, Guid>>(); }
        }

        public static Repository<Group, Guid> Group
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Group, Guid>>(); }
        }

        public static Repository<Category, Guid> Category
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Category, Guid>>(); }
        }

        public static Repository<Tag, Guid> Tag
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Tag, Guid>>(); }
        }

        public static Repository<Comment, Guid> Comment
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Comment, Guid>>(); }
        }

        public static Repository<Board, Guid> Message
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Board, Guid>>(); }
        }
    }
}