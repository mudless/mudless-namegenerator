using System;
using System.Collections.Generic;

namespace Mudless.NameGenerator.Utils
{
    internal static class ListExtensions
    {
        public static T TakeRandom<T>(this IList<T> elements, Random random)
        {
            var i = random.Next(elements.Count);

            return elements[i];
        }
    }
}