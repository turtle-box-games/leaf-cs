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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a double-precision floating-point value to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">Double value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(double value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 16-bit signed integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(short value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 32-bit signed integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(int value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 64-bit signed integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(long value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a single-precision floating-point value to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">Single value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(float value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ushort value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(uint value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ulong value)
        {
            throw new NotImplementedException();
        }
    }
}
