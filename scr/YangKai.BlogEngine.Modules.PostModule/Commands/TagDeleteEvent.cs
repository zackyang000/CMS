using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Commands
{
    public class TagDeleteEvent : IEvent
    {
        public Guid TagId { get; set; }
    }
}
