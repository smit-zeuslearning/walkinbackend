using MimeKit;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace WalkinPortalAPI.src.Mail
{
    public class EmailService: IEmailService
    {
        //private readonly MailSettings _mailsettings;

        //public MailService (IOptions<MailSettings> mailsettings)
        //{
        //    _mailsettings = mailsettings.Value;
        //}

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailsettings.Mail);
            email.Sender = MailboxAddress.Parse("smitpatel2301322002@gmail.com");
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.subject;

            var builder = new BodyBuilder();
            if(mailRequest.Attachments != null)
            {
                byte[] filebytes;
                foreach(var file in mailRequest.Attachments)
                {
                    if(file.Length > 0)
                    {
                        using(var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            filebytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, filebytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //smtp.Connect("_mailsettings.Host", _mailsettings.Port, SecureSocketOptions.StartTls);
            //smtp.Authenticate(_mailsettings.Mail, _mailsettings.Password);
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("smitpatel2301322002@gmail.com", "bqvrtuprhlncaxyo");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
