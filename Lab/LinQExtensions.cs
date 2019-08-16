﻿using Lab.Entities;
using System;
using System.Collections.Generic;

namespace Lab
{
    public static class LinQExtensions
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current, index))
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current);
            }
        }

        public static IEnumerable<TSource> JoeySelect<TSource>(this IEnumerable<TSource> source, Func<TSource, int, TSource> selector)
        {
            var index = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return selector(enumerator.Current, index);
                index++;
            }
        }

        public static IEnumerable<TSource> JoeyTake<TSource>(this IEnumerable<TSource> source, int count)
        {
            var alreadyReturnCount = 1;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext() && alreadyReturnCount <= count)
            {
                yield return enumerator.Current;
                alreadyReturnCount++;
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> source, int count)
        {
            var enumerator = source.GetEnumerator();
            var skippedCount = 0;
            while (enumerator.MoveNext())
            {
                if (skippedCount >= count)
                {
                    yield return enumerator.Current;
                }

                skippedCount++;
            }
        }

        public static IEnumerable<TSource> JoeyTakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var card = enumerator.Current;
                if (predicate(card))
                {
                    yield return card;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}