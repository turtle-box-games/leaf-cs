using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Int32Node))]
    public class Int32NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Int32Node(77777);
            Assert.That(node.Type, Is.EqualTo(NodeType.Int32));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Int32Node(77777);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest(
            [Random(int.MinValue, int.MaxValue, Constants.RandomTestCount),
            Values(0, int.MinValue, int.MaxValue)] int value)
        {
            var node = new Int32Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(int.MinValue, int.MaxValue, 1),
             Values(0, int.MinValue, int.MaxValue)] int oldValue,
            [Random(int.MinValue, int.MaxValue, Constants.RandomTestCount),
             Values(0, int.MinValue, int.MaxValue)] int newValue)
        {
            var node = new Int32Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
