﻿using Lab;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAnyTests
    {
        [Test]
        public void three_employees()
        {
            var emptyEmployees = new Employee[]
            {
                new Employee(),
                new Employee(),
                new Employee(),
            };

            var actual = emptyEmployees.JoeyAny();
            Assert.IsTrue(actual);
        }

        [Test]
        public void empty_employees()
        {
            var emptyEmployees = new Employee[]
            {
            };

            var actual = emptyEmployees.JoeyAny();
            Assert.IsFalse(actual);
        }
    }
}