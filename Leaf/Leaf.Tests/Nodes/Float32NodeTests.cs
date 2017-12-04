using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(Float32Node))]
    public class Float32NodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new Float32Node(1234.56f);
            Assert.That(node.Type, Is.EqualTo(NodeType.Float32));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new Float32Node(1234.56f);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest(
            [Random(float.MinValue, float.MaxValue, Constants.RandomTestCount),
            Values(0f, float.MinValue, float.MaxValue, float.NaN,
                float.NegativeInfinity, float.PositiveInfinity, float.Epsilon)] float value)
        {
            var node = new Float32Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(float.MinValue, float.MaxValue, 1),
             Values(0f, float.MinValue, float.MaxValue, float.NaN,
                 float.NegativeInfinity, float.PositiveInfinity, float.Epsilon)] float oldValue,
            [Random(float.MinValue, float.MaxValue, Constants.RandomTestCount),
             Values(0f, float.MinValue, float.MaxValue, float.NaN,
                 float.NegativeInfinity, float.PositiveInfinity, float.Epsilon)] float newValue)
        {
            var node = new Float32Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
