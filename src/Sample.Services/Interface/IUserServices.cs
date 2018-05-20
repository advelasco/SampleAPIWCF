namespace Sample.Services.Interface
{
    using Sample.DTO;
    using System;
    using System.Collections.Generic;

    public interface IUserServices
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(long id);
        void Insert(UserDTO user);
        void Update(UserDTO user);
        void UpdateSubscription(long id, Guid subscriptionId);
        void Delete(long id);
    }
}
