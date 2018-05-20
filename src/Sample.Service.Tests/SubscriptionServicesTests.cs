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
    public class SubscriptionServicesTests
    {
        private SubscriptionServices target;
        private Mock<IUnityOfWork> _unityOfWorkMock;
        private Mock<ISubscriptionRepository> _subscriptionRepositoryMock;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _unityOfWorkMock = new Mock<IUnityOfWork>();
            _subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();

            target = new SubscriptionServices(_unityOfWorkMock.Object, _subscriptionRepositoryMock.Object);

        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _unityOfWorkMock = null;
            _subscriptionRepositoryMock = null;
            _subscriptionRepositoryMock = null;
            target = null;
        }

        [TestMethod]
        public void TestGetAllMustResultDtoIEnumerable()
        {
            var data = Builder<SubscriptionModel>.CreateListOfSize(10).Build();
            _subscriptionRepositoryMock.Setup(m => m.GetAll()).Returns(data);

            var result = target.GetAll();

            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void TestGetByIdMustResultDto()
        {
            var data = Builder<SubscriptionModel>.CreateNew().Build();
            _subscriptionRepositoryMock.Setup(m => m.GetById(data.Id)).Returns(data);

            var result = target.GetById(data.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(data.Name, result.Name);
            Assert.AreEqual(data.Price, result.Price);
            Assert.AreEqual(data.PriceIncVatAmount, result.PriceIncVatAmount);
        }

        [TestMethod]
        public void TestInsertMustCallRepository()
        {
            var data = Builder<SubscriptionDTO>.CreateNew().Build();

            target.Insert(data);

            _subscriptionRepositoryMock.Verify(m => m.Insert(It.IsAny<SubscriptionModel>()), Times.Once);
        }

        [TestMethod]
        public void TestUpdateMustCallRepository()
        {
            var dataToUpdate = Builder<SubscriptionDTO>.CreateNew().Build();
            dataToUpdate.Name = "NameNew";
            var dataToOld = Builder<SubscriptionModel>.CreateNew().Build();
            dataToOld.Name = "NameOld";

            _subscriptionRepositoryMock.Setup(m => m.GetById(dataToUpdate.Id)).Returns(dataToOld);

            target.Update(dataToUpdate);

            _subscriptionRepositoryMock.Verify(m => m.Update(It.Is<SubscriptionModel>(arg => arg.Name == "NameNew")), Times.Once);
        }

        [TestMethod]
        public void TestDeleteMustCallRepository()
        {
            var deleteId = Guid.NewGuid();

            target.Delete(deleteId);

            _subscriptionRepositoryMock.Verify(m => m.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
