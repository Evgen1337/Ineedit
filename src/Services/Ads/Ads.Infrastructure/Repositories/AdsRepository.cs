using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.Infrastructure.Repositories
{
    public class AdsRepository : IAdRepository
    {
        private readonly AdsContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public AdsRepository(AdsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Ad> AddAsync(Ad ad)
        {
            _context.Entry(ad.AdType).State = EntityState.Unchanged;

            var adEntity = await _context.Ads.AddAsync(ad);

            return adEntity.Entity;
        }

        public async Task DeleteAsync(int adId)
        {
            var ad = await _context.Ads.FindAsync(adId);
            _context.Ads.Remove(ad);
        }

        public async Task<Ad> GetByIdAsync(int adId)
        {
            var ad = await _context.Ads
                .Include(i => i.AdType)
                .SingleOrDefaultAsync(s => s.Id == adId);

            return ad;
        }

        public Ad Update(Ad ad)
        {
            var adType = _context.AdTypes.Where(w => w.Id == ad.Id);
            ad.AdType = adType.Single();

            var entity = _context.Update(ad).Entity;
            return entity;
        }
    }
}
