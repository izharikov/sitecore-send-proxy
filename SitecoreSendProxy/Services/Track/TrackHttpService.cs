using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SitecoreSendProxy.Models.Track;

namespace SitecoreSendProxy.Services.Track
{
    public class TrackHttpService : ITrackHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TrackHttpService> _logger;

        public TrackHttpService(IHttpClientFactory httpClientFactory, ILogger<TrackHttpService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<TrackResult> SendEvent(Event @event)
        {
            var client = _httpClientFactory.CreateClient(Constants.ClientName);
            var url = @event.ActionType == ActionType.IDENTIFY ? "/identify" : "/track";
            _logger.LogInformation("Send ActionType: {actionType}", @event.ActionType);
            var response = await client.PostAsync(url, JsonContent.Create(@event, options: new JsonSerializerOptions()
            {
                Converters = {new JsonStringEnumConverter(),},
            }));
            return new TrackResult()
            {
                ActionType = @event.ActionType,
                Status = response.IsSuccessStatusCode ? TrackStatus.SENT : TrackStatus.ERROR,
                Details = response.IsSuccessStatusCode
                    ? null
                    : new
                    {
                        Status = (int) response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync(),
                    }
            };
        }
    }
}