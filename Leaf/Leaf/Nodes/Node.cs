using Leaf.Serialization;

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

        /// <summary>
        /// Version number this node type was introduced in.
        /// </summary>
        public abstract int Version { get; }

        /// <summary>
        /// Calls the correct method to serialize the node.
        /// </summary>
        /// <param name="nodeWriter">Instance that handles serializing node data.</param>
        internal abstract void Serialize(INodeWriter nodeWriter);
    }
}
