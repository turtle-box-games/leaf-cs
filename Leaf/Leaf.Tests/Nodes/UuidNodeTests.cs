using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class UuidNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.AreEqual(NodeType.Uuid, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = new UuidNode(Guid.NewGuid());
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            var value = Guid.NewGuid();
            var node = new UuidNode(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            Guid value = Guid.Empty, newValue = Guid.NewGuid();
            var node = new UuidNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
