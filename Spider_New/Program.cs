using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spider1;
using System.Configuration;

namespace Spider_New
{
    class Program
    {
        List<string> all = new List<string>(); //存放解析出来的链接
        List<string> no_download = new List<string>();  //存放不需要进行解析的页面
        public static ArrayList arr = new ArrayList();
        int num = 0;//记录遍历的是深度
        //oldUrl用来在往数据库中存入url时去重复
        //TestHaShUrl用来测试对Url进行Hash计算时出现的问题
        //newUrl用来在加工通过正则表达式获取到的Url(Replace操作)
        //在GetUrl方法中Url是最终返回的符合一定规范的Url
        //HrefUrl用于在通过第一次便利出来的url进行再次获取时Url的去重复
        public static string DBoldUrl = "", TestHashUrl = "", newUrl = "", url = "", HrefoldUrl = "";
        static byte[] writebytes = new byte[10000];
        static void Main(string[] args)
        {
            ArrayList arr1, arr2 = new ArrayList();
            string Starturl = "http://www.hactcm.edu.cn/";
            arr1 = GetUrl(Starturl); //获取首页面上的链接地址
            for (int i = 0; i < arr1.Count; i++)
            {
                arr2 = GetUrl(arr1[i].ToString());

                arr1.RemoveAt(0);
            }
            arr1.AddRange(arr2);
        }

        public static ArrayList GetUrl(string url)
        {

            if (HrefoldUrl == url)
            {
                return null;
            }
            System.Net.WebClient client = new WebClient();
            byte[] page = client.DownloadData(url);
            string content = System.Text.Encoding.UTF8.GetString(page);
            string regex = "href=[\\\"\\\'](http:\\/\\/|\\.\\/|\\/)?\\w+(\\.\\w+)*(\\/\\w+(\\.\\w+)?)*(\\/|\\?\\w*=\\w*(&\\w*=\\w*)*)?[\\\"\\\']";
            Regex re = new Regex(regex);
            MatchCollection matches = re.Matches(content);

            System.Collections.IEnumerator enu = matches.GetEnumerator();
            while (enu.MoveNext() && enu.Current != null)
            {
                Match match = (Match)(enu.Current);
                newUrl = match.Value.Replace("href=\"", "").Replace("\"", "");
                if (newUrl.Contains(".css") || newUrl.Contains("index.htm") || newUrl == "http://www.hactcm.edu.cn/")
                {
                    continue;
                }
                if (newUrl.Contains("http://") != true)
                {
                    url = "http://www.hactcm.edu.cn/" + newUrl;
                }
                else
                {
                    url = newUrl;
                }
                arr.Add(url);
            }
            HrefoldUrl = url;
            return arr;
        }

        public string menu2(HtmlAgilityPack.HtmlNode item)
        {
            string s = "";
            for (int i = 0; i < item.Attributes.Count; i++)
            {
                if (item.Attributes[i].OriginalName == "href")
                {
                    s = item.Attributes["href"].Value;
                }
            }
            return s;
        }

        public int writeLog()
        {
            foreach (var item in all)
            {
                writebytes = Encoding.UTF8.GetBytes(item.ToString() + "\r\n\r\n");
                //之前的Save方法 Save(byte[] write)
                FileStream file = new FileStream("d:\\url.txt", FileMode.Append, FileAccess.Write);
                file.Write(writebytes, 0, writebytes.Length);
                file.Dispose();
                file.Close();
                writebytes.Initialize();
            }
            return 1;
        }

        //将获取到的url存入数据库
        public static void OperateDB(string url)
        {
            //此处将重复的url去除
            if (DBoldUrl == url)
            {
                return;
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            conn.Open();
            string sql = "Insert into tableUrl values('" + url + "')";
            SqlCommand command = new SqlCommand(sql, conn);
            command.ExecuteNonQuery();
            download.GetHtml(url);
            DBoldUrl = url;
        }
    }
}
