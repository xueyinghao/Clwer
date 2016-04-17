using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using WJ.Infrastructure.Util;
using WJ.Infrastructure.Cache;
using WJ.Infrastructure.IOC;

namespace WJ.Infrastructure.HtmlHelper
{
    /// <summary>
    /// html帮助类
    /// </summary>
    public class HtmlHelper
    {
        private static object lockhelper = new object();
        private static Dictionary<string, string> ControlCache=new Dictionary<string,string>();
       private static ICacheStrategy cacheManage = IoC.Resolve<ICacheStrategy>();

        #region CheckBoxList
        /// <summary>
        /// 输出checkboxlist
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="listItem">选项列表</param>
        /// <param name="checkedList">根据Key设置选中的项</param>
        /// <returns></returns>
        public static string CheckBoxList(string id, Dictionary<string, string> listItem, List<string> checkedList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=" + id + ">");
            int i = 0;
            foreach (string key in listItem.Keys)
            {
                string value = listItem[key];
                string check = "";
                if (checkedList != null && checkedList.Contains(key))
                {
                    check = " checked=\"checked\"";
                }
                sb.Append("<span>");
                sb.Append("<input type=\"checkbox\" " + check + " value=\"" + key + "\" name=\"" + id + "$" + key + "\">");
                sb.Append("<label for=\"" + id + "_" + i + "\">" + value + "</label>");
                sb.Append("</span>");
                i++;
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        /// 输出checkboxlist
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="listItem">选项列表</param>
        /// <param name="checkedList">根据Key设置选中的项</param>
        /// <returns></returns>
        public static string CheckBoxList(string id, Dictionary<string, string> listItem)
        {
            return CheckBoxList(id, listItem, null);
        }


        #endregion

        #region RadioList
        /// <summary>
        /// 输出radiolist
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="listItem">选项列表</param>
        /// <param name="checkvalue">根据key设置选中的项</param>
        /// <returns></returns>
        public static string RadioList(string id, Dictionary<string, string> listItem, string checkvalue)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\""+id+"\">");
            int i = 0;
            foreach (string key in listItem.Keys)
            {
                string check = "";

                if ((checkvalue != null && key == checkvalue) || (i == 0 && checkvalue == null))
                {
                    check = "checked=\"checked\"";
                }
                sb.Append("<span>");
                sb.Append("<input type=\"radio\" " + check + " value=\"" + key + "\" name=\"" + id + "\">");
                sb.Append("<label for=\"" + id + "\">" + listItem[key] + "</label>");
                sb.Append("</span>");
                i++;
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        /// 输出radiolist
        /// </summary>
        /// <param name="id">控件id</param>
        /// <param name="listItem">选项列表</param>
        /// <returns></returns>
        public static string RadioList(string id, Dictionary<string, string> listItem)
        {
            return RadioList(id, listItem, null);
        }
        #endregion

        #region select
        public static string CommonSelect(Dictionary<string, string> listitem, string id, string name)
        {
           return CommonSelect(listitem, id, name,null);
        }
        /// <summary>
        /// 公共select根据name缓存
        /// </summary>
        /// <param name="listitem"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cssclass"></param>
        /// <returns></returns>
        public static string CommonSelect(Dictionary<string, string> listitem, string id, string name, string cssclass)
        {
            if(cacheManage.Exists(name))
            {
                return cacheManage.GetCache(name).ToString();
            }
            string select = Select(listitem, id, name, cssclass);
            if (!cacheManage.Exists(name))
            {
                lock (lockhelper)
                {
                    if (!cacheManage.Exists(name))
                    {
                        cacheManage.SetCache(name, select);
                    }
                }
            }
            return select;
        }

        public static string Select(Dictionary<string, string> listitem, string id, string name)
        {
            return Select(listitem, id, name, null);
        }
        /// <summary>
        /// 输出select
        /// </summary>
        /// <param name="listitem"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="cssclass"></param>
        /// <returns></returns>
        public static string Select(Dictionary<string,string> listitem, string id, string name, string cssclass)
        {
            StringBuilder sb = new StringBuilder();

            string str = "";
            if (!string.IsNullOrEmpty(cssclass))
            {
                str = "class=" + cssclass + "";
            }
            sb.Append("<select id=\"" + id + "\" name=\"" + name + "\"" + str + ">");
            foreach (string key in listitem.Keys)
            {
                sb.Append("<option value=\"" + key + "\">" + listitem[key] + "</option>");

            }
            sb.Append("</select>");
            return sb.ToString();
        }

        #endregion

        #region 获取选中项列表
        /// <summary>
        /// 获取checkbox选中项列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<string> GetCheckedList(string id)
        {
            List<string> list = new List<string>();
            foreach (string key in HttpContext.Current.Request.Form.AllKeys)
            {
                if (key.Contains(id + "$"))
                {
                    list.Add(DNTRequest.GetFormString(key));
                }
            }
            return list;
        }

        /// <summary>
        /// 获取字符窜格式的checkbox选中项列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string GetCheckedStr(string id, string splitstr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in HttpContext.Current.Request.Form.AllKeys)
            {
                if (key.Contains(id + "$"))
                {
                    sb.Append(DNTRequest.GetFormString(key) + splitstr);
                }
            }
            if (sb.Length > 0)
            {
                sb.Length -= splitstr.Length;
            }
            return sb.ToString();
        }
        #endregion

        #region 是否选中
        /// <summary>
        /// 是否选中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsChecked(string name)
        {
           
            return HttpContext.Current.Request.Form[name] != null;
        }
        #endregion

    }
}
