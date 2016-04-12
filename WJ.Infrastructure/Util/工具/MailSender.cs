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
        #region ���͵����ʼ�
        /// <summary>
        /// ���͵����ʼ�
        /// </summary>
        /// <param name="biaoti">����</param>
        /// <param name="zhengwen">����</param>
        /// <param name="to">�����˵�ַ</param>
        /// <param name="yourname">����������</param>
        /// <param name="zhanghao">�������˺�</param>
        /// <param name="mima">����������</param>
        /// <param name="smtp">smtp��ַ</param>
        public void sendMail(string biaoti, string zhengwen, List<string> to, string yourname, string zhanghao, string mima,string smtp,string smtpport)
        {
            try
            {
                System.Net.Mail.MailMessage mailObj = new System.Net.Mail.MailMessage();
                mailObj.IsBodyHtml = true;
                mailObj.Subject = biaoti;//�ʼ�����
                mailObj.Body = zhengwen;//�ʼ�����
                foreach (string str in to)
                {
                    mailObj.To.Add(str);//�����˵�ַ
                }
                System.Net.Mail.SmtpClient SmtpMail = new SmtpClient(smtp);//smtp��ַ
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
