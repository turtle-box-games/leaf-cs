using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int16Node))]
    public class Int16NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Int16Node(12345);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int16));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Int16Node(12345);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest(
            [Random(short.MinValue, short.MaxValue, 5)] short value)
        {
            var node = new Int16Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(short.MinValue, short.MaxValue, 1)] short oldValue,
            [Random(short.MinValue, short.MaxValue, 5)] short newValue)
        {
            var node = new Int16Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
