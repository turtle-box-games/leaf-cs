using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class Float64NodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new Float64Node(12345.67d);
            Assert.AreEqual(NodeType.Float64, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = new Float64Node(12345.67d);
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            const double value = 12345.67d;
            var node = new Float64Node(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            const double value = 12345.67d, newValue = 9876.543d;
            var node = new Float64Node(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
