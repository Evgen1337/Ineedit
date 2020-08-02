using Ads.API.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ads.API.Application.Queries
{
    public interface IAdQueries
    {
        Task<AdViewModel> GetAdAsync(int adId);

        Task<IReadOnlyCollection<AdViewModel>> GetUserAdsAsync(Guid userId);
    }
}
