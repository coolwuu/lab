using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyIntersectTests
    {
        [Test]
        public void intersect_numbers()
        {
            var first = new[] { 1, 3, 5, 1 };
            var second = new[] { 5, 7, 3, 3 };

            var actual = JoeyIntersect(first, second);

            var expected = new[] { 3, 5 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<int> JoeyIntersect(IEnumerable<int> first, IEnumerable<int> second)
        {
            //add IEnumerable<T> into hashSet can get a distinct-ed hashSet
            var secondHashSet = new HashSet<int>(second);
            var firstHashSet = new HashSet<int>(first);

            var firstEnumerator = firstHashSet.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var current = firstEnumerator.Current;
                if (!secondHashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}