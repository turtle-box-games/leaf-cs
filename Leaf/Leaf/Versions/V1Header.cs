using System.IO;

namespace Leaf.Versions
{
    /// <summary>
    /// First version of the node header.
    /// </summary>
    /// <seealso cref="V1Engine"/>
    internal class V1Header : Header
    {
        /// <summary>
        /// Numerical ID used to distinguish the header and engine type.
        /// For this header version, the value is 1.
        /// </summary>
        internal override int Version => 1;

        /// <summary>
        /// Indicates whether compression is enabled.
        /// Contents of the node structure will be compressed during serialization when this is true.
        /// </summary>
        internal bool Compress { get; }

        /// <summary>
        /// Creates a V1 node header.
        /// </summary>
        /// <param name="compress">Flag indicating whether the node structure should be compressed during serialization.</param>
        internal V1Header(bool compress = true)
        {
            Compress = compress;
        }

        /// <summary>
        /// Reads a header for a container from a stream.
        /// </summary>
        /// <param name="reader">Reader used to get data from the stream.</param>
        /// <returns>Version 1 header containing information on how the nodes are structured.</returns>
        internal static V1Header Read(BinaryReader reader)
        {
            var compress = reader.ReadBoolean();
            return new V1Header(compress);
        }

        /// <summary>
        /// Serializes the header and writes it to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal override void Write(BinaryWriter writer)
        {
            writer.Write(Compress);
        }
    }
}
