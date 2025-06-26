using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SimpleEmailSender.Models;
using SimpleEmailSender.Services.EmailService;

namespace SimpleEmailSender.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        } 
        [HttpPost]
        public IActionResult SendEmail(EmailDto bodyEmail)
        {
            _emailService.SendEmail(bodyEmail);
            return Ok();
        }
    }
}
