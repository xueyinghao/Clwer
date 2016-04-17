/*-------------------------ImageHelper_extend类注释---------------------------------
 // 版权所有：Copyright (C) 2013 河南智森科技
 
 // 作者：吴晗
 
 // 创建日期：2013-10-17 09:49:16  
 
 // 文件名：ImageHelper_extend 
 
 // 功能描述：
 
 // 注意事项：

 
 // 遗留BUG：

 
 -------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Web;

namespace WJ.Infrastructure.Util
{
    public partial class ImageHelper
    {
        #region 上传图片 （有时候原图太大,存原图的话太浪费空间,这里根据高宽比例生成符合要求的图片）
        /// <summary>
        ///上传图片 （有时候原图太大,存原图的话太浪费空间,这里根据高宽比例生成符合要求的图片）
        /// </summary>
        /// <param name="PostFile">需要上传的图片</param>
        /// <param name="oldimgurl">旧的图片路径，用于删除</param>
        /// <param name="path">图片存位置</param>
        /// <param name="maxwidth">图片最大的宽度</param>
        /// <param name="maxheight">图片最大的高度</param>
        /// <param name="thumb_width">缩略图宽(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="thumb_height">缩略图高(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="imagename">图片名称，为空则为guid</param>
        /// <returns>缩略图图片的相对路径</returns>
        public static string UpImage(HttpPostedFile PostFile, string oldimgurl, string path, int maxwidth, int maxheight, int thumb_width, int thumb_height, string imagename, WaterMark waterMark = null, string mode = "HW")
        {
            string result = "";

            //if (!IOHelper.ValidateImageType(PostFile))
            //{
            //    return result;
            //}
            string extension = IOHelper.GetExtension(PostFile.FileName);

            string ImgFileName;//图片名称
            if (string.IsNullOrEmpty(imagename))
            {
                ImgFileName = Guid.NewGuid().ToString().Replace("-", "");
            }
            else
            {
                ImgFileName = imagename;
            }

            string fileName = ImgFileName + "." + extension; // 文件名称  
            string fileName_s = ImgFileName + "_s." + extension;                           // 缩略图文件名称加_s前缀
            string JueDuiUrl = HttpContext.Current.Server.MapPath(path);
            string xiangduiurl = path + "/" + fileName;
            string xiangduiurl_s = path + "/" + fileName_s;

            if (!Directory.Exists(JueDuiUrl))
            {
                Directory.CreateDirectory(JueDuiUrl);//如果不存在，则创建

            }

            string webFilePath = JueDuiUrl + "\\" + fileName;        // 服务器端文件路径 
            string webFilePath_s = JueDuiUrl + "\\" + fileName_s;　　// 服务器端缩略图路径 
            if (!File.Exists(webFilePath))
            {
                using (System.Drawing.Image original_image = System.Drawing.Image.FromStream(PostFile.InputStream))
                {

                    int width = original_image.Width;
                    int height = original_image.Height;
                    int new_width = width, new_height = height;
                    if (width > maxwidth || height > maxheight) //缩放
                    {
                        float target_ratio = (float)maxwidth / (float)maxheight;
                        float image_ratio = (float)width / (float)height;

                        if (target_ratio > image_ratio)
                        {
                            new_height = maxheight;
                            new_width = (int)Math.Floor(image_ratio * (float)maxheight);
                        }
                        else
                        {
                            new_height = (int)Math.Floor((float)maxwidth / image_ratio);
                            new_width = maxwidth;
                        }
                    }

                    using (System.Drawing.Bitmap final_image = new System.Drawing.Bitmap(new_width, new_height))
                    {
                        using (System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(final_image))
                        {
                            ////设置高质量插值法
                            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                            ////设置高质量,低速度呈现平滑程度
                            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;



                            graphic.DrawImage(original_image, new System.Drawing.Rectangle(0, 0, new_width, new_height),
              new System.Drawing.Rectangle(0, 0, original_image.Width, original_image.Height), GraphicsUnit.Pixel);

                            #region 加水印
                            if (waterMark != null && waterMark.WaterMarkType_Thumb != 0)
                            {
                                ImageCodecInfo ici = null;
                                //加文字水印
                                if (waterMark.WaterMarkType_Thumb == 1)
                                {
                                    Font drawFont = new Font(waterMark.WaterMarkFont, waterMark.WaterMarkFontSize, FontStyle.Regular, GraphicsUnit.Pixel);
                                    SizeF crSize;
                                    crSize = graphic.MeasureString(waterMark.WaterMarkText, drawFont);

                                    float xpos = 0;
                                    float ypos = 0;
                                    switch (waterMark.WaterMarkPosition)
                                    {
                                        case 1:
                                            xpos = (float)final_image.Width * (float).01;
                                            ypos = (float)final_image.Height * (float).01;
                                            break;
                                        case 2:
                                            xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                            ypos = (float)final_image.Height * (float).01;
                                            break;
                                        case 3:
                                            xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                            ypos = (float)final_image.Height * (float).01;
                                            break;
                                        case 4:
                                            xpos = (float)final_image.Width * (float).01;
                                            ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                            break;
                                        case 5:
                                            xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                            ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                            break;
                                        case 6:
                                            xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                            ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                            break;
                                        case 7:
                                            xpos = (float)final_image.Width * (float).01;
                                            ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                            break;
                                        case 8:
                                            xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                            ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                            break;
                                        case 9:
                                            xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                            ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                            break;
                                    }
                                    graphic.DrawString(waterMark.WaterMarkText, drawFont, new SolidBrush(Color.Red), xpos + 1, ypos + 1);
                                }
                                //大图加图片水印
                                else if (waterMark.WaterMarkType_Big == 2)
                                {
                                    string waterImgUrl = HttpContext.Current.Server.MapPath("/" + waterMark.WaterMarkPic);
                                    if (File.Exists(waterImgUrl)) //存在的话
                                    {
                                        using (Image watermark = new Bitmap(waterImgUrl))
                                        {
                                            if (watermark.Height >= final_image.Height || watermark.Width >= final_image.Width)
                                            {
                                                final_image.Save(webFilePath);
                                                goto thumb;
                                            }

                                            ImageAttributes imageAttributes = new ImageAttributes();
                                            ColorMap colorMap = new ColorMap();

                                            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                            ColorMap[] remapTable = { colorMap };

                                            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                            float transparency = 0.5F;
                                            if (waterMark.WaterMarkTransparency >= 1 && waterMark.WaterMarkTransparency <= 10)
                                                transparency = (waterMark.WaterMarkTransparency / 10.0F);


                                            float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

                                            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                                            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                            int xpos = 0;
                                            int ypos = 0;

                                            switch (waterMark.WaterMarkPosition)
                                            {
                                                case 1:
                                                    xpos = (int)(final_image.Width * (float).01);
                                                    ypos = (int)(final_image.Height * (float).01);
                                                    break;
                                                case 2:
                                                    xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                                    ypos = (int)(final_image.Height * (float).01);
                                                    break;
                                                case 3:
                                                    xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                                    ypos = (int)(final_image.Height * (float).01);
                                                    break;
                                                case 4:
                                                    xpos = (int)(final_image.Width * (float).01);
                                                    ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                                    break;
                                                case 5:
                                                    xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                                    ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                                    break;
                                                case 6:
                                                    xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                                    ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                                    break;
                                                case 7:
                                                    xpos = (int)(final_image.Width * (float).01);
                                                    ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                                    break;
                                                case 8:
                                                    xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                                    ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                                    break;
                                                case 9:
                                                    xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                                    ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                                    break;
                                            }

                                            graphic.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                                        }
                                    }
                                    else
                                    {
                                        final_image.Save(webFilePath);
                                        goto thumb;
                                    }
                                }


                                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

                                foreach (ImageCodecInfo codec in codecs)
                                {
                                    if (codec.MimeType.IndexOf("jpeg") > -1)
                                        ici = codec;
                                }
                                EncoderParameters encoderParams = new EncoderParameters();
                                long[] qualityParam = new long[1];
                                if (waterMark.WaterMarkImgQuality < 0 || waterMark.WaterMarkImgQuality > 100)
                                    waterMark.WaterMarkImgQuality = 80;

                                qualityParam[0] = waterMark.WaterMarkImgQuality;

                                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
                                encoderParams.Param[0] = encoderParam;


                                if (ici != null)
                                    final_image.Save(webFilePath, ici, encoderParams);
                                else
                                    final_image.Save(webFilePath);
                            }
                            #endregion

                            else //直接保存
                            {
                                final_image.Save(webFilePath);
                            }

                        thumb:
                            //生成缩略图
                            if (thumb_width != 0 && thumb_height != 0)
                            {
                                MakeThumbnail(original_image, webFilePath_s, thumb_width, thumb_height, waterMark, mode);
                                result = xiangduiurl_s;
                            }
                            else
                            {
                                result = xiangduiurl;
                            }
                        }

                    }
                }

            }

            if (!string.IsNullOrEmpty(oldimgurl))
            {
                IOHelper.DeleteImg(oldimgurl);
            }

            return result;
        }
        #endregion

        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="original_image"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="thumb_width"></param>
        /// <param name="thumb_height"></param>
        public static void MakeThumbnail(System.Drawing.Image original_image, string thumbnailPath, int thumb_width, int thumb_height, WaterMark waterMark = null, string mode = "HW")
        {
            // Calculate the new width and height
            int width = original_image.Width;
            int height = original_image.Height;
            int new_width = thumb_width, new_height = thumb_height;
            int x = 0, y = 0, m = 0, n = 0;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    new_height = original_image.Height * width / original_image.Width;
                    break;
                case "H"://指定高，宽按比例
                    new_width = original_image.Width * height / original_image.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）
                    if (original_image.Height > thumb_height || original_image.Width > thumb_width)
                    {
                        if ((double)original_image.Width / (double)original_image.Height > (double)thumb_width / (double)thumb_height)
                        {
                            height = original_image.Height;
                            width = original_image.Height * thumb_width / thumb_height;

                        }
                        else
                        {
                            width = original_image.Width;
                            height = original_image.Width * height / thumb_width;

                        }
                    }
                    break;
                case "Fill":
                    if (original_image.Height > thumb_height || original_image.Width > thumb_width)
                    {
                        float target_ratio = (float)thumb_width / (float)thumb_height;
                        float image_ratio = (float)width / (float)height;

                        if (target_ratio > image_ratio)
                        {
                            new_height = thumb_height;
                            new_width = (int)Math.Floor(image_ratio * (float)thumb_height);
                        }
                        else
                        {
                            new_height = (int)Math.Floor((float)thumb_width / image_ratio);
                            new_width = thumb_width;
                        }

                        new_width = new_width > thumb_width ? thumb_width : new_width;
                        new_height = new_height > thumb_height ? thumb_height : new_height;
                        m = (thumb_width - new_width) / 2;
                        n = (thumb_height - new_height) / 2;
                    }
                    break;

            }


            using (System.Drawing.Bitmap final_image = new System.Drawing.Bitmap(thumb_width, thumb_height))
            {
                using (System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(final_image))
                {
                    graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Transparent), new System.Drawing.Rectangle(0, 0, thumb_width, thumb_height));

                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                    //graphic.DrawImage(original_image, paste_x, paste_y, original_image.Width, original_image.Height);

                    graphic.DrawImage(original_image, new System.Drawing.Rectangle(m, n, new_width, new_height),
             new System.Drawing.Rectangle(x, y, original_image.Width, original_image.Height),
             System.Drawing.GraphicsUnit.Pixel);

                    #region 加水印
                    if (waterMark != null && waterMark.WaterMarkType_Thumb != 0)
                    {
                        ImageCodecInfo ici = null;
                        //加文字水印
                        if (waterMark.WaterMarkType_Thumb == 1)
                        {
                            Font drawFont = new Font(waterMark.WaterMarkFont, waterMark.WaterMarkFontSize, FontStyle.Regular, GraphicsUnit.Pixel);
                            SizeF crSize;
                            crSize = graphic.MeasureString(waterMark.WaterMarkText, drawFont);

                            float xpos = 0;
                            float ypos = 0;
                            switch (waterMark.WaterMarkPosition)
                            {
                                case 1:
                                    xpos = (float)final_image.Width * (float).01;
                                    ypos = (float)final_image.Height * (float).01;
                                    break;
                                case 2:
                                    xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                    ypos = (float)final_image.Height * (float).01;
                                    break;
                                case 3:
                                    xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                    ypos = (float)final_image.Height * (float).01;
                                    break;
                                case 4:
                                    xpos = (float)final_image.Width * (float).01;
                                    ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                    break;
                                case 5:
                                    xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                    ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                    break;
                                case 6:
                                    xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                    ypos = ((float)final_image.Height * (float).50) - (crSize.Height / 2);
                                    break;
                                case 7:
                                    xpos = (float)final_image.Width * (float).01;
                                    ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                    break;
                                case 8:
                                    xpos = ((float)final_image.Width * (float).50) - (crSize.Width / 2);
                                    ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                    break;
                                case 9:
                                    xpos = ((float)final_image.Width * (float).99) - crSize.Width;
                                    ypos = ((float)final_image.Height * (float).99) - crSize.Height;
                                    break;
                            }
                            graphic.DrawString(waterMark.WaterMarkText, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
                            graphic.DrawString(waterMark.WaterMarkText, drawFont, new SolidBrush(Color.Black), xpos, ypos);
                        }
                        //大图加图片水印
                        else if (waterMark.WaterMarkType_Thumb == 2)
                        {
                            string waterImgUrl = HttpContext.Current.Server.MapPath("/" + waterMark.WaterMarkPic);
                            if (File.Exists(waterImgUrl)) //存在的话
                            {
                                using (Image watermark = new Bitmap(waterImgUrl))
                                {
                                    if (watermark.Height >= final_image.Height || watermark.Width >= final_image.Width)
                                    {
                                        final_image.Save(thumbnailPath);
                                        return;
                                    }

                                    ImageAttributes imageAttributes = new ImageAttributes();
                                    ColorMap colorMap = new ColorMap();

                                    colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                    colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                    ColorMap[] remapTable = { colorMap };

                                    imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                    float transparency = 0.5F;
                                    if (waterMark.WaterMarkTransparency >= 1 && waterMark.WaterMarkTransparency <= 10)
                                        transparency = (waterMark.WaterMarkTransparency / 10.0F);


                                    float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

                                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                                    imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    int xpos = 0;
                                    int ypos = 0;

                                    switch (waterMark.WaterMarkPosition)
                                    {
                                        case 1:
                                            xpos = (int)(final_image.Width * (float).01);
                                            ypos = (int)(final_image.Height * (float).01);
                                            break;
                                        case 2:
                                            xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                            ypos = (int)(final_image.Height * (float).01);
                                            break;
                                        case 3:
                                            xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                            ypos = (int)(final_image.Height * (float).01);
                                            break;
                                        case 4:
                                            xpos = (int)(final_image.Width * (float).01);
                                            ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                            break;
                                        case 5:
                                            xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                            ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                            break;
                                        case 6:
                                            xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                            ypos = (int)((final_image.Height * (float).50) - (watermark.Height / 2));
                                            break;
                                        case 7:
                                            xpos = (int)(final_image.Width * (float).01);
                                            ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                            break;
                                        case 8:
                                            xpos = (int)((final_image.Width * (float).50) - (watermark.Width / 2));
                                            ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                            break;
                                        case 9:
                                            xpos = (int)((final_image.Width * (float).99) - (watermark.Width));
                                            ypos = (int)((final_image.Height * (float).99) - watermark.Height);
                                            break;
                                    }

                                    graphic.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                                }
                            }
                            else
                            {
                                final_image.Save(thumbnailPath);
                                return;
                            }
                        }
                        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

                        foreach (ImageCodecInfo codec in codecs)
                        {
                            if (codec.MimeType.IndexOf("jpeg") > -1)
                                ici = codec;
                        }
                        EncoderParameters encoderParams = new EncoderParameters();
                        long[] qualityParam = new long[1];
                        if (waterMark.WaterMarkImgQuality < 0 || waterMark.WaterMarkImgQuality > 100)
                            waterMark.WaterMarkImgQuality = 80;

                        qualityParam[0] = waterMark.WaterMarkImgQuality;

                        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
                        encoderParams.Param[0] = encoderParam;


                        if (ici != null)
                            final_image.Save(thumbnailPath, ici, encoderParams);
                        else
                            final_image.Save(thumbnailPath);
                    }
                    #endregion
                    else
                    {
                        final_image.Save(thumbnailPath);
                    }
                }
            }
        }
        #endregion
    }
    public class WaterMark
    {
        /// <summary>
        /// 类型  0表示关闭水印 1表示文字水印 2表示图片水印
        /// </summary>
        public int WaterMarkType_Big { get; set; }

        /// <summary>
        /// 类型  0表示关闭水印 1表示文字水印 2表示图片水印
        /// </summary>
        public int WaterMarkType_Thumb { get; set; }

        /// <summary>
        /// 0左上 1中上 2右上 3左中 4居中 5右中 6左下 7中下 8右下
        /// </summary>
        public int WaterMarkPosition { get; set; }

        /// <summary>
        /// 适用于加水印的jpeg格式图片
        /// </summary>
        public int WaterMarkImgQuality { get; set; }

        /// <summary>
        /// 图片水印的图片名称
        /// </summary>
        public string WaterMarkPic { get; set; }

        /// <summary>
        /// 水印的透明度
        /// </summary>
        public int WaterMarkTransparency { get; set; }

        /// <summary>
        /// 文字水印文字的名称
        /// </summary>
        public string WaterMarkText { get; set; }

        /// <summary>
        /// 文字水印 字体
        /// </summary>
        public string WaterMarkFont { get; set; }

        /// <summary>
        /// 文字水印 字体大小
        /// </summary>
        public int WaterMarkFontSize { get; set; }
    }
}
