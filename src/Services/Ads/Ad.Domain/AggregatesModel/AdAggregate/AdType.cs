using Ads.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace Ads.Domain.AggregatesModel.AdAggregate
{
    public class AdType : Enumeration
    {
        public static AdType Things = new AdType(1, "Things");
        public static AdType Auto = new AdType(2, "Auto");
        public static AdType Toys = new AdType(3, "Toys");

        public AdType(int id, string name) :
            base(id, name)
        {
        }
    }
}