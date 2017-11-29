using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int8Node))]
    public class Int8NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new Int8Node(25);
            Assert.AreEqual(NodeType.Int8, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new Int8Node(25);
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const byte value = 130;
            var node = new Int8Node(value);
            Assert.AreEqual(value, node.Value);
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const byte value = 130, newValue = 250;
            var node = new Int8Node(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
