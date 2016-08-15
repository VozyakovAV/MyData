using System;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Common
{
    public class EmailSender
    {
        protected EmailSMTP _smtp;
        protected EmailUser _user;

        public EmailSender(EmailSMTP smtp, EmailUser user)
        {
            this._smtp = smtp;
            this._user = user;
        }

        public void Send(string emailTo, string title, string body, bool isBodyHtml = true, string[] attachedFiles = null)
        {
            using (var smtp = GetSmtpClient(_smtp))
            {
                smtp.Credentials = new NetworkCredential(_user.Username, _user.Password);
                using (var mail = new MailMessage(_user.Username, emailTo, title, body))
                {
                    mail.IsBodyHtml = isBodyHtml;
                    if (attachedFiles != null)
                    {
                        foreach (var fileName in attachedFiles)
                        {
                            if (File.Exists(fileName))
                                mail.Attachments.Add(new Attachment(fileName));
                        }
                    }
                    smtp.Send(mail);
                }
            }
        }

        protected SmtpClient GetSmtpClient(EmailSMTP smtp)
        {
            return new SmtpClient()
            {
                Host = smtp.SmtpServer,
                Port = smtp.Port,
                EnableSsl = smtp.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
            };
        }
    }

    public class EmailSMTP
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }

        public EmailSMTP(string smtpServer, int port, bool ssl)
        {
            this.SmtpServer = smtpServer;
            this.Port = port;
            this.SSL = ssl;
        }

        public static EmailSMTP Gmail
        {
            get { return new EmailSMTP("smtp.gmail.com", 587, true); }
        }

        public static EmailSMTP Mail
        {
            get { return new EmailSMTP("smtp.mail.ru", 25, false); }
        }

        public static EmailSMTP Yandex
        {
            get { return new EmailSMTP("smtp.yandex.ru", 587, true); }
        }
    }

    public class EmailUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public EmailUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}