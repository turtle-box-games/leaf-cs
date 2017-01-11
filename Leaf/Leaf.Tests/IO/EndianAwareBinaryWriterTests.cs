using System;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;

namespace Leaf.Tests.IO
{
    [TestFixture]
    public class EndianAwareBinaryWriterTests
    {
        private static void AssertBytesMatch(byte[] expected, byte[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; ++i)
                Assert.AreEqual(expected[i], actual[i]);
        }

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
                new EndianAwareBinaryWriter(null, true);
            });
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStream()
        {
            using (var output = new MemoryStream())
            {
                output.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true);
                });
            }
        }

        /// <summary>
        /// Verify that the constructor throws a <see cref="ArgumentException"/> when the stream can't be written to.
        /// </summary>
        [Test]
        public void TestNonReadableStream()
        {
            using (var output = new MemoryStream(new byte[0], false))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true);
                });
            }
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentNullException"/> when the stream is null.
        /// </summary>
        [Test]
        public void TestNullStreamEncoding()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EndianAwareBinaryWriter(null, true, Encoding.UTF8);
            });
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStreamEncoding()
        {
            using (var output = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                output.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, Encoding.UTF8);
                });
            }
        }

        /// <summary>
        /// Verify that the "encoding" constructor throws a <see cref="ArgumentException"/> when the stream can't be written to.
        /// </summary>
        [Test]
        public void TestNonReadableStreamEncoding()
        {
            using (var output = new MemoryStream(new byte[0], false))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, Encoding.UTF8);
                });
            }
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentNullException"/> when the stream is null.
        /// </summary>
        [Test]
        public void TestNullStreamLeaveOpen()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EndianAwareBinaryWriter(null, true, Encoding.UTF8, true);
            });
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentException"/> when the stream is closed.
        /// </summary>
        [Test]
        public void TestClosedStreamLeaveOpen()
        {
            using (var output = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                output.Close();
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, Encoding.UTF8, true);
                });
            }
        }

        /// <summary>
        /// Verify that the "leave open" constructor throws a <see cref="ArgumentException"/> when the stream can't be written to.
        /// </summary>
        [Test]
        public void TestNonReadableStreamLeaveOpen()
        {
            using (var output = new MemoryStream(new byte[0], false))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, Encoding.UTF8, true);
                });
            }
        }

        /// <summary>
        /// Verifies that the "encoding" constructor throws a <see cref="ArgumentNullException"/> when the encoding is null.
        /// </summary>
        [Test]
        public void TestNullEncoding()
        {
            using (var output = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, null);
                });
            }
        }

        /// <summary>
        /// Verifies that the "leave open" constructor throws a <see cref="ArgumentNullException"/> when the encoding is null.
        /// </summary>
        [Test]
        public void TestNullEncodingLeaveOpen()
        {
            using (var output = new MemoryStream())
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    new EndianAwareBinaryWriter(output, true, null, true);
                });
            }
        }

        /// <summary>
        /// Check that a decimal value can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteDecimalNoFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                AssertBytesMatch(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 16 bytes after writing a decimal.
        /// </summary>
        [Test]
        public void TestWriteDecimalNoFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(decimal), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a decimal value can be written when the stream is in the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteDecimalFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 16 bytes after writing a decimal.
        /// </summary>
        [Test]
        public void TestWriteDecimalFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(decimal), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a single-precision floating-point value can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteSingleNoFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing a single-precision floating-point value.
        /// </summary>
        [Test]
        public void TestWriteSingleNoFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(float), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a single-precision floating-point value can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteSingleFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing a single-precision floating-point value.
        /// </summary>
        [Test]
        public void TestWriteSingleFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(float), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a double-precision floating-point value can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteDoubleNoFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing a double-precision floating-point value.
        /// </summary>
        [Test]
        public void TestWriteDoubleNoFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(double), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a double-precision floating-point value can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteDoubleFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing a double-precision floating-point value.
        /// </summary>
        [Test]
        public void TestWriteDoubleFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(double), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 16-bit integer can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt16NoFlip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after writing a 16-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt16NoFlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(short), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 16-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt16Flip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after writing a 16-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt16FlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(short), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 32-bit integer can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt32NoFlip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing a 32-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt32NoFlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(int), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 32-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt32Flip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing a 32-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt32FlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(int), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 64-bit integer can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt64NoFlip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing a 64-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt64NoFlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(long), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that a 64-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteInt64Flip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing a 64-bit integer.
        /// </summary>
        [Test]
        public void TestWriteInt64FlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(long), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 16-bit integer can be written when the stream is the same endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt16NoFlip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after writing an unsigned 16-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt16NoFlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(ushort), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 16-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt16Flip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 2 bytes after writing an unsigned 16-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt16FlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(ushort), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt32NoFlip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing an unsigned 32-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt32NoFlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(uint), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt32Flip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 4 bytes after writing an unsigned 32-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt32FlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(uint), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt64NoFlip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing an unsigned 64-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt64NoFlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(ulong), output.Position);
                }
            }
        }

        /// <summary>
        /// Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.
        /// </summary>
        [Test]
        public void TestWriteUInt64Flip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.AreEqual(bytes, output.ToArray());
            }
        }

        /// <summary>
        /// Check that the stream is advanced 8 bytes after writing an unsigned 64-bit integer.
        /// </summary>
        [Test]
        public void TestWriteUInt64FlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                {
                    writer.Write(value);
                    Assert.AreEqual(sizeof(ulong), output.Position);
                }
            }
        }
    }
}
