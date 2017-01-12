using System;
using System.IO;
using System.Text;
using Leaf.IO;
using Leaf.Nodes;
using Leaf.Versions;

namespace Leaf
{
    /// <summary>
    /// Holds a node structure and information about its storage.
    /// This class is used to serialize and deserialize node data.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// Engine used to parse the node structure and header for the container.
        /// </summary>
        private readonly Engine _engine;

        /// <summary>
        /// Access to the root node of the structure.
        /// </summary>
        public Node Root { get; }

        /// <summary>
        /// Creates a container with the specified root node.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <remarks>The lowest compatible engine type will be used for serialization.</remarks>
        public Container(Node root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            Root    = root;
            _engine = new V1Engine();
        }

        /// <summary>
        /// Creates a container for a node with a specific storage engine.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <param name="engine">Engine that understands and serializes the node's structure.</param>
        public Container(Node root, Engine engine)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            if (engine == null)
                throw new ArgumentNullException(nameof(engine));

            Root    = root;
            _engine = engine;
        }

        #region Serialization

        /// <summary>
        /// Flag indicating whether or not to use big-endian during serialization.
        /// </summary>
        private const bool BigEndian = true;

        /// <summary>
        /// Bytes that prefix the contents of every container.
        /// The signature spells out LEAF in ASCII.
        /// </summary>
        private static readonly byte[] Signature = { 0x4C, 0x45, 0x41, 0x46 };

        /// <summary>
        /// Text encoding used for serialization.
        /// </summary>
        private static readonly Encoding StreamEncoding = Encoding.UTF8;

        #region Read

        /// <summary>
        /// Reads a node structure from a stream.
        /// </summary>
        /// <param name="input">Stream to read a container from.</param>
        public static Container Read(Stream input)
        {
            using (var reader = new EndianAwareBinaryReader(input, BigEndian, StreamEncoding, true))
            {
                throw new NotImplementedException();
            }
        }

        #endregion
        #region Write

        /// <summary>
        /// Writes the contents of the container and its structure to a stream.
        /// </summary>
        /// <param name="output">Stream to write the container to.</param>
        public void Write(Stream output)
        {
            var header = _engine.CreateHeader(Root);
            using (var writer = new EndianAwareBinaryWriter(output, BigEndian, StreamEncoding, true))
            {
                WriteSignature(writer, header);               // Write signature.
                header.Write(writer);                         // Write header.
                _engine.WriteStructure(writer, header, Root); // Write structure.
            }
        }

        /// <summary>
        /// Writes the Leaf signature and version to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data on the stream.</param>
        /// <param name="header">Header data created by the engine.</param>
        private static void WriteSignature(BinaryWriter writer, Header header)
        {
            writer.Write(Signature);
            writer.Write(header.Version);
        }

        #endregion
        #endregion
    }
}
