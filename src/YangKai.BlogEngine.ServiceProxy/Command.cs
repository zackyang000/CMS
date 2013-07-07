using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.ICommandServices;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.ServiceProxy
{
    public class Command
    {
        public static Command Instance
        {
            get { return new Command(); }
        }

        private Command()
        {

        }

        public void Run(IEvent e)
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.RunCommand(e);
        }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.Create(entity);
        }

        public void UpdatePost(string postUrl, Post data, bool existThumbnail)
        {
            var commandService = InstanceLocator.Current.GetInstance<ICommandService>();
            commandService.UpdatePost(postUrl, data, existThumbnail);
        }
    }
}
