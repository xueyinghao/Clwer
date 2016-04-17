using System;
using System.Text;

namespace WJ.Infrastructure.Office
{
    /// <summary>
    ///  Excel样式
    /// </summary>
    public class ExcelStyle
    {
        StringBuilder s = new StringBuilder();

        public StringBuilder StyleString
        {
            get
            {
                s.Append("<Style ss:ID=\"" + styleid + "\" ");
                if (styleid == "Default")
                {
                    s.Append(" ss:Name=\"Normal\">");
                }
                else
                {
                    s.Append(">");
                }
                s.Append("<Alignment ss:Vertical=\"Center\"/>");
                s.Append("<Borders/>");
                s.Append("<Font ss:FontName=\"" + fontname + "\" x:CharSet=\"134\" ss:Size=\"" + fontsize + "\" ss:Color=\"" + fontcolor + "\"/>");
                s.Append("<Interior/>");
                s.Append("<NumberFormat/>");
                s.Append("<Protection/>");
                s.Append("</Style>");
                return s;
            }
        }

        private string styleid = "Default";

        public string StyleID
        {
            set { styleid = value; }
        }

        private string fontname = "宋体";

        public string FontName
        {
            set { fontname = value; }
        }
        private string fontsize = "11";

        public string FontSize
        {
            set { fontsize = value; }
        }
        private string fontcolor = "#000000";

        public string FontColor
        {
            set { fontcolor = value; }
        }

    }
}
