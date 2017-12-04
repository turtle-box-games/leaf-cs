using Leaf.Nodes;
using NUnit.Framework;

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
        public void ValueGetterTest(
            [Random(double.MinValue, double.MaxValue, Constants.RandomTestCount),
             Values(0d, double.MinValue, double.MaxValue, double.NaN,
                 double.NegativeInfinity, double.PositiveInfinity, double.Epsilon)] double value)
        {
            var node = new Float64Node(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest(
            [Random(double.MinValue, double.MaxValue, 1),
             Values(0d, double.MinValue, double.MaxValue, double.NaN,
                 double.NegativeInfinity, double.PositiveInfinity, double.Epsilon)] double oldValue,
            [Random(double.MinValue, double.MaxValue, Constants.RandomTestCount),
             Values(0d, double.MinValue, double.MaxValue, double.NaN,
                 double.NegativeInfinity, double.PositiveInfinity, double.Epsilon)] double newValue)
        {
            var node = new Float64Node(oldValue);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
