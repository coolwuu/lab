using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinqExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static List<TSource> JoeyWhereWithIndex<TSource>(this List<TSource> source, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            var result = new List<TSource>();
            foreach (var item in source)
            {
                if (predicate(item, index))
                {
                    result.Add(item);
                }

                index++;
            }

            return result;
        }

        public static List<TSource> JoeySelect<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource> selector)
        {
            var result = new List<TSource>();
            foreach (var item in source)
            {
                result.Add(selector(item));
            }

            return result;
        }

        public static List<TSource> JoeySelectWithIndex<TSource>(this IEnumerable<TSource> source, Func<TSource, int, TSource> selector)
        {
            var result = new List<TSource>();
            int index = 0;
            foreach (var item in source)
            {
                result.Add(selector(item, index));
                index++;
            }

            return result;
        }
    }
}