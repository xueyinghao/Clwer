using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace WJ.Infrastructure.Util
{
    public static partial class SerializerUtil
    {

        #region 序列化位二进制
        ///   <summary>   
        ///   序列化位二进制   
        ///   </summary>   
        ///   <param   name="request">要序列化的对象</param>   
        ///   <returns>字节数组</returns>   
        public static byte[] SerializeBinary(object request)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                serializer.Serialize(memStream, request);
                return memStream.GetBuffer();
            }
        }
        #endregion

        #region 二进制反序列化
        ///   <summary>   
        ///   二进制反序列化   
        ///   </summary>   
        ///   <param   name="buf">字节数组</param>   
        ///   <returns>得到的对象</returns>   
        public static object DeserializeBinary(byte[] buf)
        {
            if (buf == null)
            {
                return null;
            }
            using (MemoryStream memStream = new MemoryStream(buf))
            {
                memStream.Position = 0;
                BinaryFormatter deserializer = new BinaryFormatter();
                object info = (object)deserializer.Deserialize(memStream);
                memStream.Close();
                return info;
            }
        }
        #endregion

        #region 序列化为xml文件
        /// <summary>
        /// 序列化为xml
        /// </summary>
        /// <param name="filePath">相对路径</param>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        public static void SaveXml(string filePath, object obj)
        {
            //此处出现了Bug 修改
            IOHelper.IfNotExistsCreateDir(filePath);
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


    /// <summary>
        /// 序列化为xml（如果传入的路径是物理路径的话 出现了Bug  故重载）
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="obj"></param>
    /// <param name="IsAbsolute">true filePath绝对路径 false filePath为相对路径</param>
        public static void SaveXml(string filePath, object obj, bool IsAbsolute)
        {
            if (!IsAbsolute)
            {
                IOHelper.IfNotExistsCreateDir(filePath);
            }
            else
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
            }

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

        #region xml文件反序列化
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object LoadXml(string filePath, System.Type type)
        {
            if (!System.IO.File.Exists(filePath))
                return null;
            try
            {
 using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);

                object obj = xs.Deserialize(reader);
                reader.Close();
                return obj; 
            }
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        #endregion

        #region 将C#数据实体转化为xml数据
        /// <summary>
        /// 将C#数据实体转化为xml数据
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <returns>xml格式字符串</returns>
        public static string XmlSerialize<T>(T obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string resultStr = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            return resultStr;
        }
        #endregion

        #region 将xml数据转化为C#数据实体
        /// <summary>
        /// 将xml数据转化为C#数据实体
        /// </summary>
        /// <param name="json">符合xml格式的字符串</param>
        /// <returns>T类型的对象</returns>
        public static T XmlDeserialize<T>(string xml)
        {

            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml.ToCharArray()));
            T obj = (T)serializer.ReadObject(ms);
            ms.Close();

            return obj;
        }
        #endregion

        #region Json序列化
        /// <summary>
        /// Json序列化
        /// </summary>
        public static string ToJson(this object item)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
                using (MemoryStream ms = new MemoryStream())
                {

                    serializer.WriteObject(ms, item);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion

        #region Json反序列化
        /// <summary>
        /// Json反序列化
        /// </summary>
        public static T FromJsonTo<T>(this string jsonString)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    T jsonObject = (T)ser.ReadObject(ms);
                    return jsonObject;
                }
            }
            catch
            {
                return default(T);//失败时返回null
            }
        }
        #endregion
    }
}
