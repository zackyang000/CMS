using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.ICommandServices;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.ServiceProxy
{
    public static class CommandFactory
    {
       public static void Run(IEvent e)
       {
           var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
           commandService.RunCommand(e);
       }

       public static void Create<TEntity>(TEntity entity) where TEntity :class
       {
           var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
           commandService.CreateEntity(entity);
       }

        public static void CreatePost(Post post)
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.CreatePost(post);
        }

        public static void CreateComment(Comment comment)
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.CreateComment(comment);
        }

        public static void CreateBoard(Board board)
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.CreateBoard(board);
        }

        public static void UpdatePost(string postUrl,Post data,bool existThumbnail)
        {
           var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.UpdatePost( postUrl, data, existThumbnail);
        }
    }
}
