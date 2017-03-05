using System;
using System.IO;
using System.Text;

namespace Leaf.IO
{
    /// <summary>
    /// Smarter binary reader that accounts for endian differences.
    /// Values read from the stream will automatically be converted to the system's native byte ordering.
    /// </summary>
    public class EndianAwareBinaryWriter : BinaryWriter
    {
        /// <summary>
        /// Underlying implementation.
        /// Set by the constructors to use a <see cref="BinaryWriter"/> or <see cref="FlipBinaryWriter"/> based on chosen endian.
        /// </summary>
        private readonly BinaryWriter _implementation;

        /// <summary>
        /// Creates a new binary writer with UTF-8 encoding for characters.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <param name="bigEndian">Flag indicating whether values in the stream should be treated as being in big-endian byte order.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream is <c>null</c>.</exception>
        public EndianAwareBinaryWriter(Stream output, bool bigEndian)
            : base(output)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryWriter(output) // Big -> little
                    : new BinaryWriter(output); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryWriter(output) // Little -> little
                    : new FlipBinaryWriter(output); // Little -> big
        }

        /// <summary>
        /// Creates a new binary writer with the specified character encoding.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <param name="bigEndian">Flag indicating whether values in the stream should be treated as being in big-endian byte order.</param>
        /// <param name="encoding">Character encoding used to translate characters to bytes.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        /// <remarks>Make sure that the endian type specified in <paramref name="encoding"/> matches the <paramref name="bigEndian"/> flag.
        /// For instance, if <paramref name="bigEndian"/> is true, <paramref name="encoding"/> should be something like <see cref="Encoding.BigEndianUnicode"/>.</remarks>
        public EndianAwareBinaryWriter(Stream output, bool bigEndian, Encoding encoding)
            : base(output, encoding)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryWriter(output, encoding) // Big -> little
                    : new BinaryWriter(output, encoding); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryWriter(output, encoding) // Little -> little
                    : new FlipBinaryWriter(output, encoding); // Little -> big
        }

        /// <summary>
        /// Creates a new binary writer with the specified character encoding and optionally leaves the stream open.
        /// </summary>
        /// <param name="output">Output stream to write to.</param>
        /// <param name="bigEndian">Flag indicating whether values in the stream should be treated as being in big-endian byte order.</param>
        /// <param name="encoding">Character encoding used to translate characters to bytes.</param>
        /// <param name="leaveOpen">Flag indicating whether the stream should be left open after this writer is destroyed.</param>
        /// <exception cref="ArgumentException">The <paramref name="output"/> stream does not support writing, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="output"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        /// <remarks>Make sure that the endian type specified in <paramref name="encoding"/> matches the <paramref name="bigEndian"/> flag.
        /// For instance, if <paramref name="bigEndian"/> is true, <paramref name="encoding"/> should be something like <see cref="Encoding.BigEndianUnicode"/>.</remarks>
        public EndianAwareBinaryWriter(Stream output, bool bigEndian, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryWriter(output, encoding, leaveOpen) // Big -> little
                    : new BinaryWriter(output, encoding, leaveOpen); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryWriter(output, encoding, leaveOpen) // Little -> little
                    : new FlipBinaryWriter(output, encoding, leaveOpen); // Little -> big
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
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a double-precision floating-point value to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">Float64 value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(double value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 16-bit signed integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(short value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 32-bit signed integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(int value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 64-bit signed integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(long value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a single-precision floating-point value to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">Single value to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(float value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer to the stream and advances the position by 2 bytes.
        /// </summary>
        /// <param name="value">16-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ushort value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer to the stream and advances the position by 4 bytes.
        /// </summary>
        /// <param name="value">32-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(uint value)
        {
            _implementation.Write(value);
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer to the stream and advances the position by 8 bytes.
        /// </summary>
        /// <param name="value">64-bit integer to write to the stream.</param>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override void Write(ulong value)
        {
            _implementation.Write(value);
        }
    }
}
