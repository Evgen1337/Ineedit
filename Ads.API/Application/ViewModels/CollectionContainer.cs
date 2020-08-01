using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ads.API.Application.ViewModels
{
    public class CollectionContainer<T>
    {
        public CollectionContainer(IReadOnlyCollection<T> items)
        {
            Items = items;
        }

        public int Count => Items.Count();

        public IReadOnlyCollection<T> Items { get; }
    }

    public static class CollectionContainerExtensions
    {
        public static CollectionContainer<T> ToCollectionContainer<T>(this IReadOnlyCollection<T> items) =>
            new CollectionContainer<T>(items);
    }
}
