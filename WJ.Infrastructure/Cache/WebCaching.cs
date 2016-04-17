using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WJ.Infrastructure.Cache
{
    public class WebCaching : ICacheStrategy
    {

        public object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        public void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        public void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        public bool Exists(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey] != null;
        }

        public void RemoveCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }
    }
}
