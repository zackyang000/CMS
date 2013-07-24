using System;
using AtomLab.Core;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Proxy
{
    public static class Repository
    {
        public static Repository<Post, Guid> Post
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Post, Guid>>(); }
        }

        public static Repository<Category, Guid> Category
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Category, Guid>>(); }
        }

        public static Repository<Channel, Guid> Channel
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Channel, Guid>>(); }
        }

        public static Repository<Comment, Guid> Comment
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Comment, Guid>>(); }
        }

        public static Repository<Group, Guid> Group
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Group, Guid>>(); }
        }

        public static Repository<QrCode, Guid> QrCode
        {
            get { return InstanceLocator.Current.GetInstance<Repository<QrCode, Guid>>(); }
        }

        public static Repository<Source, Guid> Source
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Source, Guid>>(); }
        }

        public static Repository<Thumbnail, Guid> Thumbnail
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Thumbnail, Guid>>(); }
        }

        public static Repository<Board, Guid> Board
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Board, Guid>>(); }
        }

        public static Repository<Tag, Guid> Tag
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Tag, Guid>>(); }
        }

        public static Repository<Log, Guid> Log
        {
            get { return InstanceLocator.Current.GetInstance<Repository<Log, Guid>>(); }
        }

        public static Repository<User, Guid> User
        {
            get { return InstanceLocator.Current.GetInstance<Repository<User, Guid>>(); }
        }
    }
}