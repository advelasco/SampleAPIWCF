
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
    public class UserControllerTests
    {
        private UserController target;
        private Mock<IUserMediator> _userMediator;

        public UserControllerTests()
        {
            _userMediator = new Mock<IUserMediator>();
            target = new UserController(_userMediator.Object);
        }

        [Fact]
        public void GetMustReturnBadRequest()
        {
            _userMediator.Setup(m => m.GetAll()).Throws(new System.Exception());

            var result = target.Get();
            var requestResult = result as BadRequestResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void GetMustReturnNotFound()
        {
            _userMediator.Setup(m => m.GetAll()).Returns((IEnumerable<UserDTO>)null);

            var result = target.Get();
            var requestResult = result as NotFoundResult;

            Assert.NotNull(requestResult);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void GetMustReturnOk()
        {
            _userMediator.Setup(m => m.GetAll()).Returns(new List<UserDTO> { new UserDTO() { Subscriptions = new List<SubscriptionDTO>() } });

            var result = target.Get();
            var requestResult = result as OkObjectResult;
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Value);

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public void GetByIdMustReturnBadRequest()
        {
            var id = 99;
            _userMediator.Setup(m => m.GetById(id)).Throws(new System.Exception());

            var result = target.Get(id);
            var requestResult = result as BadRequestResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void GetByIdMustReturnNotFound()
        {
            var id = 999;
            _userMediator.Setup(m => m.GetById(id)).Returns((UserDTO)null);

            var result = target.Get(id);
            var requestResult = result as NotFoundResult;

            Assert.NotNull(requestResult);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void GetByIdMustReturnOk()
        {
            var id = 9999;
            _userMediator.Setup(m => m.GetById(id)).Returns(new UserDTO() { Id = id , Subscriptions = new List<SubscriptionDTO>() });

            var result = target.Get(id);
            var requestResult = result as OkObjectResult;
            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<User>(viewResult.Value);

            Assert.NotNull(requestResult);
            Assert.Equal(200, requestResult.StatusCode);
            Assert.Equal(id, model.Id);
        }

        [Fact]
        public void PostMustReturnBadRequest()
        {
            _userMediator.Setup(m => m.Insert(It.IsAny<UserDTO>())).Throws(new System.Exception());

            var result = target.Post(new UserPost());
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void PostMustReturnCreated()
        {
            var result = target.Post(new UserPost());
            var requestResult = result as CreatedResult;

            Assert.NotNull(requestResult);
            Assert.Equal(201, requestResult.StatusCode);
            _userMediator.Verify(m => m.Insert(It.IsAny<UserDTO>()), Times.Once);
        }

        [Fact]
        public void PutMustReturnBadRequest()
        {
            _userMediator.Setup(m => m.UpdateSubscription(It.IsAny<long>(), It.IsAny<Guid>())).Throws(new System.Exception());

            var result = target.Put(9, Guid.NewGuid());
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void PutMustReturnNoContent()
        {
            var id = 99;
            var subscriptionId = Guid.NewGuid();
            _userMediator.Setup(m => m.GetById(id)).Returns(new UserDTO());

            var result = target.Put(id, subscriptionId);
            var requestResult = result as NoContentResult;

            Assert.NotNull(requestResult);
            Assert.Equal(204, requestResult.StatusCode);
            _userMediator.Verify(m => m.UpdateSubscription(id, subscriptionId), Times.Once);
        }

        [Fact]
        public void DeleteMustReturnBadRequest()
        {
            _userMediator.Setup(m => m.Delete(It.IsAny<long>())).Throws(new System.Exception());

            var result = target.Delete(0);
            var requestResult = result as BadRequestObjectResult;

            Assert.NotNull(requestResult);
            Assert.Equal(400, requestResult.StatusCode);
        }

        [Fact]
        public void DeleteMustReturnAccepted()
        {
            var result = target.Delete(0);
            var requestResult = result as AcceptedResult;

            Assert.NotNull(requestResult);
            Assert.Equal(202, requestResult.StatusCode);
            _userMediator.Verify(m => m.Delete(0), Times.Once);
        }
    }
}
