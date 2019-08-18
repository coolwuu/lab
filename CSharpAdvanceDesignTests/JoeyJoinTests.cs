﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Chen" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Chen" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets);

            var expected = new[]
            {
                Tuple.Create("David", "Didi"),
                Tuple.Create("Joey", "Lala"),
                Tuple.Create("Joey", "QQ"),
                Tuple.Create("Tom", "Fufu"),
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<Tuple<string, string>> JoeyJoin(IEnumerable<Employee> employees, IEnumerable<Pet> pets)
        {
            var employeeEnumerator = employees.GetEnumerator();
            var petEnumerator = pets.GetEnumerator();
            while (employeeEnumerator.MoveNext())
            {
                var currentEmployee = employeeEnumerator.Current;
                while (petEnumerator.MoveNext())
                {
                    var currentPet = petEnumerator.Current;
                    if (currentPet.Owner == currentEmployee)
                    {
                        yield return Tuple.Create(currentEmployee.FirstName, currentPet.Name);
                    }
                }
                petEnumerator.Reset();
            }
        }
    }
}