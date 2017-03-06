namespace Leaf.Nodes
{
    /// <summary>
    /// Stores a boolean value.
    /// </summary>
    public class FlagNode : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Flag"/>.
        /// </summary>
        public override NodeType Type => NodeType.Flag;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public FlagNode(bool value)
        {
            Value = value;
        }
    }
}
