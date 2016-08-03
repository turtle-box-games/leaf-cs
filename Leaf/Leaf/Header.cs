using System.IO;

namespace Leaf
{
    /// <summary>
    /// Contained information for how a node structure is stored.
    /// </summary>
    public abstract class Header
    {
        /// <summary>
        /// Numerical ID used to distinguish the header and engine type.
        /// </summary>
        public abstract int Version { get; }

        /// <summary>
        /// Serializes the header and writes it to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal abstract void Write(BinaryWriter writer);
    }
}