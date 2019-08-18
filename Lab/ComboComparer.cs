using System.Collections.Generic;

namespace Lab
{
    public class ComboComparer<TSource> : IComparer<TSource>
    {
        public ComboComparer(IComparer<TSource> firstComparer, IComparer<TSource> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        private IComparer<TSource> FirstComparer { get; set; }
        private IComparer<TSource> SecondComparer { get; set; }

        public int Compare(TSource x, TSource y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            if (firstCompareResult != 0)
            {
                return firstCompareResult;
            }

            return SecondComparer.Compare(x, y);
        }
    }
}