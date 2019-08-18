using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyGroupByTests
    {
        [Test]
        public void groupBy_lastName()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Lee"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Lee"},
            };

            var actual = JoeyGroupBy(employees);
            Assert.AreEqual(2, actual.Count());
            var firstGroup = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Eric", LastName = "Chen"},
                new Employee {FirstName = "John", LastName = "Chen"},
            };

            firstGroup.ToExpectedObject().ShouldMatch(actual.First().ToList());
        }

        private static IEnumerable<IGrouping<string, Employee>> JoeyGroupBy(IEnumerable<Employee> employees)
        {
            var lookup = new Dictionary<string, List<Employee>>();
            var enumerator = employees.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!lookup.ContainsKey(current.LastName))
                {
                    lookup.Add(current.LastName, new List<Employee> { current });
                }
                else
                {
                    lookup[current.LastName].Add(current);
                }
            }

            return ConvertToMyGrouping(lookup);
        }

        private static IEnumerable<IGrouping<string, Employee>> ConvertToMyGrouping(Dictionary<string, List<Employee>> lookup)
        {
            var enumerator = lookup.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                yield return new MyGrouping(current.Key, current.Value);
            }
        }
    }

    internal class MyGrouping : IGrouping<string, Employee>
    {
        private readonly List<Employee> _employees;

        public MyGrouping(string key, List<Employee> employees)
        {
            Key = key;
            _employees = employees;
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return _employees.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Key { get; }
    }
}