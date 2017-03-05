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
        private byte[] _bytes;

        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Blob"/>.
        /// </summary>
        public override NodeType Type => NodeType.Blob;

        /// <summary>
        /// Gets and sets the bytes of the node.
        /// </summary>
        /// <exception cref="ArgumentNullException">The byte array being set is <c>null</c>.</exception>
        public byte[] Bytes
        {
            get { return _bytes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _bytes = value;
            }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="bytes">Byte array of the data to store in the node.</param>
        /// <exception cref="ArgumentNullException">The array of <paramref name="bytes"/> is <c>null</c>.</exception>
        public BlobNode(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            _bytes = bytes;
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
