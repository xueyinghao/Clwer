using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Spider1
{
    public  class download
    {
        //记录下载失败的次数
        private static int Failcount = 0;
        static byte[] writebytes = new byte[10000];
        public static string GetHtml(string url)
        {
            string str = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 10000;  //下载超时时间
                request.Headers.Set("Pragma", "no-cache");
                WebResponse response = request.GetResponse();
                string contentType = response.ContentType;      //获取后缀名
                byte[] buffer = ReadInstreamIntoMemory(response.GetResponseStream());
                response.Close();
                if (!Directory.Exists(Settings.DownloadFolder))
                {
                    Directory.CreateDirectory(Settings.DownloadFolder);
                }
                string extension = GetExtensionByMimeType(contentType);
                if (extension.Contains("charset"))
                {
                    extension = extension.Substring(0, 3);
                }
                string md5 = ToBase64(url);
                //string md5 = Hash(url);
                string filename = Path.Combine(Settings.DownloadFolder, md5 + "." + extension);
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();

                //Stream streamReceive = response.GetResponseStream();
                //Encoding encoding = Encoding.GetEncoding("utf-8");
                //StreamReader streamReader = new StreamReader(streamReceive, encoding);
                //str = streamReader.ReadToEnd();
                //streamReader.Close();
            }
            catch (Exception ex)
            {
                Failcount++;
                if (Failcount > 5)
                {
                    //var result = System.Windows.Forms.MessageBox.Show("已下载失败" + Failcount + "次，是否要继续尝试？" + Environment.NewLine + ex.ToString(), "数据下载异常", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error);
                    //if (result == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    str = GetHtml(url);
                    //}
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show("下载HTML失败" + Environment.NewLine + ex.ToString(), "下载HTML失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //    throw ex;
                    //}
                    string ErrMessage = url + ex.Message;
                    DownloadErrLog(ErrMessage);
                }
                else
                {
                    str = GetHtml(url);
                }
            }
            Failcount = 0;
            return str;
           
           
        }
        //记录下载出现的错误
        public static void DownloadErrLog(string str)
        {
            writebytes = Encoding.UTF8.GetBytes(str + "\r\n\r\n");
            //之前的Save方法 Save(byte[] write)
            FileStream file = new FileStream("d:\\DownloadErrLog.txt", FileMode.Append, FileAccess.Write);
            file.Write(writebytes, 0, writebytes.Length);
            file.Dispose();
            file.Close();
            writebytes.Initialize();
        }



        //获取后缀名
        public static string GetExtensionByMimeType(string mimeType)
        {
            int pos;
            if ((pos = mimeType.IndexOf('/')) != -1)
            {
                return mimeType.Substring(pos + 1);
            }
            return string.Empty;
        }
        private static byte[] ReadInstreamIntoMemory(Stream stream)
        {
            int bufferSize = 16384;
            byte[] buffer = new byte[bufferSize];
            MemoryStream ms = new MemoryStream();
            while (true)
            {
                int numBytesRead = stream.Read(buffer, 0, bufferSize);
                if (numBytesRead <= 0) break;
                ms.Write(buffer, 0, numBytesRead);
            }
            return ms.ToArray();
        }

        public static string Hash(string url)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(url);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        //字符串使用Base64转码
        //此处对url进行压缩
        public static string ToBase64(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }
        //Base64解码
        public static string FromBase64(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Convert.ToBase64String(bytes);
        }
    }
}
