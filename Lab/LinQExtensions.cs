using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public static class LinQExtensions
    {
        public static List<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
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

        public static List<TSource> JoeyWhereWithIndex<TSource>(this IEnumerable<TSource> numbers, Func<TSource, int, bool> predicate)
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

        public static IEnumerable<TResult> JoeySelect<TSource,TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var result = new List<TResult>();
            foreach (var item in source)
            {
                result.Add(selector(item));
            }

            return result;
        }

        public static List<string> JoeySelect(this IEnumerable<string> source, Func<string, int, string> selector)
        {
            var index = 0;
            var result = new List<string>();
            foreach (var item in source)
            {
                result.Add(selector(item, index));
                index++;
            }

            return result;
        }
    }
}