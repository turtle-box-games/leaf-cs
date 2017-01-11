using System;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;

namespace Leaf.Tests.IO
{
    [TestFixture]
    public class EndianAwareBinaryReaderTests
    {
        private static byte[] GetDecimalBytes(decimal value)
        {
            var ints = decimal.GetBits(value);
            var bytes = new byte[sizeof(int) * ints.Length];
            for (int i = 0, j = 0; i < ints.Length && j < bytes.Length; ++i)
            {
                var intBytes = BitConverter.GetBytes(ints[i]);
                for (var k = 0; k < intBytes.Length; ++k, ++j)
                    bytes[j] = intBytes[k];
            }
            return bytes;
        }

        private static void FlipByteArray(byte[] bytes)
        {
            var mid = bytes.Length / 2;
            for (int i = 0, j = bytes.Length - 1; i < mid; ++i, --j)
            {
                var b = bytes[i];
                bytes[i] = bytes[j];
                bytes[j] = b;
            }
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentNullException"/> when the stream is null.
        /// </summary>
        [Test]
        public void TestNullStream()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EndianAwareBinaryReader(null, true);
            });
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStream()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true);
                });
            }
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentException"/> when the stream can't be read from.
        /// </summary>
        [Test]
        public void TestNonReadableStream()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true);
                });
            }
            File.Delete(filename);
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentNullException"/> when the stream is null.
        /// </summary>
        [Test]
        public void TestNullStreamEncoding()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EndianAwareBinaryReader(null, true, Encoding.UTF8);
            });
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStreamEncoding()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, Encoding.UTF8);
                });
            }
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentException"/> when the stream can't be read from.
        /// </summary>
        [Test]
        public void TestNonReadableStreamEncoding()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, Encoding.UTF8);
                });
            }
            File.Delete(filename);
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentNullException"/> when the stream is null.
        /// </summary>
        [Test]
        public void TestNullStreamLeaveOpen()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EndianAwareBinaryReader(null, true, Encoding.UTF8, true);
            });
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStreamLeaveOpen()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, Encoding.UTF8, true);
                });
            }
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentException"/> when the stream can't be read from.
        /// </summary>
        [Test]
        public void TestNonReadableStreamLeaveOpen()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, Encoding.UTF8, true);
                });
            }
            File.Delete(filename);
        }

        /// <summary>
        /// Verifies that the "encoding" constructor throws a <see cref="ArgumentNullException"/> when the encoding is null.
        /// </summary>
        [Test]
        public void TestNullEncoding()
        {
            using (var input = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, null);
                });
            }
        }

        /// <summary>
        /// Verifies that the "leave open" constructor throws a <see cref="ArgumentNullException"/> when the encoding is null.
        /// </summary>
        [Test]
        public void TestNullEncodingLeaveOpen()
        {
            using (var input = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new EndianAwareBinaryReader(input, true, null, true);
                });
            }
        }

        /// <summary>
        /// Check that a decimal value can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadDecimalNoFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadDecimal();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 16 bytes after reading a decimal.
        /// </summary>
        [Test]
        public void TestReadDecimalNoFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadDecimal();
                    Assert.AreEqual(sizeof(decimal), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a decimal value can be read when the stream is in the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadDecimalFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadDecimal();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 16 bytes after reading a decimal.
        /// </summary>
        [Test]
        public void TestReadDecimalFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadDecimal();
                    Assert.AreEqual(sizeof(decimal), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a single-precision floating-point value can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadSingleNoFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadSingle();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading a single-precision floating-point value.
        /// </summary>
        [Test]
        public void TestReadSingleNoFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadSingle();
                    Assert.AreEqual(sizeof(float), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a single-precision floating-point value can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadSingleFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadSingle();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading a single-precision floating-point value.
        /// </summary>
        [Test]
        public void TestReadSingleFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadSingle();
                    Assert.AreEqual(sizeof(float), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a double-precision floating-point value can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadDoubleNoFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadDouble();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading a double-precision floating-point value.
        /// </summary>
        [Test]
        public void TestReadDoubleNoFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadDouble();
                    Assert.AreEqual(sizeof(double), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a double-precision floating-point value can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadDoubleFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadDouble();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading a double-precision floating-point value.
        /// </summary>
        [Test]
        public void TestReadDoubleFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadDouble();
                    Assert.AreEqual(sizeof(double), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 16-bit integer can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt16NoFlip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt16();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after reading a 16-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt16NoFlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadInt16();
                    Assert.AreEqual(sizeof(short), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 16-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt16Flip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt16();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after reading a 16-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt16FlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadInt16();
                    Assert.AreEqual(sizeof(short), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 32-bit integer can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt32NoFlip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt32();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading a 32-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt32NoFlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadInt32();
                    Assert.AreEqual(sizeof(int), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 32-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt32Flip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt32();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading a 32-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt32FlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadInt32();
                    Assert.AreEqual(sizeof(int), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 64-bit integer can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt64NoFlip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt64();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading a 64-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt64NoFlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadInt64();
                    Assert.AreEqual(sizeof(long), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 64-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadInt64Flip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadInt64();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading a 64-bit integer.
        /// </summary>
        [Test]
        public void TestReadInt64FlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadInt64();
                    Assert.AreEqual(sizeof(long), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 16-bit integer can be read when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt16NoFlip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt16();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after reading an unsigned 16-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt16NoFlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt16();
                    Assert.AreEqual(sizeof(ushort), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 16-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt16Flip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt16();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after reading an unsigned 16-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt16FlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt16();
                    Assert.AreEqual(sizeof(ushort), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt32NoFlip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt32();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading an unsigned 32-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt32NoFlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt32();
                    Assert.AreEqual(sizeof(uint), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt32Flip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt32();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after reading an unsigned 32-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt32FlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt32();
                    Assert.AreEqual(sizeof(uint), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt64NoFlip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt64();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading an unsigned 64-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt64NoFlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt64();
                    Assert.AreEqual(sizeof(ulong), input.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestReadUInt64Flip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    var actual = reader.ReadUInt64();
                    Assert.AreEqual(value, actual);
                }
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after reading an unsigned 64-bit integer.
        /// </summary>
        [Test]
        public void TestReadUInt64FlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            {
                using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                {
                    reader.ReadUInt64();
                    Assert.AreEqual(sizeof(ulong), input.Position);
                }
            }
        }
    }
}
