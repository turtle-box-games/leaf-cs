using System;
using System.Text;
using System.IO;

namespace Leaf.IO
{
    /// <summary>
    /// Specialized binary reader that flips the byte order of multi-byte values.
    /// This effectively changes big-endian to little-endian and vice-versa.
    /// </summary>
    /// <remarks>Strings and characters are not handled by this class.
    /// If you need to flip the byte order for a text encoding, use the flipped version of encoding.
    /// For instance, use <see cref="Encoding.BigEndianUnicode"/> if you're reading big-endian on a little-endian system.</remarks>
    internal class FlipBinaryReader : BinaryReader
    {
        /// <summary>
        /// Creates a new binary reader with UTF-8 encoding for characters.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream is <c>null</c>.</exception>
        public FlipBinaryReader(Stream input)
            : base(input)
        {
            // ...
        }

        /// <summary>
        /// Creates a new binary reader with the specified character encoding.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        public FlipBinaryReader(Stream input, Encoding encoding)
            : base(input, encoding)
        {
            // ...
        }

        /// <summary>
        /// Creates a new binary reader with the specified character encoding and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <param name="leaveOpen">Flag indicating whether the stream should be left open after this reader is destroyed.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        public FlipBinaryReader(Stream input, Encoding encoding, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
            // ...
        }

        // Only methods that read multi-byte values are overridden.
        // Single-byte and byte array operations are identical and don't need to be overridden.

        /// <summary>
        /// Reads a decimal value from the stream and advances the position in the stream by 16 bytes.
        /// </summary>
        /// <returns>Decimal value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override decimal ReadDecimal()
        {
            var bits  = new int[4];
            var bytes = ReadBytes(sizeof(decimal));
            FlipBytes(bytes);
            for (var i = 0; i < bits.Length; ++i)
                bits[i] = BitConverter.ToInt32(bytes, sizeof(int) * i);
            return new decimal(bits);
        }

        /// <summary>
        /// Reads a double-precision floating-point value from the stream and advances the position by 8 bytes.
        /// </summary>
        /// <returns>Float64 value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override double ReadDouble()
        {
            var bytes = ReadBytes(sizeof(double));
            FlipBytes(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// Reads a 16-bit signed integer from the stream and advances the position by 2 bytes.
        /// </summary>
        /// <returns>16-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override short ReadInt16()
        {
            var bytes = ReadBytes(sizeof(short));
            FlipBytes(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// Reads a 32-bit signed integer from the stream and advances the position by 4 bytes.
        /// </summary>
        /// <returns>32-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override int ReadInt32()
        {
            var bytes = ReadBytes(sizeof(int));
            FlipBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads a 64-bit signed integer from the stream and advances the position by 8 bytes.
        /// </summary>
        /// <returns>64-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override long ReadInt64()
        {
            var bytes = ReadBytes(sizeof(long));
            FlipBytes(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Reads a single-precision floating-point value from the stream and advances the position by 4 bytes.
        /// </summary>
        /// <returns>Single value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override float ReadSingle()
        {
            var bytes = ReadBytes(sizeof(float));
            FlipBytes(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer from the stream and advances the position by 2 bytes.
        /// </summary>
        /// <returns>16-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override ushort ReadUInt16()
        {
            var bytes = ReadBytes(sizeof(ushort));
            FlipBytes(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer from the stream and advances the position by 4 bytes.
        /// </summary>
        /// <returns>32-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override uint ReadUInt32()
        {
            var bytes = ReadBytes(sizeof(uint));
            FlipBytes(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// Reads a 64-bit unsigned integer from the stream and advances the position by 8 bytes.
        /// </summary>
        /// <returns>64-bit integer read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override ulong ReadUInt64()
        {
            var bytes = ReadBytes(sizeof(ulong));
            FlipBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <summary>
        /// Swaps the ordering of bytes in an array.
        /// </summary>
        /// <param name="bytes">Array of bytes to flip.</param>
        private static void FlipBytes(byte[] bytes)
        {
            var mid = bytes.Length / 2;
            for (int i = 0, j = bytes.Length - 1; i < mid; ++i, --j)
            {
                var b    = bytes[i];
                bytes[i] = bytes[j];
                bytes[j] = b;
            }
        }
    }
}
