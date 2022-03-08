using System.Threading.Tasks;
using SitecoreSendProxy.Models.Track;

namespace SitecoreSendProxy.Services.Track
{
    public interface ITrackHttpService
    {
        Task<TrackResult> SendEvent(Event @event);
    }
}