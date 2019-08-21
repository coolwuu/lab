using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using System;
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
            var actual = numbers.JoeyLast();
            4.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_last_number_when_empty()
        {
            var numbers = new int[] { };
            Assert.Throws<InvalidOperationException>(() => numbers.JoeyLast());
        }

        [Test]
        public void get_last_employee_with_last_name_wuu()
        {
            var employees = new List<Employee>
            {
                new Employee() {FirstName = "Laoshu", LastName = "Wuu"},
                new Employee() {FirstName = "Lilia", LastName = "Wuu"},
                new Employee() {FirstName = "Lu", LastName = "Wuu"},
            };
            var actual = employees.JoeyLast(e => e.LastName == "Wuu");

            var expected = new Employee() { FirstName = "Lu", LastName = "Wuu" };
            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}