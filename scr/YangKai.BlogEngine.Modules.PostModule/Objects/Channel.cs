using System;
using System.Collections.Generic;
using AtomLab.Domain;

namespace YangKai.BlogEngine.Modules.PostModule.Objects
{
    /// <summary>
    /// 频道信息.
    /// </summary>
    public class Channel : Entity<Guid>
    {
        #region constructor

        public Channel()
            : base(Guid.NewGuid())
        {
            Name = string.Empty;
            Description = string.Empty;
            Url = string.Empty;
            StyleConfigurePath = string.Empty;
            IsDefault = false;
            OrderId = 255;
            CreateDate = DateTime.Now;
            IsDeleted = false;
        }

        #endregion

        #region property

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid ChannelId { get; private set; }

        /// <summary>
        /// 频道名称.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用于显示频道信息的URL地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 频道描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// css样式配置相对路径.(如"/Content/style.css")
        /// </summary>
        public string StyleConfigurePath { get; set; }

        /// <summary>
        /// 是否是默认频道.
        /// 所有频道有且仅有一个能为默认频道.默认频道将显示在未传频道信息的页面,如首页.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 频道排列顺序,默认为255.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 频道创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否被删除.(禁用)
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region relation schema

        /// <summary>
        /// 频道所有分组信息.
        /// </summary>
        public virtual List<Group> Groups { get; set; }

        #endregion

        #region handler

        #endregion
    }
}