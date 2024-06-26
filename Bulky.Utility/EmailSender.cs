﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration _config)
        {
            SendGridSecret = _config["SendGrid:SecretKey"];
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           var client = new SendGridClient(SendGridSecret);
            var from = new EmailAddress("info@apartmanibiljana.rs", "Bookly Book");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            return client.SendEmailAsync(msg);
        }
    }
}
