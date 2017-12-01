using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Float32Node))]
    public class Float32NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new Float32Node(1234.56f);
            Assert.That(node.Type, Is.EqualTo(NodeType.Float32));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new Float32Node(1234.56f);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const float value = 1234.56f;
            var node = new Float32Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const float value = 1234.56f, newValue = 789.012f;
            var node = new Float32Node(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
