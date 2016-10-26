using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class TimeNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new TimeNode(DateTime.Now);
            Assert.AreEqual(NodeId.Time, node.TypeId);
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            var value = DateTime.Now - TimeSpan.FromHours(5);
            var node = new TimeNode(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            DateTime value = DateTime.Now, newValue = DateTime.Today - TimeSpan.FromDays(3);
            var node = new TimeNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }
    }
}
