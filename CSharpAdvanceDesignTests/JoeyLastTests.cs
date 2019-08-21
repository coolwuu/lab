using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyLastTests
    {
        [Test]
        public void get_last_number()
        {
            var numbers = new[] { 1, 2, 3, 4 };
            var actual = JoeyLast(numbers);
            4.ToExpectedObject().ShouldMatch(actual);
        }

        private int JoeyLast(IEnumerable<int> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            var result = 0;
            while (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }

            return result;
        }
    }
}