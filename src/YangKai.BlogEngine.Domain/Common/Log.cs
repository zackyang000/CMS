using System;
using AtomLab.Core;

namespace YangKai.BlogEngine.Domain
{
    /// <summary>
    /// 日志信息.
    /// </summary>
    public class Log : Entity
    {
        public static Log CreateSearchLog(string key)
        {
            return new Log
                          {
                              AppName = "Web",
                              ModuleName = "PostModule",
                              ActionType = "Search",
                              Description = key,
                              User ="游客",
                          };
        }

        public static Log CreateSiteVisitLog()
        {
            return new Log
            {
                AppName = "Web",
                ModuleName = "CommonModule",
                ActionType = "SiteVisit",
                User = "游客",
            };
        }

        /// <summary>
        /// 主键.
        /// </summary>
        public Guid LogId { get; set; }

        /// <summary>
        /// 应用程序名称.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 模块名称.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 操作类型.
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// 相关业务ID.
        /// </summary>
        public Guid? BusinessId { get; set; }

        /// <summary>
        /// 描述.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 操作者.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 操作者Ip地址.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 操作者物理地址.
        /// </summary>
        public string Address { get;private set; }

        /// <summary>
        /// 操作者操作系统
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// 操作者机器名.(仅winform应用程序填写)
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 操作者Windows登录名.(仅winform应用程序填写)
        /// </summary>
        public string WindowsName { get; set; }

        /// <summary>
        /// 操作者mac地址.(仅winform应用程序填写)
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// 操作者浏览器名称.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 操作者浏览器版本.
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// 是否是机器人
        /// </summary>
        /// <returns></returns>
        public bool IsRobot
        {
            get { return BrowserVersion == "0.0" || Browser == "Unknown"; }
        }
    }

  public enum ActionTypeEnum
{
      Search,
      SiteVisit
}
}