using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DL.MailModels
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task<string>  SendWelcomeEmailAsync(WelcomeRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\ActivateTemp.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("{UserName}", request.UserName).Replace("{Link}", request.Link).Replace("[ID]",request.Id.ToString());
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "OK";

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
            
        }

        public static async Task<string> Email(string supject, string htmlString, string mail, string Sender, string Server, int Port, string Password)
        {
            try
            {
                MailMessage message = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                message.From = new MailAddress(Sender);
                message.To.Add(new MailAddress(mail));
                message.Subject = supject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = Port;
                smtp.Host = Server; //for gmail host  
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Sender, Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return "Ok";

            }
            catch (Exception ex)
            {
                return "No";
            }
        }
        public async Task<string> SendActivateEmailAsync(WelcomeRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + @"\wwwroot\EmailTemps\ActivateTempEmail.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("{UserName}", request.UserName).Replace("{Link}", request.Link).Replace("[ID]", request.Id.ToString());




                ////////////////////////////////////////////////////
                Email($"Welcome {request.UserName}", MailText, request.ToEmail, _mailSettings.Mail, _mailSettings.Host, _mailSettings.Port, _mailSettings.Password);
             return "OK";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }


        public async Task<string> Notification(string email , string Text)
        {
            try
            {
              



                
              await  Email("Warshah Tech Notification", Text, email, _mailSettings.Mail, _mailSettings.Host, _mailSettings.Port, _mailSettings.Password);
                return "OK";
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }
    }
}
