using System.IO;
using Leaf.IO;

namespace Leaf.Serialization
{
    /// <summary>
    /// Deserializes node data from a compact binary format.
    /// </summary>
    public class BinaryFormatReader
    {
        /// <summary>
        /// Create a container from serialized node data contained in an array of bytes.
        /// </summary>
        /// <param name="buffer">Byte array containing previously serialized node data.</param>
        /// <returns>Container holding the root node and associated data pulled from the buffer.</returns>
        public Container ReadFromByteArray(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
                return ReadFromStream(stream);
        }

        /// <summary>
        /// Reads serialized node data from a stream.
        /// </summary>
        /// <param name="input">Stream containing serialized node data in binary format.</param>
        /// <returns>Container holding the root node and associated data pulled from the stream.</returns>
        public Container ReadFromStream(Stream input)
        {
            using (var binaryReader = new EndianAwareBinaryReader(input,
                BinaryFormatSerialization.BigEndian, BinaryFormatSerialization.StreamEncoding))
            {
                var header     = BinaryFormatHeader.Read(binaryReader);
                var nodeReader = new BinaryVersion1NodeReader(binaryReader);
                var rootNode   = nodeReader.ReadNode(header.RootType);
                return new Container(rootNode);
            }
        }
    }
}