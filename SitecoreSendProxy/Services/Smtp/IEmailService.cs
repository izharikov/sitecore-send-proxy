using System.Threading.Tasks;

namespace SitecoreSendProxy.Services.Smtp
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string html);
    }
}