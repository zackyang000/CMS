using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule.Repositories;

namespace YangKai.BlogEngine.QueryServices
{
    public class UserQueryServices :  IUserQueryServices
   {
        readonly UserRepository _userRepository = InstanceLocator.Current.GetInstance<UserRepository>();

       private const string NecessaryUsername = "请输入用户名.";
       private const string NecessaryPassword = "请输入密码.";

        public User Find(string name)
        {
            return _userRepository.Get(p=>p.UserName == name);
        }

        public bool AccountLoginValidate(string name, string pwd, bool isSave, int? days)
        {
            if (string.IsNullOrEmpty(name)) throw new DomainException(NecessaryUsername);
            if (string.IsNullOrEmpty(pwd)) throw new DomainException(NecessaryPassword);


            var isExist = _userRepository.LoginValidate(name, pwd);

            if (isExist)
            {
                //登录成功
                var data = _userRepository.Get(p => p.LoginName == name);
                WebMasterCookie.Save(data.UserId, data.LoginName, isSave);
                return true;
            }
            //登录失败
            return false;
        }

        public void LoginOff()
        {
            WebMasterCookie.Remove();
        }

        public string UserName()
        {
            return WebMasterCookie.Load().Name;
        }

        public Guid UserId()
        {
            return WebMasterCookie.Load().Id;
        }

        public bool IsLogin()
        {
            return WebMasterCookie.IsLogin;
        }
    }
}
