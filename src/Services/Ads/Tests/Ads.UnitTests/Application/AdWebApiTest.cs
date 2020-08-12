using Ads.API.Application.Queries;
using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.Exceptions;
using IdentityLib;
using MediatR;
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
        public void GetAdByIdSuccessTest()
        {
            //Arrange
            //Act 
            //Assert
            Assert.True(false);
        }
    }
}
