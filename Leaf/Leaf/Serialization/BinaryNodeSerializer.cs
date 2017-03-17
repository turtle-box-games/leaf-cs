using System;
using System.IO;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serialize and deserialize version 1 nodes to binary format.
    /// </summary>
    internal class BinaryNodeSerializer : INodeSerializer
    {
        private readonly BinaryWriter _writer;
        private readonly BinaryReader _reader;

        /// <summary>
        /// Creates the serializer in write-mode.
        /// </summary>
        /// <param name="writer">Writer used to put node data to a stream.</param>
        public BinaryNodeSerializer(BinaryWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Creates the serializer in read-mode.
        /// </summary>
        /// <param name="reader">Reader used to get node data from a stream.</param>
        public BinaryNodeSerializer(BinaryReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Serialize a flag node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(FlagNode node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a integer-8 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Int8Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a integer-16 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Int16Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a integer-32 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Int32Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a integer-64 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Int64Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a float-32 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Float32Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a float-64 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(Float64Node node)
        {
            _writer.Write(node.Value);
        }

        /// <summary>
        /// Serialize a string node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(StringNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialize a time node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(TimeNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialize a UUID node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(UuidNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialize a blob node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(BlobNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialize a list node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(ListNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialize a composite node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(CompositeNode node)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a flag node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public FlagNode ReadFlagNode()
        {
            var value = _reader.ReadBoolean();
            return new FlagNode(value);
        }

        /// <summary>
        /// Deserialize a integer-8 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Int8Node ReadInt8Node()
        {
            var value = _reader.ReadByte();
            return new Int8Node(value);
        }

        /// <summary>
        /// Deserialize a integer-16 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Int16Node ReadInt16Node()
        {
            var value = _reader.ReadInt16();
            return new Int16Node(value);
        }

        /// <summary>
        /// Deserialize a integer-32 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Int32Node ReadInt32Node()
        {
            var value = _reader.ReadInt32();
            return new Int32Node(value);
        }

        /// <summary>
        /// Deserialize a integer-64 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Int64Node ReadInt64Node()
        {
            var value = _reader.ReadInt64();
            return new Int64Node(value);
        }

        /// <summary>
        /// Deserialize a float-32 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Float32Node ReadFloat32Node()
        {
            var value = _reader.ReadSingle();
            return new Float32Node(value);
        }

        /// <summary>
        /// Deserialize a float-64 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public Float64Node ReadFloat64Node()
        {
            var value = _reader.ReadDouble();
            return new Float64Node(value);
        }

        /// <summary>
        /// Deserialize a string node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public StringNode ReadStringNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a time node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public TimeNode ReadTimeNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a UUID node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public UuidNode ReadUuidNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a blob node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public BlobNode ReadBlobNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a list node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public ListNode ReadListNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a composite node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public CompositeNode ReadCompositeNode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a node given its type.
        /// </summary>
        /// <param name="type">Node type to read.</param>
        /// <returns>Read node type.</returns>
        public Node ReadNode(NodeType type)
        {
            switch(type)
            {
            case NodeType.Flag:
                return ReadFlagNode();
            case NodeType.Int8:
                return ReadInt8Node();
            case NodeType.Int16:
                return ReadInt16Node();
            case NodeType.Int32:
                return ReadInt32Node();
            case NodeType.Int64:
                return ReadInt64Node();
            case NodeType.Float32:
                return ReadFloat32Node();
            case NodeType.Float64:
                return ReadFloat64Node();
            case NodeType.String:
                return ReadStringNode();
            case NodeType.Time:
                return ReadTimeNode();
            case NodeType.Uuid:
                return ReadUuidNode();
            case NodeType.Blob:
                return ReadBlobNode();
            case NodeType.List:
                return ReadListNode();
            case NodeType.Composite:
                return ReadCompositeNode();
            default:
                throw new NotSupportedException($"Unrecognized node type - {type}");
            }
        }
    }
}