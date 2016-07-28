using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class StringNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new StringNode("foobar");
            Assert.AreEqual(NodeId.String, node.TypeId);
        }

        /// <summary>
        /// Verify that the constructor throws an exception for a null value.
        /// </summary>
        [Test]
        public void TestNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StringNode(null);
            });
        }

        /// <summary>
        /// Verify that the Value getter returns the correct value.
        /// </summary>
        [Test]
        public void TestValueGetter()
        {
            const string value = "foobar";
            var node = new StringNode(value);
            Assert.AreEqual(value, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter updates the value.
        /// </summary>
        [Test]
        public void TestValueSetter()
        {
            const string value = "foobar", newValue = "lorem-ipsum";
            var node = new StringNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }

        /// <summary>
        /// Verify that the Value setter throws an exception for a null value.
        /// </summary>
        [Test]
        public void TestValueSetterNull()
        {
            var node = new StringNode("foobar");
            Assert.Throws<ArgumentNullException>(() =>
            {
                node.Value = null;
            });
        }
    }
}
