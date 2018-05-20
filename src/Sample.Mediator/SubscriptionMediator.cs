namespace Sample.Mediator
{
    using Sample.DTO;
    using Sample.Mediator.Interface;
    using Samples.Service.WCF.Interface;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    public class SubscriptionMediator : ISubscriptionMediator
    {
        public ISubscriptionService _subscriptionService { get; set; }

        public SubscriptionMediator(IServiceFactory serviceFactory)
        {
            _subscriptionService = serviceFactory.GetSubscriptionService();
        }

        public void Delete(Guid id)
        {
            _subscriptionService.Delete(id);
        }

        public SubscriptionDTO GetById(Guid id)
        {
            return _subscriptionService.GetById(id);
        }

        public void Insert(SubscriptionDTO subscription)
        {
            _subscriptionService.Insert(subscription);
        }

        public void Update(SubscriptionDTO subscription)
        {
            _subscriptionService.Update(subscription);
        }

        IEnumerable<SubscriptionDTO> ISubscriptionMediator.GetAll()
        {
            return _subscriptionService.GetAll();
        }
    }
}