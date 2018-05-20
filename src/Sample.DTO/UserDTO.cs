using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sample.DTO
{

    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Firstname { get; set; }

        [DataMember]
        public string Lastname { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public decimal TotalPriceIncVatAmount { get; set; }

        [DataMember]
        public long TotalCallMinutes { get; set; }

        [DataMember]
        public IEnumerable<SubscriptionDTO> Subscriptions { get; set; }
    }
}
