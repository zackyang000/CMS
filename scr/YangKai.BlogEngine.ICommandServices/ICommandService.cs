using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.ICommandServices
{
    public interface ICommandService
    {
        void RunCommand(IEvent e);
        void CreateEntity<TEntity>(TEntity entity) where TEntity : class;
        void CreatePost(Post post);
        void CreateComment(Comment comment);
        void UpdatePost(string postUrl, Post data, bool existThumbnail);

        void CreateBoard(Modules.BoardModule.Objects.Board board);
    }
}
