using SimpleEmailSender.Models;

namespace SimpleEmailSender.Services.EmailService
{
    public interface IEmailService
    {
         Task<bool> SendEmail(EmailDto emailDto);
    }
}
