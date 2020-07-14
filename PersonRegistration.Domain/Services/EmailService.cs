using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Interfaces.IService;

namespace PersonRegistration.Domain.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void sendEmail(EmailDTO email)
        {
            var message = new MimeMessage();

            //from 
            message.From.Add(new MailboxAddress(_config["Organization:Name"], _config["Organization:Email"]));

            //to
            message.To.Add(new MailboxAddress(email.Name, email.Email));

            //subject
            message.Subject = email.Subject;

            //body
            message.Body = new TextPart("plain")
            {
                Text = email.Body
            };

            //Configuration

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("dev.softand.corp@gmail.com", "Skate.078");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
