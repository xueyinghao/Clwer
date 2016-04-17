using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace WJ.Infrastructure.EntitiesExtension
{
    public static class IQueryableExtend
    {
        #region 返回IQueryable<T>前几条数据
        /// <summary>
        /// 返回IQueryable前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="TopN"></param>
        /// <returns></returns>
        public static IQueryable<T> TopN<T>(this IQueryable<T> query, int TopN)
        {
            return query.Take(TopN);
        }
        #endregion

        #region 对IQueryable<T>进行分页
        /// <summary>
        /// 对IQueryable进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="order"></param>
        /// <param name="PageSize">每页多少条数据</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public static IQueryable<T> QueryPager<T>(this IQueryable<T> query, int PageSize, int PageIndex)
        {

            return query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }
        #endregion

        #region 得到IQueryable<T>的分页后实体集合
        /// <summary>
        /// 得到IQueryable的分页后实体集合
        /// </summary>
        /// <param name="query"></param>
        /// <param name="PageSize">每页多少条数据</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="IsTotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageData<T> ListPager<T>(this IQueryable<T> query, int PageSize, int PageIndex, bool IsTotal)
        {
            PageData<T> list = new PageData<T>();
            if (IsTotal)
            {
                list.TotalCount = query.Count();
            }
            list.DataList = query.QueryPager<T>(PageSize, PageIndex).ToList();
            return list;
        }

        #endregion

        #region 得到IQueryable<T>的分页后数据源
        /// <summary>
        /// 得到IQueryable的分页后数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="PageSize">每页几条数据</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="IsTatal">是否统计总页数</param>
        /// <returns></returns>
        public static PageQuery<T> QueryPager<T>(this IQueryable<T> query, int PageSize, int PageIndex, bool IsTatal) where T : class,new()
        {
            PageQuery<T> list = new PageQuery<T>();
            if (IsTatal)
            {
                list.TotalCount = query.Count();
            }

            list.QueryList = query.QueryPager(PageSize, PageIndex);

            return list;
        }
        #endregion

    }
}
