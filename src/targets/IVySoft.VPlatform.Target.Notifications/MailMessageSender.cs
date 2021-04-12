using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace IVySoft.VPlatform.Target.Notifications
{
    class MailMessageSender : IEmailSender
    {
        private EmailSettings settings_;

        public MailMessageSender(IOptions<EmailSettings> emailSettings)
        {
            this.settings_ = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(this.settings_.UsernameEmail, this.settings_.UsernameName)
            };
            if (!string.IsNullOrWhiteSpace(email))
            {
                mail.To.Add(new MailAddress(email));
                if (!string.IsNullOrWhiteSpace(this.settings_.CcEmail))
                {
                    mail.CC.Add(new MailAddress(this.settings_.CcEmail));
                }
            }
            else if (!string.IsNullOrWhiteSpace(this.settings_.CcEmail))
            {
                mail.To.Add(new MailAddress(this.settings_.CcEmail));
            }

            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient(this.settings_.PrimaryDomain, this.settings_.PrimaryPort))
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(
                    this.settings_.UsernameEmail,
                    this.settings_.UsernamePassword);

                await smtp.SendMailAsync(mail);
            }
        }
    }
}
