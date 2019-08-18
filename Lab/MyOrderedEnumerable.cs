using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    public interface IMyOrderedEnumerable<TSource, TKey> : IEnumerable<TSource>
    {
        IMyOrderedEnumerable<TSource, TKey> CreateOrderedEnumerable(Func<TSource, TKey> keySelector, IComparer<TKey> comparer);
    }

    public class MyOrderedEnumerable<TSource, TKey> : IMyOrderedEnumerable<TSource, TKey>
    {
        private readonly IEnumerable<TSource> _source;
        private readonly IComparer<TSource> _untilNowComparer;

        public MyOrderedEnumerable(IEnumerable<TSource> source, IComparer<TSource> untilNowComparer)
        {
            _source = source;
            _untilNowComparer = untilNowComparer;
        }

        private static IEnumerator<TSource> Sort(IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            //_untilNowComparer = untilNowComparer;
            //_source = source;
            //bubble sort
            var elements = source.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var currentElement = elements[i];
                    var finalResult = comparer.Compare(currentElement, minElement);
                    if (finalResult < 0)
                    {
                        minElement = currentElement;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return Sort(_source, _untilNowComparer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMyOrderedEnumerable<TSource, TKey> CreateOrderedEnumerable(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            var nextComparer = new CombineKeyComparer<TSource, TKey>(keySelector, comparer);
            return new MyOrderedEnumerable<TSource, TKey>(_source, new ComboComparer<TSource>(_untilNowComparer, nextComparer));
        }
    }
}