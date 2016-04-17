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
namespace Spider1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string url = txt_url.Text.ToString().Trim(), newUrl = "";
            if (url.Contains("http://") != true)
            {
                newUrl = "http://" + url;
            }
            else
            {
                newUrl = url;
            }
            HttpClient client = new HttpClient(newUrl);
            string html = client.GetString();
            txt_content.Text = html;
            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(html);
            ////Stream reader = client.GetStream();
            ////doc.Load(reader);
            //IEnumerable<HtmlAgilityPack.HtmlNode> node = doc.DocumentNode.Descendants("a");
            //foreach (var item in node)
            //{
            //    txt_a.Text += item + "\r\n";
            //}
           string [] urls= ExtractLinks(newUrl, html);
           txt_a.Text = "wancheng ";
        }


      
        /// <summary>
        /// 解析页面并获取页面解析出来的URl
        /// </summary>
        /// <param name="baseUri">种子Url</param>
        /// <param name="html">通过种子URl获取到的html页面的源代码</param>
        /// <returns></returns>
        public static string[] ExtractLinks(string baseUri, string html)
        {
            Collection<string> urls = new Collection<string>();
            try
            {
                string strRef = @"(href|HREF|src|SRC)[ ]*=[""'][^""'#>]+[""']";
                MatchCollection matches = new Regex(strRef).Matches(html);
                foreach (Match match in matches)
                {
                    strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');
                    try
                    {
                        if (IsGoodUri(strRef))
                        {
                            Normalize(baseUri, ref strRef);
                            urls.Add(strRef);
                        }
                    }
                    catch (Exception)
                    { 
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return urls.ToArray();
        }
        static bool IsGoodUri(string strUri)
        {
            if (strUri.ToLower().StartsWith("javascript:"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// foamliu, 2009/12/27.
        /// 爬虫需要两个URL是否指向相同的页面这一点可以被迅速检测出来, 这就需要URL规范化.
        /// URL规范化做的主要的事情:
        /// 转换为小写
        /// 相对URL转换成绝对URL
        /// 删除默认端口号
        /// 根目录添加斜杠
        /// 猜测的目录添加尾部斜杠
        /// 删除分块
        /// 解析路径
        /// 删除缺省名字
        /// 解码禁用字符
        /// </summary>
        /// <param name="strURL"></param>
        public static void Normalize(string baseUri, ref string strUri)
        {
            // 相对URL转换成绝对URL
            if (strUri.StartsWith("/"))
            {
                strUri = baseUri + strUri.Substring(1);
            }

            // 当查询字符串为空时去掉问号"?"
            if (strUri.EndsWith("?"))
                strUri = strUri.Substring(0, strUri.Length - 1);

            // 转换为小写
            strUri = strUri.ToLower();

            // 删除默认端口号
            // 解析路径
            // 解码转义字符
            Uri tempUri = new Uri(strUri);
            strUri = tempUri.ToString();

            // 根目录添加斜杠
            int posTailingSlash = strUri.IndexOf("/", 8);
            if (posTailingSlash == -1)
                strUri += '/';

            // 猜测的目录添加尾部斜杠
            if (posTailingSlash != -1 && !strUri.EndsWith("/") && strUri.IndexOf(".", posTailingSlash) == -1)
                strUri += '/';

            // 删除分块
            int posFragment = strUri.IndexOf("#");
            if (posFragment != -1)
            {
                strUri = strUri.Substring(0, posFragment);
            }

            // 删除缺省名字
            string[] DefaultDirectoryIndexes = 
            {
                "index.html",
                "default.asp",
                "default.aspx",
            };
            foreach (string index in DefaultDirectoryIndexes)
            {
                if (strUri.EndsWith(index))
                {
                    strUri = strUri.Substring(0, (strUri.Length - index.Length));
                    break;
                }
            }


        }

    }
}
