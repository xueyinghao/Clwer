using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.EntitiesExtension
{
    /// <summary>
    /// 分页数据源
    /// </summary>
    public class PageQuery<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页数据集合
        /// </summary>
        public IQueryable<T> QueryList { get; set; }
    }
}
