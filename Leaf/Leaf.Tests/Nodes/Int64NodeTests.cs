using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int64Node))]
    public class Int64NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Int64Node(7654321098);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int64));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Int64Node(7654321098);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest(
            [Random(long.MinValue, long.MaxValue, 5)] long value)
        {
            var node = new Int64Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(long.MinValue, long.MaxValue, 1)] long oldValue,
            [Random(long.MinValue, long.MaxValue, 5)] long newValue)
        {
            var node = new Int64Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
