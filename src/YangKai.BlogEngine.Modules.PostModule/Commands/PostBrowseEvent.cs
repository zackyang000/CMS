using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Commands
{
   public class PostBrowseEvent:IEvent
    {
       public Guid PostId { get; set; }
    }
}
