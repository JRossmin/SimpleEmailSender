using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SimpleEmailSender.Models;

namespace SimpleEmailSender.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmail(EmailDto request)
        {
            try
            {
                var email = new MimeKit.MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConf:EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = request.Body 
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.GetSection("EmailConf:EmailHost").Value, Convert.ToInt32(_config.GetSection("EmailConf:EmailPort").Value), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.GetSection("EmailConf:EmailUserName").Value, _config.GetSection("EmailConf:EmailPassword").Value);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return true; // Return true if email is sent successfully  
            }
            catch
            {
                return false; // Return false if an exception occurs  
            }
        }
    }
}
