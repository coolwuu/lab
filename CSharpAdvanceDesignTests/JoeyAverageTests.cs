using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyAverageTests
    {
        [Test]
        public void average_with_value()
        {
            var numbers = new int?[] { 2, 4, 6 };

            var actual = JoeyAverage(numbers);

            var expected = 4;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void average_with_value1()
        {
            var numbers = new int?[] { 2, 4, 6, 6 };

            var actual = JoeyAverage(numbers);

            var expected = 4.5;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void average_with_null_value()
        {
            var numbers = new int?[] { 2, 4, 6, null };

            var actual = JoeyAverage(numbers);

            var expected = 4;
            Assert.AreEqual(expected, actual);
        }

        private double? JoeyAverage(IEnumerable<int?> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            double? sum = 0;
            int count = 0;
            while (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                if (number.HasValue)
                {
                    sum += number;
                    count++;
                }
            }

            return sum / count;
        }
    }
}