using System.IO;

namespace Leaf.Versions
{
    /// <summary>
    /// First version of the node header.
    /// </summary>
    internal class V1Header : Header
    {
        /// <summary>
        /// Numerical ID used to distinguish the header and engine type.
        /// For this header version, the value is 1.
        /// </summary>
        internal override int Version { get; }

        /// <summary>
        /// Serializes the header and writes it to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal override void Write(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
