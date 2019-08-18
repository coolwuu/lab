using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipTests
    {
        [Test]
        public void skip_2_employees()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            var actual = employees.JoeySkip(2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            expected.ToExpectedObject().ShouldMatch(actual.ToList());
        }

        [Test]
        public void skip_last_2()
        {
            var numbers = new[] { 10, 20, 30, 40, 50 };
            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            //var enumerator = numbers.GetEnumerator();
            //var totalCount = numbers.Count();
            //var counter = 1;
            //while (enumerator.MoveNext())
            //{
            //    if (counter <= totalCount - count)
            //    {
            //        var current = enumerator.Current;
            //        yield return current;
            //    }

            //    counter++;
            //}
            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }

                queue.Enqueue(current);
            }
        }
    }
}