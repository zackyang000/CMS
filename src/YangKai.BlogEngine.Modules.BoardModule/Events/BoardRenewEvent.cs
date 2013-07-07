using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.BoardModule.Events
{
    public class BoardRenewEvent : IEvent
    {
        public Guid BoardId { get; set; }
    }
}
