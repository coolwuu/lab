using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinQExtensions
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

        public static List<TSource> JoeyWhereWithIndex<TSource>(this List<TSource> numbers, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            var result = new List<TSource>();

            foreach (var number in numbers)
            {
                if (predicate(number, index))
                {
                    result.Add(number);
                }

                index++;
            }

            return result;
        }

        public static IEnumerable<string> JoeySelect(this IEnumerable<string> urls, Func<string, string> selector)
        {
            var result = new List<string>();
            foreach (var url in urls)
            {
                result.Add(selector(url));
            }

            return result;
        }

        public static List<string> JoeySelectWithIndex(this IEnumerable<string> urls, Func<string, int, string> selector)
        {
            var result = new List<string>();
            var index = 0;
            foreach (var url in urls)
            {
                result.Add(selector(url, index));
                index++;
            }

            return result;
        }
    }
}