namespace Samples.Service.WCF
{
    using System;
    using System.Collections.Generic;
    using Sample.DTO;
    using Sample.Services.Interface;
    using Samples.Service.WCF.Interface;

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionServices _subscriptionServices;

        public SubscriptionService() : this(DependencyFactory.Resolve<ISubscriptionServices>())
        {

        }

        public SubscriptionService(ISubscriptionServices subscriptionServices)
        {
            _subscriptionServices = subscriptionServices;
        }

        public IEnumerable<SubscriptionDTO> GetAll()
        {
            return _subscriptionServices.GetAll();
        }

        public SubscriptionDTO GetById(Guid id)
        {
            return _subscriptionServices.GetById(id);
        }

        public void Insert(SubscriptionDTO subscription)
        {
            _subscriptionServices.Insert(subscription);
        }

        public void Update(SubscriptionDTO subscription)
        {
            _subscriptionServices.Update(subscription);
        }

        public void Delete(Guid id)
        {
            _subscriptionServices.Delete(id);
        }
    }
}
