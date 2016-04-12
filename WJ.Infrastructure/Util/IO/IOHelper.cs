using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;
 
namespace WJ.Infrastructure.Util
{
    public partial class IOHelper
    {
        #region 删除图片
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteImg(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            string serverpath = DNTUtils.GetMapPath(path);
            FileInfo file = new FileInfo(serverpath);
            if (File.Exists(serverpath))
            {
                File.Delete(serverpath);
            }
            if (File.Exists(serverpath.Replace("_s", "")))
            {
                File.Delete(serverpath.Replace("_s", ""));
            }
        }
        #endregion

        #region 获取文件扩展名
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }
        #endregion

        #region 删除文章中使用的图片
        /// <summary>
        /// 删除文章中使用的图片
        /// </summary>
        /// <param name="content"></param>
        public static void clearimg(string content)
        {
            try
            {
                string[] url = RegHelper.MyGetImgUrl(content).Split(',');
                foreach (string u in url)
                {
                    if (u != "")
                        DeleteImg(u);
                }
            }
            catch
            { }
        }
        #endregion

        #region 上传图片
        /// <summary>
        ///上传图片 
        /// </summary>
        /// <param name="stream">需要上传的图片</param>
        /// <param name="oldimgurl">旧的图片路径，用于删除</param>
        /// <param name="path">图片存位置</param>
        /// <param name="width">缩略图宽(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="height">缩略图高(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="imagename">图片名称，为空则为guid</param>
        /// <returns>缩略图图片的相对路径</returns>
        public static string UpImage(Stream stream, string oldimgurl, string path, int width, int height, string imagename, string extension)
        {
            string result = "";
            string ImgFileName;//图片名称
            if (string.IsNullOrEmpty(imagename))
            {
                ImgFileName = Guid.NewGuid().ToString();
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
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    image.Save(webFilePath);
                }

                if (height != 0 && width != 0)
                {
                    ImageHelper.MakeThumbnail(stream, webFilePath_s, width, height, "HW");
                    result = xiangduiurl_s;
                }
                else
                {
                    result = xiangduiurl;
                }
                if (!string.IsNullOrEmpty(oldimgurl))
                {
                    DeleteImg(oldimgurl);
                }
            } 
            return result;
        }

        /// <summary>
        ///上传图片 
        /// </summary>
        /// <param name="PostFile">需要上传的图片</param>
        /// <param name="oldimgurl">旧的图片路径，用于删除</param>
        /// <param name="path">图片存位置</param>
        /// <param name="width">缩略图宽(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="height">缩略图高(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="imagename">图片名称，为空则为guid</param>
        /// <returns>缩略图图片的相对路径</returns>
        public static string UpImage(HttpPostedFile PostFile, string oldimgurl, string path, int width, int height, string imagename)
        {
            string result = "";
            if (!ValidateImageType(PostFile))
            {
                return result;
            }
            string extension = GetExtension(PostFile.FileName);
            result = UpImage(PostFile.InputStream, oldimgurl, path, width, height, imagename, extension);

            return result;
        }

        /// <summary>
        ///上传图片   手机版用
        /// </summary>
        /// <param name="PostFile">需要上传的图片</param>
        /// <param name="oldimgurl">旧的图片路径，用于删除</param>
        /// <param name="path">图片存位置</param>
        /// <param name="width">缩略图宽(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="height">缩略图高(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="imagename">图片名称，为空则为guid</param>
        /// <returns>缩略图图片的相对路径</returns>
        public static string UpImage(HttpPostedFile PostFile, string oldimgurl, string path, int width, int height, string imagename, string meiyong)
        {
            string result = "";
            string extension = GetExtension(PostFile.FileName);
            result = UpImage(PostFile.InputStream, oldimgurl, path, width, height, imagename, extension);

            return result;
        }
         
        /// <summary>
        ///上传图片 
        /// </summary>
        /// <param name="PostFile">需要上传的图片</param>
        /// <param name="oldimgurl">旧的图片路径，用于删除</param>
        /// <param name="path">图片存位置</param>
        /// <param name="width">缩略图宽(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="height">缩略图高(缩略图宽高都为0则不生成缩略图)</param>
        /// <param name="imagename">图片名称，为空则为guid</param>
        /// <returns>缩略图图片的相对路径</returns>
        public static string UpImage(byte[] filebyte, string oldimgurl, string path, int width, int height, string imagename, string extension)
        {
            return UpImage(
                new MemoryStream(filebyte),
                oldimgurl, path, width, height, imagename, extension);
        }
         
        /// <summary>
        /// 上传图片 根据图片的Base64编码
        /// </summary>
        /// <returns></returns>
        public static string UpImage(string base64Str, string extension, string oldimgurl, string path, int width, int height, string imagename)
        {
            string result = "";
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(base64Str);
            }
            catch
            {
                return result;
            }
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                result = UpImage(stream, oldimgurl, path, width, height, imagename, extension);
            }
            return result;
        }
         
        /// <summary>
        /// 验证是否是图片
        /// </summary>
        /// <param name="PostFile"></param>
        /// <returns></returns>
        public static bool ValidateImageType(HttpPostedFile PostFile)
        {
            return PostFile.ContentType.StartsWith("image");
        }

        /// <summary>
        /// 验证图片大小
        /// </summary>
        /// <param name="PostFile"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ValidateImageLength(HttpPostedFile PostFile, long length)
        {
            return PostFile.InputStream.Length <= length;
        }
        #endregion

        #region 读取文件（默认编码）
        /// <summary>
        /// 读取文件（默认编码）
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string Read(string path)
        {
            return Read(path, Encoding.Default);
        }
        #endregion

        #region 读取文件（指定编码）
        /// <summary>
        /// 读取文件（指定编码）
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static string Read(string path, Encoding encode)
        {
            FileStream fs = null;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    fs = new FileStream(DNTUtils.GetMapPath(path), FileMode.Open, FileAccess.Read, FileShare.Read);
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
            if (fs == null) return "";
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fs, encode);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }
        #endregion

        #region 返回文件行的数组
        /// <summary>
        /// 返回文件行的数组
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string[] ReadLines(string path)
        {
            return ReadLines(path, Encoding.Default);
        }
        /// <summary>
        /// 返回文件行的数组
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="encode">编码方式</param>
        /// <returns></returns>
        public static string[] ReadLines(string path, Encoding encode)
        {
            try
            {
                return File.ReadAllLines(DNTUtils.GetMapPath(path), encode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 向文件写入内容（默认编码）
        /// <summary>
        /// 向文件写入内容（默认编码）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool Write(string path, string content)
        {
            return Write(path, content, Encoding.Default);
        }
        #endregion

        #region 向文件写入内容（指定编码）
        /// <summary>
        /// 向文件写入内容（指定编码）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static bool Write(string path, string content, Encoding encode)
        {
            FileStream fs = null;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    fs = new FileStream(DNTUtils.GetMapPath(path), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
            if (fs == null) return false;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fs, encode);
                sw.Write(content);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
        #endregion

        #region 向文件追加内容（默认编码）
        /// <summary>
        /// 向文件追加内容（默认编码）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool Append(string path, string content)
        {
            return Append(path, content, Encoding.Default);
        }
        #endregion

        #region 向文件追加内容（指定编码）
        /// <summary>
        /// 向文件追加内容（指定编码）
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <param name="encode">编码</param>
        /// <returns></returns>
        public static bool Append(string path, string content, Encoding encode)
        {
            FileStream fs = null;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    fs = new FileStream(DNTUtils.GetMapPath(path), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
            if (fs == null) return false;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fs, encode);
                sw.Write(content);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }
        #endregion

        #region 删除指定文件
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool Delete(string path)
        {
            try
            {
                path = DNTUtils.GetMapPath(path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取指定目录下所有文件
        /// <summary>
        /// 获取指定目录下所有文件
        /// </summary>
        /// <param name="dir">目录</param>
        /// <returns></returns>
        public static string[] GetFiles(string dir)
        {
            try
            {
                return Directory.GetFiles(DNTUtils.GetMapPath(dir));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取目录中所有文件路径
        /// <summary>
        /// 获取目录中所有文件路径
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="patt">匹配正则(如：*.(txt|log))</param>
        /// <returns></returns>
        public static List<string> GetFiles(string dir, string patt)
        {
            string pattern = "(?i)^" + patt.Replace(".", @"\.") + "$";

            Regex reg = new Regex(pattern);

            List<string> list = new List<string>();

            try
            {
                string[] files = Directory.GetFiles(DNTUtils.GetMapPath(dir));

                foreach (string file in files)
                {
                    if (reg.IsMatch(file))
                        list.Add(file);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        #endregion

        #region 根据文件路径，或文件夹路径，创建文件夹
        /// <summary>
        /// 根据文件路径，或文件夹路径，创建文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void IfNotExistsCreateDir(string dir)
        {
            dir = DNTUtils.GetMapPath(dir);
            if (dir.Contains("."))
            {
                dir = dir.Substring(0, dir.LastIndexOf("\\"));
            }
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        #endregion

        #region 返回所给文件路径的字节数组
        /// <summary>
        /// getBinaryFile：返回所给文件路径的字节数组。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] getBinaryFile(string filename)
        {
            filename = DNTUtils.GetMapPath(filename);
            if (File.Exists(filename))
            {
                try
                {
                    ///打开现有文件以进行读取。
                    FileStream s = File.OpenRead(filename);
                    return ConvertStreamToByteBuffer(s);
                }
                catch
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }
        #endregion

        #region 把给定的文件流转换为二进制字节数组
        /// <summary>
        /// ConvertStreamToByteBuffer：把给定的文件流转换为二进制字节数组。
        /// </summary>
        /// <param name="theStream"></param>
        /// <returns></returns>
        public static byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
        {
            int b1;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            while ((b1 = theStream.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream.ToArray();
        }
        #endregion

        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(DNTUtils.GetMapPath(fileName), FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(bytes);
            }
            finally
            {
                bw.Close();
                fs.Close();
            }
        } 
        #endregion

        #region 读取文件
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(DNTUtils.GetMapPath(fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            try
            {
                fileStream.Read(bytes, 0, bytes.Length);
            }
            finally
            {
                fileStream.Close();
            }
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        } 
        #endregion


    }
}
