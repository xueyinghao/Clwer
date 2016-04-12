using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;

namespace WJ.Infrastructure.Util
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IOHelper
    {
        #region 判断远程服务器文件是否存在
        /// <summary>
        /// 判断远程服务器文件是否存在
        /// </summary>
        /// <param name="host"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RemoteFileExists(string host, string filePath)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "isFileExist");
            client.PostingData.Add("fileName", filePath);
            string res = client.GetString();
            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判读远程服务器文件夹是否存在
        /// <summary>
        /// 判读远程服务器文件夹是否存在
        /// </summary>
        /// <returns></returns>
        public static bool RemoteDirExists(string host, string dirPath)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "isDirExist");
            client.PostingData.Add("dirName", dirPath);
            string res = client.GetString();
            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 向远程服务器上传xml文件
        /// <summary>
        /// 向远程服务器上传xml文件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="filePath"></param>
        /// <param name="o">将要序列化xml的对象</param>
        /// <returns></returns>
        public static bool RemotePutFile(string host, string filePath, object o)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "putFile");
            client.PostingData.Add("filePath", filePath);
            client.AttachFile(SerializerUtil.SaveRomoteXml(o), Path.GetFileName(filePath), "file", "text/XML");
            string res = client.GetString();
            string xmlpath = HttpContext.Current.Server.MapPath(ConfigUtil.GetConfig("ErrorLogPath") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
            Dictionary<string,string> dic=new Dictionary<string,string>();
            dic.Add("服务器返回值", res);
            if (XmlUtil.CreateDocument(xmlpath, "root"))
            {
                XmlUtil.InsertElement(xmlpath, "root", "error", dic, "");
            }

            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 远程复制文件
        /// <summary>
        /// 远程复制文件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// <returns></returns>
        public static bool RemoteCopyFile(string host, string sourceFilePath, string targetFilePath)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "copyFile");
            client.PostingData.Add("sourceFilePath", sourceFilePath);
            client.PostingData.Add("targetFilePath", targetFilePath);
            string res = client.GetString();
            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 远程删除文件
        /// <summary>
        /// 远程删除文件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RemoteDeletFile(string host, string filePath)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "deleteFile");
            client.PostingData.Add("fileName", filePath);
            string res = client.GetString();
            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 远程创建文件夹
        /// <summary>
        /// 远程创建文件夹
        /// </summary>
        /// <param name="host"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RemoteCreateDirectory(string host, string dirPath)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/ClientServices/Utilts.ashx";
            client.PostingData.Add("action", "createDir");
            client.PostingData.Add("dirName", dirPath);
            string res = client.GetString();
            if (res == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        #endregion

        #region 远程上传图片
        /// <summary>
        /// 远程上传图片
        /// </summary>
        /// <param name="host">服务器地址</param>
        /// <param name="base64Str">base64流</param>
        /// <param name="ext">文件格式 图片类型格式</param>
        /// <returns></returns>
        public static string UploadImage(string host, string base64Str, string ext)
        {
            byte[] bytes = Encoding.Default.GetBytes(base64Str);
            string result = "";
            byte[] buffer;
            try
            {
                buffer = Convert.FromBase64String(base64Str);
                HttpClient client = new HttpClient();
                client.Url = host + "/ClientServices/UploadImg.ashx";
                client.AttachFile(buffer, "eee." + ext, "file", "application/octet-stream");
                result = client.GetString();
                return result;
            }
            catch
            {
                return "";
            }
        }
        #endregion
        
    }
}
