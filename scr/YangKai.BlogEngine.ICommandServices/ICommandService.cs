using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.ICommandServices
{
    public interface ICommandService
    {
        void RunCommand(IEvent e);
        void Create<TEntity>(TEntity entity) where TEntity : class;
        void UpdatePost(string postUrl, Post data, bool existThumbnail);
    }
}
