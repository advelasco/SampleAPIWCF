namespace Sample.Mediator.Interface
{
    using Sample.DTO;
    using System;
    using System.Collections.Generic;

    public interface IUserMediator
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(long id);
        void Insert(UserDTO user);
        void UpdateSubscription(long id, Guid subscriptionId);
        void Update(UserDTO user);
        void Delete(long id);
    }
}