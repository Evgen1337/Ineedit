using Ads.API.Application.ViewModels;
using Ads.API.Infrastructure;
using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.Exceptions;
using Ads.Infrastructure;
using Ads.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        [Fact]
        public async Task UpdateTest()
        {
            //var context1 = new AdsContextDesignFactory().CreateDbContext(null);
            //await new AdContextSeed().SeedAsync(context1);

            //var context12 = new AdsContextDesignFactory().CreateDbContext(null);
            //var _adRepository = new AdsRepository(context12);


            //var identity = Guid.NewGuid();
            //var name = "Test1";
            //var adType = AdType.Things;

            //var fakeAd = new Ad(identity, name, adType);
            //await _adRepository.AddAsync(fakeAd);
            //await _adRepository.UnitOfWork.SaveChangesAsync();

            //var ad = await _adRepository.GetByIdAsync(1);

            //ad.Name = "update123";
            //ad.AdType = AdType.FromValue<AdType>(2);
            //ad.Comment = "123";

            //_adRepository.Update(ad);

            //await _adRepository.UnitOfWork.SaveChangesAsync();

            //var adNew = await _adRepository.GetByIdAsync(1);
        }
    }
}
