using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int64Node))]
    public class Int64NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new Int64Node(7654321098);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int64));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new Int64Node(7654321098);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const long value = 7654321098;
            var node = new Int64Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const long value = 7654321098, newValue = 8907654321;
            var node = new Int64Node(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
