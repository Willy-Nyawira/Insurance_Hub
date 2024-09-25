using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Application.ServiceInterfaces;
using Microsoft.Extensions.Options;

namespace InsuranceHub.Application.Services
{
    public class EmailService :  IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
            {
                smtpClient.Host = _emailSettings.Host;
                smtpClient.Port = _emailSettings.Port;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.UserName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
        public async Task SendPasswordResetEmail(string email, string token)
        {
            var subject = "Password Reset Request";
            var resetUrl = $"https://localhost:7129/reset-password?token={token}";
            var message = $@"
        <p>To reset your password, please click the following link:</p>
        <p><a href='{resetUrl}'>Reset Password</a></p>";
            await SendEmailAsync(email, subject, message);
        }
    }

}

