#region  版权所有 杨立伟 Benny
//************************************
//   Created Time 2008-07-12
//   Created By Benny
//************************************
#endregion

using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Text;

namespace WJ.Infrastructure.Office
{
    /// <summary>
    /// Excel工作本
    /// </summary>
    public class ExcelWorkbook
    {
        private string filename=Guid.NewGuid().ToString().Replace("-","");
        /// <summary>
        /// 导出EXCEL文件名
        /// </summary>
        public string FileName
        {
            set { filename = value; }
        }

        ExcelStyleCollection styles = new ExcelStyleCollection();

        /// <summary>
        /// 样式集合
        /// </summary>
        public ExcelStyleCollection Styles
        {
            set { styles = value; }
        }

        WorksheetCollection worksheets = new WorksheetCollection();

        /// <summary>
        /// 表单集合
        /// </summary>
        public WorksheetCollection WorkSheets
        {
            get { return worksheets; }
            set { worksheets = value; }
        }

        public void OutPut()
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename="+filename+".xml");
            Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/ms-excel";
            FormatExcel(Response);
            Response.End();
        }

        private void FormatExcel(HttpResponse Response)
        {
            WriteBegininfo(Response);
            WriteDocumentProperties(Response);
            WriteOfficeDocumentSettings(Response);
            WriteExcelWorkbook(Response);
            WriteStyles(Response, styles);
            WriteWorkSheets(Response, worksheets);
            Response.Write("</Workbook>");
        }

        private void WriteWorkSheets(HttpResponse Response, WorksheetCollection worksheets)
        {
            foreach (Worksheet ws in worksheets)
            {
                ws.OutPut();
                WriteWorksheetOptions(Response);
                Response.Write("</Worksheet>");
            }  
        }

        private void WriteWorksheetOptions(HttpResponse Response)
        {
            Response.Write("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<PageSetup><Header x:Margin=\"0.3\"/><Footer x:Margin=\"0.3\"/><PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/></PageSetup>");
            Response.Write("<Print><ValidPrinterInfo/><PaperSizeIndex>9</PaperSizeIndex><HorizontalResolution>200</HorizontalResolution><VerticalResolution>200</VerticalResolution></Print>");
            Response.Write("<Selected/>");
            Response.Write("<Panes><Pane><Number>3</Number><ActiveRow>2</ActiveRow><ActiveCol>2</ActiveCol></Pane></Panes>");
            Response.Write("<ProtectObjects>False</ProtectObjects>");
            Response.Write(" <ProtectScenarios>False</ProtectScenarios>");
            Response.Write("</WorksheetOptions>");
        }

        private void WriteStyles(HttpResponse Response, ExcelStyleCollection styles)
        {
            Response.Write("<Styles>");
            foreach (ExcelStyle s in styles)
            {
                Response.Write(s.StyleString.ToString());
            }
            Response.Write("</Styles>");
        }

        private void WriteExcelWorkbook(HttpResponse Response)
        {
            Response.Write("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel \">");
            Response.Write("<WindowHeight>11640</WindowHeight>");
            Response.Write("<WindowWidth>15480</WindowWidth>");
            Response.Write("<WindowTopX>0</WindowTopX>");
            Response.Write("<WindowTopY>90</WindowTopY>");
            Response.Write("<ProtectStructure>False</ProtectStructure>");
            Response.Write("<ProtectWindows>False</ProtectWindows>");
            Response.Write("</ExcelWorkbook>");
        }

        private void WriteOfficeDocumentSettings(HttpResponse Response)
        {
            Response.Write(" <OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">");
            Response.Write(" <RemovePersonalInformation/>");
            Response.Write(" </OfficeDocumentSettings>");
        }

        private void WriteDocumentProperties(HttpResponse Response)
        {
            Response.Write("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            Response.Write("<Created>"+DateTime.Now.ToString()+"</Created>");
            Response.Write("<LastSaved>"+DateTime.Now.ToString()+"</LastSaved>");
            Response.Write("<Version>12.00</Version>");
            Response.Write("</DocumentProperties>");
        }

        private void WriteBegininfo(HttpResponse Response)
        {
            #region
            Response.Write("<?xml version=\"1.0\" encoding=\"GB2312\" ?>");
            Response.Write("<?mso-application progid=\"Excel.Sheet\"?>");
            Response.Write("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"  xmlns:o=\"urn:schemas-microsoft-com:office:office\"  xmlns:x=\"urn:schemas-microsoft-com:office:excel\"  xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"  xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            #endregion
        }
    }
}
