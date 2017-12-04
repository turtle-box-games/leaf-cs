using System;
using System.Collections.Generic;
using System.Linq;
using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(UuidNode))]
    public class UuidNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.That(node.Type, Is.EqualTo(NodeType.Uuid));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        [TestCaseSource(nameof(RandomGuids))]
        [TestCaseSource(nameof(SpecialGuids))]
        public void ValueGetterTest(Guid value)
        {
            var node = new UuidNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        [TestCaseSource(nameof(RandomGuidPairs))]
        [TestCaseSource(nameof(SpecialGuidPairs))]
        public void ValueSetterTest(Guid oldValue, Guid newValue)
        {
            var node   = new UuidNode(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }

        private static IEnumerable<Guid> RandomGuids()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
                yield return randomizer.NextGuid();
        }

        private static IEnumerable<Guid> SpecialGuids()
        {
            yield return Guid.Empty;
        }

        private static IEnumerable<Guid[]> RandomGuidPairs()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
                yield return new[] {randomizer.NextGuid(), randomizer.NextGuid()};
        }

        private static IEnumerable<Guid[]> SpecialGuidPairs()
        {
            var all = RandomGuids().Union(SpecialGuids()).ToList();
            foreach (var first in all)
            foreach (var second in all)
                yield return new[] {first, second};
        }
    }
}