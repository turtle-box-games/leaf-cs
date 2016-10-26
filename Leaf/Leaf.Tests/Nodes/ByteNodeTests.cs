using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class ByteNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new ByteNode(25);
            Assert.AreEqual(NodeId.Byte, node.TypeId);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            const byte value = 130;
            var node = new ByteNode(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            const byte value = 130, newValue = 250;
            var node = new ByteNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
