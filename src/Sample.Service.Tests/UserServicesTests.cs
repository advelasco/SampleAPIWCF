using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sample.DTO;
using Sample.Model;
using Sample.Repository.Interface;
using Sample.Services;
using System;
using System.Linq;

namespace Sample.Service.Tests
{
    [TestClass]
    public class UserServicesTests
    {
        private UserServices target;
        private Mock<IUnityOfWork> _unityOfWorkMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ISubscriptionRepository> _subscriptionRepositoryMock;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _unityOfWorkMock = new Mock<IUnityOfWork>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();

            target = new UserServices(_unityOfWorkMock.Object,
                _userRepositoryMock.Object, _subscriptionRepositoryMock.Object);

        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _unityOfWorkMock = null;
            _userRepositoryMock = null;
            _subscriptionRepositoryMock = null;
            target = null;
        }

        [TestMethod]
        public void TestGetAllMustResultDtoIEnumerable()
        {
            var data = Builder<UserModel>.CreateListOfSize(10).Build();
            _userRepositoryMock.Setup(m => m.GetAll()).Returns(data);

            var result = target.GetAll();

            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void TestGetByIdMustResultDto()
        {
            var data = Builder<UserModel>.CreateNew().Build();
            _userRepositoryMock.Setup(m => m.GetById(data.UserId)).Returns(data);

            var result = target.GetById(data.UserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Email, result.Email);
            Assert.AreEqual(data.Firstname, result.Firstname);
            Assert.AreEqual(data.Lastname, result.Lastname);
        }

        [TestMethod]
        public void TestGetByIdMustCalculateBaseOnSubscription()
        {
            var data = Builder<UserModel>.CreateNew().Build();
            var dataSubscription = Builder<SubscriptionModel>.CreateListOfSize(3).Build();

            _userRepositoryMock.Setup(m => m.GetById(data.UserId)).Returns(data);
            _subscriptionRepositoryMock.Setup(m => m.GetByUserId(data.Id)).Returns(dataSubscription);

            var result = target.GetById(data.UserId);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Subscriptions);

            Assert.AreEqual(dataSubscription.Sum(m => m.CallMinutes), result.TotalCallMinutes);
            Assert.AreEqual(dataSubscription.Sum(m => m.PriceIncVatAmount), result.TotalPriceIncVatAmount);
        }

        [TestMethod]
        public void TestInsertMustCallRepository()
        {
            var data = Builder<UserDTO>.CreateNew().Build();

            target.Insert(data);

            _userRepositoryMock.Verify(m => m.Insert(It.IsAny<UserModel>()), Times.Once);
        }

        [TestMethod]
        public void TestUpdateMustCallRepository()
        {
            var dataToUpdate = Builder<UserDTO>.CreateNew().Build();
            dataToUpdate.Email = "EmailNew";
            var dataToOld = Builder<UserModel>.CreateNew().Build();
            dataToOld.Email = "EmailOld";

            _userRepositoryMock.Setup(m => m.GetById(dataToUpdate.Id)).Returns(dataToOld);

            target.Update(dataToUpdate);

            _userRepositoryMock.Verify(m => m.Update(It.Is<UserModel>(arg => arg.Email == "EmailNew")), Times.Once);
        }

        [TestMethod]
        public void TestUpdateSubscriptionMustCallSubscriptionRepository()
        {
            var id = Guid.NewGuid();
            var userId = 1;
            var subscriptionId = Guid.NewGuid();

            _userRepositoryMock.Setup(m => m.GetById(userId)).Returns(new UserModel() { Id = id });
            _subscriptionRepositoryMock.Setup(m => m.GetById(subscriptionId)).Returns(new SubscriptionModel());

            target.UpdateSubscription(userId, subscriptionId);

            _subscriptionRepositoryMock.Verify(m => m.Update(It.Is<SubscriptionModel>(arg => arg.UserId == id)), Times.Once);
        }

        [TestMethod]
        public void TestDeleteMustCallRepository()
        {
            target.Delete(1);

            _userRepositoryMock.Verify(m => m.Delete(It.IsAny<long>()), Times.Once);
        }
    }
}
