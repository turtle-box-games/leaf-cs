using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int32Node))]
    public class Int32NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new Int32Node(77777);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int32));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new Int32Node(77777);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const int value = 77777;
            var node = new Int32Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const int value = 77777, newValue = 12345;
            var node = new Int32Node(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
