namespace Sample.API.Models
{
    public class SubscriptionPost
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncVatAmount { get; set; }
        public long CallMinutes { get; set; }
    }
}