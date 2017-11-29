using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int16Node))]
    public class Int16NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new Int16Node(12345);
            Assert.AreEqual(NodeType.Int16, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new Int16Node(12345);
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const short value = 12345;
            var node = new Int16Node(value);
            Assert.AreEqual(value, node.Value);
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const short value = 12345, newValue = 4321;
            var node = new Int16Node(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
