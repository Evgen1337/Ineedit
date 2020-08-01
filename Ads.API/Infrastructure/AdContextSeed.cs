using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.SeedWork;
using Ads.Infrastructure;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Infrastructure
{
    public class AdContextSeed
    {
        public async Task SeedAsync(AdsContext context, ILogger<AdContextSeed> logger)
        {
            if (!context.AdTypes.Any())
            {
                var contextAdTypes = context.AdTypes.AsList();
                var types = Enumeration.GetAll<AdType>();

                if (!contextAdTypes.SequenceEqual(types))
                {
                    context.AdTypes.RemoveRange(contextAdTypes);
                    await context.AdTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }

                logger.LogInformation("Context ad types updated.");
            }
        }
    }
}
