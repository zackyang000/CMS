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
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using AtomLab.Domain;
using AtomLab.Domain.Infrastructure;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using Comment = NPOI.SS.UserModel.Comment;
using YangKai.BlogEngine.IQueryServices;
using YangKai.BlogEngine.Modules.CommonModule.Repositories;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Repositories;
using Font = System.Drawing.Font;

namespace YangKai.BlogEngine.QueryServices
{
    public class LabQueryServices :  ILabQueryServices
    {
        readonly LogRepository _logRepository = InstanceLocator.Current.GetInstance<LogRepository>();
        readonly PostRepository _postRepository = InstanceLocator.Current.GetInstance<PostRepository>();
        readonly CommentRepository _commentRepository = InstanceLocator.Current.GetInstance<CommentRepository>();
        readonly GuidRepository<Board> _boardRepository = InstanceLocator.Current.GetInstance<GuidRepository<Board>>();

        #region 统计

        //动态图片使用的字体
        private const string RefstatPictureFontFamily = "宋体";
        //动态图片标题
        private const string RefstatPictureTitle = "最近30天的访问图表:";
        //动态图片日期格式
        private const string RefstatPictureDateFormatMd = "M.d";
        //动态图片准备图片保存路径
        private const string RefstatPicturePrePath = "\\Content\\img\\lab\\refstat_pre.jpg";
        //动态图片完成图片保存路径
        private const string RefstatPicturePath = "\\Content\\img\\lab\\refstat.jpg";
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

            int todayVisitorCount = _logRepository.SiteVisitCountOnToday();
            int totalVisitorCount = _logRepository.SiteVisitCount();

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

        #endregion

        #region 导出

        public void ExportPosts( string filepath,IList<Post> data)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            hssfworkbook.DocumentSummaryInformation = GetDocumentSummaryInformation();
            hssfworkbook.SummaryInformation = GetSummaryInformation();
            var sheet = hssfworkbook.CreateSheet("导出数据");
            SetTitle(hssfworkbook);
            SetColTitle(hssfworkbook);
            SetContent(data, hssfworkbook);
            SetSum(data.Count, hssfworkbook);
            var patriarch = sheet.CreateDrawingPatriarch();
            SetPic(data.Count, hssfworkbook, patriarch);
            SetComment(data.Count, sheet, patriarch);
            sheet.CreateFreezePane(1, 2, 1, 2);//冻结

            //生成文件
            FileStream file = new FileStream(filepath, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        #region 设置文件信息

        private SummaryInformation GetSummaryInformation()
        {
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Comments = string.Format("这是一个数据从{0}导出的数据副本", Config.URL.Domain);
            si.Keywords = Config.URL.Domain;
            si.Title = "导出数据";
            return si;
        }

        private DocumentSummaryInformation GetDocumentSummaryInformation()
        {
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = Config.URL.Domain;
            return dsi;
        }

        #endregion

        #region 设置主标题

        /// <summary>
        /// 设置主标题
        /// </summary>
        /// <param name="hssfworkbook"></param>
        private void SetTitle(HSSFWorkbook hssfworkbook)
        {
            var sheet = hssfworkbook.GetSheetAt(0);
            var cell = sheet.CreateRow(0).CreateCell(1);//创建单元格
            sheet.GetRow(0).HeightInPoints = 30;//设置行高
            cell.SetCellValue("文章列表");//设置文字
            cell.CellStyle = GetTitleStyle(hssfworkbook);//设置样式
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 9));//合并单元格
        }

        private CellStyle GetTitleStyle(HSSFWorkbook hssfworkbook)
        {
            CellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            var font = hssfworkbook.CreateFont();
            font.FontHeight = 20 * 20;
            font.FontName = "微软雅黑";
            font.Color = HSSFColor.BLACK.index;
            style.SetFont(font);
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PINK.index;//前景色
            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index;//背景色
            return style;
        }

        #endregion

        #region 设置列标题

        /// <summary>
        /// 设置列标题
        /// </summary>
        /// <param name="hssfworkbook"></param>
        private void SetColTitle(HSSFWorkbook hssfworkbook)
        {
            var sheet = hssfworkbook.GetSheetAt(0);
            Row row = sheet.CreateRow(1);
            IDictionary<string, int> cols = new Dictionary<string, int>();
            cols.Add("序号", 5);
            cols.Add("标题", 40);
            cols.Add("主题", 5);
            cols.Add("发布时间", 15);
            cols.Add("点击数", 10);
            cols.Add("回复数", 10);
            cols.Add("地址", 80);
            var colTitleStyle = GetColTitleStyle(hssfworkbook);
            int i = 0;
            foreach (var item in cols)
            {
                row.CreateCell(i).SetCellValue(item.Key);//插入列名
                row.GetCell(i).CellStyle = colTitleStyle;//设置样式
                sheet.SetColumnWidth(i, item.Value * 256);//设置列宽
                i++;
            }
        }

        private CellStyle GetColTitleStyle(HSSFWorkbook hssfworkbook)
        {
            CellStyle style = hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            var font = hssfworkbook.CreateFont();
            font.Boldweight = short.MaxValue;
            font.FontName = "微软雅黑";
            style.SetFont(font);
            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
            style.FillForegroundColor = 48;//前景色
            style.FillBackgroundColor = 48;//背景色
            return style;
        }

        #endregion

        #region 填充数据

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hssfworkbook"></param>
        private void SetContent(IList<Post> data, HSSFWorkbook hssfworkbook)
        {
            var sheet = hssfworkbook.GetSheetAt(0);
            int i = 2;//数据起始行数,0为第一行
            foreach (var item in data)
            {
                Row row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(i - 1);
                row.CreateCell(1).SetCellValue(item.Title);
                row.CreateCell(2).SetCellValue(item.Group.Name);
                row.CreateCell(3).SetCellValue(string.Join(",", item.Categorys.Select(p => p.Name).ToList()));
                row.CreateCell(4).SetCellValue(string.Join(",", item.Tags.Select(p => p.Name).ToList()));
                row.CreateCell(5).SetCellValue(item.PubAdmin.UserName);
                row.CreateCell(6).SetCellValue(item.PubDate.ToString("yyyy年MM月dd日"));
                row.CreateCell(7).SetCellValue(item.ViewCount);
                row.CreateCell(8).SetCellValue(item.ReplyCount);
                row.CreateCell(9).SetCellValue(string.Format("{0}/{1}-{2}", Config.URL.Domain, item.Group.Url, item.Url));

                row.GetCell(0).CellStyle = GetRowTitle(hssfworkbook);
                if (i % 2 == 0)
                {
                    CellStyle cs = GetRowEach(hssfworkbook);
                    row.GetCell(1).CellStyle = cs;
                    row.GetCell(2).CellStyle = cs;
                    row.GetCell(3).CellStyle = cs;
                    row.GetCell(4).CellStyle = cs;
                    row.GetCell(5).CellStyle = cs;
                    row.GetCell(6).CellStyle = cs;
                    row.GetCell(7).CellStyle = cs;
                    row.GetCell(8).CellStyle = cs;
                    row.GetCell(9).CellStyle = cs;
                }
                else
                {
                    CellStyle cs = GetRowEach2(hssfworkbook);
                    row.GetCell(1).CellStyle = cs;
                    row.GetCell(2).CellStyle = cs;
                    row.GetCell(3).CellStyle = cs;
                    row.GetCell(4).CellStyle = cs;
                    row.GetCell(5).CellStyle = cs;
                    row.GetCell(6).CellStyle = cs;
                    row.GetCell(7).CellStyle = cs;
                    row.GetCell(8).CellStyle = cs;
                    row.GetCell(9).CellStyle = cs;
                }
                i++;
            }
        }

        private CellStyle GetRowTitle(HSSFWorkbook hssfworkbook)
        {
            CellStyle style = hssfworkbook.CreateCellStyle();
            var font = hssfworkbook.CreateFont();
            font.Boldweight = short.MaxValue;
            font.FontName = "微软雅黑";
            style.SetFont(font);
            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
            style.FillForegroundColor = 29;//前景色
            style.FillBackgroundColor = 29;//背景色
            return style;
        }

        private CellStyle GetRowEach(HSSFWorkbook hssfworkbook)
        {
            CellStyle style = hssfworkbook.CreateCellStyle();
            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
            style.FillForegroundColor = 26;//前景色
            style.FillBackgroundColor = 26;//背景色
            return style;
        }

        private CellStyle GetRowEach2(HSSFWorkbook hssfworkbook)
        {
            CellStyle style = hssfworkbook.CreateCellStyle();
            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
            style.FillForegroundColor = 43;//前景色
            style.FillBackgroundColor = 43;//背景色
            return style;
        }

        #endregion

        #region 计算合计

        /// <summary>
        /// 计算合计
        /// </summary>
        /// <param name="count"></param>
        /// <param name="hssfworkbook"></param>
        private void SetSum(int count, HSSFWorkbook hssfworkbook)
        {
            var sheet = hssfworkbook.GetSheetAt(0);

            Row row = sheet.CreateRow(2 + count);

            row.CreateCell(6).SetCellValue("合计:");
            row.CreateCell(7).CellFormula = "sum(H3:H" + (2 + count) + ")";
            row.CreateCell(8).CellFormula = "sum(I3:I" + (2 + count) + ")";

            CellStyle style = GetSumStyle(hssfworkbook);
            row.GetCell(6).CellStyle = style;
            row.GetCell(7).CellStyle = style;
            row.GetCell(8).CellStyle = style;
        }

        private CellStyle GetSumStyle(HSSFWorkbook hssfworkbook)
        {
            CellStyle tstyle = hssfworkbook.CreateCellStyle();
            tstyle.Alignment = HorizontalAlignment.RIGHT;
            var font = hssfworkbook.CreateFont();
            font.Boldweight = short.MaxValue;
            tstyle.SetFont(font);
            return tstyle;
        }

        #endregion

        #region 插入批注

        /// <summary>
        /// 插入批注
        /// </summary>
        /// <param name="count"></param>
        /// <param name="sheet"></param>
        /// <param name="patriarch"></param>
        private void SetComment(int count, Sheet sheet, Drawing patriarch)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("这个Excel文件完全由代码生成,并非是在原有模板上填充数据.\r\n");
            sb.Append("文件创建于" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss\r\n"));
            sb.Append("\r\n给力啊,IT！！！");
            sb.Append("\r\nhttp://www.woshinidezhu.com/");
            sb.Append("\r\nhttp://woshinidezhu.cnblogs.com/");

            /*****************************************************************
             * HSSFClientAnchor:批注位置,8个参数分别表示
             * {1}距离左边单元格的距离,1024为100%,即从右方一个单元格开始
             * {2}距离上方单元格的距离,255为100%,即从下方一个单元格开始
             * {3}右边单元格超出的距离,1024为100%,即充满到右方一个单
             * {4}下方单元格超出的距离,255为100%,即充满到下方一个单元格
             * {5}起始列
             * {6}起始行
             * {7}结束列
             * {8}结束行
             *****************************************************************/
            Comment comment = patriarch.CreateCellComment(new HSSFClientAnchor(600, 100, 500, 200, 1, 3 + count, 7, 3 + 7 + count));
            comment.String = new HSSFRichTextString(sb.ToString());
            comment.Visible = true;//默认打开即可见
            sheet.GetRow(2 + count).CreateCell(0).CellComment = comment;
        }

        #endregion

        #region 插入图片

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="count"></param>
        /// <param name="hssfworkbook"></param>
        /// <param name="patriarch"></param>
        private void SetPic(int count, HSSFWorkbook hssfworkbook, Drawing patriarch)
        {
            //byte[] bytes = System.IO.File.ReadAllBytes(Site.AVATAR_PATH);
            //int pictureIdx = hssfworkbook.AddPicture(bytes, PictureType.PNG);
            //HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, 3 + count, 0, 0);
            //Picture pict = patriarch.CreatePicture(anchor, pictureIdx);
            //pict.Resize();
        }

        #endregion

        #endregion
    }
}
