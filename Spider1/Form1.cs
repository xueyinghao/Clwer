//当前只抓取了中医学院首页的链接并将页面下载保存
//存在的问题有以下：
//1、需要将首页抓去的链接再次作为种子url进行获取新的链接
//2、url的去重复
//3、后期需要改为多线程抓去
//4、在数据展示的时候需要异步显示


//出现一个严重的问题，在往数据库里面存url地址时发现，存入到数据库66个地址，但实际下载了65个网页

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
namespace Spider1
{
    public partial class Form1 : Form
    {
        //分别用于存放新货去的Url和访问过的Url
        public static SeqQueue<string> NewQueue = new SeqQueue<string>(500000);
        public  static SeqQueue<string>OldQueue = new SeqQueue<string>(500000);


        List<string> all = new List<string>(); //存放解析出来的链接
        public static ArrayList arr = new ArrayList();
        int num = 0;    //记录遍历的是深度

        //oldUrl用来在往数据库中存入url时去重复
        //TestHaShUrl用来测试对Url进行Hash计算时出现的问题
        //newUrl用来在加工通过正则表达式获取到的Url(Replace操作)
        //在GetUrl方法中Url是最终返回的符合一定规范的Url
        //HrefUrl用于在通过第一次便利出来的url进行再次获取时Url的去重复
        public static string DBoldUrl = "", TestHashUrl = "", newUrl = "", url = "", HrefoldUrl = "";

        public Form1()
        {
            InitializeComponent();
        }

        static byte[] writebytes = new byte[10000];
        //用来存放解析出来链接
        private void btn_start_Click(object sender, EventArgs e)
        {
            //ArrayList arr1, arr2 = new ArrayList();
            //string Starturl = "http://www.hactcm.edu.cn/";
            //arr1 = GetUrl(Starturl); //获取首页面上的链接地址
            //for (int i = 0; i < arr1.Count; i++)
            //{
            //    arr2 = GetUrl(arr1[i].ToString());

            //    arr1.RemoveAt(0);
            //}
            //arr1.AddRange(arr2);
            ////sum++;

            ////all.Add(url);

            //// GetUrl(url);
            //// num++;
            //// txt_depth.Text = "当前的深度为:" + num;
            ////int i= writeLog();  //将获取到的链接写入文本

            ////List<string> hashName = new List<string>();
            ////string s = "";
            //// //测试hash出现的错误
            ////foreach (var item in all)
            ////{
            ////    if (item==TestHashUrl)
            ////    {
            ////        return;
            ////    }
            ////    s = download.Hash(item) + ".html";
            ////    textBox2.Text += s + "\r\n";
            ////    TestHashUrl = item;
            ////}


            ////if (i == 1)
            ////{
            ////    txt_dqUrl.Text = "下载已完成";
            ////}


            
            
            //string Starturl = "http://www.hactcm.edu.cn/";
            string Starturl = txt_url.Text;
            service(Starturl); //获取首页面上的链接地址
            int Newnum = NewQueue.Count();
            for (int i = 0; i < Newnum; i++)
            {
                string s = NewQueue.Dequeue();
                OldQueue.Enqueue(s);
                service(s);
            }
            int num2 = NewQueue.Getlength();
            for (int j = 0; j < num2; j++)
            {
                string s = NewQueue.Dequeue();
                OldQueue.Enqueue(s);
                service(s);
            }
            int num3 = NewQueue.Getlength();
            for (int k = 0; k < num3; k++)
            {
                string s = NewQueue.Dequeue();
                OldQueue.Enqueue(s);
                service(s);
            }
            txt_depth.Text = "当前的深度为:3";
            txt_dqUrl.Text = "下载已完成";
            
        }

        public static void service(string str)
        {
            GetUrl(str);
        }




        #region
        /// <summary>
        /// 用来获取页面的链接
        /// </summary>
        /// <param name="url"></param>
        //public  void GetUrl(string url)
        //{
        //    HttpClient client = new HttpClient(url);
        //    string html = client.GetString();
        //    HtmlAgilityPack.HtmlDocument hd = new HtmlAgilityPack.HtmlDocument();
        //    hd.LoadHtml(html);
        //    HtmlNodeCollection nodes = hd.DocumentNode.SelectNodes("//a");  //获取到的a链接
        //    foreach (HtmlAgilityPack.HtmlNode item in nodes)
        //    {
        //        #region
        //        //针对首页的特殊情况
        //        //if (item.Attributes[0].OriginalName == "id" || item.Attributes[0].OriginalName == "target")
        //        //{ continue; }
        //        //if (item.Attributes[0].OriginalName=="class")
        //        //{
        //        //    Menu(item);
        //        //}
        //        //string href_value = item.Attributes["href"].Value;
        //        #endregion
        //        string href_value = menu2(item);
        //        if (href_value.Contains("javascript:") || href_value == "#" || String.IsNullOrEmpty(href_value) ||
        //                href_value.StartsWith("#") ||
        //                href_value.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase) ||
        //                href_value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) || item.Attributes[0].OriginalName == "HREF" || item.Attributes[0].OriginalName == "id")
        //        {
        //            continue;
        //        }
        //        #region
        //        //针对首页的特殊情况
        //        //else if (href_value == "http://www.hactcm.edu.cn/" || href_value == "http://xctz.hactcm.edu.cn/syss.htm")
        //        //{
        //        //    txt_href.Text += href_value + "\r\n\r\n";
        //        //    download.GetHtml(href_value);
        //        //    all.Add(href_value);
        //        //}
        //        #endregion
        //        else
        //        {
        //            if (href_value.Contains("http://") != true)
        //            {
        //                href_value = "http://www.hactcm.edu.cn/" + href_value;
        //            }

        //            txt_href.Text += href_value + "\r\n\r\n";
        //            //存入数据库
        //           OperateDB(href_value);
        //           //download.GetHtml(href_value);
        //           all.Add(href_value);
        //        }
        //    }
        //}
        #endregion      //使用Xpath来获取页面中存在的链接

        public static void GetUrl(string url)
        {

            if (HrefoldUrl == url)
            {
                //return null;
                return;
            }
            System.Net.WebClient client = new WebClient();
            if (url.Contains(".xml"))
            {
                return;
            }
            byte[] page = client.DownloadData(url);
            var  s = client.ResponseHeaders;
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
               //arr.Add(url);
                NewQueue.Enqueue(url);
            }
            HrefoldUrl = url;
            //return arr;
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
