using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitecoreSendProxy.Models;
using SitecoreSendProxy.Models.Track;
using SitecoreSendProxy.Services;
using SitecoreSendProxy.Services.Track;

namespace SitecoreSendProxy.Controllers
{
    [Route("[controller]")]
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
    }
}