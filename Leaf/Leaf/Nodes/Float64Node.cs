using System;
using Leaf.Serialization;

namespace Leaf.Nodes
{
    /// <summary>
    /// Large 64-bit floating-point value that can be stored.
    /// Stores a value in the range +/- 1.7 x 10^308 (15 digits).
    /// </summary>
    public class Float64Node : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Float64"/>.
        /// </summary>
        public override NodeType Type => NodeType.Float64;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public Float64Node(double value)
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
