using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Leaf.Nodes;
using Leaf.Serialization;
using NUnit.Framework;

namespace Leaf.Tests.Serialization
{
    [TestFixture]
    public class BinaryFormatSerializerTests
    {
        private const int HeaderSize = 8;

        #region Serialized node data

        private static readonly byte[] Flag = {0x01};
        #endregion

        /// <summary>
        /// Check that data is actually written.
        /// </summary>
        [Test]
        public void TestSerialize()
        {
            var result = Serialize();
            CollectionAssert.IsNotEmpty(result);
        }

        /// <summary>
        /// Check that the stream is left open after serializing.
        /// </summary>
        [Test]
        public void TestSerializeLeaveStreamOpen()
        {
            var container = GenerateContainer();
            using(var stream = new MemoryStream())
            {
                new BinaryFormatSerializer().Serialize(container, stream);
                Assert.Greater(stream.Length, 0);
            }
        }

        /// <summary>
        /// Check that the header contains the signature.
        /// </summary>
        [Test]
        public void TestSerializeHeaderSignature()
        {
            var result = Serialize();
            Assert.AreEqual(0x4c, result[0]);
            Assert.AreEqual(0x45, result[1]);
            Assert.AreEqual(0x41, result[2]);
            Assert.AreEqual(0x46, result[3]);
        }

        /// <summary>
        /// Check that the header contains the version.
        /// </summary>
        [Test]
        public void TestSerializeHeaderVersion()
        {
            var result = Serialize();
            Assert.AreEqual(0x00, result[4]);
            Assert.AreEqual(0x00, result[5]);
            Assert.AreEqual(0x00, result[6]);
            Assert.AreEqual(0x01, result[7]);
        }

        /// <summary>
        /// Check that the header contains the node type.
        /// </summary>
        [Test]
        public void TestSerializeHeaderType()
        {
            var node = GenerateRootNode();
            var result = Serialize(node);
            Assert.AreEqual((byte)node.Type, result[HeaderSize]);
        }

        /// <summary>
        /// Check that the data serialized for a flag node is correct.
        /// </summary>
        [Test]
        public void TestSerializeFlagNode()
        {
            var node = new FlagNode(true);
            byte[] expected = {0x01};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a Int8Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeInt8Node()
        {
            var node = new Int8Node(123);
            byte[] expected = {0x7b};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for an Int16Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeInt16Node()
        {
            var node = new Int16Node(12345);
            byte[] expected = {0x30, 0x39};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for an Int32Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeInt32Node()
        {
            var node = new Int32Node(1234567);
            byte[] expected = {0x00, 0x12, 0xd6, 0x87};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for an Int64Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeInt64Node()
        {
            var node = new Int64Node(98765432101234567);
            byte[] expected = {0x01, 0x5e, 0xe2, 0xa3, 0x20, 0x7b, 0x67, 0x87};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a Float32Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeFloat32Node()
        {
            var node = new Float32Node(1.2345f);
            byte[] expected = {0x3f, 0x9e, 0x04, 0x19};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a Float64Node is correct.
        /// </summary>
        [Test]
        public void TestSerializeFloat64Node()
        {
            var node = new Float64Node(12345.6789d);
            byte[] expected = {0x40, 0xc8, 0x1c, 0xd6, 0xe6, 0x31, 0xf8, 0xa1};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a StringNode is correct.
        /// </summary>
        [Test]
        public void TestSerializeStringNode()
        {
            var node = new StringNode("foobarbaz");
            byte[] expected = {0x00, 0x09, 0x66, 0x6f, 0x6f, 0x62, 0x61, 0x72, 0x62, 0x61, 0x7a};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for an empty ListNode is correct.
        /// </summary>
        [Test]
        public void TestSerializeEmptyList()
        {
            var node = new ListNode(NodeType.Flag);
            byte[] expected = {0x00, 0x00, 0x00, 0x00, (byte) NodeType.Flag};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a ListNode is correct.
        /// </summary>
        [Test]
        public void TestSerializeList()
        {
            var nodes = new Node[]
            {
                new Int32Node(12345),
                new Int32Node(67890),
                new Int32Node(1337)
            };
            var node = new ListNode(NodeType.Int32, nodes);
            byte[] preface = {0x00, 0x00, 0x00, 0x03, (byte) NodeType.Int32};
            var expected = ConcatNodes(preface, nodes);
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized for a ListNode containing ListNodes is correct.
        /// </summary>
        [Test]
        public void TestSerializeListOfList()
        {
            var set1 = new Node[]
            {
                new Int32Node(12345),
                new Int32Node(67890),
                new Int32Node(1337)
            };
            var set2 = new Node[]
            {
                new StringNode("foo"),
                new StringNode("bar"),
                new StringNode("baz"),
            };
            var lists = new Node[]
            {
                new ListNode(NodeType.Int32, set1),
                new ListNode(NodeType.Int32, set1),
                new ListNode(NodeType.String, set2),
                new ListNode(NodeType.String, set2),
            };
            var node = new ListNode(NodeType.List, lists);
            byte[] preface0 = {0x00, 0x00, 0x00, 0x04, (byte) NodeType.List};
            byte[] preface1 = {0x00, 0x00, 0x00, 0x03, (byte) NodeType.Int32};
            byte[] preface2 = {0x00, 0x00, 0x00, 0x03, (byte) NodeType.String};
            var bytes1 = ConcatNodes(preface1, set1);
            var bytes2 = ConcatNodes(preface2, set2);
            var expected = ConcatByteSet(preface0, bytes1, bytes1, bytes2, bytes2);
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from a list of CompositeNodes is correct.
        /// </summary>
        [Test]
        public void TestSerializeListOfComposite()
        {
            var pairs = new KeyValuePair<string, Node>[]
            {
                new KeyValuePair<string, Node>("flag", new FlagNode(true)),
                new KeyValuePair<string, Node>("float", new Float32Node(123.45f)),
                new KeyValuePair<string, Node>("time", new TimeNode(DateTime.Now)),
            };
            var composites = new Node[]
            {
                new CompositeNode(pairs),
                new CompositeNode(pairs),
                new CompositeNode(pairs),
                new CompositeNode(pairs)
            };
            var node = new ListNode(NodeType.Composite, composites);
            byte[] preface = {0x00, 0x00, 0x00, 0x04, (byte) NodeType.Composite};
            var expected = ConcatNodes(preface, composites);
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from an empty CompositeNode is correct.
        /// </summary>
        [Test]
        public void TestSerializeEmptyComposite()
        {
            var node = new CompositeNode();
            byte[] expected = {(byte) NodeType.End};
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from a CompositeNode is correct.
        /// </summary>
        [Test]
        public void TestSerializeComposite()
        {
            var pairs = new[]
            {
                new KeyValuePair<string, Node>("int", new Int32Node(1234567)),
                new KeyValuePair<string, Node>("string", new StringNode("foobar")),
                new KeyValuePair<string, Node>("blob", new BlobNode(new byte[] {0x12, 0x34, 0x56, 0x78, 0x90}))
            };
            var node = new CompositeNode(pairs);
            var expected = ConcatNodes(pairs);
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from a CompositeNode containing ListNodes is correct.
        /// </summary>
        [Test]
        public void TestSerializeCompositeOfList()
        {
            var nodes = new Node[]
            {
                new Int32Node(12345),
                new Int32Node(67890),
                new Int32Node(98765),
                new Int32Node(43210)
            };
            var pairs = new[]
            {
                new KeyValuePair<string, Node>("l1", new ListNode(NodeType.Int32, nodes)),
                new KeyValuePair<string, Node>("l2", new ListNode(NodeType.Int32, nodes)),
                new KeyValuePair<string, Node>("l3", new ListNode(NodeType.Int32, nodes))
            };
            var node = new CompositeNode(pairs);
            var expected = ConcatNodes(pairs);
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from a CompositeNode containing CompositeNodes is correct.
        /// </summary>
        [Test]
        public void TestSerializeCompositeOfComposite()
        {
            var pairs = new[]
            {
                new KeyValuePair<string, Node>("n1", new UuidNode(Guid.Empty)),
                new KeyValuePair<string, Node>("n2", new Float64Node(12345.6789f)),
                new KeyValuePair<string, Node>("n3", new Int8Node(42))
            };
            var pairsOfComposites = new[]
            {
                new KeyValuePair<string, Node>("c1", new CompositeNode(pairs)),
                new KeyValuePair<string, Node>("c2", new CompositeNode(pairs)),
                new KeyValuePair<string, Node>("c3", new CompositeNode(pairs))
            };
            var node = new CompositeNode(pairsOfComposites);
            var sets = ConcatNodes(pairs);
            var expected = ConcatByteSet(SerializePair("c1", sets), SerializePair("c2", sets),
                SerializePair("c3", sets), new[] {(byte) NodeType.End});
            CheckSerializedNodeData(node, expected);
        }

        /// <summary>
        /// Check that the data serialized from a CompositeNode containing a mix of nodes is correct.
        /// </summary>
        [Test]
        public void TestSerializeComplexComposite()
        {
            var listNodes = new[]
            {
                new Int64Node(1234567890),
                new Int64Node(9876543210)
            };
            var compositePairs = new[]
            {
                new KeyValuePair<string, Node>("foo", new StringNode("foo")),
                new KeyValuePair<string, Node>("bar", new TimeNode(DateTime.Now)),
                new KeyValuePair<string, Node>("baz",
                    new BlobNode(new byte[] {0x90, 0x89, 0x78, 0x67, 0x56, 0x45, 0x34, 0x23, 0x12}))
            };
            var pairs = new[]
            {
                new KeyValuePair<string, Node>("flag", new FlagNode(false)),
                new KeyValuePair<string, Node>("short", new Int16Node(1337)),
                new KeyValuePair<string, Node>("float", new Float32Node(123.45f)),
                new KeyValuePair<string, Node>("list", new ListNode(NodeType.Int64, listNodes)),
                new KeyValuePair<string, Node>("composite", new CompositeNode(compositePairs))
            };
            Assert.Inconclusive();
        }

        private static byte[] Serialize(Node root = null)
        {
            var container = new Container(root ?? GenerateRootNode());
            using(var stream = new MemoryStream())
            {
                new BinaryFormatSerializer().Serialize(container, stream);
                return stream.ToArray();
            }
        }

        private static byte[] SerializeNode(Node node)
        {
            var bytes = Serialize(node);
            return bytes.Skip(HeaderSize + 1).ToArray();
        }

        private static byte[] ConcatByteSet(params byte[][] arrays)
        {
            return ConcatByteArrays(arrays);
        }

        private static byte[] ConcatByteArrays(ICollection<byte[]> arrays)
        {
            var size = arrays.Sum(a => a.Length);
            var bytes = new byte[size];
            var i = 0;
            foreach(var array in arrays)
            foreach(var b in array)
                bytes[i++] = b;
            return bytes;
        }

        private static byte[] ConcatNodes(byte[] preface, IEnumerable<Node> nodes)
        {
            var nodeData = new[] {preface}.Union(nodes.Select(SerializeNode));
            return ConcatByteArrays(nodeData.ToList());
        }

        private static byte[] ConcatNodes(IEnumerable<KeyValuePair<string, Node>> pairs)
        {
            var sets = pairs.Select(SerializePair).ToList();
            sets.Add(new[] {(byte)NodeType.End});
            return ConcatByteArrays(sets);
        }

        private static byte[] SerializeString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var size = bytes.Length;
            byte[] sizeByte = {(byte) size};
            return ConcatByteSet(sizeByte, bytes);
        }

        private static byte[] SerializePair(KeyValuePair<string, Node> pair)
        {
            var keyBytes = SerializeString(pair.Key);
            var nodeBytes = SerializeNode(pair.Value);
            return ConcatByteSet(new[] {(byte) pair.Value.Type}, keyBytes, nodeBytes);
        }

        private static byte[] SerializePair(string key, byte[] nodeBytes)
        {
            var keyBytes = SerializeString(key);
            return ConcatByteSet(new[] {(byte) NodeType.Composite}, keyBytes, nodeBytes);
        }

        private static Container GenerateContainer()
        {
            var node = GenerateRootNode();
            return new Container(node);
        }

        private static Node GenerateRootNode()
        {
            return new Int32Node(1234567);
        }

        private static void CheckSerializedNodeData(Node node, byte[] expected)
        {
            var actual = Serialize(node);
            Assert.AreEqual((byte)node.Type, actual[HeaderSize]);
            Assert.AreEqual(expected.Length + HeaderSize + 1, actual.Length, "Serialization length mismatch");
            for(int i = 0, j = HeaderSize + 1; i < expected.Length; ++i, ++j)
                Assert.AreEqual(expected[i], actual[j], $"Index {i} ({j}) expected: {expected[i]}, actual: {actual[j]}");
        }
    }
}
