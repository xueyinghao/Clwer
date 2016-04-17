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
        /// �Ƿ�����������
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static bool Exists(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey] != null;
        }
		/// <summary>
		/// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string CacheKey)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return objCache[CacheKey];
		}

		/// <summary>
		/// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject);
		}
        /// <summary>
        /// �Ƴ�Cache����
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);
        }
        /// <summary>
        /// ���û��棬ָ������ʱ�䣬������ʱ���һ�η��ʻ�����󣬻�������Ƴ�ʱ��Ϊ0��
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="minutes"></param>
        public static void SetCache(string cacheKey, object objObject, int minutes)
        {
            SetCache(cacheKey, objObject, DateTime.Now.AddMinutes(minutes), TimeSpan.Zero);
        }
		/// <summary>
		/// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration,TimeSpan slidingExpiration )
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject,null,absoluteExpiration,slidingExpiration);
		}

        /// <summary>
        /// ������������ļ�����
        /// </summary>
        /// <param name="key">����Key</param>
        /// <param name="obj">object����</param>
        /// <param name="fileName">�ļ�����·��</param>
        public static void Insert(string key, object obj, string fileName)
        {
            //��������������
            CacheDependency dep = new CacheDependency(fileName);
            //��������
            HttpContext.Current.Cache.Insert(key, obj, dep);
        }


        #region ������л���
        /// <summary>
        /// ��ʱ������Ҫ��������,����ͱ����ֹ����һ��Cache 
        /// Cache����һ��Remove����,���÷�����Ҫ�ṩһ��CacheKey,��������վ��CacheKey�������޷���֪�� 
        /// ֻ�ܾ������� 
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

        #region ��ʾ���л���   
        /// <summary>
        /// ��ʾ���л���
        /// </summary>
        /// <returns></returns>
        public static string ShowAllCache()
        {
            string str = "";
            IDictionaryEnumerator cacheEnum = HttpRuntime.Cache.GetEnumerator();

            while (cacheEnum.MoveNext())
            {
                str += "<br />������<b title=" + cacheEnum.Value.ToString() + ">[" + cacheEnum.Key + "]</b><br />";
            }
            return "��ǰ�ܻ�����:" + HttpRuntime.Cache.Count + "<br />" + str;
        }
        #endregion
	}
}
