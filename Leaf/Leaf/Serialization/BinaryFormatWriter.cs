using System;
using System.IO;
using Leaf.IO;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serializes a node container to a compact binary format.
    /// </summary>
    public class BinaryFormatWriter
    {
        private readonly Node _rootNode;
        private readonly BinaryFormatHeader _header;
        
        /// <summary>
        /// Creates a writer for a node container.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data to be serialized.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="container"/> is null.</exception>
        public BinaryFormatWriter(Container container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            _rootNode = container.Root;
            _header   = new BinaryFormatHeader(_rootNode.Type, _rootNode.Version);
        }
        
        /// <summary>
        /// Generate a byte array containing the node data from a container.
        /// </summary>
        /// <returns>Bytes containing the serialized node data.</returns>
        public byte[] WriteToByteArray()
        {
            using (var stream = new MemoryStream())
            {
                WriteToStream(stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Serialize the node data and write it to a stream.
        /// </summary>
        /// <param name="output">Stream to write serialized node data to.</param>
        /// <remarks>The <paramref name="output"/> stream is left open after writing to it.</remarks>
        public void WriteToStream(Stream output)
        {
            using(var writer = new EndianAwareBinaryWriter(output,
                BinaryFormatSerialization.BigEndian, BinaryFormatSerialization.StreamEncoding))
            {
                var nodeWriter = new BinaryVersion1NodeWriter(writer);
                _header.Write(writer);
                nodeWriter.WriteNode(_rootNode);
            }
        }
    }
}