using System;
using System.IO;
using System.Text;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serialize and deserialize version 1 nodes to binary format.
    /// </summary>
    internal class BinaryNodeSerializer : INodeSerializer
    {
        private const long TicksPerSecond = 10000000;
        private const long MicroSecond = 1000000;
        private const long TicksPerMicro = TicksPerSecond / MicroSecond;

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
            var bytes  = Encoding.UTF8.GetBytes(node.Value);
            var length = bytes.Length;
            _writer.Write((short)length);
            _writer.Write(bytes);
        }

        /// <summary>
        /// Serialize a time node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(TimeNode node)
        {
            var ticks = node.Value.Ticks;
            var micro = ticks / TicksPerMicro;
            _writer.Write(micro);
        }

        /// <summary>
        /// Serialize a UUID node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(UuidNode node)
        {
            var guid  = node.Value;
            var bytes = guid.ToByteArray();
            var data1 = BitConverter.ToInt32(bytes, 0);
            var data2 = BitConverter.ToInt16(bytes, 4);
            var data3 = BitConverter.ToInt16(bytes, 6);
            _writer.Write(data1);
            _writer.Write(data2);
            _writer.Write(data3);
            _writer.Write(bytes, 8, 8);
        }

        /// <summary>
        /// Serialize a blob node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(BlobNode node)
        {
            var bytes  = node.Bytes;
            var length = bytes.Length;
            _writer.Write(length);
            _writer.Write(bytes);
        }

        /// <summary>
        /// Serialize a list node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(ListNode node)
        {
            _writer.Write(node.Count);
            _writer.Write((byte)node.ElementType);
            foreach(var child in node)
                child.Serialize(this);
        }

        /// <summary>
        /// Serialize a composite node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        public void Write(CompositeNode node)
        {
            foreach(var pair in node)
            {
                var bytes  = Encoding.UTF8.GetBytes(pair.Key);
                var length = bytes.Length;
                _writer.Write((byte)pair.Value.Type);
                _writer.Write((byte)length);
                _writer.Write(bytes);
                pair.Value.Serialize(this);
            }
            _writer.Write((byte)NodeType.End);
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
            var length = _reader.ReadInt16();
            var value  = ReadString(length);
            return new StringNode(value);
        }

        /// <summary>
        /// Deserialize a time node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public TimeNode ReadTimeNode()
        {
            var micro = _reader.ReadInt64();
            var ticks = micro * TicksPerMicro;
            var time  = new DateTime(ticks);
            return new TimeNode(time);
        }

        /// <summary>
        /// Deserialize a UUID node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public UuidNode ReadUuidNode()
        {
            var data1 = _reader.ReadInt32();
            var data2 = _reader.ReadInt16();
            var data3 = _reader.ReadInt16();
            var data4 = _reader.ReadBytes(8);
            var guid  = new Guid(data1, data2, data3, data4);
            return new UuidNode(guid);
        }

        /// <summary>
        /// Deserialize a blob node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public BlobNode ReadBlobNode()
        {
            var length = _reader.ReadInt32();
            var bytes  = _reader.ReadBytes(length);
            return new BlobNode(bytes);
        }

        /// <summary>
        /// Deserialize a list node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public ListNode ReadListNode()
        {
            var length      = _reader.ReadInt32();
            var elementType = (NodeType)_reader.ReadByte();
            var reader = GetReaderForNodeType(elementType);
            var list = new ListNode(elementType);
            for(var i = 0; i < length; ++i)
            {
                var node = reader();
                list.Add(node);
            }
            return list;
        }

        /// <summary>
        /// Deserialize a composite node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        public CompositeNode ReadCompositeNode()
        {
            var composite = new CompositeNode();
            var nodeType  = (NodeType) _reader.ReadByte();
            while(nodeType != NodeType.End)
            {
                var keyLength = _reader.ReadByte();
                var key       = ReadString(keyLength);
                var node      = ReadNode(nodeType);
                nodeType      = (NodeType) _reader.ReadByte();
                composite.Add(key, node);
            }
            return composite;
        }

        /// <summary>
        /// Reads a node given its type.
        /// </summary>
        /// <param name="type">Node type to read.</param>
        /// <returns>Read node type.</returns>
        public Node ReadNode(NodeType type)
        {
            var reader = GetReaderForNodeType(type);
            return reader();
        }

        /// <summary>
        /// Describes a method that reads a node and returns it.
        /// </summary>
        private delegate Node NodeReader();

        /// <summary>
        /// Figures out which method to use for a specific node type.
        /// </summary>
        /// <param name="type">Node type to read.</param>
        /// <returns>Method that can be used to read the node.</returns>
        /// <exception cref="NotSupportedException">The node <paramref name="type"/> is unrecognized.
        /// Either the node format is too new or the data is corrupt.</exception>
        private NodeReader GetReaderForNodeType(NodeType type)
        {
            switch(type)
            {
            case NodeType.Flag:
                return ReadFlagNode;
            case NodeType.Int8:
                return ReadInt8Node;
            case NodeType.Int16:
                return ReadInt16Node;
            case NodeType.Int32:
                return ReadInt32Node;
            case NodeType.Int64:
                return ReadInt64Node;
            case NodeType.Float32:
                return ReadFloat32Node;
            case NodeType.Float64:
                return ReadFloat64Node;
            case NodeType.String:
                return ReadStringNode;
            case NodeType.Time:
                return ReadTimeNode;
            case NodeType.Uuid:
                return ReadUuidNode;
            case NodeType.Blob:
                return ReadBlobNode;
            case NodeType.List:
                return ReadListNode;
            case NodeType.Composite:
                return ReadCompositeNode;
            default:
                throw new NotSupportedException($"Unrecognized node type - {type}");
            }
        }

        /// <summary>
        /// Reads a UTF-8 string from the stream.
        /// </summary>
        /// <param name="length">Reported length of the string.</param>
        /// <returns>String read from the stream.</returns>
        private string ReadString(int length)
        {
            var bytes = _reader.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}