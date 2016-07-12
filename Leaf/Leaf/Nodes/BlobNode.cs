using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Node that stores arbitrary binary data.
    /// Stores the data as an array of bytes.
    /// </summary>
    public class BlobNode : Node
    {
        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public byte[] Bytes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="bytes">Byte array of the data to store in the node.</param>
        public BlobNode(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal BlobNode Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the contents of the node to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal override void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
