using System.IO;
using System.Text;
using Leaf.IO;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serializes node data to a compact, non-human readable format.
    /// </summary>
    public class BinaryFormatSerializer : IFormatSerializer<byte[]>
    {
        /// <summary>
        /// Flag indicating whether or not to use big-endian during serialization.
        /// </summary>
        private const bool BigEndian = true;

        /// <summary>
        /// Text encoding used for serialization.
        /// </summary>
        private static readonly Encoding StreamEncoding = Encoding.UTF8;

        /// <summary>
        /// Generate a byte array containing the node data from a container.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data to be serialized.</param>
        /// <returns>Output containing the serialized node data.</returns>
        public byte[] Serialize(Container container)
        {
            var root   = container.Root;
            var header = new BinaryFormatHeader(root.Type, root.Version);
            using(var stream = new MemoryStream())
            using(var writer = new EndianAwareBinaryWriter(stream, BigEndian, StreamEncoding))
            {
                var serializer = new BinaryNodeSerializer(writer);
                header.Write(writer);
                root.Serialize(serializer);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Create a container from serialized node data.
        /// </summary>
        /// <param name="input">Byte array containing previously serialized node data.</param>
        /// <returns>Container holding the root node and associated data pull from the input.</returns>
        public Container Deserialize(byte[] input)
        {
            Node root;
            using(var stream = new MemoryStream(input))
            using(var reader = new EndianAwareBinaryReader(stream, BigEndian, StreamEncoding))
            {
                var header = BinaryFormatHeader.Read(reader);
                var serializer = new BinaryNodeSerializer(reader);
                root = serializer.ReadNode(header.RootType);
            }
            return new Container(root);
        }
    }
}