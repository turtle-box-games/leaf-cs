using System.Collections.Generic;
using Leaf.Nodes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(BlobNode))]
    public class BlobNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new BlobNode(new byte[0]);
            Assert.That(node.Type, Is.EqualTo(NodeType.Blob));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = new BlobNode(new byte[0]);
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Check that the constructor throws an exception when given null.")]
        public void NullBytesTest()
        {
            Assert.That(() => { new BlobNode(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the Bytes getter returns the correct value.")]
        [TestCaseSource(nameof(RandomBlobs))]
        public void BytesGetterTest(byte[] bytes)
        {
            var node = new BlobNode(bytes);
            Assert.That(node.Bytes, Is.EquivalentTo(bytes));
        }

        [Test(Description = "Verify that the Bytes setter updates the byte array.")]
        [TestCaseSource(nameof(RandomBlobPairs))]
        public void BytesSetterTest(byte[] oldBytes, byte[] newBytes)
        {
            var node   = new BlobNode(oldBytes);
            node.Bytes = newBytes;
            Assert.That(node.Bytes, Is.EquivalentTo(newBytes));
        }

        [Test(Description = "Verify that the Bytes setter throws an exception when trying to use null.")]
        public void BytesSetterNullTest()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};
            var node  = new BlobNode(bytes);
            Assert.That(() => { node.Bytes = null; }, Throws.ArgumentNullException);
        }

        private static IEnumerable<byte[]> RandomBlobs()
        {
            var randomizer = new Randomizer();
            for (var i = 0; i < 5; ++i)
                yield return randomizer.NextBytes();
        }

        private static IEnumerable<byte[][]> RandomBlobPairs()
        {
            var randomizer = new Randomizer();
            for (var i = 0; i < 5; ++i)
                yield return new[] {randomizer.NextBytes(), randomizer.NextBytes()};
        }
    }
}