using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SitecoreSendProxy.Extensions;
using SitecoreSendProxy.Models.Track;

namespace SitecoreSendProxy.Services.Track
{
    public class TrackService : ITrackService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITrackHttpService _trackHttpService;
        private readonly ISitecoreSendService _sitecoreSendService;

        public TrackService(IServiceScopeFactory serviceScopeFactory, ITrackHttpService trackHttpService, ISitecoreSendService sitecoreSendService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _trackHttpService = trackHttpService;
            _sitecoreSendService = sitecoreSendService;
        }

        public async Task<IEnumerable<TrackResult>> Track(IEnumerable<Event> input)
        {
            var events = input.OrderBy(x => x.ActionType).ToArray();
            if (events.Length == 0)
            {
                return Enumerable.Empty<TrackResult>();
            }

            var first = events[0];
            if (string.IsNullOrEmpty(first.ContactEmailAddress))
            {
                return new[]
                {
                    new TrackResult()
                    {
                        Status = TrackStatus.ERROR,
                        Details = "Error: ContactEmailAddress can't be null",
                    },
                };
            }

            if (first.ActionType == ActionType.IDENTIFY)
            {
                var rest = events[1..];
                var result = new List<TrackResult> {await _trackHttpService.SendEvent(first)};
                result.AddRange(SendWithDelay(first.ContactEmailAddress, rest));
                return result;
            }
            await EnsureUserExists(_sitecoreSendService, first.ContactEmailAddress);
            return await events.Select(x => _trackHttpService.SendEvent(x));
        }

        private IEnumerable<TrackResult> SendWithDelay(string email, IEnumerable<Event> events)
        {
            _ = Task.Run(async () =>
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var sitecoreSendService = scope.ServiceProvider.GetService<ISitecoreSendService>();
                    await EnsureUserExists(sitecoreSendService, email);
                    var trackHttpService = scope.ServiceProvider.GetService<ITrackHttpService>();
                    foreach (var @event in events)
                    {
                        await trackHttpService.SendEvent(@event);
                    }
                }
            );
            return events.Select(x => new TrackResult()
            {
                ActionType = x.ActionType,
                Status = TrackStatus.LATER,
            });
        }

        private static async Task EnsureUserExists(ISitecoreSendService sitecoreSendService, string email,
            int maxRetries = 50)
        {
            for (var i = 0; i < maxRetries; i++)
            {
                var userExists = await sitecoreSendService.IsSubscriberExists(email);
                if (userExists)
                {
                    break;
                }

                await Task.Delay(2000);
            }
        }
    }
}