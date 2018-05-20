namespace Sample.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Subscription")]
    public class SubscriptionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncVatAmount { get; set; }
        public long CallMinutes { get; set; }
        public bool Enabled { get; set; }
        public Guid? UserId { get; set; }
        //[ForeignKey("User")]
        //public Guid UserRefId { get; set; }
        //public UserModel User { get; set; }
    }
}