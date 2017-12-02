using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(FlagNode))]
    public class FlagNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new FlagNode(false);
            Assert.That(node.Type, Is.EqualTo(NodeType.Flag));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new FlagNode(false);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        [TestCase(true), TestCase(false)]
        public void ValueGetterTest(bool value)
        {
            var node = new FlagNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        [TestCase(true), TestCase(false)]
        public void ValueSetterTest(bool value)
        {
            var newValue = !value;
            var node = new FlagNode(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
