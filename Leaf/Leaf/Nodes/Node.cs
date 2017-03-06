namespace Leaf.Nodes
{
    /// <summary>
    /// Base class for all node types.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// </summary>
        public abstract NodeType Type { get; }
    }
}
