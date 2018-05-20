namespace Samples.Service.WCF.Interface
{
    using Sample.DTO;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface ISubscriptionService
    {
        [OperationContract]
        IEnumerable<SubscriptionDTO> GetAll();

        [OperationContract]
        SubscriptionDTO GetById(Guid id);

        [OperationContract]
        void Insert(SubscriptionDTO user);

        [OperationContract]
        void Update(SubscriptionDTO user);

        [OperationContract]
        void Delete(Guid id);
    }
}
