using System.IO;

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
        public abstract NodeId TypeId { get; }

        /// <summary>
        /// Writes the data of the node to a stream.
        /// </summary>
        /// <param name="writer">Writer to use for putting data in the stream.</param>
        internal abstract void Write(BinaryWriter writer);
    }
}
