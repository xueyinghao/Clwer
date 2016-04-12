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
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="p_ToMail">目的邮件地址</param>
        /// <param name="p_MailTitle">发送邮件的标题</param>
        /// <param name="p_MailBoby">发送邮件的内容</param>
        /// <param name="p_MailAddress">发送者的邮箱地址</param>
        /// <param name="p_SmtpClient">网络上的代理服务器</param>
        /// <param name="p_Pwd">发送邮件的密码</param>
        /// <param name="returnstr">发送成功提示语</param>
        public static string SendEMail(string p_ToMail, string p_MailTitle, string p_MailBoby, string p_MailAddress, string p_SmtpClient, string p_Pwd, string returnstr)
        {
            MailMessage objMailMessage;
            //Attachment objMailAttachment;
            // 创建一个附件对象
            //objMailAttachment = new Attachment("f:\\世界杯赛程.rtf");//发送邮件的附件
            // 创建邮件消息
            objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(p_MailAddress);//发送者的邮箱地址

            objMailMessage.To.Add(p_ToMail);//目的邮件地址

            objMailMessage.Subject = p_MailTitle;//发送邮件的标题

            objMailMessage.Body = p_MailBoby;//发送邮件的内容
            objMailMessage.IsBodyHtml = true;
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            //objMailMessage.Attachments.Add(objMailAttachment);//将附件附加到邮件消息对象中
            //SMTP地址
            SmtpClient smtpClient = new SmtpClient(p_SmtpClient);//网络上的代理服务器
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(objMailMessage.From.Address, p_Pwd);//设置发件人身份的信息

            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            //smtpClient.Host = "smtp." + objMailMessage.From.Host;

            try
            {
                smtpClient.Send(objMailMessage);
                return returnstr;
            }
            catch (SmtpException ex)
            {
                return ex.Message;
            }
        }
    }
}
