using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace WJ.Infrastructure.Util
{
    public class ControlValueUtil
    {
        /// <summary>
        /// 绑定dropdownlist
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="datasource"></param>
        /// <param name="datavalue"></param>
        /// <param name="datatext"></param>
        public static void BindDDL(DropDownList ddl,object datasource, string datavalue, string datatext)
        {
            ddl.DataSource = datasource;
            ddl.DataValueField = datavalue;
            ddl.DataTextField = datatext;
            ddl.DataBind();
        }

        /// <summary>
        /// 设置radiobuttonlist选中的值
        /// </summary>
        /// <param name="rbtlist"></param>
        /// <param name="value"></param>
        public static void setRbtListValue(RadioButtonList rbtlist, string value)
        {
            foreach (ListItem li in rbtlist.Items)
            {
                if (li.Value == value)
                {
                    li.Selected = true;
                }
            }
        }


        /// <summary>
        /// 获取checkboxlist所有选中的value
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="checkboxlistname"></param>
        /// <returns></returns>
        public static List<string> getCheckBoxListValue(CheckBoxList chkboxlist)
        {
            List<string> list = new List<string>();

            foreach (ListItem li in chkboxlist.Items)
            {
                if (li.Selected)
                {
                    list.Add(li.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 设置checkboxlist选中项
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="checkboxlistname"></param>
        /// <param name="kemulist"></param>
        public static void setCheckBoxListValue(List<string> valuelist, CheckBoxList chkboxlist)
        {
            if (valuelist == null || valuelist.Count == 0)
                return;
            foreach (string value in valuelist)
            {
                if (String.IsNullOrEmpty(value))
                    continue;
                foreach (ListItem li in chkboxlist.Items)
                {
                    if (li.Value == value.ToString())
                    {
                        li.Selected = true;
                    }
                }

            }
        }

        /// <summary>
        /// 绑定checkboxlist
        /// </summary>
        /// <param name="valuelist"></param>
        public static void bindCheckBoxList(Dictionary<string, string> valuelist, CheckBoxList checkboxlist)
        {
            foreach (string key in valuelist.Keys)
            {
                checkboxlist.Items.Add(new ListItem(valuelist[key], key.ToString()));
            }
        }

        /// <summary>
        /// 获取radiobuttonlist中选中的值
        /// </summary>
        /// <param name="rdblist"></param>
        /// <returns></returns>
        public static string getRadioButtonListSelected(RadioButtonList rdblist)
        {
            string result = "";
            foreach (ListItem item in rdblist.Items)
            {
                if (item.Selected)
                {
                    result= item.Value;
                }
            }
            return result;
        }

    }
}
