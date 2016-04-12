using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;


namespace WJ.Infrastructure.Util
{
    public class PageListHelper:Page
    {
        /// <summary>
        /// 调用分页存储过程
        /// </summary>
        /// <param name="pagesize">每页显示的记录个数 </param>
        /// <param name="currentpage">要显示那一页的记录</param>
        /// <param name="tblName">要显示的表或多个表的连接</param>
        /// <param name="fldName">要显示的字段列表</param>
        /// <param name="fldSort">排序字段列表或条件</param>
        /// <param name="Sort">排序方法，0为升序，1为降序(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')</param>
        /// <param name="strCondition">查询条件,不需where</param>
        /// <param name="id">主表的主键</param>
        /// <param name="Dist">是否添加查询字段的 DISTINCT 默认0不添加/1添加</param>
        /// <param name="rowcount">数据总行数</param>
        /// <returns></returns>
        public static DataTable RunProcedure(int pagesize, int currentpage, string tblName, string fldName,
             string fldSort, int Sort, string strCondition, string id, int Dist,out int rowcount)
        {
            IDataParameter[] param = { 
                                    new SqlParameter("@tblName",tblName),
                                    new SqlParameter("@fldName",fldName),
                                    new SqlParameter("@pageSize",pagesize),
                                    new SqlParameter("@page",currentpage),
                                    new SqlParameter("@fldSort",fldSort),
                                    new SqlParameter("@Sort",Sort),
                                    new SqlParameter("@strCondition",strCondition),
                                    new SqlParameter("@ID",id),
                                    new SqlParameter("@Dist",Dist),
                                    new SqlParameter("@Counts",SqlDbType.Int)
                                   };
            param[9].Direction = ParameterDirection.Output;
            DataTable dt = DbHelperSQL.RunProcedure("usp_PagingLarge", param, "list").Tables[0];
            rowcount = Convert.ToInt32(param[9].Value);
            return dt ;
        }

        /// <summary>
        /// 存储过程分页
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="pk">主键</param>
        /// <param name="sort">排序</param>
        /// <param name="PageNumber">当前页码</param>
        /// <param name="PageSize">每页多少条数据</param>
        /// <param name="Fields">列</param>
        /// <param name="Filter">条件</param>
        /// <param name="Group">分组</param>
        /// <param name="RecordCount">返回总行数</param>
        /// <returns></returns>
        public static DataTable Paging_RowCount(string tables, string pk, string sort, int PageNumber, int PageSize, string Fields, string Filter, string Group, out int rowcount)
        {
            IDataParameter[] param = { 
                                    new SqlParameter("@Tables",tables),
                                    new SqlParameter("@PK",pk),
                                    new SqlParameter("@Sort",sort),
                                    new SqlParameter("@PageNumber",PageNumber),
                                    new SqlParameter("@PageSize",PageSize),
                                    new SqlParameter("@Fields",Fields),
                                    new SqlParameter("@Filter",Filter),
                                    new SqlParameter("@Group",Group),
                                    new SqlParameter("@RecordCount",SqlDbType.Int)
                                   };
            param[8].Direction = ParameterDirection.Output;
            DataTable dt = DbHelperSQL.RunProcedure("Paging_RowCount", param, "list").Tables[0];
            rowcount = Convert.ToInt32(param[8].Value);
            return dt;
        }
    }
}
