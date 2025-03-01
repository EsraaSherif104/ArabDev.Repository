using ArabDev.Data.Entities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace ArabDevCommunity.PL.Helper
{
    public class EmailSettings : IMailService
    {
        private MailSettings _Options;
        public EmailSettings(IOptions<MailSettings> options)
        {
            _Options = options.Value;
        }


        public void SendEmail(Email email)
        {
            var mail = new MimeMessage()
            {
                Sender=MailboxAddress.Parse(_Options.Email),
                Subject=email.Subject,

            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_Options.DisplayName, _Options.Email));

            var builder = new BodyBuilder();
            builder.TextBody=email.Body;
            mail.Body=builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_Options.Host, _Options.Port,MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(_Options.Email, _Options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
