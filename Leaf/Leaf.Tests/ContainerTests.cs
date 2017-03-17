using System;
using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentNullException"/> when the root node is null.
        /// </summary>
        [Test]
        public void TestNullRoot()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Container(null);
            });
        }

        /// <summary>
        /// Verify that the root node property is set properly.
        /// </summary>
        [Test]
        public void TestRootNode()
        {
            var root = new Int32Node(50);
            var container = new Container(root);
            Assert.AreEqual(root, container.Root);
        }
    }
}
