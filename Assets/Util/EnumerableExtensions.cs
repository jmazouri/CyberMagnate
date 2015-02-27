using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace EnumerableExtensions
{
    public static class EnumerableExtensions
    {
        public static bool IsIdenticalTo<T>(this IEnumerable<T> firstEnumerable, IEnumerable<T> secondEnumerable)
        {
            var enumerable1 = firstEnumerable as T[] ?? firstEnumerable.ToArray();
            var enumerable2 = secondEnumerable as T[] ?? secondEnumerable.ToArray();

            return (enumerable1.Count() == enumerable2.Count()) &&
                   (!enumerable1.Except(enumerable2).Any()) &&
                   (!enumerable2.Except(enumerable1).Any());
        }
    }
}