using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.Exceptions;
using System;
using Xunit;

namespace Ads.UnitTests.Domain
{
    public class AdAggregateTest
    {
        public AdAggregateTest()
        { }

        [Fact]
        public void CreateAdSuccess()
        {
            //Arrange    
            var identity = Guid.NewGuid();
            var name = "Test1";
            var adType = AdType.Things;

            //Act 
            var fakeAd = new Ad(identity, name, adType);

            //Assert
            Assert.NotNull(fakeAd);
        }

        [Fact]
        public void CreateAdFail()
        {
            //Arrange
            var identity = Guid.NewGuid();
            var name = "Test1";
            var adType = AdType.Things;

            //Act
            var adWithUserIdNull = new Ad(null, name, adType);
            var adWithNameNull = new Ad(identity, null, adType);
            var adWithAdTypeNull = new Ad(identity, name, null);

            //Assert
            Assert.Throws<AdsDomainException>(() => adWithUserIdNull);
            Assert.Throws<AdsDomainException>(() => adWithNameNull);
            Assert.Throws<AdsDomainException>(() => adWithAdTypeNull);
        }
    }
}
