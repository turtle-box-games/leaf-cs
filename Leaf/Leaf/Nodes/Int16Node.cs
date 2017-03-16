using System;
using Leaf.Serialization;

namespace Leaf.Nodes
{
    /// <summary>
    /// Small integer value that can be stored.
    /// Stores a value from -32,768 to 32,767 (16-bit).
    /// </summary>
    public class Int16Node : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Int16"/>.
        /// </summary>
        public override NodeType Type => NodeType.Int16;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public short Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public Int16Node(short value)
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
            throw new NotImplementedException();
        }
    }
}
