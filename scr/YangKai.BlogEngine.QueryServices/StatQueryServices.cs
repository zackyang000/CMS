//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 7/25/2012 11:24:38 PM
//===========================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Modules.BoardModule.Repositories;
using YangKai.BlogEngine.Modules.CommonModule.Repositories;
using YangKai.BlogEngine.Modules.PostModule.Repositories;

namespace YangKai.BlogEngine.QueryServices
{
    public class StatQueryServices :  IStatQueryServices
    {
        readonly LogRepository _logRepository = InstanceLocator.Current.GetInstance<LogRepository>();
        readonly PostRepository _postRepository = InstanceLocator.Current.GetInstance<PostRepository>();
        readonly CommentRepository _commentRepository = InstanceLocator.Current.GetInstance<CommentRepository>();
        readonly BoardRepository _boardRepository = InstanceLocator.Current.GetInstance<BoardRepository>();

        //动态图片使用的字体
        private const string RefstatPictureFontFamily = "宋体";
        //动态图片标题
        private const string RefstatPictureTitle = "最近30天的访问图表:";
        //动态图片日期格式
        private const string RefstatPictureDateFormatMd = "M.d";
        //动态图片准备图片保存路径
        private const string RefstatPicturePrePath = "\\Content\\Image\\refstat_pre.jpg";
        //动态图片完成图片保存路径
        private const string RefstatPicturePath = "\\Content\\Image\\refstat.jpg";
        //统计信息格式
        private const string RefstatInfoFormat = "{0}：{1}<br>";
        //IP数据库路径
        private const string RefstatIpDbPath = "~/App_Data/QQWry.Dat";

        #region 读取累计访问人数
        /// <summary>
        /// 读取累计访问人数
        /// </summary>
        public int TotalVisitorCount()
        {
            return 0/*refstatRepository.Count()*/;
        }
        #endregion

        #region 读取今日访问人数
        /// <summary>
        /// 读取今日访问人数
        /// </summary>
        public int TodayVisitorCount()
        {
            return 0/*refstatRepository.TodayCount()*/;
        }
        #endregion

        /// <summary>
        /// 得到统计信息
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetRefstatInfo()
        {
            double totalDayCount = (DateTime.Now - Convert.ToDateTime("2009-01-01")).Days;

            int todayVisitorCount = _logRepository.SiteVisitCount();
            int totalVisitorCount = _logRepository.SiteVisitCountOnToday();

            int PostCount = _postRepository.Count();
            int commentCount =_commentRepository.Count();
            int boardCount = _boardRepository.Count();

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("今日访问量", todayVisitorCount.ToString());
            data.Add("总访问人数", totalVisitorCount.ToString());
            data.Add("文章总数", PostCount.ToString());
            data.Add("评论总数", commentCount.ToString());
            data.Add("留言总数", boardCount.ToString());
            data.Add("平均日访问量", (totalVisitorCount / totalDayCount).ToString("#0.00"));
            data.Add("平均日新增文章数", (PostCount / totalDayCount).ToString("#0.00"));
            data.Add("平均日新增评论数", (commentCount / totalDayCount).ToString("#0.00"));
            data.Add("平均日新增留言数", (boardCount / totalDayCount).ToString("#0.00"));
            return data;
        }

        #region 更新访问统计图
        /// <summary>
        /// 更新访问统计图
        /// </summary>
        public void UpdateRefStatPicture()
        {
            int yaxis_max = 0;          //纵坐标最大值
            int[] yaxis;                //纵坐标最大值的允许值
            yaxis = new int[] { 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 500000, 1000000 };//设置纵坐标可能的最大值

            //读取数据
            IList<int> data = _logRepository.GetVisitStat(30);
            int len = data.Count;

            //得到纵坐标最大值
            for (int i = 0; i < yaxis.Length; i++)
            {
                if (data.Max() <= yaxis[i])
                {
                    yaxis_max = yaxis[i];
                    break;
                }
            }

            //设置背景图片
            System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(RefstatPicturePrePath));
            //载入背景图片
            Graphics g = Graphics.FromImage(image);
            //设置抗锯齿方式
            g.SmoothingMode = SmoothingMode.HighQuality;
            //定义文字
            Font font = new Font(RefstatPictureFontFamily, 10);
            //定义笔刷
            SolidBrush sb = new SolidBrush(Color.Black);
            SolidBrush sbRed = new SolidBrush(Color.Red);
            SolidBrush sbGray = new SolidBrush(Color.Gray);
            //定义一个新Pen
            Pen pen = new Pen(new SolidBrush(Color.Blue));
            //定义一个文字显示格式
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            //生成标题
            g.DrawString(RefstatPictureTitle, font, sb, new PointF(50, 0), sf);

            //生成纵坐标:循环6次生成日访问量坐标,每循环一次:日访问量-20%,纵坐标-40
            for (int i = 0; i < 6; i++) g.DrawString((yaxis_max / 5 * i).ToString(), font, sb, new PointF(8, 222 - i * 40), sf);
            //生成横坐标:循环14次(最后一个日期单独生成)生成日期,每循环一次:日期-2,横坐标-40
            for (int i = 1; i < 15; i++) g.DrawString(DateTime.Now.AddDays(-i * 2).ToString(RefstatPictureDateFormatMd), font, sb, new PointF(610 - i * 40, 232), sf);
            //当前日期使用红色显示
            g.DrawString(DateTime.Now.ToString(RefstatPictureDateFormatMd), font, sbRed, new PointF(610, 232), sf);
            //生成柱状
            for (int i = 0; i < (data.Count > 30 ? 30 : data.Count); i++)
            {
                //循环30次生成柱状图顶部,每循环一次:横坐标-20,纵坐标根据日访问量变化
                g.DrawLine(pen, new PointF(621 - i * 20, 230 - data[i] * 200 / yaxis_max), new PointF(601 - i * 20, 230 - data[i] * 200 / yaxis_max));
                //循环30次生成柱状图右侧部分,每循环一次:横坐标-20,纵坐标根据日访问量变化
                g.DrawLine(pen, new PointF(621 - i * 20, 230), new PointF(621 - i * 20, 230 - data[i] * 200 / yaxis_max));
                //循环30次生成柱状图左侧部分,每循环一次:横坐标-20,纵坐标根据日访问量变化
                g.DrawLine(pen, new PointF(601 - i * 20, 230), new PointF(601 - i * 20, 230 - data[i] * 200 / yaxis_max));
                //循环30次填充柱状图内部颜色,每循环一次:横坐标-20,纵坐标根据日访问量变化
                g.FillRectangle(new SolidBrush(Color.LightBlue), 601 - i * 20, 230 - data[i] * 200 / yaxis_max, 20, data[i] * 200 / yaxis_max);
                //循环30次生成柱状图顶部数字提示,每循环一次:横坐标-20,纵坐标根据日访问量变化
                g.DrawString(data[i].ToString(), font, sbGray, new PointF(611 - i * 20, 230 - (data[i] * 200 / yaxis_max) - 13), sf);
            }

            //保存图片
            //若不使用try,可能发生2个用户同时保存图片,导致以下错误:
            //A generic error occurred in GDI+.
            try
            {
                image.Save(HttpContext.Current.Server.MapPath(RefstatPicturePath), ImageFormat.Jpeg);
            }
            catch(Exception e)
            {

            }
            //释放资源
            g.Dispose();
            image.Dispose();
        }
        #endregion
    }
}
