using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MyWebApi.Models;
using MyWebApi.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.Mail
{
    public class MailSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public interface ISendMailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
    public class SendMailService : ISendMailService
    {
        private readonly IOptions<MailSettings> _mailsettings;

        public SendMailService(IOptions<MailSettings> mailSettings)
        {
            _mailsettings = mailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();

            email.Sender = new MailboxAddress(_mailsettings.Value.DisplayName, _mailsettings.Value.UserName);
            email.From.Add(new MailboxAddress(_mailsettings.Value.DisplayName, _mailsettings.Value.UserName));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                client.Connect(_mailsettings.Value.Host, _mailsettings.Value.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_mailsettings.Value.UserName, _mailsettings.Value.Password);
                await client.SendAsync(email);
                
            }
            catch(Exception ex)
            {
                throw new BusinessException(Resource.COMFIRMED_FAIL);
            }

            client.Disconnect(true);
        }
    }

}

