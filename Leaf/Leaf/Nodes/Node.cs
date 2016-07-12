using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Base class for all node types.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Writes the data of the node to a stream.
        /// </summary>
        /// <param name="writer">Writer to use for putting data in the stream.</param>
        public abstract void Write(BinaryWriter writer);
    }
}
