using System;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.CommonModule.Commands;

namespace YangKai.BlogEngine.Modules.CommonModule.Objects
{
    /// <summary>
    /// 管理员信息.
    /// </summary>
    public class User : Entity<Guid>,
                        IEntityEventHandler<UserEditPasswordCommand>
    {
        #region constructor

        public User()
            : base(Guid.NewGuid())
        {
            UserName = string.Empty;
            LoginName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            IsDeleted = false;
            CreateDate = DateTime.Now;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// 用户名.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录名.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登陆密码.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 电子邮件.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否被删除.(禁用)
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreateDate { get; private set; }

        #endregion

        #region handler

        #region IEntityEventHandler<UserEditPasswordCommand> Members

        public object GetEntityId(UserEditPasswordCommand e)
        {
            return e.UserId;
        }

        public void Handle(UserEditPasswordCommand e)
        {
            if (this.Password != e.BeforePwd)
            {
                throw new DomainException("原密码不正确.");
            }
            if (e.NewPwd != e.ConfirmNewPwd)
            {
                throw new DomainException("两次输入密码不一致.");
            }
            this.Password = e.NewPwd;
        }

        #endregion

        #endregion


    }
}