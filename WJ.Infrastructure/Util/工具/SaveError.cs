using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WJ.Infrastructure.Util
{
    public class SaveError
    {
        public static string xmlpath;


        public void getpath()
        {
            xmlpath = DNTUtils.GetMapPath(ConfigUtil.GetConfig("ErrorLogPath")+"\\" + DateTime.Now.ToString("yyyyMMdd") + ".xml");
        }
        public static void Save(string error)
        {
            try
            {
                if (xmlpath == null)
                {
                    new SaveError().getpath();
                }
                if (XmlUtil.CreateDocument(xmlpath, "root"))
                {
                    XmlUtil.InsertElement(xmlpath, "root", "error", error);
                }
            }
            catch
            { }
        }
    }
}
