﻿using System;
using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => url.Replace("http://", "https://"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void append_port_9191_to_urls()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => $"{url}:9191");
            var expected = new List<string>
            {
                "http://tw.yahoo.com:9191",
                "https://facebook.com:9191",
                "https://twitter.com:9191",
                "http://github.com:9191",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void select_with_seq_no()
        {
            var urls = GetUrls();

            var actual = JoeySelectWithIndex(urls, (url, index) => selector(url, index));
            var expected = new List<string>
            {
                "1. http://tw.yahoo.com",
                "2. https://facebook.com",
                "3. https://twitter.com",
                "4. http://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private static List<string> JoeySelectWithIndex(IEnumerable<string> urls, Func<string, int, string> selector)
        {
            var result = new List<string>();
            int index = 0;
            foreach (var url in urls)
            {
                result.Add(selector(url, index));
                index++;
            }

            return result;
        }

        private static string selector(string url, int index)
        {
            return $"{index + 1}. {url}";
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}