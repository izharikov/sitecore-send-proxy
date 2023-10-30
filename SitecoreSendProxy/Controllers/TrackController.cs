using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitecoreSendProxy.Models.Track;
using SitecoreSendProxy.Services.Track;

namespace SitecoreSendProxy.Controllers
{
    [Route("[controller]/[action]")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpPost]
        public async Task<IEnumerable<TrackResult>> Track([FromBody] IList<Event> events)
        {
            return await _trackService.Track(events);
        }

        [HttpPost]
        public async Task<IEnumerable<TrackResult>> TrackSampleEmail([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new List<TrackResult>()
                {
                    new TrackResult()
                    {
                        Status = TrackStatus.ERROR,
                        Details = "null email",
                    }
                };
            }

            var contactId = Guid.NewGuid().ToString("D");
            var sessionId = Guid.NewGuid().ToString("D");
            var siteId = "426efbad-a306-4cf2-9071-1a847285785e";
            var product = new
            {
                itemCode = "CH002",
                itemPrice = 279,
                itemUrl = "https://arabian-oud-demo.vercel.app/perfumes/chanel-coco-mademoiselle-fresh-hair-mist",
                itemName = "Chanel Coco Mademoiselle Fresh Hair Mist",
                itemImage =
                    "https://mydemoheadstartstorage.blob.core.windows.net/assets/db57c8a8-a4c6-4fd9-b9ab-a5f8ee0427f0",
                currencyCode = "SAR",
                itemQuantity = 1,
                itemTotalPrice = 279,
            };
            var properties = new Dictionary<string, object>()
            {
                {"ContactId", contactId},
                {"sessionId", sessionId},
                {"siteId", siteId},
                {"Url", product.itemUrl},
                {
                    "properties", new List<object>()
                    {
                        new
                        {
                            product,
                        },
                    }
                },
            };
            var events = new List<Event>()
            {
                new Event()
                {
                    ActionType = ActionType.IDENTIFY,
                    ContactEmailAddress = email,
                    Properties = new Dictionary<string, object>()
                    {
                        {"ContactId", contactId},
                        {"sessionId", sessionId},
                        {"siteId", siteId},
                    },
                },
                new Event()
                {
                    ActionType = ActionType.PAGE_VIEWED,
                    ContactEmailAddress = email,
                    Properties = properties,
                },
                new Event()
                {
                    ActionType = ActionType.ADDED_TO_ORDER,
                    ContactEmailAddress = email,
                    Properties = properties,
                },
            };
            return await _trackService.Track(events);
        }
    }
}