using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Commands
{
  public      class PostDeleteEvent : IEvent
    {
      public Guid PostId { get; set; }
    }
}
