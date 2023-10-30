namespace SitecoreSendProxy.Options
{
    public class MoosendOptions
    {
        public string ApiKey { get; set; }
        public MoosendCampaigns Campaigns { get; set; }
    }

    public class MoosendCampaigns
    {
        public string OrderConfirmation { get; set; }
        public string TrackOrderConfirmation { get; set; }
    }
}