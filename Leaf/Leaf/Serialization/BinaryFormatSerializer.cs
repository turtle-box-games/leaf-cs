using System.IO;
using System.Text;
using Leaf.IO;
using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serializes node data to a compact, non-human readable format.
    /// This format is ideal for saving in a compact format.
    /// </summary>
    public class BinaryFormatSerializer : IFormatSerializer
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
        /// Write node data to a stream.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data
        /// to be written to the stream.</param>
        /// <param name="output">Output stream to write data to.</param>
        public void Serialize(Container container, Stream output)
        {
            var root   = container.Root;
            var header = new BinaryFormatHeader(root.Type, root.Version);
            using(var writer = new EndianAwareBinaryWriter(output, BigEndian, StreamEncoding, true))
            {
                var serializer = new BinaryNodeSerializer(writer);
                header.Write(writer);
                root.Serialize(serializer);
            }
        }

        /// <summary>
        /// Read node data from a stream.
        /// </summary>
        /// <param name="input">Input stream to read data from.</param>
        /// <returns>Container holding the root node and associated data read from the stream.</returns>
        public Container Deserialize(Stream input)
        {
            Node root;
            using(var reader = new EndianAwareBinaryReader(input, BigEndian, StreamEncoding, true))
            {
                var header = BinaryFormatHeader.Read(reader);
                var serializer = new BinaryNodeSerializer(reader);
                root = serializer.ReadNode(header.RootType);
            }
            return new Container(root);
        }
    }
}