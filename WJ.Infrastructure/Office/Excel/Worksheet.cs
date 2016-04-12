using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Text;

namespace WJ.Infrastructure.Office
{
    /// <summary>
    /// Excel表单类
    /// </summary>
    public class Worksheet
    {
        private string sheetname = Guid.NewGuid().ToString().Replace("-", "");
        /// <summary>
        /// 表单名称
        /// </summary>
        public string SheetName
        {
            set { sheetname = value.Replace(">", "").Replace("<", "").Replace("\"", "").Replace("&", "").Replace("'", ""); }
        }

        private bool columnfilter = false;
        /// <summary>
        /// 是否自动条件过滤
        /// 注意：如果设置不导出标题行，此属性无效！
        /// </summary>
        public bool ColumnFilter
        {
            set { columnfilter = value; }
            get 
            {
                if (!writeheaders)
                    return false;
                else
                    return columnfilter;
            }
        }

        private DataTable datasource = new DataTable();

        public DataTable DataSource
        {
            set { datasource = value; }
        }

        private bool writeheaders = true;

        public bool WriteHeaders
        {
            set { writeheaders = value; }
        }

        private ArrayList columnname = new ArrayList();

        public ArrayList ColumnName
        {
            set { columnname = value; }
            get
            {
                if (columnname.Count > 0)
                    return columnname;
                else
                {
                    ArrayList temp = new ArrayList();
                    foreach (DataColumn dc in datasource.Columns)
                    {
                        temp.Add(dc.ColumnName);
                    }
                    return temp;
                }
            }
        }

        public void OutPut()
        {
            int columscnt = datasource.Columns.Count;
            int rowcnt = datasource.Rows.Count + 1;
            HttpResponse Response = HttpContext.Current.Response;
            Response.Write("<Worksheet ss:Name=\"" + sheetname + "\">");
            if (!ColumnFilter)
            {
                #region
                Response.Write("<Table ss:ExpandedColumnCount=\"" + columscnt + "\" ss:ExpandedRowCount=\"" + rowcnt + "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"13.5\">");
                if (writeheaders)
                {
                    Response.Write("<Row ss:AutoFitHeight=\"0\">");
                    foreach (object o in ColumnName)//<![CDATA[这里放置需要显示的字符]]>
                    {
                        Response.Write("<Cell><Data ss:Type=\"String\"><![CDATA[" + o.ToString() + "]]></Data></Cell>");
                    }
                    Response.Write("</Row>");
                }
                foreach (DataRow dr in datasource.Rows)
                {
                    Response.Write("<Row ss:AutoFitHeight=\"0\">");
                    for (int i = 0; i < datasource.Columns.Count; i++)
                    {
                        Response.Write("<Cell><Data ss:Type=\"String\"><![CDATA[" + dr[i].ToString() + "]]></Data></Cell>");
                    }
                    Response.Write("</Row>");
                }

                Response.Write("</Table>");
                #endregion
            }
            else
            {
                #region
                Response.Write("<Names><NamedRange ss:Name=\"_FilterDatabase\" ss:RefersTo=\"=" + sheetname + "!R1C1:R" + (rowcnt - 1) + "C" + columscnt + "\"  ss:Hidden=\"1\"/></Names>");
                Response.Write("<Table ss:ExpandedColumnCount=\"" + columscnt + "\" ss:ExpandedRowCount=\"" + rowcnt + "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"13.5\">");
                if (writeheaders)
                {
                    Response.Write("<Row ss:AutoFitHeight=\"0\">");
                    foreach (object o in ColumnName)
                    {
                        Response.Write("<Cell><Data ss:Type=\"String\"><![CDATA[" + o.ToString() + "]]></Data><NamedCell ss:Name=\"_FilterDatabase\"/></Cell>");
                    }
                    Response.Write("</Row>");
                }
                foreach (DataRow dr in datasource.Rows)
                {
                    Response.Write("<Row ss:AutoFitHeight=\"0\">");
                    for (int i = 0; i < datasource.Columns.Count; i++)
                    {
                        Response.Write("<Cell><Data ss:Type=\"String\"><![CDATA[" + dr[i].ToString() + "]]></Data><NamedCell ss:Name=\"_FilterDatabase\"/></Cell>");
                    }
                    Response.Write("</Row>");
                }
                Response.Write("</Table>");
                Response.Write("<AutoFilter x:Range=\"R1C1:R" + (rowcnt - 1) + "C" + columscnt + "\" xmlns=\"urn:schemas-microsoft-com:office:excel\"></AutoFilter>");
                #endregion
            } 
        }

    }
}
