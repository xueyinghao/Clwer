/*-------------------------Class类注释---------------------------------
 // 版权所有：Copyright (C) 2012 河南智森科技
 
 // 作者：魏飞
 
 // 创建日期：2012-2-8
 
 // 文件名：CommonEqualityComparer.cs
 
 // 功能描述：linq Distinct 扩展
 
 // 注意事项：
 
 // 遗留BUG：
 
 -------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.EntitiesExtension
{
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        { }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinctx<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> Distinctx<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }
    } 

}
