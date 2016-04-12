using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WJ.Infrastructure.Util
{
    public class RegHelper
    {
        //返回单个路径

        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            //string sPattern = @"^<img\s+[^>]*>";
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
            RegexOptions.Compiled);
            Match m = r.Match(HTMLStr.ToLower());
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }
        //返回多个路径的情况
        public static string MyGetImgUrl(string text)
        {
            string str = "";
            string pat = @"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>";

            Regex r = new Regex(pat, RegexOptions.Compiled);

            Match m = r.Match(text.ToLower());
            //int matchCount = 0;
            while (m.Success)
            {
                Group g = m.Groups[2];
                str += g;
                str += ",";
                m = m.NextMatch();
            }
            return str;
        }

    }
}
