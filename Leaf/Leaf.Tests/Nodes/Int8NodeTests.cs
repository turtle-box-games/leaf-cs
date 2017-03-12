using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class Int8NodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new Int8Node(25);
            Assert.AreEqual(NodeType.Int8, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = new Int8Node(25);
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            const byte value = 130;
            var node = new Int8Node(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            const byte value = 130, newValue = 250;
            var node = new Int8Node(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
