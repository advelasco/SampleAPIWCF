namespace Sample.API.Models
{
    using System.Collections.Generic;

    public class User
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public decimal TotalPriceIncVatAmount { get; set; }
        public long TotalCallMinutes { get; set; }
        public IEnumerable<Subscription> Subscription { get; set; }
    }
}
