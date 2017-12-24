using System;
using System.IO;
using System.Text;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Writer that can serialize nodes into a compact binary format.
    /// </summary>
    internal class BinaryVersion1NodeWriter : INodeWriter
    {
        private readonly BinaryWriter _writer;
        
        /// <summary>
        /// Creates the node writer.
        /// </summary>
        /// <param name="writer">Writer used to put node data to a stream.</param>
        public BinaryVersion1NodeWriter(BinaryWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Redirects to the correct serialization method.
        /// </summary>
        /// <param name="node">Node type to serialize.</param>
        public void WriteNode(Node node)
        {
            if (node.Version > 1)
                throw new NotSupportedException();
            node.Serialize(this);
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
            var micro = ticks / BinaryFormatSerialization.TicksPerMicro;
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
    }
}