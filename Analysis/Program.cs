using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spider1;
using WJ.Infrastructure.Util;
using System.IO;
using System.Collections;
using HtmlAgilityPack;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
namespace Analysis
{
    class Program
    {
        public static string code = "", html = "";
        static void Main(string[] args)
        {
            #region
            //string url = "http://www.hactcm.edu.cn/info/1014/11217.htm";
            //HttpClient client = new HttpClient(url);
            //string html = client.GetString();
            //string result = HtmlUtil.GetElementById(html,"p");
            //Console.ReadKey();
            #endregion
            //存放各个页面的源代码
            ArrayList res = GetFile();
            //遍历源代码解析页面
            foreach (string item in res)
            {
                if (item.StartsWith("<script"))
                {
                    continue;
                }
                GetContent(item);
            }
            Console.WriteLine("执行完毕");
            Console.ReadKey();
        }
        //从下载页面的文件夹下将html文件获取到
        public static ArrayList GetFile()
        {
            ArrayList arr = new ArrayList();
            string foldername = "C:\\Users\\UpdatusUser\\Desktop\\download";
            DirectoryInfo TheFolder = new DirectoryInfo(foldername);
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                string path = @"C:\\Users\\UpdatusUser\\Desktop\\download\\" + NextFile;
                arr.Add(GetCode(path));

            }
            return arr;
        }

        //获取html页面的源代码
        public static string GetCode(string path)
        {
            #region
            //FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            //StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8"));
            //code = sr.ReadToEnd();
            //if (code.Contains("charset=gb2312"))
            //{
            //    FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            //    StreamReader sr1 = new StreamReader(fs1, Encoding.GetEncoding("gb2312"));
            //    code = sr1.ReadToEnd();
            //    sr1.Close();
            //    fs.Close();
            //    return code;
            //}
            //sr.Close();
            //fs.Close();
            //return code;
            #endregion
            code = IsLuan(path, "UTF-8");
            if (code.Contains("charset=gb2312"))
           {
               code = IsLuan(path, "gb2312");
           }
           if (code.Contains("charset=utf-8"))
           {
               code = IsLuan(path, "utf-8");
           }
           return code;
        }

        //解析HTML源代码，来获取Title标签的内容和p标签的内容
        //此处获取到页面的title内容和p标签的内容
        //接着需要将获取到的该内容进行分词索引

        public static void GetContent(string item)
        {
            string P_content = "", Title_Conten = "";
            HtmlAgilityPack.HtmlDocument hd = new HtmlAgilityPack.HtmlDocument();
            hd.LoadHtml(item);
            HtmlNodeCollection nodes = hd.DocumentNode.SelectNodes("//p");
            if (nodes != null)
            {
                foreach (HtmlAgilityPack.HtmlNode p_Content in nodes)
                {
                    P_content += p_Content.InnerText;
                }
            }
            HtmlNode node = hd.DocumentNode.SelectSingleNode("//title");
            if (node == null)
            {
                return;
            }
            Title_Conten = node.InnerText;
            if (Title_Conten == null)
            {
                Title_Conten = P_content;
            }
            OperateDB(Title_Conten, P_content);

        }

        //操作数据库,将页面上解析出来的内容存放到数据库中
        public static void OperateDB(string Title_Conten, string P_content)
        {
            if (P_content.StartsWith("<!--") || P_content.Contains("javascript"))
            {
                return;
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            conn.Open();
            //string sql = "Insert into tableContent values('" + Title_Conten + "','" + P_content + "')";
            string sql = "Insert into TestIndex values('" + Title_Conten + "','" + P_content + "')";
            SqlCommand command = new SqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        //判断获取到的页面的html源代码是否出现乱码
        public static string IsLuan(string path,string charset)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(charset));
            code = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return code;
        }
     
    }
}
