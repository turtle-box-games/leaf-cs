using System;
using System.IO;
using System.Text;

namespace Leaf.IO
{
    /// <summary>
    /// Smarter binary reader that accounts for endian differences.
    /// Values read from the stream will automatically be converted to the system's native byte ordering.
    /// </summary>
    public class EndianAwareBinaryReader : BinaryReader
    {
        /// <summary>
        /// Underlying implementation.
        /// Set by the constructors to use a <see cref="BinaryReader"/> or <see cref="FlipBinaryReader"/> based on chosen endian.
        /// </summary>
        private readonly BinaryReader _implementation;

        /// <summary>
        /// Creates a new binary reader with UTF-8 encoding for characters.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <param name="bigEndian">Flag indicating whether values from the stream should be treated as being in big-endian byte order.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream is <c>null</c>.</exception>
        public EndianAwareBinaryReader(Stream input, bool bigEndian)
            : base(input)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryReader(input) // Big -> little
                    : new BinaryReader(input); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryReader(input) // Little -> little
                    : new FlipBinaryReader(input); // Little -> big
        }

        /// <summary>
        /// Creates a new binary reader with the specified character encoding.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <param name="bigEndian">Flag indicating whether values from the stream should be treated as being in big-endian byte order.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        /// <remarks>Make sure that the endian type specified in <paramref name="encoding"/> matches the <paramref name="bigEndian"/> flag.
        /// For instance, if <paramref name="bigEndian"/> is true, <paramref name="encoding"/> should be something like <see cref="Encoding.BigEndianUnicode"/>.</remarks>
        public EndianAwareBinaryReader(Stream input, bool bigEndian, Encoding encoding)
            : base(input, encoding)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryReader(input, encoding) // Big -> little
                    : new BinaryReader(input, encoding); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryReader(input, encoding) // Little -> little
                    : new FlipBinaryReader(input, encoding); // Little -> big
        }

        /// <summary>
        /// Creates a new binary reader with the specified character encoding and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">Input stream to read from.</param>
        /// <param name="bigEndian">Flag indicating whether values from the stream should be treated as being in big-endian byte order.</param>
        /// <param name="encoding">Character encoding used to translate bytes to characters.</param>
        /// <param name="leaveOpen">Flag indicating whether the stream should be left open after this reader is destroyed.</param>
        /// <exception cref="ArgumentException">The <paramref name="input"/> stream does not support reading, is <c>null</c>, or is already closed.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="input"/> stream or <paramref name="encoding"/> is <c>null</c>.</exception>
        /// <remarks>Make sure that the endian type specified in <paramref name="encoding"/> matches the <paramref name="bigEndian"/> flag.
        /// For instance, if <paramref name="bigEndian"/> is true, <paramref name="encoding"/> should be something like <see cref="Encoding.BigEndianUnicode"/>.</remarks>
        public EndianAwareBinaryReader(Stream input, bool bigEndian, Encoding encoding, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
            if (bigEndian)
                _implementation = BitConverter.IsLittleEndian
                    ? new FlipBinaryReader(input, encoding, leaveOpen) // Big -> little
                    : new BinaryReader(input, encoding, leaveOpen); // Bit -> big
            else
                _implementation = BitConverter.IsLittleEndian
                    ? new BinaryReader(input, encoding, leaveOpen) // Little -> little
                    : new FlipBinaryReader(input, encoding, leaveOpen); // Little -> big
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
            return _implementation.ReadDecimal();
        }

        /// <summary>
        /// Reads a double-precision floating-point value from the stream and advances the position by 8 bytes.
        /// </summary>
        /// <returns>Double value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override double ReadDouble()
        {
            return _implementation.ReadDouble();
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
            return _implementation.ReadInt16();
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
            return _implementation.ReadInt32();
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
            return _implementation.ReadInt64();
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
            return _implementation.ReadSingle();
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
            return _implementation.ReadUInt16();
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
            return _implementation.ReadUInt32();
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
            return _implementation.ReadUInt64();
        }
    }
}
