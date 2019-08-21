﻿using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = employees.JoeyFirstOrDefault();

            Assert.IsNull(actual);
        }

        [Test]
        public void nullable_of_int_first_or_default()
        {
            var numbers = new List<int?>();

            var actual = numbers.JoeyFirstOrDefault();

            Assert.IsNull(actual);
        }

        [Test]
        public void return_first_girl_age_equal_to_20()
        {
            var girls = new[]
            {
                new Girl(){Age = 10},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = girls.JoeyFirstOrDefault(g => g.Age == 20);
            var expected = new Girl { Age = 20 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }
    }
}