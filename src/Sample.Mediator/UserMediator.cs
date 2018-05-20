namespace Sample.Mediator
{
    using Sample.DTO;
    using Sample.Mediator.Interface;
    using Samples.Service.WCF.Interface;
    using System;
    using System.Collections.Generic;

    public class UserMediator : IUserMediator
    {
        public IUserService _userService { get; set; }
        public UserMediator(IServiceFactory serviceFactory)
        {
            _userService = serviceFactory.GetUserService();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _userService.GetAll();
        }

        public UserDTO GetById(long id)
        {
            return _userService.GetById(id);
        }

        public void Insert(UserDTO user)
        {
            _userService.Insert(user);
        }

        public void UpdateSubscription(long id, Guid subscriptionId)
        {
            _userService.UpdateSubscription(id, subscriptionId);
        }

        public void Update(UserDTO user)
        {
            _userService.Update(user);
        }

        public void Delete(long id)
        {
            _userService.Delete(id);
        }
    }
}