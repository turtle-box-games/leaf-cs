using System;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;

namespace Leaf.Tests.IO
{
    [TestFixture(TestOf = typeof(EndianAwareBinaryReader))]
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

        [Test(Description = "Verify that the constructor throws an exception when the stream is null.")]
        public void TestNullStream()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream is closed.")]
        public void TestClosedStream()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.That(() => { new EndianAwareBinaryReader(input, true); }, Throws.ArgumentException);
            }
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream can't be read from.")]
        public void TestNonReadableStream()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
                Assert.That(() => { new EndianAwareBinaryReader(input, true); }, Throws.ArgumentException);
            File.Delete(filename);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is null.")]
        public void TestNullStreamEncoding()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true, Encoding.UTF8); },
                Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is closed.")]
        public void TestClosedStreamEncoding()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.That(() => { new EndianAwareBinaryReader(input, true, Encoding.UTF8); },
                    Throws.ArgumentException);
            }
        }

        [Test(Description =
            "Verify that the \"encoding\" constructor throws an exception when the stream can't be read from.")]
        public void TestNonReadableStreamEncoding()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
                Assert.That(() => { new EndianAwareBinaryReader(input, true, Encoding.UTF8); },
                    Throws.ArgumentException);
            File.Delete(filename);
        }

        [Test(Description = "Verify that the \"leave open\" constructor throws an exception when the stream is null.")]
        public void TestNullStreamLeaveOpen()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true, Encoding.UTF8, true); },
                Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verify that the \"leave open\" constructor throws an exception when the stream is closed.")]
        public void TestClosedStreamLeaveOpen()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.That(() => { new EndianAwareBinaryReader(input, true, Encoding.UTF8, true); },
                    Throws.ArgumentException);
            }
        }

        [Test(Description =
            "Verify that the \"leave open\" constructor throws an exception when the stream can't be read from.")]
        public void TestNonReadableStreamLeaveOpen()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
                Assert.That(() => { new EndianAwareBinaryReader(input, true, Encoding.UTF8, true); },
                    Throws.ArgumentException);
            File.Delete(filename);
        }

        [Test(Description =
            "Verifies that the \"encoding\" constructor throws an exception when the encoding is null.")]
        public void TestNullEncoding()
        {
            using (var input = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryReader(input, true, null); }, Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verifies that the \"leave open\" constructor throws an exception when the encoding is null.")]
        public void TestNullEncodingLeaveOpen()
        {
            using (var input = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryReader(input, true, null, true); },
                    Throws.ArgumentNullException);
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is the same endian as the system's.")]
        public void TestReadDecimalNoFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 16 bytes after reading a decimal.")]
        public void TestReadDecimalNoFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadDecimal();
                Assert.That(input.Position, Is.EqualTo(sizeof(decimal)));
            }
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is in the opposite endian as the system's.")]
        public void TestReadDecimalFlip()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 16 bytes after reading a decimal.")]
        public void TestReadDecimalFlipPosition()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadDecimal();
                Assert.That(input.Position, Is.EqualTo(sizeof(decimal)));
            }
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the same endian as the system's.")]
        public void TestReadSingleNoFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that the stream is advanced 4 bytes after reading a single-precision floating-point value.")]
        public void TestReadSingleNoFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadSingle();
                Assert.That(input.Position, Is.EqualTo(sizeof(float)));
            }
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        public void TestReadSingleFlip()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that the stream is advanced 4 bytes after reading a single-precision floating-point value.")]
        public void TestReadSingleFlipPosition()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadSingle();
                Assert.That(input.Position, Is.EqualTo(sizeof(float)));
            }
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the same endian as the system's.")]
        public void TestReadDoubleNoFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that the stream is advanced 8 bytes after reading a double-precision floating-point value.")]
        public void TestReadDoubleNoFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadDouble();
                Assert.That(input.Position, Is.EqualTo(sizeof(double)));
            }
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        public void TestReadDoubleFlip()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that the stream is advanced 8 bytes after reading a double-precision floating-point value.")]
        public void TestReadDoubleFlipPosition()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadDouble();
                Assert.That(input.Position, Is.EqualTo(sizeof(double)));
            }
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the same endian as the system's.")]
        public void TestReadInt16NoFlip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after reading a 16-bit integer.")]
        public void TestReadInt16NoFlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadInt16();
                Assert.That(input.Position, Is.EqualTo(sizeof(short)));
            }
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadInt16Flip()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after reading a 16-bit integer.")]
        public void TestReadInt16FlipPosition()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadInt16();
                Assert.That(input.Position, Is.EqualTo(sizeof(short)));
            }
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the same endian as the system's.")]
        public void TestReadInt32NoFlip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after reading a 32-bit integer.")]
        public void TestReadInt32NoFlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadInt32();
                Assert.That(input.Position, Is.EqualTo(sizeof(int)));
            }
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadInt32Flip()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after reading a 32-bit integer.")]
        public void TestReadInt32FlipPosition()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadInt32();
                Assert.That(input.Position, Is.EqualTo(sizeof(int)));
            }
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the same endian as the system's.")]
        public void TestReadInt64NoFlip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after reading a 64-bit integer.")]
        public void TestReadInt64NoFlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadInt64();
                Assert.That(input.Position, Is.EqualTo(sizeof(long)));
            }
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadInt64Flip()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after reading a 64-bit integer.")]
        public void TestReadInt64FlipPosition()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadInt64();
                Assert.That(input.Position, Is.EqualTo(sizeof(long)));
            }
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the same endian as the system's.")]
        public void TestReadUInt16NoFlip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after reading an unsigned 16-bit integer.")]
        public void TestReadUInt16NoFlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadUInt16();
                Assert.That(input.Position, Is.EqualTo(sizeof(ushort)));
            }
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadUInt16Flip()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after reading an unsigned 16-bit integer.")]
        public void TestReadUInt16FlipPosition()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadUInt16();
                Assert.That(input.Position, Is.EqualTo(sizeof(ushort)));
            }
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadUInt32NoFlip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt32(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after reading an unsigned 32-bit integer.")]
        public void TestReadUInt32NoFlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadUInt32();
                Assert.That(input.Position, Is.EqualTo(sizeof(uint)));
            }
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadUInt32Flip()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt32(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after reading an unsigned 32-bit integer.")]
        public void TestReadUInt32FlipPosition()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadUInt32();
                Assert.That(input.Position, Is.EqualTo(sizeof(uint)));
            }
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadUInt64NoFlip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt64(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after reading an unsigned 64-bit integer.")]
        public void TestReadUInt64NoFlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
            {
                reader.ReadUInt64();
                Assert.That(input.Position, Is.EqualTo(sizeof(ulong)));
            }
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void TestReadUInt64Flip()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt64(), Is.EqualTo(value));
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after reading an unsigned 64-bit integer.")]
        public void TestReadUInt64FlipPosition()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
            {
                reader.ReadUInt64();
                Assert.That(input.Position, Is.EqualTo(sizeof(ulong)));
            }
        }
    }
}