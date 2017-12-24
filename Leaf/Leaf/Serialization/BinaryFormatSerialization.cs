using System.Text;

namespace Leaf.Serialization
{
    /// <summary>
    /// Information about serialization in binary format.
    /// </summary>
    internal abstract class BinaryFormatSerialization
    {
        /// <summary>
        /// Flag indicating whether or not to use big-endian during serialization.
        /// </summary>
        internal const bool BigEndian = true;

        /// <summary>
        /// Text encoding used for serialization.
        /// </summary>
        internal static readonly Encoding StreamEncoding = Encoding.UTF8;
        
        private const long TicksPerSecond = 10_000_000;
        private const long MicroSecond    =  1_000_000;
        
        /// <summary>
        /// Number of (time) ticks per microsecond.
        /// This is the maximum precision that can be stored in binary format.
        /// </summary>
        internal const long TicksPerMicro = TicksPerSecond / MicroSecond;
    }
}