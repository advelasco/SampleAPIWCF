
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sample.API.Controllers;
using Sample.API.Models;
using Sample.DTO;
using Sample.Mediator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sample.API.Tests.Controller
{
    public class SubscriptionControllerTests
    {
        private SubscriptionController target;
        private Mock<ISubscriptionMediator> _subscriptionMediator;

        public SubscriptionControllerTests()
        {
            _subscriptionMediator = new Mock<ISubscriptionMediator>();
            target = new SubscriptionController(_subscriptionMediator.Object);
        }

        [Fact]
        public void GetMustReturnBadRequest()
        {
            _subscriptionMediator.Setup(m => m.GetAll()).Throws(new System.Exception());

            var result = target.Get();
            var requestResult = result as BadRequestResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void GetMustReturnNotFound()
        {
            _subscriptionMediator.Setup(m => m.GetAll()).Returns((IEnumerable<SubscriptionDTO>)null);

            var result = target.Get();
            var requestResult = result as NotFoundResult;

            Assert.NotNull(requestResult);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void GetMustReturnOk()
        {
            _subscriptionMediator.Setup(m => m.GetAll()).Returns(new List<SubscriptionDTO> { new SubscriptionDTO() });

            var result = target.Get();
            var requestResult = result as OkObjectResult;
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Subscription>>(viewResult.Value);

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public void GetByIdMustReturnBadRequest()
        {
            var id = Guid.NewGuid();
            _subscriptionMediator.Setup(m => m.GetById(id)).Throws(new System.Exception());

            var result = target.Get(id);
            var requestResult = result as BadRequestResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void GetByIdMustReturnNotFound()
        {
            var id = Guid.NewGuid();
            _subscriptionMediator.Setup(m => m.GetById(id)).Returns((SubscriptionDTO)null);

            var result = target.Get(id);
            var requestResult = result as NotFoundResult;

            Assert.NotNull(requestResult);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void GetByIdMustReturnOk()
        {
            var id = Guid.NewGuid();
            _subscriptionMediator.Setup(m => m.GetById(id)).Returns(new SubscriptionDTO() { Id = id });

            var result = target.Get(id);
            var requestResult = result as OkObjectResult;
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<Subscription>(viewResult.Value);

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public void PostMustReturnBadRequest()
        {
            _subscriptionMediator.Setup(m => m.Insert(It.IsAny<SubscriptionDTO>())).Throws(new System.Exception());

            var result = target.Post(new SubscriptionPost());
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void PostMustReturnCreated()
        {
            var result = target.Post(new SubscriptionPost());
            var requestResult = result as CreatedResult;

            Assert.NotNull(requestResult);
            Assert.Equal(201, requestResult.StatusCode);
            _subscriptionMediator.Verify(m => m.Insert(It.IsAny<SubscriptionDTO>()), Times.Once);
        }

        [Fact]
        public void PutMustReturnBadRequest()
        {
            _subscriptionMediator.Setup(m => m.GetById(It.IsAny<Guid>())).Throws(new System.Exception());

            var result = target.Put(Guid.NewGuid(), new SubscriptionPost());
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void PutMustReturnNoContent()
        {
            _subscriptionMediator.Setup(m => m.GetById(It.IsAny<Guid>())).Returns(new SubscriptionDTO());

            var result = target.Put(Guid.NewGuid(), new SubscriptionPost());
            var requestResult = result as NoContentResult;

            Assert.NotNull(requestResult);
            Assert.Equal(204, requestResult.StatusCode);
            _subscriptionMediator.Verify(m => m.Update(It.IsAny<SubscriptionDTO>()), Times.Once);
        }

        [Fact]
        public void DeleteMustReturnBadRequest()
        {
            _subscriptionMediator.Setup(m => m.Delete(It.IsAny<Guid>())).Throws(new System.Exception());

            var result = target.Delete(Guid.NewGuid());
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void DeleteMustReturnAccepted()
        {
            var result = target.Delete(Guid.NewGuid());
            var requestResult = result as AcceptedResult;

            Assert.NotNull(requestResult);
            Assert.Equal(202, requestResult.StatusCode);
            _subscriptionMediator.Verify(m => m.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
