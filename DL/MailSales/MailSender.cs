using DL.MailModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace DL.MailSales
{
    public class MailSender
    {

        private readonly MailSettings _mailSettings;

         public static string SmtpServer_Host = "mail.warshahtech.net";
        public static string SmtpServer_UserName = "NoReplay@warshahtech.net";
        public static string SmtpServer_Port = "587";
        // public static string webServiceURL = ConfigurationManager.AppSettings["WSurl"];
        public static string SmtpServer_Password = "OneTMocha2020#@";
        public byte[] bytes;
        public string FileName;

        public bool SendMail(string ToEmail, string Subject, string Body, bool IsThread)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpServer_UserName, SmtpServer_Password);
                SmtpServer.Port = Convert.ToInt32(SmtpServer_Port);
                SmtpServer.Host = SmtpServer_Host;

                mail = new MailMessage();
                mail.From = new MailAddress("NoReplay@warshahtech.net");
                mail.To.Add(ToEmail.Trim());
                //mail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes),FileName ));
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpServer.EnableSsl = true;
                if (IsThread)
                {
                    Thread email = new Thread(() => SmtpServer.Send(mail));
                    email.IsBackground = true;
                    email.Start();
                }
                else
                    SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendMailcontactusInfo(string ToEmail, string Subject, string Body, bool IsThread)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@warshahtec.sa", "info$1800");
                SmtpServer.Port = Convert.ToInt32(SmtpServer_Port);
                SmtpServer.Host = "webmail.warshahtech.sa";

                mail = new MailMessage();
                mail.From = new MailAddress("info@warshahtec.sa");
                mail.To.Add(ToEmail.Trim());
                //mail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes),FileName ));
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpServer.EnableSsl = false;
                if (IsThread)
                {
                    Thread email = new Thread(() => SmtpServer.Send(mail));
                    email.IsBackground = true;
                    email.Start();
                }
                else
                    SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool SendAttachMail(string ToEmail, string attachment, string Subject, string Body, bool IsThread)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(SmtpServer_UserName, SmtpServer_Password);
                SmtpServer.Port = Convert.ToInt32(SmtpServer_Port);
                SmtpServer.Host = SmtpServer_Host;

                mail = new MailMessage();
                mail.From = new MailAddress(SmtpServer_UserName);
                mail.To.Add(ToEmail.Trim());



                mail.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes), FileName));
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.High;
                SmtpServer.EnableSsl = true;
                if (IsThread)
                {
                    Thread email = new Thread(() => SmtpServer.Send(mail));
                    email.IsBackground = true;
                    email.Start();
                }
                else
                    SmtpServer.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

