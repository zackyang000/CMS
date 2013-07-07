using System;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.IQueryServices
{
    public interface IUserQueryServices
    {
        User Find(string name);
        bool AccountLoginValidate(string name, string pwd, bool isSave, int? days);
        void LoginOff();
        string UserName();
        Guid UserId();
        bool IsLogin();
    }
}