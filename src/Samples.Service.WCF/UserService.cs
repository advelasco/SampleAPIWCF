namespace Samples.Service.WCF
{
    using Sample.DTO;
    using Sample.Services.Interface;
    using Samples.Service.WCF.Interface;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly IUserServices _userServices;

        public UserService() : this(DependencyFactory.Resolve<IUserServices>())
        {

        }

        public UserService(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _userServices.GetAll();
        }

        public UserDTO GetById(long id)
        {
            return _userServices.GetById(id);
        }

        public void Insert(UserDTO user)
        {
            _userServices.Insert(user);
        }

        public void UpdateSubscription(long id, Guid subscriptionId)
        {
            _userServices.UpdateSubscription(id, subscriptionId);
        }

        public void Update(UserDTO user)
        {
            _userServices.Update(user);
        }

        public void Delete(long id)
        {
            _userServices.Delete(id);
        }
    }
}