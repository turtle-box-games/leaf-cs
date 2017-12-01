using System;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(StringNode))]
    public class StringNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new StringNode("foobar");
            Assert.That(node.Type, Is.EqualTo(NodeType.String));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new StringNode("foobar");
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Verify that the constructor throws an exception for a null value.")]
        public void NullValueTest()
        {
            Assert.That(() => { new StringNode(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the Value getter returns the correct value.")]
        public void ValueGetterTest()
        {
            const string value = "foobar";
            var node = new StringNode(value);
            Assert.That(node.Value, Is.EqualTo(value));
        }

        [Test(Description = "Verify that the Value setter updates the value.")]
        public void ValueSetterTest()
        {
            const string value = "foobar", newValue = "lorem-ipsum";
            var node = new StringNode(value);
            node.Value = newValue;
            Assert.That(node.Value, Is.EqualTo(newValue));
        }

        [Test(Description = "Verify that the Value setter throws an exception for a null value.")]
        public void ValueSetterNullTest()
        {
            const string value = "foobar";
            var node = new StringNode(value);
            Assert.Multiple(() =>
            {
                Assert.That(() => { node.Value = null; }, Throws.ArgumentNullException);
                Assert.That(node.Value, Is.EqualTo(value));
            });
        }
    }
}