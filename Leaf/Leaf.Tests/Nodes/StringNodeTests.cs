using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(StringNode))]
    public class StringNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new StringNode("foobar");
            Assert.AreEqual(NodeType.String, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new StringNode("foobar");
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Verify that the constructor throws an exception for a null value.")]
        public void TestNullValue()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new StringNode(null);
            });
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void TestValueGetter()
        {
            const string value = "foobar";
            var node = new StringNode(value);
            Assert.AreEqual(value, node.Value);
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void TestValueSetter()
        {
            const string value = "foobar", newValue = "lorem-ipsum";
            var node = new StringNode(value);
            node.Value = newValue;
            Assert.AreEqual(newValue, node.Value);
        }

        [Test(Description = "Verify that the Value setter throws an exception for a null value.")]
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
