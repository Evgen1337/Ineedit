using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ads.Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual int? Id { get; set; }

        public override bool Equals(object obj) =>
             obj is Entity ad &&
                base.Equals(obj) &&
                Id == ad.Id;

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(), Id);

        public static bool operator ==(Entity left, Entity right) =>
             Equals(left, null)
                ? Equals(right, null)
                : left.Equals(right);

        public static bool operator !=(Entity left, Entity right) =>
            !(left == right);
    }
}
