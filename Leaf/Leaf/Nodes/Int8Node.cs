namespace Leaf.Nodes
{
    /// <summary>
    /// Smallest integer value that can be stored.
    /// Stores a value from 0 to 255 (8-bit).
    /// </summary>
    public class Int8Node : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Int8"/>.
        /// </summary>
        public override NodeType Type => NodeType.Int8;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public Int8Node(byte value)
        {
            Value = value;
        }
    }
}
