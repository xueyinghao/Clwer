using System;
using System.Collections.Generic;
using System.Text;

namespace WJ.Infrastructure.Util
{
    public class DateTimeUtil
    {
        /// <summary>
        /// 获取时间中的日期部分
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetDate(DateTime d)
        {
            return GetDate(d, '-');
        }
        /// <summary>
        /// 获取时间中的日期部分
        /// </summary>
        /// <param name="d"></param>
        /// <param name="c">分隔符[-/.]</param>
        /// <returns></returns>
        public static string GetDate(DateTime d, char c)
        {
            return GetDate(d, c, true);
        }
        /// <summary>
        /// 获取时间中的日期部分
        /// </summary>
        /// <param name="d"></param>
        /// <param name="c">分隔符</param>
        /// <param name="yearAtFirst">年是否在前</param>
        /// <returns></returns>
        public static string GetDate(DateTime d, char c, bool yearAtFirst)
        {
            if (yearAtFirst)
                return string.Format("{0}{1}{2}{1}{3}", d.Year, c, d.Month, d.Day);
            else
                return string.Format("{0}{1}{2} {3}", d.Month, c, d.Day, d.Year);
        }
        /// <summary>
        /// 获取星期几（中文）
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetWeekDay(DateTime d)
        {
            string[] weeks = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

            return weeks[(int)d.DayOfWeek];
        }
        /// <summary>
        /// 获取星期几（英文）
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetWeekDayEn(DateTime d)
        {
            return d.DayOfWeek.ToString();
        }
        /// <summary>
        /// 获取日期的英语格式(Jan 3, 2010)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetDateEn(DateTime d)
        {
            string[] months = { "January", "Febrhuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            return months[d.Month - 1].Substring(0, 3) + " " + d.Day.ToString() + ", " + d.Year.ToString();
        }
    }
}
