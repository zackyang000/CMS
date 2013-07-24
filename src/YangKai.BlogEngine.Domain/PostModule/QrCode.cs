using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 文章缩略图信息.
    /// </summary>
    public class QrCode : Entity
    {
        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid QrCodeId { get; set; }

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