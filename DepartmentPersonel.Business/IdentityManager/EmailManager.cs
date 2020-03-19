using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities.Enums;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DepartmentPersonel.Business.IdentityManager
{
    public class EmailManager : IIdentityMessageService
    {
        //private string userId = HttpContext.Current.User.Identity.GetUserId();
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string FilePath { get; set; }
        public MessageStates MessageState { get; set; }
        public string SenderMail { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }

        public EmailManager()
        {
            this.SenderMail = "youremail@youremail.com";
            this.Password = "yourpassword";
            this.Smtp = "smtp.gmail.com";
            this.SmtpPort = 587;
        }
        public void Send(IdentityMessage message)
        {
            Task.Run(async () =>
            {
                await this.SendAsync(message);
            });
        }

        public async Task SendAsync(IdentityMessage message)
        {
            //var userID = userId ?? "system";
            try
            {
                var mail = new MailMessage { From = new MailAddress(this.SenderMail) };
                mail.To.Add(new MailAddress(message.Destination));
                if (!string.IsNullOrEmpty(FilePath))
                {
                    mail.Attachments.Add(new Attachment(FilePath));
                }

                if ((Cc != null) && (Cc.Length > 0))
                {
                    foreach (var cc in Cc)
                    {
                        mail.CC.Add(new MailAddress(cc));
                    }
                }
                if ((Bcc != null) && (Bcc.Length > 0))
                {
                    foreach (var bcc in Bcc)
                    {
                        mail.Bcc.Add(new MailAddress(bcc));
                    }
                }
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.HeadersEncoding = Encoding.UTF8;

                var smtpClient = new SmtpClient(this.Smtp, this.SmtpPort)
                {
                    Credentials = new NetworkCredential(this.SenderMail, this.Password),
                    EnableSsl = true
                };
                await smtpClient.SendMailAsync(mail);
                MessageState = MessageStates.Delivered;

            }
            catch (Exception)
            {
                MessageState = MessageStates.NotDelivered;
            }
        }
    }
}
