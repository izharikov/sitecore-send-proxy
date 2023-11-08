using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SitecoreSendProxy.Models.OrderConfirmation;
using SitecoreSendProxy.Options;
using SitecoreSendProxy.Services.Razor;
using SitecoreSendProxy.Services.Smtp;

namespace SitecoreSendProxy.Controllers
{
    [Route("[controller]/[action]")]
    public class OrderConfirmationController : Controller
    {
        private readonly RazorViewService _razorViewService;
        private readonly IEmailService _emailService;
        private readonly ILogger<OrderConfirmationController> _logger;
        private readonly MoosendOptions _options;

        public OrderConfirmationController(RazorViewService razorViewService, IEmailService emailService,
            ILogger<OrderConfirmationController> logger, IOptions<MoosendOptions> options)
        {
            _razorViewService = razorViewService;
            _emailService = emailService;
            _logger = logger;
            _options = options.Value;
        }

        [HttpPost]
        public ViewResult Render([FromBody] OrderModel request)
        {
            _logger.LogInformation("Render Order Confirmation {orderId}", request.Order.Id);
            return View("Index", request);
        }

        [HttpPost]
        public async Task<object> Send([FromBody] OrderModel request)
        {
            _logger.LogInformation("Send Email for {orderId}", request.Order.Id);
            var html = await _razorViewService.RenderPartialViewToString(this, Render(request));
            try
            {
                await _emailService.Send(request.Order.FromUser.Email, $"Order Confirmation {request.Order.Id}", html,
                    _options.Campaigns.TrackOrderConfirmation);
                return new
                {
                    Success = true,
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
                return new
                {
                    Message = e.Message,
                    Success = false,
                };
            }
        }
    }
}