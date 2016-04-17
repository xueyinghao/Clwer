/*public List<User> GetFriends(int userId)
{
    string cacheKey = "friends_of_user_" + userId;
 
    return CacheHelper.Get(
        delegate(out List<User> data) // cache getter
        {
            object objData = cacheManager.Get(cacheKey);
            data = (objData == null) ? null : (List<User>)objData;
 
            return objData != null;
        },
        () => // source getter
        {
            return new UserService().GetFriends(userId);
        },
        (data) => // cache setter
        {
            cacheManager.Set(cacheKey, data);
        });
 
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.Cache
{
    public static partial class CacheHelper
    {
        public delegate bool CacheGetter<TData>(out TData data);

        public static TData Get<TData>(
            CacheGetter<TData> cacheGetter,
            Func<TData> sourceGetter,
            Action<TData> cacheSetter)
        {
            TData data;
            if (cacheGetter(out data))
            {
                return data;
            }

            data = sourceGetter();
            cacheSetter(data);

            return data;
        }
    }
}
