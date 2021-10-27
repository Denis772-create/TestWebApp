﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@metanit.com"));
            emailMessage.To.Add(new MailboxAddress("Receiver", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("aleksandrovdenis418@gmail.com", "3denis10sveta");

            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}