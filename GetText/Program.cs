using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetText
{
    class Program
    {
        public static string m_outstr="";
        public static  string m_html; 
        static void Main(string[] args)
        {
            Program pro = new Program();
            string s = pro.Context();
            Console.ReadLine();

           
        }
        public  string Context()
        {
            if (m_outstr=="")
            {
                m_outstr = getContext(Int16.MaxValue);
            }
            return m_outstr;
       
        }

        /// <summary>
        /// 此公有方法提取网页中一定字数的纯文本，包括链接文字
        /// </summary>
        /// <param name="firstN">字数</param>
        /// <returns></returns>
        public  string getContext(int firstN)
        {
            string s = "";
            HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create("http://www.hactcm.edu.cn");
            using (StreamReader sr = new StreamReader(hwr.GetResponse().GetResponseStream()))
            {
                m_html = sr.ReadToEnd();
            }
            s = getFirstNchar(m_html, firstN, true);
            return s;
        }
        /// <summary>
        /// 此私有方法从一段HTML文本中提取出一定字数的纯文本
        /// </summary>
        /// <param name="instr">HTML代码</param>
        /// <param name="firstN">提取从头数多少个字</param>
        /// <param name="withLink">是否要链接里面的字</param>
        /// <returns>纯文本</returns>
        private string getFirstNchar(string instr, int firstN, bool withLink)
        {
            if (m_outstr == "")
            {
                m_outstr = instr.Clone() as string;
                m_outstr = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                m_outstr = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                m_outstr = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                if (!withLink) m_outstr = new Regex(@"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
                Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)| ", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                m_outstr = objReg.Replace(m_outstr, "");
                Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                m_outstr = objReg2.Replace(m_outstr, " ");

            }
            return m_outstr.Length > firstN ? m_outstr.Substring(0, firstN) : m_outstr;
        }


    }
}
