using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace WJ.Infrastructure.EntitiesExtension
{
    public static class ObjectContextExtension
    {
        /// <summary>
        /// 获取内部DbContext封装的OjbectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //public static ObjectContext ToOjbectContext(this DbContext context)
        //{
        //    return ((IObjectContextAdapter)context).ObjectContext;
        //}

        #region 得到所有数据

        /// <summary>
        /// 得到所有数据

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IQueryable<T> GetQList<T>(this ObjectContext context) where T : class,new()
        {
            return context.GetObjectSet<T>();
        }
        /// <summary>
        /// 得到所有数据

        /// </summary>
        /// <returns></returns>
        public static List<T> GetTList<T>(this ObjectContext context) where T : class,new()
        {
            return context.GetQList<T>().ToList();
        }
        #endregion

        #region 根据条件表达式返回前几条数据
        /// <summary>
        /// 根据条件表达式返回前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition"></param>
        /// <param name="topn"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQTopN<T>(this ObjectContext context, Expression<Func<T, bool>> condition, int topn) where T : class,new()
        {
            return context.GetQuery<T>(condition).TopN<T>(topn);
        }
        /// <summary>
        /// 根据条件表达式返回前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition"></param>
        /// <param name="topn"></param>
        /// <returns></returns>
        public static List<T> GetTTopN<T>(this ObjectContext context, Expression<Func<T, bool>> condition, int topn) where T : class,new()
        {
            return context.GetQTopN<T>(condition, topn).ToList();
        }
        #endregion

        #region 根据条件表达式返回前几条数据
        /// <summary>
        /// 根据条件表达式返回前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="topn"></param>
        /// <returns></returns>
        public static IQueryable<T> GetQTopN<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby, int topn) where T : class,new()
        {
            return context.GetQuery<T>(condition, orderby).TopN<T>(topn);
        }
        /// <summary>
        /// 根据条件表达式返回前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <param name="topn"></param>
        /// <returns></returns>
        public static List<T> GetTTopN<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby, int topn) where T : class,new()
        {
            return context.GetQTopN<T>(condition, orderby, topn).ToList();
        }
        #endregion

        #region 根据条件表达式取得相关数据

        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">Lambda表达式</param>
        /// <returns></returns>
        public static IQueryable<T> GetQList<T>(this ObjectContext context, Expression<Func<T, bool>> condition) where T : class ,new()
        {
            return context.GetQuery<T>(condition);
        }
        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">Lambda表达式</param>
        /// <returns></returns>
        public static List<T> GetTList<T>(this ObjectContext context, Expression<Func<T, bool>> condition) where T : class ,new()
        {
            return context.GetQList<T>(condition).ToList();
        }
        #endregion

        #region 根据条件表达式取得相关数据

        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">Lambda表达式</param>
        /// <param name="orderby">排序条件，例如(it.id desc)</param>
        /// <returns></returns>
        public static IQueryable<T> GetQList<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby) where T : class ,new()
        {
            return context.GetQuery<T>(condition, orderby);
        }
        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">Lambda表达式</param>
        /// <param name="orderby">排序条件，例如(it.id desc)</param>
        /// <returns></returns>
        public static List<T> GetTList<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby) where T : class ,new()
        {
            return context.GetQList<T>(condition, orderby).ToList();
        }
        #endregion

        #region 根据条件表达式取得相关数据

        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">字符串表达式</param>
        /// <returns></returns>
        public static IQueryable<T> GetQList<T>(this ObjectContext context, string condition) where T : class,new()
        {
            return context.GetQuery<T>(condition);
        }
        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">字符串表达式</param>
        /// <returns></returns>
        public static List<T> GetTList<T>(this ObjectContext context, string condition) where T : class,new()
        {
            return context.GetQList<T>(condition).ToList();
        }
        #endregion

        #region 根据条件表达式取得相关数据

        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">字符串表达式</param>
        /// <param name="orderby">排序条件，例如(it.id desc)</param>
        /// <returns></returns>
        public static IQueryable<T> GetQList<T>(this ObjectContext context, string condition, string orderby) where T : class,new()
        {
            return context.GetQuery<T>(condition, orderby);
        }
        public static IQueryable<T> GetQList<T>(this ObjectContext context, string condition, string orderby, string ziduan) where T : class,new()
        {
            return context.GetQuery<T>(condition, orderby, ziduan);
        }
        /// <summary>
        /// 根据条件表达式取得相关数据

        /// </summary>
        /// <param name="condition">字符串表达式</param>
        /// <param name="orderby">排序条件，例如(it.id desc)</param>
        /// <returns></returns>
        public static List<T> GetTList<T>(this ObjectContext context, string condition, string orderby) where T : class,new()
        {
            return context.GetQList<T>(condition, orderby).ToList();
        }

        #endregion

        #region 返回指定条件的实体

        /// <summary>
        /// 返回指定条件的实体

        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetByCondition<T>(this ObjectContext context, Expression<Func<T, bool>> condition) where T : class,new()
        {
            return context.GetQuery<T>(condition).FirstOrDefault();
        }
        #endregion

        #region 将实体数据保存到数据库中
        /// <summary>
        /// 将实体数据保存到数据库中
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int NewSave<T>(this ObjectContext context, T model) where T : class,new()
        {
            context.GetObjectSet<T>().AddObject(model);
            return context.SaveChanges();
        }
        #endregion

        #region 删除指定条件的数据（传入表达式树条件）

        /// <summary>
        /// 删除指定条件的数据（传入表达式树条件）

        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int Removes<T>(this ObjectContext context, Expression<Func<T, bool>> condition) where T : class,new()
        {
            var query = context.GetQuery<T>(condition);
            foreach (var q in query)
            {
                context.DeleteObject(q);
            }
            return context.SaveChanges();
        }
        #endregion

        #region 数据分页
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition">分页条件</param>
        /// <param name="orderby">排序字段，例如(it.id desc)</param>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="istotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageQuery<T> QueryPager<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby, int pagesize, int pageindex, bool istotal) where T : class,new()
        {
            var iquery = context.GetQuery<T>(condition, orderby);

            return iquery.QueryPager(pagesize, pageindex, istotal);
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition">分页条件</param>
        /// <param name="orderby">排序字段，例如(it.id desc)</param>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="istotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageQuery<T> QueryPager<T>(this ObjectContext context, string condition, string orderby, int pagesize, int pageindex, bool istotal) where T : class,new()
        {
            var iquery = context.GetQuery<T>(condition, orderby);

            return iquery.QueryPager(pagesize, pageindex, istotal);
        }
        #endregion

        #region 数据分页
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition">分页条件</param>
        /// <param name="orderby">排序字段，例如(it.id desc)</param>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="istotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageData<T> ListPager<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby, int pagesize, int pageindex, bool istotal) where T : class,new()
        {
            var iquery = context.GetQuery<T>(condition, orderby);

            return iquery.ListPager(pagesize, pageindex, istotal);
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="condition">分页条件</param>
        /// <param name="orderby">排序字段，例如(it.id desc)</param>
        /// <param name="pagesize">每页多少条数据</param>
        /// <param name="pageindex">当前页数</param>
        /// <param name="istotal">是否统计总行数</param>
        /// <returns></returns>
        public static PageData<T> ListPager<T>(this ObjectContext context, string condition, string orderby, int pagesize, int pageindex, bool istotal) where T : class,new()
        {
            var iquery = context.GetQuery<T>(condition, orderby);

            return iquery.ListPager(pagesize, pageindex, istotal);
        }
        #endregion

        #region private

        private static IQueryable<T> GetQuery<T>(this ObjectContext context, Expression<Func<T, bool>> condition, string orderby) where T : class,new()
        {
            return context.GetObjectSet<T>().OrderBy(orderby).Where(condition);
        }

        private static IQueryable<T> GetQuery<T>(this ObjectContext context, Expression<Func<T, bool>> condition) where T : class,new()
        {
            return context.GetObjectSet<T>().Where(condition);
        }

        private static IQueryable<T> GetQuery<T>(this ObjectContext context, string condition, string orderby) where T : class,new()
        {
            return context.GetObjectSet<T>().Where(condition).OrderBy(orderby);
        }
        private static IQueryable<T> GetQuery<T>(this ObjectContext context, string condition, string orderby, string ziduan) where T : class,new()
        {
            return context.GetObjectSet<T>().Where(condition).OrderBy(orderby).SelectValue<T>(ziduan);
        }
        private static IQueryable<T> GetQuery<T>(this ObjectContext context, string condition) where T : class,new()
        {
            return context.GetObjectSet<T>().Where(condition);
        }

        private static ObjectSet<T> GetObjectSet<T>(this ObjectContext context) where T : class,new()
        {
            return (context.CreateObjectSet<T>());
        }
        #endregion


    }
}
