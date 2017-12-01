using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Float64Node))]
    public class Float64NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Float64Node(12345.67d);
            Assert.That(node.Type, Is.EqualTo(NodeType.Float64));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Float64Node(12345.67d);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest()
        {
            const double value = 12345.67d;
            var node = new Float64Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest()
        {
            const double value = 12345.67d, newValue = 9876.543d;
            var node = new Float64Node(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
