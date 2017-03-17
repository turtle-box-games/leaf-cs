using System;
using System.IO;

namespace Leaf.Serialization
{
    /// <summary>
    /// Contains header information needed for serialization in binary format.
    /// </summary>
    internal class BinaryFormatHeader
    {
        /// <summary>
        /// Bytes that prefix the contents of every container.
        /// The signature spells out LEAF in ASCII.
        /// </summary>
        private static readonly byte[] Signature = { 0x4C, 0x45, 0x41, 0x46 };

        /// <summary>
        /// Type of the root node.
        /// </summary>
        internal NodeType RootType { get; }

        /// <summary>
        /// Minimum version required to understand the node data.
        /// </summary>
        internal int Version { get; }

        /// <summary>
        /// Creates a new header.
        /// </summary>
        /// <param name="rootType">Type of the root node.</param>
        /// <param name="version">Minimum version required to understand the node data.</param>
        internal BinaryFormatHeader(NodeType rootType, int version)
        {
            RootType = rootType;
            Version  = version;
        }

        /// <summary>
        /// Writes the header to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data onto the stream.</param>
        internal void Write(BinaryWriter writer)
        {
            writer.Write(Signature);
            writer.Write(Version);
            writer.Write((byte)RootType);
        }

        /// <summary>
        /// Reads a header from a stream.
        /// </summary>
        /// <param name="reader">Reader used to get data from the stream.</param>
        /// <returns>Header assembled from stream data.</returns>
        /// <exception cref="FormatException">The data read from the stream is invalid.</exception>
        internal static BinaryFormatHeader Read(BinaryReader reader)
        {
            CheckSignature(reader);
            var version = reader.ReadInt32();
            var rootType = (NodeType) reader.ReadByte();
            return new BinaryFormatHeader(rootType, version);
        }

        /// <summary>
        /// Checks if bytes read from a stream match the expected signature.
        /// </summary>
        /// <param name="reader">Reader used to get data from the stream.</param>
        /// <exception cref="FormatException">The signature read from the stream is invalid.</exception>
        private static void CheckSignature(BinaryReader reader)
        {
            var bytes = reader.ReadBytes(Signature.Length);
            if(bytes.Length != Signature.Length)
                throw new FormatException("Invalid signature");
            // ReSharper disable once LoopCanBeConvertedToQuery - avoid dependency on LINQ library.
            for(var i = 0; i < bytes.Length; ++i)
                if(bytes[i] != Signature[i])
                    throw new Exception("Invalid signature");
        }
    }
}