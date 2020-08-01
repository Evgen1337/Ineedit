using Ads.Domain.SeedWork;
using System;

namespace Ads.Domain.AggregatesModel.AdAggregate
{
    public class Ad : Entity, IAggregateRoot
    {
        public Guid OwnerId { get; }

        public DateTime CreationDate { get; } = DateTime.UtcNow;

        public string Name { get; set; }

        public AdType AdType { get; set; }

        public string Comment { get; set; }

        public Ad()
        { }

        public Ad(Guid? ownerId, string name, AdType adType, string comment = null)
        {
            OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            AdType = adType ?? throw new ArgumentNullException(nameof(adType));
            Comment = comment;
        }
    }
}
