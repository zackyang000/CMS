using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 用户信息.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// 主键.
        /// </summary>
        public Guid UserId { get; set; }

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
        /// 头像URL.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }
    }
}