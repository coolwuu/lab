﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                var item = enumerator.Current;
                if (predicate(item))
                {
                    yield return item;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<TSource> JoeySkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            var shouldContinueReturn = false;
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (!predicate(item) || shouldContinueReturn)
                {
                    yield return item;
                    shouldContinueReturn = true;
                }
            }
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                if (predicate(number))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> source)
        {
            return source.GetEnumerator().MoveNext();
        }

        public static bool JoeyAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        public static int JoeyCount<TSource>(this IEnumerable<TSource> source)
        {
            if (source is ICollection<TSource> n)
            {
                return n.Count;
            }

            var enumerator = source.GetEnumerator();
            var counter = 0;
            while (enumerator.MoveNext())
            {
                counter++;
            }

            return counter;
        }

        public static TSource JoeyFirst<TSource>(this IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            throw new InvalidOperationException();
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            return default(TSource);
        }

        public static TSource JoeyLast<TSource>(this IEnumerable<TSource> source)
        {
            var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            var result = enumerator.Current;
            while (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }

            return result;
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var result = default(TSource);
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (predicate(item))
                {
                    result = enumerator.Current;
                }
            }

            return result;
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var result = default(TSource);
            var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }

            return result;
        }

        public static IEnumerable<TSource> JoeyDefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultData)
        {
            return source.IsEmpty() ? DefaultResult(defaultData) : source;
        }

        private static IEnumerable<TSource> DefaultResult<TSource>(TSource defaultData)
        {
            yield return defaultData;
        }

        public static IEnumerable<TSource> JoeyOrderBy<TSource, TKey>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            return new MyOrderedEnumerable<TSource, TKey>(source, comparer);
        }

        public static IMyOrderedEnumerable<TSource, TKey> JoeyOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return new MyOrderedEnumerable<TSource, TKey>(source,
                new CombineKeyComparer<TSource, TKey>(keySelector, Comparer<TKey>.Default));
        }

        public static IMyOrderedEnumerable<TSource, TKey> JoeyThenBy<TSource, TKey>(this IMyOrderedEnumerable<TSource, TKey> source, Func<TSource, TKey> keySelector)
        {
            var comparer = Comparer<TKey>.Default;
            return source.CreateOrderedEnumerable(keySelector, comparer);
        }

        public static IEnumerable<T> JoeyOfType<T>(this IEnumerable values)
        {
            var enumerator = values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current is T z)
                {
                    yield return z;
                }
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> employees, IEqualityComparer<TSource> comparer)
        {
            var enumerator = employees.GetEnumerator();
            var hashSet = new HashSet<TSource>(comparer);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(IEnumerable<TSource> numbers)
        {
            return numbers.Distinct(EqualityComparer<TSource>.Default);
        }
    }
}