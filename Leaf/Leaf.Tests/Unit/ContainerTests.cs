using NUnit.Framework;
using System;
using System.IO;
using Leaf.Nodes;
using Leaf.Versions;

namespace Leaf.Tests.Unit
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
        /// Verify that the custom engine constructor throws a <see cref="ArgumentNullException"/> when the root node is null.
        /// </summary>
        [Test]
        public void TestNullRootEngine()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Container(null, new V1Engine());
            });
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentNullException"/> when the engine is null.
        /// </summary>
        [Test]
        public void TestNullEngine()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Container(new IntNode(50), null);
            });
        }

        /// <summary>
        /// Verify that the root node property is set properly.
        /// </summary>
        [Test]
        public void TestRootNode()
        {
            var root = new IntNode(50);
            var container = new Container(root);
            Assert.AreEqual(root, container.Root);
        }

        /// <summary>
        /// Check that something (anything) is written to the stream.
        /// </summary>
        [Test]
        public void TestWriteAny()
        {
            var root = new IntNode(50);
            var container = new Container(root);
            using (var stream = new MemoryStream())
            {
                container.Write(stream);
                Assert.Greater(stream.Position, 0);
            }
        }

        /// <summary>
        /// Check that the stream stays open after writing to it.
        /// </summary>
        [Test]
        public void TestWriteKeepOpen()
        {
            var root = new IntNode(50);
            var container = new Container(root);
            using (var stream = new MemoryStream())
            {
                container.Write(stream);
                Assert.IsTrue(stream.CanRead);
            }
        }
    }
}
