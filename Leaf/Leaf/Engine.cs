using System.IO;
using Leaf.Nodes;

namespace Leaf
{
    /// <summary>
    /// Handles serialization and meta-data of nodes in a container.
    /// </summary>
    public abstract class Engine
    {
        /// <summary>
        /// Numerical version used to distinguish this engine from others.
        /// </summary>
        public abstract int Version { get; }

        /// <summary>
        /// Create a header that contains the node structure information that the engine can use later after serialization.
        /// </summary>
        /// <return>Header containing information on how the nodes are structured.</return>
        internal abstract Header CreateHeader();

        /// <summary>
        /// Reads a root node (structure) from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Root node (structure).</returns>
        internal abstract Node ReadStructure(BinaryReader reader);

        /// <summary>
        /// Writes a root node (structure) to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data into the stream.</param>
        /// <param name="node">Root node (structure).</param>
        internal abstract void WriteStructure(BinaryWriter writer, Node node);
    }
}
