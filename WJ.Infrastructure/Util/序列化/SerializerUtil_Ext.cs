using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace WJ.Infrastructure.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class SerializerUtil
    {
        #region xml文件反序列化
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object LoadRemoteXml(string host, string filePath, System.Type type)
        {
            HttpClient client = new HttpClient();
            client.Url = host + "/" + filePath;
            client.Context.Cookies.Add(new Cookie("4", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            client.Context.Cookies.Add(new Cookie("0", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            client.Context.Cookies.Add(new Cookie("1", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            client.Context.Cookies.Add(new Cookie("2", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            client.Context.Cookies.Add(new Cookie("9", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            client.Context.Cookies.Add(new Cookie("10", SecurityUtils.EncryptAES("1"), "/", ConfigUtil.GetConfig("domain")));
            try
            {
                using (Stream stream = client.GetStream())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8))
                    {
                        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
                        try
                        {
                            object obj = xs.Deserialize(reader);
                            reader.Close();
                            return obj;
                        }
                        catch (Exception e)
                        {
                            reader.Close();
                            return null;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 序列化为xml文件流
        /// <summary>
        /// 序列化为xml文件流
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SaveRomoteXml(object obj)
        {
            MemoryStream ms = new MemoryStream();
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ms))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                xs.Serialize(writer, obj);
                writer.Close();

                return ms.ToArray();
            }
        }
        #endregion
        #region 序列化为xml文件
        /// <summary>
        /// 序列化为xml
        /// </summary>
        /// <param name="filePath">绝对路经</param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        public static void SaveXmlnew(string filePath, object obj)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                    xs.Serialize(writer, obj);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
