using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecondGetUrl
{
    class Program
    {
        public static string newUrl = "", url = "", AnotherUrl = "";
        public static int i = 0;
        public static ArrayList arr = new ArrayList();

        [STAThread]
        static void Main(string[] args)
        {
            ArrayList arr1,arr2 = new ArrayList();
            int sum = 0;
            string url = "http://www.hactcm.edu.cn";
            //首页上面的url
            arr1 = GetUrl(url);
            //循环首页的url将各个url里面的url在抓取出来
            for (int i = 0; i < arr1.Count; i++)
            {
                arr2 = GetUrl(arr1[i].ToString());
                arr1.RemoveAt(0);
            }
            arr1.AddRange(arr2);
            sum++;
            
            Console.ReadLine();
        }
        public static ArrayList GetUrl(string url)
        {
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
            return arr;
        }


    }
}
