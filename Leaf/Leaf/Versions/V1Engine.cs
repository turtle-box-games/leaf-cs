using System.IO;
using Leaf.Nodes;

namespace Leaf.Versions
{
    /// <summary>
    /// First version of the node engine.
    /// </summary>
    public class V1Engine : Engine
    {
        /// <summary>
        /// Numerical version used to distinguish this engine from others.
        /// For this engine, the value is 1.
        /// </summary>
        public override int Version { get; }

        /// <summary>
        /// Create a header that contains the node structure information that the engine can use later after serialization.
        /// </summary>
        /// <return>Version 1 header containing information on how the nodes are structured.</return>
        /// <seealso cref="V1Header"/>
        internal override Header CreateHeader()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Reads a root node (structure) from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Root node (structure).</returns>
        internal override Node ReadStructure(BinaryReader reader)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Writes a root node (structure) to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data into the stream.</param>
        /// <param name="node">Root node (structure).</param>
        internal override void WriteStructure(BinaryWriter writer, Node node)
        {
            throw new System.NotImplementedException();
        }
    }
}