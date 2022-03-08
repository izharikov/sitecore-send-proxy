using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SitecoreSendProxy.Models.OrderConfirmation
{
    public class OrderModel
    {
        public Order Order { get; set; }
        public IList<LineItem> LineItems { get; set; } = new List<LineItem>();
        public IList<Payment> Payments { get; set; } = new List<Payment>();

        [JsonIgnore]
        public Address ShippingAddress => LineItems.Select(x => x.ShippingAddress).FirstOrDefault(x => x != null);
        [JsonIgnore]
        public Address BillingAddress => Order?.BillingAddress;
        [JsonIgnore]
        public string Currency =>
            LineItems.Select(x => x.Product?.Xp?.Currency).FirstOrDefault(x => !string.IsNullOrEmpty(x));
    }
}