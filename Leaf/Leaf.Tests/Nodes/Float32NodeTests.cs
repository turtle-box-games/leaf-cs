using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class Float32NodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new Float32Node(1234.56f);
            Assert.AreEqual(NodeType.Float32, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = new Float32Node(1234.56f);
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            const float value = 1234.56f;
            var node = new Float32Node(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            const float value = 1234.56f, newValue = 789.012f;
            var node = new Float32Node(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
