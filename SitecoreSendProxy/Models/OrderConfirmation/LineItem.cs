using System.Linq;
using System.Text.Json.Serialization;

namespace SitecoreSendProxy.Models.OrderConfirmation
{
    public class LineItem
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal LineSubtotal { get; set; }
        public Product Product { get; set; }
        public Address ShippingAddress { get; set; }

        [JsonIgnore]
        public string ImageUrl => Product?.Xp?.Images?.Select(x => x.ThumbnailUrl)
            .FirstOrDefault(x => !string.IsNullOrEmpty(x));

        [JsonIgnore]
        public string ProductUrl =>
            $"https://arabian-oud-demo.vercel.app/{Product?.Xp?.CategorySeoName}/{Product?.Xp?.SeoName}";
    }
}