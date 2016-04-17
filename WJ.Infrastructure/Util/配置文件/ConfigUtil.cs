using System;
using System.Configuration;
using System.Web.Configuration;
using System.Text;

namespace WJ.Infrastructure.Util
{
    /// <summary>
    /// 提供对.config文件的访问
    /// </summary>
    public class ConfigUtil
    {
        /// <summary>
        /// 获取配置字符串(&lt;add key='' value='' /&gt;)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 获取数据库连接字符串(&lt;connectionStrings&gt;节点)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        /// <summary>
        /// 设置/重写一个key:value对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetSetting(string key, string value)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(null);
                AppSettingsSection section = config.AppSettings;
                if (section.Settings[key] == null)
                {
                    section.Settings.Add(key, value);
                }
                else
                {
                    section.Settings[key].Value = value;
                }
                config.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 设置/重写一个数据库连接串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static bool SetConnString(string key, string connString)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(null);
                ConnectionStringsSection section = config.ConnectionStrings;
                if (section.ConnectionStrings[key] == null)
                {
                    section.ConnectionStrings.Add(new ConnectionStringSettings(key, connString));
                }
                else
                {
                    section.ConnectionStrings[key].ConnectionString = connString;
                    section.ConnectionStrings[key].ProviderName = "System.Data.EntityClient";
                }
                config.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一个key:value节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveSetting(string key)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration(null);
                AppSettingsSection section = config.AppSettings;
                if (section.Settings[key] != null)
                {
                    section.Settings.Remove(key);
                }
                config.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
