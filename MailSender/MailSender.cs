using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender
{
    public static class MailSender
    {
        public static void Send(string toAddress, string subject, string message)
        {
            if (toAddress == null)
            {
                throw new ArgumentNullException(nameof(toAddress));
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("message", nameof(subject));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message", nameof(message));
            }

            //TODO: SMTP settings to config file
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465);

            smtpClient.Credentials = new System.Net.NetworkCredential("bms.service.confirmation@gmail.com", "BMSPassword");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("info@MyWebsiteDomainName", "MyWeb Site");
            mail.Subject = subject;
            mail.Body = message;
            mail.To.Add(new MailAddress(toAddress));

            smtpClient.Send(mail);
        }
    }
}
