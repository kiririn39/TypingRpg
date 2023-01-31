using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class CommonExtensions
    {
        private static readonly Random rand = new Random();


        public static T randomElement<T>(this Array array)
        {
            if (array.Length == 0)
                return default;

            int idx = rand.Next(0, array.Length);
            return (T)array.GetValue(idx);
        }

        public static T randomElement<T>(this IReadOnlyList<T> list)
            => randomElement(list, rand);

        public static T randomElement<T>(this IReadOnlyList<T> list, Random rnd)
        {
            if (list.Count == 0)
                return default;

            int idx = rnd.Next(0, list.Count);
            return list[idx];
        }

        public static T randomElement<T>(this T[] list)
            => randomElement(list, rand);

        public static T randomElement<T>(this T[] list, Random rnd)
        {
            if (list.Length == 0)
                return default;

            int idx = rnd.Next(0, list.Length);
            return list[idx];
        }

        public static T randomElement<T>(this IReadOnlyCollection<T> enumerable)
        {
            return enumerable.randomElement(new Random());
        }

        private static T randomElement<T>( this IReadOnlyCollection<T> enumerable, Random rand )
        {
            int index = rand.Next( 0, enumerable.Count );
            return enumerable.ElementAt( index );
        }

        public static TValue safeGet<TKey, TValue>( this IReadOnlyDictionary<TKey, TValue> dict, TKey key )
        {
            if ( dict != null && key != null && dict.TryGetValue( key, out TValue value ) )
                return value;
            return default;
        }

        public static List<T> forEach<T>( this IEnumerable<T> enumerable, Action<T> action )
        {
            return enumerable.Select(
                x =>
                {
                    action( x );
                    return x;
                }
            ).ToList();
        }
        
        public static T[] asArray<T>( this T item ) => new T[] { item };
    }
}
