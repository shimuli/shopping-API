using Shopping.Models.DTOs;
using Shopping.Repo.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shopping.Repo
{
    public class EmailRepo : IEmailRepo
    {
        public async Task Send(string emailAddress, string body, EmailOptionsDto emailOptions)
        {
            // Set Up cleint
            var client = new SmtpClient();
            client.Host = emailOptions.Host;
            client.Credentials = new NetworkCredential(emailOptions.ApiKey, emailOptions.ApiKeySecret);
            client.Port = emailOptions.Port;

            //Set Up message
            var message = new MailMessage(emailOptions.SenderEmail, emailAddress);
            message.Body = body;
            message.IsBodyHtml = true;

            // Send EMail
            await client.SendMailAsync(message);


        }
    }
}
