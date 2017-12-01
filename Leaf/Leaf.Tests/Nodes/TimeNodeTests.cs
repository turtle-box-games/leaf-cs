using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(TimeNode))]
    public class TimeNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new TimeNode(DateTime.Now);
            Assert.That(node.Type, Is.EqualTo(NodeType.Time));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new TimeNode(DateTime.Now);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest()
        {
            var value = DateTime.Now - TimeSpan.FromHours(5);
            var node = new TimeNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest()
        {
            DateTime value = DateTime.Now, newValue = DateTime.Today - TimeSpan.FromDays(3);
            var node = new TimeNode(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }
    }
}
