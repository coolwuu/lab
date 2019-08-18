using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5, 1 };
            var second = new[] { 5, 3, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            var enumerator1 = first.GetEnumerator();
            var enumerator2 = second.GetEnumerator();
            var hashSet = new HashSet<int>();

            while (enumerator1.MoveNext())
            {
                var current = enumerator1.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }

            while (enumerator2.MoveNext())
            {
                var current = enumerator2.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}