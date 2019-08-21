using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, string> firstKeySelector, Comparer<string> firstKeyComparer)
        {
            FirstKeySelector = firstKeySelector;
            FirstKeyComparer = firstKeyComparer;
        }

        public Func<Employee, string> FirstKeySelector { get; private set; }
        public Comparer<string> FirstKeyComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            return FirstKeyComparer.Compare(FirstKeySelector(x), FirstKeySelector(y));
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}
        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyOrderByLastNameAndFirstName(employees, new CombineKeyComparer(current => current.LastName, Comparer<string>.Default), current1 => current1.FirstName, Comparer<string>.Default);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(
            IEnumerable<Employee> employees,
            CombineKeyComparer combineKeyComparer,
            Func<Employee, string> secondKeySelector,
            Comparer<string> secondKeyComparer)
        {
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var current = elements[i];
                    var firstCompareResult = combineKeyComparer.Compare(current, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = current;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        var secondCompareResult = secondKeyComparer.Compare(secondKeySelector(current), secondKeySelector(minElement));
                        if (secondCompareResult < 0)
                        {
                            minElement = current;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}