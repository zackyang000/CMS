//===========================================================
// Copyright @ 2010 YangKai. All Rights Reserved.
// Framework: 4.0
// Author: 杨凯
// Email: yangkai-13896222@sohu.com
// QQ: 83448327
// CreateTime: 2/1/2011 10:27:19 PM
//===========================================================

using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;
using System.Xml;
using System.Web;
using System;

namespace AtomLab.Utility
{
    public class ImageHelper
    {
        public static string CreateWeaterText(string paraOldFileName, string paraNewsFileName, string AddText, string FileExtensionName)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(paraOldFileName);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);

            //文字抗锯齿
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.DrawImage(image, 0, 0, image.Width, image.Height);
            Font font = new Font("黑体", 14, FontStyle.Bold);
            Brush b = new SolidBrush(Color.Yellow);

            int Xpos = 0;
            int Ypos = 0;
            SizeF crsize = new SizeF();
            crsize = g.MeasureString(AddText, font);
            int crsize_With = Convert.ToInt32(crsize.Width);
            int crsize_Height = Convert.ToInt32(crsize.Height);

            int WaterMark_xPos = 10;
            int WaterMark_yPos = 10;

            int WaterMark_Position = 4;

            switch (WaterMark_Position)//==========从图片的左上角开始算起(0,0)
            {
                case 1:
                    Xpos = WaterMark_xPos;
                    Ypos = WaterMark_yPos;
                    break;
                case 2:
                    Xpos = image.Width - (crsize_With + WaterMark_xPos);
                    Ypos = WaterMark_yPos;
                    break;
                case 3:
                    Xpos = WaterMark_xPos;
                    Ypos = image.Height - (crsize_Height + WaterMark_yPos);
                    break;
                case 4:
                    Xpos = image.Width - (crsize_With + WaterMark_xPos);
                    Ypos = image.Height - (crsize_Height + WaterMark_yPos);
                    break;
            }

            //画阴影
            PointF pt = new PointF(0, 0);
            System.Drawing.Brush TransparentBrush0 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(50, System.Drawing.Color.Black));
            System.Drawing.Brush TransparentBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(20, System.Drawing.Color.Black));

            g.DrawString(AddText, font, TransparentBrush0, Xpos, Ypos + 1);
            g.DrawString(AddText, font, TransparentBrush0, Xpos + 1, Ypos);
            g.DrawString(AddText, font, TransparentBrush1, Xpos + 1, Ypos + 1);
            g.DrawString(AddText, font, TransparentBrush1, Xpos, Ypos + 2);
            g.DrawString(AddText, font, TransparentBrush1, Xpos + 2, Ypos);

            TransparentBrush0.Dispose();
            TransparentBrush1.Dispose();


            g.DrawString(AddText, font, b, Xpos, Ypos);

            switch (FileExtensionName.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".gif":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case ".bmp":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".png":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
            g.Dispose();

            image.Save(paraNewsFileName);
            image.Dispose();
            HttpContext.Current.Response.Clear();

            if (File.Exists(paraOldFileName) == true)
            {
                File.Delete(paraOldFileName);
            }

            return paraNewsFileName;

        }

        public static string CreateWeaterPicture(string paraOldFileName, string paraNewsFileName, string AddPicture, string FileExtensionName)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(paraOldFileName);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(AddPicture);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(200, 0, 200, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            Single WaterMark_Transparency = 0.9f;
            float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  WaterMark_Transparency, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


            Graphics g = Graphics.FromImage(image);


            //设定合成图像的质量
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            int xPos = image.Width - copyImage.Width - 10;
            int yPos = image.Height - copyImage.Height - 10;

            int WaterMark_Position = 4;

            switch (WaterMark_Position)//==========从图片的左上角开始算起(0,0)
            {
                case 1:
                    xPos = 10;
                    yPos = 10;
                    break;
                case 2:
                    xPos = image.Width - copyImage.Width - 10;
                    yPos = 10;
                    break;
                case 3:
                    xPos = 10;
                    yPos = image.Height - copyImage.Height - 10;
                    break;
                case 4:
                    xPos = image.Width - copyImage.Width - 10;
                    yPos = image.Height - copyImage.Height - 10;
                    break;
            }

            g.DrawImage(copyImage, new Rectangle(xPos, yPos, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel, imageAttributes);

            switch (FileExtensionName.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".gif":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case ".bmp":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".png":
                    image.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
            g.Dispose();

            image.Save(paraNewsFileName);
            image.Dispose();
            imageAttributes.Dispose();

            HttpContext.Current.Response.Clear();

            if (File.Exists(paraOldFileName) == true)
            {
                File.Delete(paraOldFileName);
            }

            return paraNewsFileName;
        }
    }
}
