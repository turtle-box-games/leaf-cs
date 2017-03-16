using System;
using Leaf.Serialization;

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
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

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
        /// Calls the correct method to serialize the node.
        /// Uses the strategy algorithm.
        /// </summary>
        /// <param name="serializer">Instance that handles serializing node data.</param>
        internal override void Serialize(INodeSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
