using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.Cache
{

    public interface ICacheStrategy
    {
        object GetCache(string CacheKey);

        void SetCache(string CacheKey, object objObject);

        void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration);

        bool Exists(string CacheKey);

        void RemoveCache(string CacheKey);
    }

}
