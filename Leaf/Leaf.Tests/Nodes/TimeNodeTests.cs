using System;
using System.Collections.Generic;
using Leaf.Nodes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(TimeNode))]
    public class TimeNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new TimeNode(DateTime.Now);
            Assert.That(node.Type, Is.EqualTo(NodeType.Time));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new TimeNode(DateTime.Now);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        [TestCaseSource(nameof(RandomDateTimes))]
        public void ValueGetterTest(DateTime value)
        {
            var node = new TimeNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        [TestCaseSource(nameof(RandomDateTimePairs))]
        public void ValueSetterTest(DateTime oldValue, DateTime newValue)
        {
            var node   = new TimeNode(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }

        private static IEnumerable<DateTime> RandomDateTimes()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < 5; ++i)
                yield return randomizer.NextDateTime();
        }

        private static IEnumerable<DateTime[]> RandomDateTimePairs()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < 5; ++i)
                yield return new[] {randomizer.NextDateTime(), randomizer.NextDateTime()};
        }
    }
}
