using System;
using System.Text;
using System.IO;

namespace Leaf.IO
{
    /// <summary>
    /// Specialized binary writer that flips the byte order of multi-byte values.
    /// This effectively changes big-endian to little-endian and vice-versa.
    /// </summary>
    /// <remarks>Strings and characters are not handled by this class.
    /// If you need to flip the byte order for a text encoding, use the flipped version of encoding.
    /// For instance, use <see cref="Encoding.BigEndianUnicode"/> if you're writing big-endian on a little-endian system.</remarks>
    internal class FlipBinaryWriter : BinaryWriter
    {
        /// <summary>
        /// Creates a new binary writer with UTF-8 encoding for characters.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream is <c>null</c>.</exception>
        public FlipBinaryWriter(Stream output)
            : base(output)
        {
            // ...
        }

        /// <summary>
        /// Creates a new binary writer with the specified character encoding.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        public FlipBinaryWriter(Stream output, Encoding encoding)
            : base(output, encoding)
        {
            // ...
        }

        /// <summary>
        /// Creates a new binary writer with the specified character encoding and optionally leaves the stream open.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <param name="leaveOpen">Flag indicating whether the stream should be left open after this writer is destroyed.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        public FlipBinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            // ...
        }

        // Only methods that write multi-byte values are overridden.
        // Single-byte and byte array operations are identical and don't need to be overridden.

        /// <summary>
        /// Writes a decimal value to the stream and advances the position in the stream by 16 bytes.
        /// </summary>
        /// <param name="value">Decimal value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(decimal value)
        {
            var bits  = decimal.GetBits(value);
            var bytes = new byte[sizeof(decimal)];
            for (var i = 0; i < bits.Length; ++i)
            {
                var bitBytes = BitConverter.GetBytes(bits[i]);
                for (var j = 0; j < bitBytes.Length; ++j)
                    bytes[i * sizeof(int) + j] = bitBytes[sizeof(int) - j - 1];
            }
            Write(bytes);
        }

        /// <summary>
        /// Writes a double-precision floating-point value to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">Double value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 16-bit signed integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 32-bit signed integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 64-bit signed integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a single-precision floating-point value to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">Single value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            FlipBytes(bytes);
            Write(bytes);
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
                var b = bytes[i];
                bytes[i] = bytes[j];
                bytes[j] = b;
            }
        }
    }
}
