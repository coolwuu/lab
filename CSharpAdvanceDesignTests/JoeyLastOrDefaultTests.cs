﻿using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = JoeyLastOrDefault(employees);
            Assert.IsNull(actual);
        }

        private Employee JoeyLastOrDefault(IEnumerable<Employee> employees)
        {
            var enumerator = employees.GetEnumerator();
            var result = default(Employee);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                result = current;
            }

            return result;
        }
    }
}