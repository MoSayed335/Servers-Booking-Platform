using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Shatbly.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("ahmedwebdevelop2@gmail.com", "qxua yvou nyfl hxvg"),

            };
            var mailMessage = new MailMessage("ahmedwebdevelop2@gmail.com", email, subject, htmlMessage)
            {
                IsBodyHtml = true
            };
            return client.SendMailAsync(mailMessage);
        }
    }
}
