using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Commands
{
    public class CommentRenewEvent : IEvent
    {
        public Guid CommentId { get; set; }
    }
}
