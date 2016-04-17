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
        /// �����ʼ�
        /// </summary>
        /// <param name="p_ToMail">Ŀ���ʼ���ַ</param>
        /// <param name="p_MailTitle">�����ʼ��ı���</param>
        /// <param name="p_MailBoby">�����ʼ�������</param>
        /// <param name="p_MailAddress">�����ߵ������ַ</param>
        /// <param name="p_SmtpClient">�����ϵĴ��������</param>
        /// <param name="p_Pwd">�����ʼ�������</param>
        /// <param name="returnstr">���ͳɹ���ʾ��</param>
        public static string SendEMail(string p_ToMail, string p_MailTitle, string p_MailBoby, string p_MailAddress, string p_SmtpClient, string p_Pwd, string returnstr)
        {
            MailMessage objMailMessage;
            //Attachment objMailAttachment;
            // ����һ����������
            //objMailAttachment = new Attachment("f:\\���籭����.rtf");//�����ʼ��ĸ���
            // �����ʼ���Ϣ
            objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(p_MailAddress);//�����ߵ������ַ

            objMailMessage.To.Add(p_ToMail);//Ŀ���ʼ���ַ

            objMailMessage.Subject = p_MailTitle;//�����ʼ��ı���

            objMailMessage.Body = p_MailBoby;//�����ʼ�������
            objMailMessage.IsBodyHtml = true;
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            //objMailMessage.Attachments.Add(objMailAttachment);//���������ӵ��ʼ���Ϣ������
            //SMTP��ַ
            SmtpClient smtpClient = new SmtpClient(p_SmtpClient);//�����ϵĴ��������
            smtpClient.EnableSsl = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(objMailMessage.From.Address, p_Pwd);//���÷�������ݵ���Ϣ

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
