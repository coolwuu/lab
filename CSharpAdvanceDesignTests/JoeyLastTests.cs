using ExpectedObjects;
using NUnit.Framework;
using System;
using Lab;

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
    }
}