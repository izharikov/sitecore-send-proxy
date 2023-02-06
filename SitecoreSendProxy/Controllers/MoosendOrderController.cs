using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moosend.Wrappers.CSharpWrapper.Api;
using Nustache.Core;
using SitecoreSendProxy.Models.OrderConfirmation;
using SitecoreSendProxy.Options;
using SitecoreSendProxy.Services.Smtp;

namespace SitecoreSendProxy.Controllers
{
    [Route("[controller]/[action]")]
    public class MoosendOrderController : Controller
    {
        private readonly ICampaignsApi _campaignsApi;
        private readonly IEmailService _emailService;
        private readonly IOptions<MoosendOptions> _options;

        public MoosendOrderController(ICampaignsApi campaignsApi, IEmailService emailService, IOptions<MoosendOptions> options)
        {
            _campaignsApi = campaignsApi;
            _emailService = emailService;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> RenderFromCampaign([FromBody] OrderModel request)
        {
            var config = _options.Value;
            var campaign =
                await _campaignsApi.GettingCampaignDetailsAsync("json", config.ApiKey,
                    config.Campaigns.OrderConfirmation);
            var html = Render.StringToString(campaign.Context.HTMLContent, request);
            return new ContentResult()
            {
                Content = html,
                ContentType = "text/html",
            };
        }

        [HttpPost]
        public async Task<IActionResult> SendFromCampaign([FromBody] OrderModel request)
        {
            var config = _options.Value;
            var campaign =
                await _campaignsApi.GettingCampaignDetailsAsync("json", config.ApiKey,
                    config.Campaigns.OrderConfirmation);
            var subject = Render.StringToString(campaign.Context.Subject, request);
            var html = Render.StringToString(campaign.Context.HTMLContent, request);
            await _emailService.Send(request.Order.FromUser.Email, subject, html, config.Campaigns.OrderConfirmation);
            return new ObjectResult(new
            {
                Success = true,
            });
        }
    }
}