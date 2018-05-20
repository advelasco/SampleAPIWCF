namespace Sample.API.Models
{
    using System;

    public class Subscription
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncVatAmount { get; set; }
        public long CallMinutes { get; set; }
    }
}
