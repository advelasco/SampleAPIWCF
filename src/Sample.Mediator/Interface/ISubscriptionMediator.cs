namespace Sample.Mediator.Interface
{
    using Sample.DTO;
    using System;
    using System.Collections.Generic;

    public interface ISubscriptionMediator
    {
        IEnumerable<SubscriptionDTO> GetAll();
        SubscriptionDTO GetById(Guid id);
        void Insert(SubscriptionDTO subscription);
        void Update(SubscriptionDTO subscription);
        void Delete(Guid id);
    }
}
