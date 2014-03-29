using System.Web.UI;

namespace AtomLab.Utility
{
    public class JavaScriptHelper
    {
        /// <summary>
        /// 弹出JS对话框
        /// </summary>
        public static void Alert(string msg)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "Alert", string.Format("alert('{0}');", msg), true);
        }

        /// <summary>
        /// 弹出对话框并跳转到URL
        /// </summary>
        public static void AlertAndReturn(string msg, string url)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "AlertAndReturn", string.Format("alert('{0}');location.href = '{1}';", msg, url), true);
        }

        /// <summary>
        /// 弹出确定对话框
        /// </summary>
        public static void Confirm(string msg)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "AlertAndReturn", string.Format("return confirm('{0}')", msg), true);
        }

        /// <summary>
        /// 返回到指定步数的页面
        /// </summary>
        public static void BackPage(int step)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "BackPage", string.Format("window.history.go('{0}')", step), true);
        }

        /// <summary>
        /// 弹出提示,确定后后退指定步数
        /// </summary>
        public static void AlertAndBack(string msg, int step)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "AlertAndBack", string.Format("alert('{0}');window.history.go('{1}')", msg, step), true);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public static void Refurbish()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>window.location.href=document.URL;</script>");
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public static void Close()
        {
            System.Web.HttpContext.Current.Response.Write("<script language=javascript>window.close();></script>");
        }

        public static void Mod(string js)
        {
            var currentPage = (Page)System.Web.HttpContext.Current.Handler;
            currentPage.ClientScript.RegisterStartupScript(currentPage.GetType(), "mod", js, true);
        }
    }
}