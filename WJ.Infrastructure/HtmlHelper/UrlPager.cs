using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using WJ.Infrastructure.Util;
using WJ.Infrastructure.Extension;

namespace WJ.Infrastructure.HtmlHelper
{
    public class UrlPager
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前页码参数
        /// </summary>
        public string CurrPageIndexParamName { get; set; }
        /// <summary>
        /// 每页多少条数据
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 分页按钮数量
        /// </summary>
        public int ButtonCount { get; set; }
        /// <summary>
        /// class
        /// </summary>
        public string CssClass { get; set; }
        /// <summary>
        /// 当前页码的class
        /// </summary>
        public string CurrPageClass { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        private int currPageIndex = 1;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrPageIndex
        {
            get
            {
                return currPageIndex;
            }

            set
            {
                currPageIndex = value;
            }
        }

        public UrlPager(string CurrPageIndexParamName)
        {
            this.CurrPageIndexParamName = CurrPageIndexParamName;
            int QuerypageIndex = DNTRequest.GetQueryNumber(CurrPageIndexParamName).ToInt32();
            CurrPageIndex = QuerypageIndex == -1 ? 1 : QuerypageIndex;
        }
        public string Pager()
        {
            if (ButtonCount < 1)
            {
                ButtonCount = 10;
            }
            string url = HttpContext.Current.Request.RawUrl;
            return ResponPageList(CurrPageIndex, url);
        }

        private string ResponPageList(int StarPage, string url)
        {
            int PageCount = (int)TotalCount / PageSize;
            if (TotalCount % PageSize != 0)
            {
                PageCount++;
            }
            StarPage = StarPage - (int)(ButtonCount - 1) / 2;
            if (StarPage <= 1) { StarPage = 1; }
            bool ExistQuery = false;
            if (url.Contains(CurrPageIndexParamName))
            {
                url = Regex.Replace(url, "[?|&]" + CurrPageIndexParamName + @"=\d+", "");
            }
            if (url.Contains("?"))
            {
                ExistQuery = true;
            }
            if (ExistQuery)
            {
                url += "&";
            }
            else
            {
                url += "?";
            }

            StringBuilder sb = new StringBuilder();
            string cssclass = "";
            if (!string.IsNullOrEmpty(CssClass))
            {
                cssclass = "class=\"" + CssClass + "\"";
            }
            string id = "";
            if (!string.IsNullOrEmpty(Id))
            {
                id = "id=\"" + Id + "\"";
            }
            sb.Append("<span " + cssclass + " " + id + ">");
            if (CurrPageIndex != 1)
            {
                sb.Append("<a href=\"" + url + CurrPageIndexParamName + "=1\">首页</a>");
                sb.Append("<a href=\"" + url + CurrPageIndexParamName + "=" + (CurrPageIndex - 1) + "\">上页</a>");
            }

            string pageurl = "";
            string currpageclass = "";
            string href = "";
            for (int i = 0; i < ButtonCount; i++)
            {
                if (StarPage > PageCount)
                    break;
                pageurl = url + CurrPageIndexParamName + "=" + StarPage;

                if (StarPage == CurrPageIndex)
                {
                    currpageclass = "class=\"" + CurrPageClass + "\"";
                    href = "href=\"javascript:void(0);\"";
                }
                else
                {
                    currpageclass = "";
                    href = "href=\"" + pageurl + "\"";
                }
                sb.Append("<a " + href + " " + currpageclass + ">" + StarPage + "</a>");
                StarPage++;
            }

            if (CurrPageIndex != PageCount)
            {
                sb.Append("<a href=\"" + url + CurrPageIndexParamName + "=" + (CurrPageIndex + 1) + "\">下页</a>");
                sb.Append("<a href=\"" + url + CurrPageIndexParamName + "=" + PageCount + "\">末页</a>");
            }
            sb.Append("</span>");

            return sb.ToString();
        }
    }
}
