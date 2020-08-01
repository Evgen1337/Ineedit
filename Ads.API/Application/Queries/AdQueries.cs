using Ads.API.Application.Exceptions;
using Ads.API.Application.ViewModels;
using Ads.Domain.AggregatesModel.AdAggregate;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.Queries
{
    public class AdQueries : IAdQueries
    {
        private readonly string _connectionString;

        public AdQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<AdViewModel> GetAdAsync(int adId)
        {
            await using var connection = new SqlConnection(_connectionString);

            const string query = @"
SELECT
    [Ads].[Id] AS [AdId],
    [Ads].[OwnerId],
    [Ads].[Name] AS [AdName],
    [Ads].[CreationDate],
    [Ads].[Comment],
    [AdTypes].[Id] AS [AdTypeId],
    [AdTypes].[Name] AS [AdTypeName]
FROM
    [dbo].[Ads]
LEFT JOIN
    [dbo].[AdTypes]
ON
    [AdTypes].[Id] = [Ads].[AdTypeId]
WHERE
    [Ads].[Id] = @adId
";
            var dynamicAdModel = await connection.QuerySingleOrDefaultAsync(query, new { adId });

            if (dynamicAdModel is null)
                throw new AdNotFoundException("Ad not found");

            var ad = MapAdViewModel(dynamicAdModel);
            return ad;
        }

        public async Task<IReadOnlyCollection<AdViewModel>> GetUserAdsAsync(Guid userId)
        {
            await using var connection = new SqlConnection(_connectionString);

            const string query = @"
SELECT
    [Ads].[Id] AS [AdId],
    [Ads].[OwnerId],
    [Ads].[Name] AS [AdName],
    [Ads].[CreationDate],
    [Ads].[Comment],
    [AdTypes].[Id] AS [AdTypeId],
    [AdTypes].[Name] AS [AdTypeName]
FROM
    [dbo].[Ads]
LEFT JOIN
    [dbo].[AdTypes]
ON
    [AdTypes].[Id] = [Ads].[AdTypeId]
WHERE
    [Ads].[OwnerId] = @userId
";
            var dynamicAdModels = await connection.QueryAsync(query, new { userId });

            var ads = dynamicAdModels
                .Select(s => (AdViewModel)MapAdViewModel(s))
                .ToArray();

            return ads;
        }

        private AdViewModel MapAdViewModel(dynamic dynamicAdModel)
        {
            return new AdViewModel
            {
                Id = dynamicAdModel.AdId,
                AdType = new AdTypeViewModel
                {
                    Id = dynamicAdModel.AdTypeId,
                    Name = dynamicAdModel.AdTypeName
                },
                Name = dynamicAdModel.AdName,
                Comment = dynamicAdModel.Comment,
                OwnerId = dynamicAdModel.OwnerId
            };
        }
    }
}
