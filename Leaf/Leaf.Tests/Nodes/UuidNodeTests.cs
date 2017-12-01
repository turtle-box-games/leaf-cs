using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(UuidNode))]
    public class UuidNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.That(node.Type, Is.EqualTo(NodeType.Uuid));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            var value = Guid.NewGuid();
            var node = new UuidNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            Guid value = Guid.Empty, newValue = Guid.NewGuid();
            var node = new UuidNode(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
