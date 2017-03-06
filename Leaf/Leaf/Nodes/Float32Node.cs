namespace Leaf.Nodes
{
    /// <summary>
    /// Standard 32-bit floating-point value that can be stored.
    /// Stores a value in the range +/- 3.4 x 10^38 (7 digits).
    /// </summary>
    public class Float32Node : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Float32"/>.
        /// </summary>
        public override NodeType Type => NodeType.Float32;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public Float32Node(float value)
        {
            Value = value;
        }
    }
}
