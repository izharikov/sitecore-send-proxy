using System.Collections.Generic;
using System.Threading.Tasks;
using SitecoreSendProxy.Models.Track;

namespace SitecoreSendProxy.Services.Track
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackResult>> Track(IEnumerable<Event> input);
    }
}