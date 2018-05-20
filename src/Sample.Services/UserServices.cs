namespace Sample.Services
{
    using Sample.DTO;
    using Sample.Model;
    using Sample.Repository.Interface;
    using Sample.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserServices : IUserServices
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public UserServices(IUnityOfWork unityOfWork, IUserRepository userRepository, ISubscriptionRepository subscriptionRepository)
        {
            _unityOfWork = unityOfWork;
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            IEnumerable<UserDTO> result = null;

            var users = _userRepository.GetAll();

            if (users != null)
            {
                result = users.Select(u => ConvertToDto(u));
            }

            return result;
        }

        public UserDTO GetById(long id)
        {
            UserDTO result = null;

            var users = _userRepository.GetById(id);

            if (users != null)
            {
                result = ConvertToDto(users);
            }

            return result;
        }

        public void Insert(UserDTO user)
        {
            var model = ConvertToModel(user);
            model.Enabled = true;

            _userRepository.Insert(model);

            _unityOfWork.Save();
        }

        public void UpdateSubscription(long id, Guid subscriptionId)
        {
            var user = _userRepository.GetById(id);

            if (user != null)
            {
                var subscription = _subscriptionRepository.GetById(subscriptionId);

                if (subscription != null)
                {
                    subscription.UserId = user.Id;

                    _subscriptionRepository.Update(subscription);

                    _unityOfWork.Save();
                }
            }
        }
        public void Update(UserDTO user)
        {
            _userRepository.Update(ConvertToModel(user));

            _unityOfWork.Save();
        }

        public void Delete(long id)
        {
            _userRepository.Delete(id);

            _unityOfWork.Save();
        }

        private UserDTO ConvertToDto(UserModel model)
        {
            if(model != null)
            {
                var subscriptions = _subscriptionRepository.GetByUserId(model.Id);

                var userDto = new UserDTO()
                {
                    Id = model.UserId,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Subscriptions = subscriptions.Select(s => ConvertToSubDto(s))
                };

                userDto.TotalCallMinutes = subscriptions.Sum(m => m.CallMinutes);
                userDto.TotalPriceIncVatAmount = subscriptions.Sum(m => m.PriceIncVatAmount);

                return userDto;
            }

            return null;
        }

        private UserModel ConvertToModel(UserDTO dto)
        {
            if (dto != null)
            {
                var userModel = new UserModel()
                {
                    UserId = dto.Id,
                    Email = dto.Email,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                };

                return userModel;
            }

            return null;
        }

        private SubscriptionDTO ConvertToSubDto(SubscriptionModel model)
        {
            if (model != null)
            {
                var subscriptionDto = new SubscriptionDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
                    CallMinutes = model.CallMinutes,
                    Price = model.Price,
                    PriceIncVatAmount = model.PriceIncVatAmount
                };

                return subscriptionDto;
            }

            return null;
        }
    }
}
