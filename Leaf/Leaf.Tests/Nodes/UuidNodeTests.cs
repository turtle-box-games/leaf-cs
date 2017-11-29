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
            Assert.AreEqual(NodeType.Uuid, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            var value = Guid.NewGuid();
            var node = new UuidNode(value);
            Assert.AreEqual(value, node.Value);
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            Guid value = Guid.Empty, newValue = Guid.NewGuid();
            var node = new UuidNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
