using Ads.Domain.SeedWork;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.Domain.AggregatesModel.AdAggregate
{
    public interface IAdRepository : IRepository<Ad>
    {
        Task<Ad> AddAsync(Ad ad);

        Ad Update(Ad ad);

        Task DeleteAsync(int adId);

        Task<Ad> GetByIdAsync(int adId);
    }
}
