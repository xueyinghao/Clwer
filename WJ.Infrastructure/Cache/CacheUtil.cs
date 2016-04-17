using System;
using System.Web;
using System.Collections;
using System.Web.Caching;

namespace WJ.Infrastructure.Cache
{
	/// </summary>
	public class CacheUtil
	{
        /// <summary>
        /// 是否存在这个缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static bool Exists(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey] != null;
        }
		/// <summary>
		/// 获取当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string CacheKey)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return objCache[CacheKey];
		}

		/// <summary>
		/// 设置当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject);
		}
        /// <summary>
        /// 移出Cache对象
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);
        }
        /// <summary>
        /// 设置缓存，指定过期时间，（过期时最后一次访问缓存对象，缓存对象移除时间为0）
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="minutes"></param>
        public static void SetCache(string cacheKey, object objObject, int minutes)
        {
            SetCache(cacheKey, objObject, DateTime.Now.AddMinutes(minutes), TimeSpan.Zero);
        }
		/// <summary>
		/// 设置当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration,TimeSpan slidingExpiration )
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject,null,absoluteExpiration,slidingExpiration);
		}

        /// <summary>
        /// 创建缓存项的文件依赖
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="obj">object对象</param>
        /// <param name="fileName">文件绝对路径</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //创建缓存依赖项
            CacheDependency dep = new CacheDependency(fileName);
            //创建缓存
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }


        #region 清除所有缓存
        /// <summary>
        /// 有时可能需要立即更新,这里就必须手工清除一下Cache 
        /// Cache类有一个Remove方法,但该方法需要提供一个CacheKey,但整个网站的CacheKey我们是无法得知的 
        /// 只能经过遍历 
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }

            foreach (string key in al)
            {
                cache.Remove(key);
            }
        }
        #endregion

        #region 显示所有缓存   
        /// <summary>
        /// 显示所有缓存
        /// </summary>
        /// <returns></returns>
        public static string ShowAllCache()
        {
            string str = "";
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();

            while (cacheEnum.MoveNext())
            {
                str += "<br />缓存名<b title=" + cacheEnum.Value.ToString() + ">[" + cacheEnum.Key + "]</b><br />";
            }
            return "当前总缓存数:" + HttpRuntime.Cache.Count + "<br />" + str;
        }
        #endregion
	}
}
