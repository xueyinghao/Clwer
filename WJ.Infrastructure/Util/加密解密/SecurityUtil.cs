using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WJ.Infrastructure.Util
{
    /// <summary>
    /// 提供加密/解密函数
    /// </summary>
    public class SecurityUtils
    {

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static readonly string aeskey = ConfigUtil.GetConfig("AESkeyword");

        #region DES
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="key">加密密钥(8位)</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string str, string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="key"> 解密密钥(8位),和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string str, string key)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(str);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return str;
            }
        }
        
        #endregion

        #region MD5
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>MD5加密后的字符串，失败返回源串</returns>
        public static string MD5String(string str)
        {
            //try
            //{
            //    MD5 md5 = new MD5CryptoServiceProvider();

            //    byte[] _byte = Encoding.Default.GetBytes(str);
            //    byte[] _byteRst = md5.ComputeHash(_byte);

            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i < _byteRst.Length; i++)
            //    {
            //        sb.Append(_byteRst[i].ToString("X2"));
            //    }
            //    return sb.ToString();
            //}
            //catch
            //{
            //    return str;
            //}
            //等效与
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
        }


        /// <summary>
        /// MD5加密字符串  将错就错    tostring  x 和x2  是不一样的  犹豫注册用户一直用的  x
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>MD5加密后的字符串，失败返回源串</returns>
        public static string MD5StringNew(string str)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像  
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　  
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得  
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                //这里是不对的，应该是("X2"),只能将错就错了
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        } 
        #endregion

        #region AES
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string EncryptAES(string toEncrypt)
        {
            if (string.IsNullOrWhiteSpace(toEncrypt))
                return string.Empty;
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(aeskey);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string DecryptAES(string toDecrypt)
        {
            if (string.IsNullOrWhiteSpace(toDecrypt))
                return string.Empty;
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(aeskey);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        } 
        #endregion
    }
}
