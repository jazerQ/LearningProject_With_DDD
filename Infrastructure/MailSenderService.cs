using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace Infrastructure
{
    public class MailSenderService : IMailSenderService
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;

        private const string senderEmail = "saltanhayrliev2006@gmail.com";
        private static readonly string senderPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD")!;


        public async Task SendEmailAsync(string recipientEmail, string subject, string message)
        {
            using (var client = new SmtpClient(SmtpServer, SmtpPort))
            {
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.EnableSsl = true;
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, "Салтан"),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(recipientEmail);
                await client.SendMailAsync(mailMessage);
                Console.WriteLine($"Email sent to {recipientEmail}");
            }
        }
    }
}
