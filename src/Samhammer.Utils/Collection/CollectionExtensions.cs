using System;
using System.Collections.Generic;
using System.Linq;

namespace Samhammer.Utils.Collection
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static IEnumerable<T> DefaultIfNullOrEmpty<T>(this IEnumerable<T> collection, T defaultValue)
        {
            return collection?.DefaultIfEmpty(defaultValue) ?? new[] { defaultValue };
        }

        public static bool Contains(this IEnumerable<string> list, string value, StringComparison comparison)
        {
            return list.Any(s => string.Equals(value, s, comparison));
        }
    }
}
