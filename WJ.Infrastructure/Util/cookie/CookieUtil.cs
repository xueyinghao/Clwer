using System;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace WJ.Infrastructure.Util
{
    /// <summary>
    /// 对cookie的操作
    /// </summary>
    public class CookieUtil
    {
        public static void SetCookie(string name, string valu)
        {
            SetCookie(name, valu, null, null);
        }

        public static void SetCookie(string name, string valu, string domain)
        {
            SetCookie(name, valu, domain, null);
        }
        public static void SetCookie(string name, string valu, DateTime expires)
        {
            SetCookie(name, valu, null, expires);
        }

        public static void SetCookie(string name, string valu, string domain, DateTime? expires)
        {
            HttpCookie cookie = new HttpCookie(name, HttpUtility.UrlEncode(valu, Encoding.UTF8));
            if (expires != null)
            {
                cookie.Expires = (DateTime)expires;
            }

            if (!string.IsNullOrEmpty(domain)) cookie.Domain = domain;
            cookie.HttpOnly = true;

            HttpContext context = HttpContext.Current;
            context.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string name)
        {
            string valu = "";
            HttpContext context = HttpContext.Current;
            HttpCookie cookie = context.Request.Cookies[name];
            if (cookie != null)
            {
                valu = HttpUtility.UrlDecode(cookie.Value, Encoding.UTF8);
            }
            return valu;
        }
        public static void RemoveCookie(string name)
        {
            SetCookie(name, "", "", DateTime.Now.AddDays(-1));
        }
    }
}
