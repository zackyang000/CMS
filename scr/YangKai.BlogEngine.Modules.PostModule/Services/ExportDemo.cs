////===========================================================
//// Copyright @ 2010 YangKai. All Rights Reserved.
//// Framework: 4.0
//// Author: 杨凯
//// Email: yangkai-13896222@sohu.com
//// QQ: 83448327
//// CreateTime: 3/19/2011 6:00:03 PM
////===========================================================

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using NPOI.HPSF;
//using NPOI.HSSF.UserModel;
//using NPOI.HSSF.Util;
//using NPOI.SS.UserModel;
//using NPOI.SS.Util;
//using YangKai.BlogEngine.Domain.Entities;
//using YangKai.BlogEngine.Modules.Post.DomainObjects;
//using Comment = NPOI.SS.UserModel.Comment;
//using YangKai.BlogEngine.Common;

//namespace YangKai.BlogEngine.Repositories
//{
//    internal class ExportDemo
//    {
//        internal void ExportPosts(IList<Post> data, string filepath)
//        {
//            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
//            hssfworkbook.DocumentSummaryInformation = GetDocumentSummaryInformation();
//            hssfworkbook.SummaryInformation = GetSummaryInformation();
//            var sheet = hssfworkbook.CreateSheet("导出数据");
//            SetTitle(hssfworkbook);
//            SetColTitle(hssfworkbook);
//            SetContent(data, hssfworkbook);
//            SetSum(data.Count, hssfworkbook);
//            var patriarch = sheet.CreateDrawingPatriarch();
//            SetPic(data.Count, hssfworkbook, patriarch);
//            SetComment(data.Count, sheet, patriarch);
//            sheet.CreateFreezePane(1, 2, 1, 2);//冻结

//            //生成文件
//            FileStream file = new FileStream(filepath, FileMode.Create);
//            hssfworkbook.Write(file);
//            file.Close();
//        }

//        #region 设置文件信息

//        private SummaryInformation GetSummaryInformation()
//        {
//            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
//            si.Author = Site.NICKNAME;
//            si.Comments = string.Format("这是一个数据从{0}导出的数据副本", Site.ROOT_URI);
//            si.Keywords = Site.ROOT_URI;
//            si.Title = "导出数据";
//            return si;
//        }

//        private DocumentSummaryInformation GetDocumentSummaryInformation()
//        {
//            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
//            dsi.Company = Site.ROOT_URI;
//            return dsi;
//        }

//        #endregion

//        #region 设置主标题

//        /// <summary>
//        /// 设置主标题
//        /// </summary>
//        /// <param name="hssfworkbook"></param>
//        private void SetTitle(HSSFWorkbook hssfworkbook)
//        {
//            var sheet = hssfworkbook.GetSheetAt(0);
//            var cell = sheet.CreateRow(0).CreateCell(1);//创建单元格
//            sheet.GetRow(0).HeightInPoints = 30;//设置行高
//            cell.SetCellValue("文章列表");//设置文字
//            cell.CellStyle = GetTitleStyle(hssfworkbook);//设置样式
//            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 9));//合并单元格
//        }

//        private CellStyle GetTitleStyle(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle style = hssfworkbook.CreateCellStyle();
//            style.Alignment = HorizontalAlignment.CENTER;
//            Font font = hssfworkbook.CreateFont();
//            font.FontHeight = 20 * 20;
//            font.FontName = "微软雅黑";
//            font.Color = HSSFColor.BLACK.index;
//            style.SetFont(font);
//            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PINK.index;//前景色
//            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
//            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index;//背景色
//            return style;
//        }

//        #endregion

//        #region 设置列标题

//        /// <summary>
//        /// 设置列标题
//        /// </summary>
//        /// <param name="hssfworkbook"></param>
//        private void SetColTitle(HSSFWorkbook hssfworkbook)
//        {
//            var sheet = hssfworkbook.GetSheetAt(0);
//            Row row = sheet.CreateRow(1);
//            IDictionary<string, int> cols = new Dictionary<string, int>();
//            cols.Add("序号", 5);
//            cols.Add("标题", 40);
//            cols.Add("主题", 5);
//            cols.Add("分类", 15);
//            cols.Add("标签", 15);
//            cols.Add("作者", 10);
//            cols.Add("发布时间", 15);
//            cols.Add("点击数", 10);
//            cols.Add("回复数", 10);
//            cols.Add("地址", 80);
//            var colTitleStyle = GetColTitleStyle(hssfworkbook);
//            int i = 0;
//            foreach (var item in cols)
//            {
//                row.CreateCell(i).SetCellValue(item.Key);//插入列名
//                row.GetCell(i).CellStyle = colTitleStyle;//设置样式
//                sheet.SetColumnWidth(i, item.Value * 256);//设置列宽
//                i++;
//            }
//        }

//        private CellStyle GetColTitleStyle(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle style = hssfworkbook.CreateCellStyle();
//            style.Alignment = HorizontalAlignment.CENTER;
//            Font font = hssfworkbook.CreateFont();
//            font.Boldweight = short.MaxValue;
//            font.FontName = "微软雅黑";
//            style.SetFont(font);
//            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
//            style.FillForegroundColor = 48;//前景色
//            style.FillBackgroundColor = 48;//背景色
//            return style;
//        }

//        #endregion

//        #region 填充数据

//        /// <summary>
//        /// 填充数据
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="hssfworkbook"></param>
//        private void SetContent(IList<Post> data, HSSFWorkbook hssfworkbook)
//        {
//            var sheet = hssfworkbook.GetSheetAt(0);
//            int i = 2;//数据起始行数,0为第一行
//            foreach (var item in data)
//            {
//                Row row = sheet.CreateRow(i);
//                row.CreateCell(0).SetCellValue(i - 1);
//                row.CreateCell(1).SetCellValue(item.Title);
//                row.CreateCell(2).SetCellValue(item.MainClass.Name);
//                row.CreateCell(3).SetCellValue(string.Join(",", item.Categorys.Select(p => p.Name).ToList()));
//                row.CreateCell(4).SetCellValue(string.Join(",", item.Tags.Select(p => p.Name).ToList()));
//                row.CreateCell(5).SetCellValue(item.PubAdmin.Name);
//                row.CreateCell(6).SetCellValue(item.PubDate.ToString("yyyy年MM月dd日"));
//                row.CreateCell(7).SetCellValue(item.ViewCount);
//                row.CreateCell(8).SetCellValue(item.ReplyCount);
//                row.CreateCell(9).SetCellValue(string.Format("{0}/{1}-{2}", Site.ROOT_URI, item.MainClass.Url, item.Url));//TODO:url格式

//                row.GetCell(0).CellStyle = GetRowTitle(hssfworkbook);
//                if (i % 2 == 0)
//                {
//                    CellStyle cs = GetRowEach(hssfworkbook);
//                    row.GetCell(1).CellStyle = cs;
//                    row.GetCell(2).CellStyle = cs;
//                    row.GetCell(3).CellStyle = cs;
//                    row.GetCell(4).CellStyle = cs;
//                    row.GetCell(5).CellStyle = cs;
//                    row.GetCell(6).CellStyle = cs;
//                    row.GetCell(7).CellStyle = cs;
//                    row.GetCell(8).CellStyle = cs;
//                    row.GetCell(9).CellStyle = cs;
//                }
//                else
//                {
//                    CellStyle cs = GetRowEach2(hssfworkbook);
//                    row.GetCell(1).CellStyle = cs;
//                    row.GetCell(2).CellStyle = cs;
//                    row.GetCell(3).CellStyle = cs;
//                    row.GetCell(4).CellStyle = cs;
//                    row.GetCell(5).CellStyle = cs;
//                    row.GetCell(6).CellStyle = cs;
//                    row.GetCell(7).CellStyle = cs;
//                    row.GetCell(8).CellStyle = cs;
//                    row.GetCell(9).CellStyle = cs;
//                }
//                i++;
//            }
//        }

//        private CellStyle GetRowTitle(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle style = hssfworkbook.CreateCellStyle();
//            Font font = hssfworkbook.CreateFont();
//            font.Boldweight = short.MaxValue;
//            font.FontName = "微软雅黑";
//            style.SetFont(font);
//            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
//            style.FillForegroundColor = 29;//前景色
//            style.FillBackgroundColor = 29;//背景色
//            return style;
//        }

//        private CellStyle GetRowEach(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle style = hssfworkbook.CreateCellStyle();
//            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
//            style.FillForegroundColor = 26;//前景色
//            style.FillBackgroundColor = 26;//背景色
//            return style;
//        }

//        private CellStyle GetRowEach2(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle style = hssfworkbook.CreateCellStyle();
//            style.FillPattern = FillPatternType.LEAST_DOTS;//前景图案
//            style.FillForegroundColor = 43;//前景色
//            style.FillBackgroundColor = 43;//背景色
//            return style;
//        }

//        #endregion

//        #region 计算合计

//        /// <summary>
//        /// 计算合计
//        /// </summary>
//        /// <param name="count"></param>
//        /// <param name="hssfworkbook"></param>
//        private void SetSum(int count, HSSFWorkbook hssfworkbook)
//        {
//            var sheet = hssfworkbook.GetSheetAt(0);

//            Row row = sheet.CreateRow(2 + count);

//            row.CreateCell(6).SetCellValue("合计:");
//            row.CreateCell(7).CellFormula = "sum(H3:H" + (2 + count) + ")";
//            row.CreateCell(8).CellFormula = "sum(I3:I" + (2 + count) + ")";

//            CellStyle style = GetSumStyle(hssfworkbook);
//            row.GetCell(6).CellStyle = style;
//            row.GetCell(7).CellStyle = style;
//            row.GetCell(8).CellStyle = style;
//        }

//        private CellStyle GetSumStyle(HSSFWorkbook hssfworkbook)
//        {
//            CellStyle tstyle = hssfworkbook.CreateCellStyle();
//            tstyle.Alignment = HorizontalAlignment.RIGHT;
//            Font font = hssfworkbook.CreateFont();
//            font.Boldweight = short.MaxValue;
//            tstyle.SetFont(font);
//            return tstyle;
//        }

//        #endregion

//        #region 插入批注

//        /// <summary>
//        /// 插入批注
//        /// </summary>
//        /// <param name="count"></param>
//        /// <param name="sheet"></param>
//        /// <param name="patriarch"></param>
//        private void SetComment(int count, Sheet sheet, Drawing patriarch)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append("这个Excel文件完全由代码生成,并非是在原有模板上填充数据.\r\n");
//            sb.Append("文件创建于" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss\r\n"));
//            sb.Append("\r\n给力啊,IT！！！");
//            sb.Append("\r\nhttp://www.woshinidezhu.com/");
//            sb.Append("\r\nhttp://woshinidezhu.cnblogs.com/");

//            /*****************************************************************
//             * HSSFClientAnchor:批注位置,8个参数分别表示
//             * {1}距离左边单元格的距离,1024为100%,即从右方一个单元格开始
//             * {2}距离上方单元格的距离,255为100%,即从下方一个单元格开始
//             * {3}右边单元格超出的距离,1024为100%,即充满到右方一个单
//             * {4}下方单元格超出的距离,255为100%,即充满到下方一个单元格
//             * {5}起始列
//             * {6}起始行
//             * {7}结束列
//             * {8}结束行
//             *****************************************************************/
//            Comment comment = patriarch.CreateCellComment(new HSSFClientAnchor(600, 100, 500, 200, 1, 3 + count, 7, 3 + 7 + count));
//            comment.String = new HSSFRichTextString(sb.ToString());
//            comment.Author = Site.NICKNAME;
//            comment.Visible = true;//默认打开即可见
//            sheet.GetRow(2 + count).CreateCell(0).CellComment = comment;
//        }

//        #endregion

//        #region 插入图片

//        /// <summary>
//        /// 插入图片
//        /// </summary>
//        /// <param name="count"></param>
//        /// <param name="hssfworkbook"></param>
//        /// <param name="patriarch"></param>
//        private void SetPic(int count, HSSFWorkbook hssfworkbook, Drawing patriarch)
//        {
//            byte[] bytes = System.IO.File.ReadAllBytes(Site.AVATAR_PATH);
//            int pictureIdx = hssfworkbook.AddPicture(bytes, PictureType.PNG);
//            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 1, 3 + count, 0, 0);
//            Picture pict = patriarch.CreatePicture(anchor, pictureIdx);
//            pict.Resize();
//        }

//        #endregion
//    }
//}
