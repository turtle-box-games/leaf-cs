using NUnit.Framework;
using System;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(BlobNode))]
    public class BlobNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new BlobNode(new byte[0]);
            Assert.AreEqual(NodeType.Blob, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = new BlobNode(new byte[0]);
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Check that the constructor throws an exception when given null.")]
        public void TestNullBytes()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BlobNode(null);
            });
        }

        [Test(Description = "Verify that the Bytes getter returns the correct value.")]
        public void TestBytesGetter()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            Assert.AreEqual(bytes, node.Bytes);
        }

        [Test(Description = "Verify that the Bytes setter updates the byte array.")]
        public void TestBytesSetter()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            var newBytes = new byte[] {6, 7, 8, 9, 0};
            node.Bytes   = newBytes;
            Assert.AreEqual(newBytes, node.Bytes);
        }

        [Test(Description = "Verify that the Bytes setter throws an exception when trying to use null.")]
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
