﻿using System;
using System.Collections.Generic;

namespace AuthorityIdentity.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> selector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource item in source)
            {
                if (seenKeys.Add(selector(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
