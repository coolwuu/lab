using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class GroupSumTests
    {
        [Test]
        public void group_sum_of_saving()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = JoeyGroupSum(accounts,3);

            var expected = new[] { 60, 150, 240, 210 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_of_saving2()
        {
            var accounts = new[]
            {
                new Account {Name = "Joey", Saving = 10},
                new Account {Name = "David", Saving = 20},
                new Account {Name = "Tom", Saving = 30},
                new Account {Name = "Joseph", Saving = 40},
                new Account {Name = "Jackson", Saving = 50},
                new Account {Name = "Terry", Saving = 60},
                new Account {Name = "Mary", Saving = 70},
                new Account {Name = "Peter", Saving = 80},
                new Account {Name = "Jerry", Saving = 90},
                new Account {Name = "Martin", Saving = 100},
                new Account {Name = "Bruce", Saving = 110},
                new Account {Name = "Wuu", Saving = 120},
            };

            //sum of all Saving of each group which 3 Account per group
            var actual = JoeyGroupSum(accounts,3);

            var expected = new[] { 60, 150, 240, 330 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        public static IEnumerable<int> JoeyGroupSum(IEnumerable<Account> accounts, int groupCount)
        {
            var enumerator = accounts.GetEnumerator();
            var group = (accounts.Count() + groupCount - 1) / groupCount;
            var count = 0;
            while (enumerator.MoveNext() && count < group)
            {
                yield return accounts.Skip(count * groupCount).Take(groupCount).Sum(x => x.Saving);
                count++;
            }
        }
    }

    
}