using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int8Node))]
    public class Int8NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Int8Node(25);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int8));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Int8Node(25);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest(
            [Random(byte.MinValue, byte.MaxValue, 5)] byte value)
        {
            var node = new Int8Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(byte.MinValue, byte.MaxValue, 1)] byte oldValue,
            [Random(byte.MinValue, byte.MaxValue, 5)] byte newValue)
        {
            var node = new Int8Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}