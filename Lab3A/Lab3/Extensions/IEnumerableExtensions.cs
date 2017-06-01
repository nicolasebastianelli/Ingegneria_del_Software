using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        private readonly static Random _random = new Random();

        public static T GetRandomElementFrom<T>(this IEnumerable<T> list)
        {
            return list.ElementAtOrDefault(_random.Next(list.Count()));
        }
    }
}
