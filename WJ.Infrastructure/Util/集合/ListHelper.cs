using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Reflection;
using System.Data;
namespace WJ.Infrastructure.Util
{
    public static class ListHelper<T>
    {
        #region 读取DataReader为泛型集合
        /// <summary>
        /// 读取DataReader为泛型集合
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T> GetList(IDataReader dr)
        {
            List<T> list = new List<T>();

            T obj = Activator.CreateInstance<T>();
            PropertyInfo[] pi = obj.GetType().GetProperties();
            try
            {
                while (dr.Read())
                {

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        object value = dr.GetValue(i);
                        if (value == DBNull.Value)
                            continue;
                        for (int j = 0; j < pi.Length; j++)
                        {
                            if (pi[j].Name == dr.GetName(i))
                            {
                                pi[j].SetValue(obj, value, null);
                            }
                        }
                    }
                    list.Add(obj);
                }
            }
            finally
            {
                dr.Close();
            }
            return list;
        }
        #endregion

        #region 字符串转化为泛型集合
        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="splitstr">要分割的字符</param>
        /// <returns></returns>
        public static List<T> StrToList(string str, char splitstr)
        {
            string[] strarray = str.Split(splitstr);
            List<T> list = new List<T>();
            foreach (string s in strarray)
            {
                if (s != "")
                    list.Add((T)Convert.ChangeType(s, typeof(T)));
            }
            return list;
        }

        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static List<T> StrToList(string str)
        {
            return StrToList(str, ',');
        }
        #endregion

        #region 转换集合中所有元素的类型
        /// <summary>
        /// 转换集合中所有元素的类型
        /// </summary>
        /// <typeparam name="TO"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<TO> ConvertListType<TO>(List<T> list)
        {
            if (list == null)
            {
                return null;
            }
            List<TO> newlist = new List<TO>();
            foreach (T t in list)
            {
                newlist.Add((TO)Convert.ChangeType(t, typeof(TO)));
            }
            return newlist;
        }
        #endregion

    }
}
