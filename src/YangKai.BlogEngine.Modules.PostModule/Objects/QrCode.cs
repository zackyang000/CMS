using System;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 文章缩略图信息.
    /// </summary>
    public class QrCode : Entity<Guid>
    {
        #region constructor

        public QrCode()
            : base(Guid.NewGuid())
        {
            Content = string.Empty;
            Url = string.Empty;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid QrCodeId { get; private set; }

        /// <summary>
        /// 二维码内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 二维码文件名.
        /// </summary>
        public string Url { get; set; }

        #endregion
    }
}