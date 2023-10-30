using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SitecoreSendProxy.Models.Track
{
    public class Event
    {
        public ActionType ActionType { get; set; }
        public string ContactEmailAddress { get; set; }

        [JsonExtensionData] public IDictionary<string, object> Properties { get; set; }
    }
}