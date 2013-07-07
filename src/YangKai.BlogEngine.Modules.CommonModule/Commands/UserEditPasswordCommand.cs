using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.CommonModule.Commands
{
    public class UserEditPasswordCommand : IEvent
    {
        public Guid UserId { get; set; }
        public string BeforePwd { get; set; }
        public string NewPwd { get; set; }
        public string ConfirmNewPwd { get; set; }
    }
}
