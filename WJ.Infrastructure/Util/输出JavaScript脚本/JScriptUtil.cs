using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WJ.Infrastructure.Util
{
    /**/
    /// <summary>
    /// �ṩ��ҳ������ͻ��˴���ʵ�����⹦�ܵķ���
    /// </summary>
    /// <remarks>
    /// </remarks>

    public class JScriptUtil
    {

        /**/
        /// <summary>
        /// �����ղؼ�
        /// </summary>
        /// <param name="url">�ղ���ַ</param>
        /// <param name="urlname">�ղ���ַ��ʾ</param>
        /// <param name="name">�ղ���ַ����</param>

        public static void aa(string url, string urlname, string name)
        {
            string js = @"<a href=# onClick=window.external.addFavorite('" + url + ",'" + urlname + "') target=_self title='" + urlname + "'>" + name + "</a>  ";
            HttpContext.Current.Response.Write(js);
        }


        /**/
        /// <summary>
        /// �����Ժ�д�Լ��Ľű�
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="yourJs"></param>
        public static void ClientWrite(string yourJs)
        {
            Page pages;
            pages = HttpContext.Current.Handler as System.Web.UI.Page;
            pages.Controls.Add(new System.Web.UI.LiteralControl("<script language=javascript>" + yourJs + "</script>"));
        }
        /**/
        /**/
        /**/
        /// <summary>
        /// ������ǰд�Լ��Ľű�
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="yourJs"></param>
        public static void ClientWrite2(string yourJs)
        {
            Page pages;
            pages = HttpContext.Current.Handler as System.Web.UI.Page;
            pages.Response.Write("<script language=javascript>");
            pages.Response.Write(yourJs);
            pages.Response.Write(" </script>");
        }

        /**/
        /**/

        /**/
        /**/
        /**/
        /// <summary>
        ///  ��������Ժ�̽���Ի���
        ///  </summary>
        public static void Alert(string msg)
        {
            Page pages;
            pages = HttpContext.Current.Handler as System.Web.UI.Page;
            msg = msg.Replace("'", "");
            msg = msg.Replace("\"", "");
            msg = msg.Replace("\n", @"\n").Replace("\r", @"\r").Replace("\"", @"\""");

            pages.Controls.Add(new System.Web.UI.LiteralControl("<script language=javascript>alert('" + msg + "');</script>"));
        }
        /**/
        /**/
        /**/
        /// <summary>
        /// ����û�м��ص�ʱ����pageload��ʱ��̽���Ի���
        /// </summary>
        public static void Alert_none(string msg)
        {
            Page pages;
            pages = HttpContext.Current.Handler as System.Web.UI.Page;
            msg = msg.Replace("'", "");
            msg = msg.Replace("\"", "");
            msg = msg.Replace("\n", @"\n").Replace("\r", @"\r").Replace("\"", @"\""");
            string retu = " alert('" + msg + "');";
            ClientWrite2(retu);
        }
        /**/
        /**/
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }


        /**/
        /// <summary>
        /// ����JavaScriptС����
        /// </summary>
        /// <param name="js">������Ϣ</param>

        public static void Alert(object message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('{0}');  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, message.ToString()));
        }

        public static void RtnRltMsgbox(object message, string strWinCtrl)
        {
            string js = @"<Script language='JavaScript'>
                     strWinCtrl = true;
                     strWinCtrl = if(!confirm('" + message + "'))return false;</Script>";
            HttpContext.Current.Response.Write(string.Format(js, message.ToString()));
        }

        /**/
        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
        }

        /**/
        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        /**/
        /// <summary>
        /// ˢ�±�ҳ
        /// </summary>
        public static void Refreshself()
        {
            string js = @"<Script language='JavaScript'>
                    window.location=window.location.href;
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }
        /**/
        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void RefreshParent(string parurl)
        {
            string js = @"<Script language='JavaScript'>
                  window.opener.location.reload('" + parurl + "')</Script>";
            HttpContext.Current.Response.Write(js);
        }
        /**/
        /// <summary>
        /// ˢ�¸����
        /// </summary>
        public static void RefreshParentifarme(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.location.reload('" + url + "')</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        /// ��ʽ��ΪJS�ɽ��͵��ַ���
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JSStringFormat(string s)
        {
            return s.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"");
        }

        /**/
        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }


        /**/
        /// <summary>
        /// ��С����
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="left"></param>
        public static void OpenLittleWindow(string url, int width, int height, int top, int left)
        {
            string str, js;
            str = "var popup;popup=window.open('{url}',null,'scrollbars=yes,status=no,width={width},height={height},top={top},left={left}');popup.opener=self.opener;self.close();";
            str = str.Replace("{width}", width.ToString());
            str = str.Replace("{height}", height.ToString());
            str = str.Replace("{top}", top.ToString());
            str = str.Replace("{left}", left.ToString());
            str = str.Replace("{url}", url);
            js = @"<Script language='JavaScript'>" + str + " </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /**/

        /**/
        /// <summary>
        /// �򿪴���
        /// </summary>
        /// <param name="url"></param>
        public static void OpenWebForm(string url)
        {
            /**/
            /*��������������������������������������������������������������������*/
            /**/
            /*�޸�Ŀ��:    �¿�ҳ��ȥ��ie�Ĳ˵�������                        */

            string js = @"<Script language='JavaScript'>
            //window.open('" + url + @"');
            window.open('" + url + @"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');
            </Script>";
            /**/
            /*����*/
            /**/
            /*��������������������������������������������������������������������*/


            HttpContext.Current.Response.Write(js);
        }
        public static void OpenWebForm(string url, string name, string future)
        {
            string js = @"<Script language='JavaScript'>
                     window.open('" + url + @"','" + name + @"','" + future + @"')
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }
        public static void OpenWebForm(string url, string formName)
        {
            /**/
            /*��������������������������������������������������������������������*/
            /**/
            /*�޸�Ŀ��:    �¿�ҳ��ȥ��ie�Ĳ˵�������                        */
            /**/
            /*ע������:                                */
            /**/
            /*��ʼ*/
            string js = @"<Script language='JavaScript'>
            window.open('" + url + @"','" + formName + @"','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');
            </Script>";
            /**/
            /*����*/
            /**/
            /*��������������������������������������������������������������������*/

            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:OpenWebForm    
        /// ��������:��WEB����    
        /// </summary>
        /// <param name="url">WEB����</param>
        /// <param name="isFullScreen">�Ƿ�ȫ��Ļ</param>
        public static void OpenWebForm(string url, bool isFullScreen)
        {
            string js = @"<Script language='JavaScript'>";
            if (isFullScreen)
            {
                js += "var iWidth = 0;";
                js += "var iHeight = 0;";
                js += "iWidth=window.screen.availWidth-10;";
                js += "iHeight=window.screen.availHeight-50;";
                js += "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no';";
                js += "window.open('" + url + @"','',szFeatures);";
            }
            else
            {
                js += "window.open('" + url + @"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');";
            }
            js += "</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url"></param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        /// ָ���Ŀ��ҳ��ת��
        /// </summary>
        /// <param name="FrameName"></param>
        /// <param name="url"></param>
        public static void JavaScriptFrameHref(string FrameName, string url)
        {
            string js = @"<Script language='JavaScript'>
                    
                    @obj.location.replace(""{0}"");
                  </Script>";
            js = js.Replace("@obj", FrameName);
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        ///����ҳ��
        /// </summary>
        public static void JavaScriptResetPage(string strRows)
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.CenterFrame.rows='" + strRows + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        /// ������:JavaScriptSetCookie
        /// ��������:�ͻ��˷�������Cookie
        /// </summary>
        /// <param name="strName">Cookie��</param>
        /// <param name="strValue">Cookieֵ</param>
        public static void JavaScriptSetCookie(string strName, string strValue)
        {
            string js = @"<script language=Javascript>
            var the_cookie = '" + strName + "=" + strValue + @"'
            var dateexpire = 'Tuesday, 01-Dec-2020 12:00:00 GMT';
            //document.cookie = the_cookie;//д��Cookie<BR>} <BR>
            document.cookie = the_cookie + '; expires='+dateexpire;            
            </script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:GotoParentWindow    
        /// ��������:���ظ�����    
        /// </summary>
        /// <param name="parentWindowUrl">������</param>        
        public static void GotoParentWindow(string parentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    this.parent.location.replace('" + parentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:ReplaceParentWindow    
        /// ��������:�滻������    
        /// </summary>
        /// <param name="parentWindowUrl">������</param>
        /// <param name="caption">������ʾ</param>
        /// <param name="future">������������</param>
        public static void ReplaceParentWindow(string parentWindowUrl, string caption, string future)
        {
            string js = "";
            if (future != null && future.Trim() != "")
            {
                js = @"<script language=javascript>this.parent.location.replace('" + parentWindowUrl + "','" + caption + "','" + future + "');</script>";
            }
            else
            {
                js = @"<script language=javascript>var iWidth = 0 ;var iHeight = 0 ;iWidth=window.screen.availWidth-10;iHeight=window.screen.availHeight-50;
                            var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';this.parent.location.replace('" + parentWindowUrl + "','" + caption + "',szFeatures);</script>";
            }

            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:ReplaceOpenerWindow    
        /// ��������:�滻��ǰ����Ĵ򿪴���    
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴���</param>        
        public static void ReplaceOpenerWindow(string openerWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.replace('" + openerWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:ReplaceOpenerParentWindow    
        /// ��������:�滻��ǰ����Ĵ򿪴��ڵĸ�����    
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴��ڵĸ�����</param>        
        public static void ReplaceOpenerParentFrame(string frameName, string frameWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent." + frameName + ".location.replace('" + frameWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:ReplaceOpenerParentWindow    
        /// ��������:�滻��ǰ����Ĵ򿪴��ڵĸ�����    
        /// </summary>
        /// <param name="openerWindowUrl">��ǰ����Ĵ򿪴��ڵĸ�����</param>        
        public static void ReplaceOpenerParentWindow(string openerParentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent.location.replace('" + openerParentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>        
        /// ������:CloseParentWindow    
        /// ��������:�رմ���    
        /// </summary>
        public static void CloseParentWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void CloseOpenerWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /// <summary>
        /// ������:ShowModalDialogJavascript    
        /// ��������:���ش�ģʽ���ڵĽű�    
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl)
        {
            string js = @"<script language=javascript>
                            var iWidth = 0 ;
                            var iHeight = 0 ;
                            iWidth=window.screen.availWidth-10;
                            iHeight=window.screen.availHeight-50;
                            var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';
                            showModalDialog('" + webFormUrl + "','',szFeatures);</script>";
            return js;
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            string js = @"<script language=javascript>                            
                            showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
        }

        /**/
        /// <summary>
        /// ������:ShowModalDialogWindow    
        /// ��������:��ģʽ����    
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static void ShowModalDialogWindow(string webFormUrl)
        {
            string js = ShowModalDialogJavascript(webFormUrl);
            HttpContext.Current.Response.Write(js);
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=no";
            ShowModalDialogWindow(webFormUrl, features);
        }

        public static void SetHtmlElementValue(string formName, string elementName, string elementValue)
        {
            string js = @"<Script language='JavaScript'>if(document." + formName + "." + elementName + "!=null){document." + formName + "." + elementName + ".value =" + elementValue + ";}</Script>";
            HttpContext.Current.Response.Write(js);
        }

        /**/
        /**/
        /**/
        /// <summary>
        /// �س�-��tab
        /// </summary>
        /// <param name="page"></param>
        public static void ToTab()
        {
            Page page;
            page = HttpContext.Current.Handler as System.Web.UI.Page;
            System.Text.StringBuilder scriptFunction = new StringBuilder();
            scriptFunction.Append("<script language='javascript'>");
            scriptFunction.Append("       function returnTotab()");
            scriptFunction.Append("         {");
            scriptFunction.Append("          if(event.keyCode==13)    ");
            scriptFunction.Append("             {event.keyCode=9;     ");
            scriptFunction.Append("               return true;}       ");
            scriptFunction.Append("          } ");
            scriptFunction.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "totab", scriptFunction.ToString());

        }
        /**/
        /**/
        /**/
        /// <summary>
        /// tab->enter
        /// </summary>
        /// <param name="page"></param>
        public static void tabToEnter()
        {
            Page page;
            page = HttpContext.Current.Handler as System.Web.UI.Page;
            System.Text.StringBuilder scriptFunction = new StringBuilder();
            scriptFunction.Append("<script language='javascript'>");
            scriptFunction.Append("    function Tcheck()");
            scriptFunction.Append("         {");
            scriptFunction.Append("         if(event.keyCode==8||event.keyCode==9) ");
            scriptFunction.Append("          return true;");
            scriptFunction.Append("         else ");
            scriptFunction.Append("         {");
            scriptFunction.Append("          if(((event.keyCode>=48)++(event.keyCode<=57))||((event.keyCode>=96)++(event.keyCode<=105)))");
            scriptFunction.Append("              return true;");
            scriptFunction.Append("          else");
            scriptFunction.Append("          if(event.keyCode==13||event.keyCode==110||event.keyCode==190||event.keyCode==39)");
            scriptFunction.Append("             {event.keyCode=9;");
            scriptFunction.Append("               return true;}");
            scriptFunction.Append("            else");
            scriptFunction.Append("              return false;");
            scriptFunction.Append("        }");
            scriptFunction.Append("          }     ");
            scriptFunction.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "switch", scriptFunction.ToString());
        }
        /**/
        /**/
        /**/
        /// <summary>
        /// attachEvent
        /// </summary>
        /// <param name="controlToFocus"></param>
        /// <param name="page"></param>
        public static void attachEvent(Control[] controlToFocus)
        {
            Page page;
            page = HttpContext.Current.Handler as System.Web.UI.Page;
            System.Text.StringBuilder scriptFunction = new StringBuilder();
            string scriptClientId;
            scriptFunction.Append("<script language='javascript'>");

            foreach (Control con in controlToFocus)
            {
                scriptClientId = con.ClientID;
                scriptFunction.Append("document.getElementById('" + scriptClientId + "').attachEvent('onkeydown', Tcheck);");
            }
            scriptFunction.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "attach", scriptFunction.ToString());
        }


        /**/
        /**/
        /**/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlToFocus"></param>
        /// <param name="page"></param>
        /// <param name="eventStr"></param>
        /// <param name="FuncStr"></param>
        public static void AttachEvent(Control[] controlToFocus, string

    eventStr, string FuncStr)
        {
            Page page;
            page = HttpContext.Current.Handler as System.Web.UI.Page;
            System.Text.StringBuilder scriptFunction = new StringBuilder();
            string scriptClientId;
            scriptFunction.Append("<script language='javascript'>");
            foreach (Control con in controlToFocus)
            {
                scriptClientId = con.ClientID;
                scriptFunction.Append("document.getElementById('" + scriptClientId + "').attachEvent('" + eventStr + "', " + FuncStr + ");");
            }
            scriptFunction.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "attach2", scriptFunction.ToString());
        }
        /**/
        /**/
        /**/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public static void NumOnlyFun()
        {
            Page page;
            page = HttpContext.Current.Handler as System.Web.UI.Page;
            System.Text.StringBuilder scriptFunction = new StringBuilder();
            scriptFunction.Append("<script language='javascript'>");
            scriptFunction.Append("       function isNum()");
            scriptFunction.Append("         {");
            scriptFunction.Append("              if(event.keyCode==8||event.keyCode==9) ");
            scriptFunction.Append("                  return true;");
            scriptFunction.Append("             else ");
            scriptFunction.Append("             {");
            scriptFunction.Append("          if(((event.keyCode>=48)++(event.keyCode<=57))||((event.keyCode>=96)++(event.keyCode<=105)))");
            scriptFunction.Append("              return true;");
            scriptFunction.Append("          else");
            scriptFunction.Append("                return false;");
            scriptFunction.Append("        }");
            scriptFunction.Append("          } ");
            scriptFunction.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "numonly", scriptFunction.ToString());

        }

    }

}
