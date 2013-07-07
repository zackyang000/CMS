using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.BoardModule.Events
{
    public class BoardDeleteEvent : IEvent
    {
        public Guid BoardId { get; set; }
    }
}
