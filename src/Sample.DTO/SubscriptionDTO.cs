namespace Sample.DTO
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class SubscriptionDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal PriceIncVatAmount { get; set; }

        [DataMember]
        public long CallMinutes { get; set; }
    }
}
