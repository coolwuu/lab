using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using System;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyFirstTests
    {
        [Test]
        public void get_first_girl()
        {
            var girls = new[]
            {
                new Girl(){Age = 10},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = girls.JoeyFirst();
            var expected = new Girl { Age = 10 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }

        [Test]
        public void get_first_girl_with_age_30()
        {
            var girls = new[]
            {
                new Girl(){Age = 10},
                new Girl(){Age = 20},
                new Girl(){Age = 30},
            };

            var girl = girls.JoeyFirst(g => g.Age == 30);
            var expected = new Girl { Age = 30 };

            expected.ToExpectedObject().ShouldEqual(girl);
        }

        [Test]
        public void get_first_girl_when_girls_is_empty()
        {
            var girls = new Girl[] { };

            Assert.Throws<InvalidOperationException>(() => girls.JoeyFirst());
        }
    }
}