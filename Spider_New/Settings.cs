
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace Spider1
{
    class Settings
    {
        private static string ConfigurationFilePath;
        static Settings()
        {
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ConfigurationFilePath = Path.Combine(folder, "config.ini");
        }
        public static string DownloadFolder
        {
            get
            {
                //string s = Convert.ToString(GetValue("DownloadFolder", "download"));
                return Convert.ToString(GetValue("DownloadFolder", "download"));
            }
            set
            {
                SetValue("DownloadFolder", value);
            }
        }

        static void SetValue(string keyName, object value)
        {
            NativeMethods.WritePrivateProfileString("Crawler", keyName, value.ToString(), ConfigurationFilePath);
        }

        static object GetValue(string keyName, object defaultValue)
        {
            StringBuilder retVal = new StringBuilder(1024);
            NativeMethods.GetPrivateProfileString("Crawler", keyName, defaultValue.ToString(), retVal, 1024, ConfigurationFilePath);
            return retVal.ToString();
        }
    }
    class NativeMethods
    {
        [DllImport("kernel32")]
        internal static extern long WritePrivateProfileString(
            string appName,
            string keyName,
            string value,
            string fileName);

        [DllImport("kernel32")]
        internal static extern int GetPrivateProfileString(
            string appName,
            string keyName,
            string _default,
            StringBuilder returnedValue,
            int size,
            string fileName);

    }
}
