using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SitecoreSendProxy.Services.Smtp
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public async Task Send(string to, string subject, string html)
        {
            var from = _configuration.GetValue<string>("Smtp:From");
            var message = new MailMessage(from, to, subject, html)
            {
                IsBodyHtml = true,
            };
            _logger.LogInformation("Send Email '{subject}' to {to} (from '{from}')", subject, to, from);
            using var smtp = CreateClient();
            await smtp.SendMailAsync(message);
        }

        private SmtpClient CreateClient()
        {
            var config = _configuration.GetSection("Smtp");
            var client = new SmtpClient(config.GetValue<string>("Server"), config.GetValue<int>("Port"));
            client.Credentials =
                new NetworkCredential(config.GetValue<string>("User"), config.GetValue<string>("Password"));
            return client;
        }
    }
}