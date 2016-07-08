using System;
using System.Text;
using System.IO;

namespace Leaf.IO
{
    /// <summary>
    /// Specialized binary reader that flips the byte order of multi-byte values.
    /// This effectively changes big-endian to little-endian and vice-versa.
    /// </summary>
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        // Only methods that read multi-byte values are overridden.
        // Single-byte and byte array operations are identical and don't need to be overridden.

        /// <summary>
        /// Retrieves the next character from the stream without advancing the position in the stream.
        /// </summary>
        /// <returns>Next character from the stream.</returns>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ArgumentException">The current character cannot be decoded.</exception>
        public override int PeekChar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the next character from the stream and advances the position.
        /// </summary>
        /// <returns>Next character in the stream or -1 if the end of the stream has been reached.</returns>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        public override int Read()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a specified number of characters from the stream.
        /// </summary>
        /// <param name="buffer">Array to store the read character into.</param>
        /// <param name="index">Index in <paramref name="buffer"/> to start storing characters at.</param>
        /// <param name="count">Number of characters to try to read from the stream.</param>
        /// <returns>Actual number of characters read from the stream.
        /// This can be less than <paramref name="count"/> when there are fewer characters available.</returns>
        /// <exception cref="ArgumentException">The length of <paramref name="buffer"/> minus the <paramref name="index"/> is less than <paramref name="count"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override int Read(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a character from the stream and advances a number of bytes based on the encoding.
        /// </summary>
        /// <returns>Next character from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached and a character can't be read.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ArgumentException">A surrogate character was read.</exception>
        public override char ReadChar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a specified number of characters from the stream and returns them as an array.
        /// The position in the stream is advanced based on the character encoding.
        /// </summary>
        /// <param name="count">Number of characters to read.</param>
        /// <returns>Array of characters read from the stream.
        /// The length may be less than <paramref name="count"/> if the end of the stream was reached.</returns>
        /// <exception cref="ArgumentException">A surrogate character was read.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public override char[] ReadChars(int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a decimal value from the stream and advances the position in the stream by 16 bytes.
        /// </summary>
        /// <returns>Decimal value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override decimal ReadDecimal()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a string from the stream that has a prefixed length.
        /// </summary>
        /// <returns>String read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream is reached.</exception>
        /// <exception cref="ObjectDisposedException">The stream is closed.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public override string ReadString()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
