using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Node that holds a universally unique identifier (UUID).
    /// </summary>
    /// <remarks>.NET uses <see cref="Guid"/> for it's UID system.
    /// This is nearly identical to a UUID except for some small details.
    /// For ease or use, this class uses .NET's <see cref="Guid"/> structure.
    /// However, for serialization, the GUID format is serialized to UUID.
    /// See https://en.wikipedia.org/wiki/Globally_unique_identifier#Binary_encoding for more details.</remarks>
    public class UuidNode : Node
    {
        private Guid _value;

        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeId.Uuid"/>.
        /// </summary>
        public override NodeId TypeId
        {
            get { return NodeId.Uuid; }
        }

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public Guid Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public UuidNode(Guid value)
        {
            _value = value;
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal UuidNode Read(BinaryReader reader)
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
