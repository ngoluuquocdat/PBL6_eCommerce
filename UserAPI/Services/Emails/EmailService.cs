using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace UserAPI.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(string email, string content){

            string MyEmailAddress = _config["MyEmail:EmailAddress"];
            string Password = _config["MyEmail:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("eComPBL6", MyEmailAddress));
            message.To.Add(new MailboxAddress("Client", email));
            message.Subject = "Reset password";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = content;
            bodyBuilder.TextBody = "This is some plain text";

            message.Body = bodyBuilder.ToMessageBody();
            
            using (var client = new SmtpClient()){
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate( MyEmailAddress, Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        
    }
}