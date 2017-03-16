using System;
using Leaf.Serialization;

namespace Leaf.Nodes
{
    /// <summary>
    /// Typical integer value that can be stored.
    /// Stores a value from -2,147,483,648 to 2,147,483,647 (32-bit).
    /// </summary>
    public class Int32Node : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Int32"/>.
        /// </summary>
        public override NodeType Type => NodeType.Int32;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public Int32Node(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Calls the correct method to serialize the node.
        /// Uses the strategy algorithm.
        /// </summary>
        /// <param name="serializer">Instance that handles serializing node data.</param>
        internal override void Serialize(INodeSerializer serializer)
        {
            serializer.Write(this);
        }
    }
}
