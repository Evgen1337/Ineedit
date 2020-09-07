using Ads.API.Application.Commands;
using Ads.API.Application.Exceptions;
using Ads.API.Application.Queries;
using Ads.API.Application.ViewModels;
using Ads.API.Controllers;
using Ads.Dtos.Ad;
using IdentityLib;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Ads.UnitTests.Domain
{
    public class AdWebApiTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAdQueries> _adQueriesMock;
        private readonly Mock<IIdentityService> _identityServiceMock;

        public AdWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adQueriesMock = new Mock<IAdQueries>();
            _identityServiceMock = new Mock<IIdentityService>();
        }

        [Fact]
        public async Task GetAdsSuccessTest()
        {
            //Arrange
            var fakeDynamicResult = Array.Empty<AdViewModel>();
            var fakeUserId = Guid.NewGuid();

            _identityServiceMock.Setup(x => x.GetUserIdentity())
                .Returns(fakeUserId.ToString());

            _adQueriesMock.Setup(s => s.GetUserAdsAsync(fakeUserId))
                .ReturnsAsync(fakeDynamicResult);

            //Act 
            var adController = new AdController(_mediatorMock.Object, _identityServiceMock.Object, _adQueriesMock.Object);
            var actionResult = await adController.GetAdsAsync() as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAdByIdSuccessTest()
        {
            //Arrange
            var fakeOrderId = new GettingAdDto { AdId = 123 };
            _adQueriesMock.Setup(s => s.GetAdAsync(It.IsAny<int>()));

            //Act 
            var adController = new AdController(_mediatorMock.Object, _identityServiceMock.Object, _adQueriesMock.Object);
            var actionResult = await adController.GetAdAsync(fakeOrderId) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAdByIdNotFoundTest()
        {
            //Arrange
            var fakeOrderId = new GettingAdDto { AdId = 123 };
            _adQueriesMock.Setup(s => s.GetAdAsync(It.IsAny<int>())).Throws(new AdNotFoundException());

            //Act 
            var adController = new AdController(_mediatorMock.Object, _identityServiceMock.Object, _adQueriesMock.Object);
            var actionResult = await adController.GetAdAsync(fakeOrderId) as NotFoundObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateAdSuccessTest()
        {
            //Arrange
            var fakeAd = new UpdatingAdDto { AdId = 123, Comment = "asd", TypeId = 1, Name = "asd" };
            var fakeUserId = Guid.NewGuid();

            _identityServiceMock.Setup(x => x.GetUserIdentity())
                .Returns(fakeUserId.ToString());

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateAdCommand>(), default));

            //Act 
            var adController = new AdController(_mediatorMock.Object, _identityServiceMock.Object, _adQueriesMock.Object);
            var actionResult = await adController.UpdateAdAsync(fakeAd) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateAdBadRequestTest()
        {
            //Arrange
            var fakeAd = new UpdatingAdDto { AdId = 123, Comment = "asd", TypeId = 1, Name = "asd" };
            var fakeUserId = Guid.NewGuid();

            _identityServiceMock.Setup(x => x.GetUserIdentity())
                .Returns(fakeUserId.ToString());

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateAdCommand>(), default))
                .Throws(new UserWithAdOwnerDoesntEqualsException());

            //Act 
            var adController = new AdController(_mediatorMock.Object, _identityServiceMock.Object, _adQueriesMock.Object);
            var actionResult = await adController.UpdateAdAsync(fakeAd) as BadRequestObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        //e.t.c tests were
    }
}
