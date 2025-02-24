
namespace Infrastructure
{
    public interface IMailSenderService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message);
    }
}