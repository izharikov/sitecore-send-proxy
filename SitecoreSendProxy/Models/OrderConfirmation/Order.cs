namespace SitecoreSendProxy.Models.OrderConfirmation
{
    public class Order
    {
        public string Id { get; set; }
        public User FromUser { get; set; }
        public Address BillingAddress { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TaxCost { get; set; }
        public decimal Total { get; set; }
    }
}