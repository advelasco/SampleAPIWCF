namespace Sample.Repository.Interface
{
    using Sample.Model;
    using System;
    using System.Collections.Generic;

    public interface ISubscriptionRepository
    {
        IEnumerable<SubscriptionModel> GetAll();
        SubscriptionModel GetById(Guid id);
        IEnumerable<SubscriptionModel> GetByUserId(Guid id);
        void Insert(SubscriptionModel subscription);
        void Delete(Guid id);
        void Update(SubscriptionModel subscription);
    }
}
