using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SitecoreSendProxy.Options;

namespace SitecoreSendProxy.Services.Smtp
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpOptions> settings, ILogger<EmailService> logger)
        {
            _smtpOptions = settings.Value;
            _logger = logger;
        }


        public async Task Send(string to, string subject, string html, string campaignId = null, string listId = null)
        {
            var from = _smtpOptions.From;
            var message = new MailMessage(from, to, subject, html)
            {
                IsBodyHtml = true,
            };
            if (!string.IsNullOrEmpty(campaignId))
            {
                message.Headers.Add("campaign_guid", campaignId);
            }

            if (!string.IsNullOrEmpty(listId))
            {
                message.Headers.Add("mailing_list_id", listId);
            }

            _logger.LogInformation("Send Email '{subject}' to {to} (from '{from}')", subject, to, from);
            using var smtp = CreateClient();
            await smtp.SendMailAsync(message);
        }

        private SmtpClient CreateClient()
        {
            var client = new SmtpClient(_smtpOptions.Server, _smtpOptions.Port);
            client.Credentials = new NetworkCredential(_smtpOptions.User, _smtpOptions.Password);
            return client;
        }
    }
}