namespace Samples.Service.WCF.Interface
{
    using Sample.DTO;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        IEnumerable<UserDTO> GetAll();

        [OperationContract]
        UserDTO GetById(long id);

        [OperationContract]
        void Insert(UserDTO user);

        [OperationContract]
        void UpdateSubscription(long id, Guid subscriptionId);

        [OperationContract]
        void Update(UserDTO user);

        [OperationContract]
        void Delete(long id);
    }
}
