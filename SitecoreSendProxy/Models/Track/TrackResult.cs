using System.Diagnostics.CodeAnalysis;

namespace SitecoreSendProxy.Models.Track
{
    public class TrackResult
    {
        public ActionType ActionType { get; set; }
        public TrackStatus Status { get; set; }
        public object Details { get; set; }
    }
}