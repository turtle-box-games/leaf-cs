using System;

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
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Uuid"/>.
        /// </summary>
        public override NodeType Type => NodeType.Uuid;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public Guid Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public UuidNode(Guid value)
        {
            Value = value;
        }
    }
}
