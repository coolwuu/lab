using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer<T, TKey> : IComparer<T>
    {
        public CombineKeyComparer(Func<T, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<T, TKey> KeySelector { get; set; }
        private IComparer<TKey> KeyComparer { get; set; }

        public int Compare(T x, T y)
        {
            return KeyComparer.Compare(KeySelector(x), KeySelector(y));
        }
    }

    public class ComboComparer<T> : IComparer<T>
    {
        public ComboComparer(IComparer<T> firstComparer, IComparer<T> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        private IComparer<T> FirstComparer { get; set; }
        private IComparer<T> SecondComparer { get; set; }

        public int Compare(T x, T y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            if (firstCompareResult != 0)
            {
                return firstCompareResult;
            }

            return SecondComparer.Compare(x, y);
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = OrderBy(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstComparer =
                new CombineKeyComparer<Employee, string>(employee => employee.LastName, Comparer<string>.Default);
            var secondComparer =
                new CombineKeyComparer<Employee, string>(employee => employee.FirstName, Comparer<string>.Default);
            var comparer = new ComboComparer<Employee>(firstComparer, secondComparer);

            var actual = OrderBy(employees, comparer);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<TSource> OrderBy<TSource>(
            IEnumerable<TSource> employees, IComparer<TSource> comparer)
        {
            //bubble sort
            var elements = employees.ToList();
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
    }
}