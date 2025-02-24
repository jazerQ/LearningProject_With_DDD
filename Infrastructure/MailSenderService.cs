using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
namespace Infrastructure
{
    public class MailSenderService
    {
        private const string SmtpServer = "smtp.mail.ru";
        private const int SmtpPort = 587;

        private const string senderEmail = "saltanhayrliev2006@mail.ru";
        private const string senderPassword = Environment.GetEnvironmentVariable("SMTP");
    }
}
