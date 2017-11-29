using System;
using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests
{
    [TestFixture(TestOf = typeof(Container))]
    public class ContainerTests
    {
        [Test(Description = "Verify that the constructor throws an exception when the root node is null.")]
        public void TestNullRoot()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Container(null);
            });
        }

        [Test(Description = "Verify that the root node property is set properly.")]
        public void TestRootNode()
        {
            var root = new Int32Node(50);
            var container = new Container(root);
            Assert.AreEqual(root, container.Root);
        }
    }
}
