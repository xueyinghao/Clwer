using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace WJ.Infrastructure.Util
{
    public partial class MailSender
    {
        #region 发送电子邮件
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="biaoti">标题</param>
        /// <param name="zhengwen">正文</param>
        /// <param name="to">收信人地址</param>
        /// <param name="yourname">发信人姓名</param>
        /// <param name="zhanghao">发信人账号</param>
        /// <param name="mima">发信人密码</param>
        /// <param name="smtp">smtp地址</param>
        public void sendMail(string biaoti, string zhengwen, List<string> to, string yourname, string zhanghao, string mima,string smtp,string smtpport)
        {
            try
            {
                System.Net.Mail.MailMessage mailObj = new System.Net.Mail.MailMessage();
                mailObj.IsBodyHtml = true;
                mailObj.Subject = biaoti;//邮件标题
                mailObj.Body = zhengwen;//邮件正文
                foreach (string str in to)
                {
                    mailObj.To.Add(str);//收信人地址
                }
                System.Net.Mail.SmtpClient SmtpMail = new SmtpClient(smtp);//smtp地址
                mailObj.From = new MailAddress(zhanghao, yourname, System.Text.Encoding.UTF8);
                SmtpMail.Credentials = new System.Net.NetworkCredential(zhanghao, mima);
                if (!string.IsNullOrEmpty(smtpport))
                {
                    //SmtpMail.Port = 587;
                    SmtpMail.Port = Convert.ToInt32(smtpport);
                }
                SmtpMail.EnableSsl = false;
                //SmtpMail.SendCompleted += new SendCompletedEventHandler(back);
                //SmtpMail.SendAsync(mailObj,null);
                SmtpMail.Send(mailObj);
            }
            catch
            {
            }

        }
        #endregion
        //private void back(object sender, AsyncCompletedEventArgs e)
        //{

        //}
    }
}
