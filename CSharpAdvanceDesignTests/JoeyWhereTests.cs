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
    public class JoeyWhereTests
    {
        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.JoeyWhere(product => product.Price > 200 && product.Price < 500);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void Find_the_first_name_length_less_than_5()
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Claire", LastName = "Chen"},
                new Employee {FirstName = "May", LastName = "Chen"},
            };

            var actual = employees.JoeyWhere(e => e.FirstName.Length < 5);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "May", LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void skip_second_and_take_others_positive_numbers()
        {
            var numbers = new List<int> { 1, 2, 3, 4, -5 };
            var actual = JoeyWhereWithIndex(numbers, (index, number) => index != 1 && number > 0);
            var expected = new List<int> { 1, 3, 4 };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private List<int> JoeyWhereWithIndex(List<int> source, Func<int, int, bool> predicate)
        {
            var index = 0;
            var result = new List<int>();
            foreach (var item in source)
            {
                if (predicate(index, item))
                {
                    result.Add(item);
                }

                index++;
            }

            return result;
        }
    }
}