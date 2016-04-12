using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace WJ.Infrastructure.Util
{
    public partial class ImageHelper
    {
        public static void MakeThumbnail(System.Drawing.Image originalImage, string thumbnailPath, int width, int height, string mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充

            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图

                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(Stream stream, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(stream);

            MakeThumbnail(originalImage, thumbnailPath, width, height, mode);
        }




        /// <summary>    
        /// 在图片上增加文字水印    
        /// </summary>    
        /// <param name="Path">原服务器图片路径</param>    
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>    
        public static void AddWater(string Path, string Path_sy)
        {

            string addText = "";
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 60);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            g.DrawString(addText, f, b, 35, 35);

            g.Dispose();
            image.Save(Path_sy);
            image.Dispose();
        }

        /// <summary>    
        /// 在图片上生成图片水印    
        /// </summary>    
        /// <param name="Path">原服务器图片路径</param>    
        /// <param name="Path_syp">生成的带图片水印的图片路径</param>    
        /// <param name="Path_sypf">水印图片路径</param>    
        public static void AddWaterPic(string Path, string Path_syp, string Path_sypf)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            image.Save(Path_syp);
            image.Dispose();
        }

        /// <summary>    
        /// 给图片上水印    
        /// </summary>    
        /// <param name="waterFile">水印图片地址</param>    
        public static void MarkWater(string filePaths, string waterFile)
        {
            //GIF不水印    
            int i = filePaths.LastIndexOf(".");
            string ex = filePaths.Substring(i, filePaths.Length - i);
            if (string.Compare(ex, ".gif", true) == 0)
            {
                return;
            }
            string ModifyImagePath = filePaths;// FilePath + filePaths;//修改的图像路径    
            int lucencyPercent = 25;
            System.Drawing.Image modifyImage = null;
            System.Drawing.Image drawedImage = null;
            Graphics g = null;
            try
            {
                //建立图形对象    
                modifyImage = System.Drawing.Image.FromFile(ModifyImagePath, true);
                //  drawedImage = System.Drawing.Image.FromFile(FilePath + waterFile, true);   
                drawedImage = System.Drawing.Image.FromFile(waterFile, true);
                g = Graphics.FromImage(modifyImage);
                //获取要绘制图形坐标    
                int x = modifyImage.Width - drawedImage.Width;
                int y = modifyImage.Height - drawedImage.Height;
                //设置颜色矩阵    
                float[][] matrixItems ={     
        new float[] {1, 0, 0, 0, 0},     
        new float[] {0, 1, 0, 0, 0},     
        new float[] {0, 0, 1, 0, 0},     
        new float[] {0, 0, 0, (float)lucencyPercent/1f, 0},     
        new float[] {0, 0, 0, 0, 1}};
                ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //绘制阴影图像    
                g.DrawImage(drawedImage, new Rectangle(x, y, drawedImage.Width, drawedImage.Height), 0, 0, drawedImage.Width, drawedImage.Height, GraphicsUnit.Pixel, imgAttr);
                //保存文件    
                string[] allowImageType = { ".jpg", ".gif", ".png", ".bmp", ".tiff", ".wmf", ".ico" };
                FileInfo fi = new FileInfo(ModifyImagePath);
                ImageFormat imageType = ImageFormat.Gif;
                switch (fi.Extension.ToLower())
                {
                    case ".jpg":
                        imageType = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        imageType = ImageFormat.Gif;
                        break;
                    case ".png":
                        imageType = ImageFormat.Png;
                        break;
                    case ".bmp":
                        imageType = ImageFormat.Bmp;
                        break;
                    case ".tif":
                        imageType = ImageFormat.Tiff;
                        break;
                    case ".wmf":
                        imageType = ImageFormat.Wmf;
                        break;
                    case ".ico":
                        imageType = ImageFormat.Icon;
                        break;
                    default:
                        break;
                }
                MemoryStream ms = new MemoryStream();
                modifyImage.Save(ms, imageType);
                byte[] imgData = ms.ToArray();
                modifyImage.Dispose();
                drawedImage.Dispose();
                g.Dispose();
                FileStream fs = null;
                File.Delete(ModifyImagePath);
                fs = new FileStream(ModifyImagePath, FileMode.Create, FileAccess.Write);
                if (fs != null)
                {
                    fs.Write(imgData, 0, imgData.Length);
                    fs.Close();
                }
            }
            finally
            {
                try
                {
                    drawedImage.Dispose();
                    modifyImage.Dispose();
                    g.Dispose();
                }
                catch { }
            }
        }
    }
}
