namespace Sample.Services
{
    using Sample.DTO;
    using Sample.Model;
    using Sample.Repository.Interface;
    using Sample.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionServices(IUnityOfWork unityOfWork, ISubscriptionRepository subscriptionRepository)
        {
            _unityOfWork = unityOfWork;
            _subscriptionRepository = subscriptionRepository;
        }

        public IEnumerable<SubscriptionDTO> GetAll()
        {
            IEnumerable<SubscriptionDTO> result = null;

            var subscription = _subscriptionRepository.GetAll();

            if (subscription != null)
            {
                result = subscription.Select(u => ConvertToDto(u));
            }

            return result;
        }

        public SubscriptionDTO GetById(Guid id)
        {
            SubscriptionDTO result = null;

            var subscription = _subscriptionRepository.GetById(id);

            if (subscription != null)
            {
                result = ConvertToDto(subscription);
            }

            return result;
        }

        public void Insert(SubscriptionDTO subscription)
        {
            var subscriptionModel = ConvertToModel(subscription);
            subscriptionModel.Enabled = true;
            _subscriptionRepository.Insert(subscriptionModel);

            _unityOfWork.Save();
        }

        public void Update(SubscriptionDTO subscription)
        {
            var subscriptionOld = _subscriptionRepository.GetById(subscription.Id);

            subscriptionOld.CallMinutes = subscription.CallMinutes;
            subscriptionOld.Name = subscription.Name;
            subscriptionOld.Price = subscription.Price;
            subscriptionOld.PriceIncVatAmount = subscription.PriceIncVatAmount;

            subscriptionOld.Enabled = subscriptionOld.Enabled;
            subscriptionOld.UserId = subscriptionOld.UserId;

            _subscriptionRepository.Update(subscriptionOld);

            _unityOfWork.Save();
        }

        public void Delete(Guid id)
        {
            _subscriptionRepository.Delete(id);

            _unityOfWork.Save();
        }

        private SubscriptionDTO ConvertToDto(SubscriptionModel model)
        {
            if(model != null)
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

        private SubscriptionModel ConvertToModel(SubscriptionDTO dto)
        {
            if (dto != null)
            {
                var subscriptionModel = new SubscriptionModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CallMinutes = dto.CallMinutes,
                    Price = dto.Price,
                    PriceIncVatAmount = dto.PriceIncVatAmount
                };

                return subscriptionModel;
            }

            return null;
        }
    }
}