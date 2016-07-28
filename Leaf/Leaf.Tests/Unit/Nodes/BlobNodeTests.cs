using NUnit.Framework;
using System;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class BlobNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new BlobNode(new byte[0]);
            Assert.AreEqual(NodeId.Blob, node.TypeId);
        }

        /// <summary>
        /// Check that the constructor throws an exception when given null.
        /// </summary>
        [Test]
        public void TestNullBytes()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BlobNode(null);
            });
        }

        /// <summary>
        /// Verify that the Bytes getter returns the correct value.
        /// </summary>
        [Test]
        public void TestBytesGetter()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            Assert.AreEqual(bytes, node.Bytes);
        }

        /// <summary>
        /// Verify that the Bytes setter updates the byte array.
        /// </summary>
        [Test]
        public void TestBytesSetter()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            var newBytes = new byte[] {6, 7, 8, 9, 0};
            node.Bytes   = newBytes;
            Assert.AreEqual(newBytes, node.Bytes);
        }

        /// <summary>
        /// Verify that the Bytes setter throws an exception when trying to use null.
        /// </summary>
        [Test]
        public void TestBytesSetterNull()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            Assert.Throws<ArgumentNullException>(() =>
            {
                node.Bytes = null;
            });
        }
    }
}
