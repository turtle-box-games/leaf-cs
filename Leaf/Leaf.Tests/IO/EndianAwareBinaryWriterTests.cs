using System;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;

namespace Leaf.Tests.IO
{
    [TestFixture(TestOf = typeof(EndianAwareBinaryWriter))]
    public class EndianAwareBinaryWriterTests
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
        public void NullStreamTest()
        {
            Assert.That(() => { new EndianAwareBinaryWriter(null, true); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream is closed.")]
        public void ClosedStreamTest()
        {
            using (var output = new MemoryStream())
            {
                output.Close();
                Assert.That(() => { new EndianAwareBinaryWriter(output, true); }, Throws.ArgumentException);
            }
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream can't be written to.")]
        public void NonReadableStreamTest()
        {
            using (var output = new MemoryStream(new byte[0], false))
                Assert.That(() => { new EndianAwareBinaryWriter(output, true); }, Throws.ArgumentException);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is null.")]
        public void NullStreamEncodingTest()
        {
            Assert.That(() => { new EndianAwareBinaryWriter(null, true, Encoding.UTF8); },
                Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is closed.")]
        public void ClosedStreamEncodingTest()
        {
            using (var output = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                output.Close();
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, Encoding.UTF8); },
                    Throws.ArgumentException);
            }
        }

        [Test(Description =
            "Verify that the \"encoding\" constructor throws an exception when the stream can't be written to.")]
        public void NonReadableStreamEncodingTest()
        {
            using (var output = new MemoryStream(new byte[0], false))
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, Encoding.UTF8); },
                    Throws.ArgumentException);
        }

        [Test(Description = "Verify that the \"leave open\" constructor throws an exception when the stream is null.")]
        public void NullStreamLeaveOpenTest()
        {
            Assert.That(() => { new EndianAwareBinaryWriter(null, true, Encoding.UTF8, true); },
                Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verify that the \"leave open\" constructor throws an exception when the stream is closed.")]
        public void ClosedStreamLeaveOpenTest()
        {
            using (var output = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                output.Close();
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, Encoding.UTF8, true); },
                    Throws.ArgumentException);
            }
        }

        [Test(Description =
            "Verify that the \"leave open\" constructor throws an exception when the stream can't be written to.")]
        public void NonReadableStreamLeaveOpenTest()
        {
            using (var output = new MemoryStream(new byte[0], false))
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, Encoding.UTF8, true); },
                    Throws.ArgumentException);
        }

        [Test(Description =
            "Verifies that the \"encoding\" constructor throws an exception when the encoding is null.")]
        public void NullEncodingTest()
        {
            using (var output = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, null); }, Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verifies that the \"leave open\" constructor throws an exception when the encoding is null.")]
        public void NullEncodingLeaveOpenTest()
        {
            using (var output = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryWriter(output, true, null, true); },
                    Throws.ArgumentNullException);
        }

        [Test(Description =
            "Check that a decimal value can be written when the stream is the same endian as the system's.")]
        public void WriteDecimalNoFlipTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 16 bytes after writing a decimal.")]
        public void WriteDecimalNoFlipPositionTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(decimal)));
            }
        }

        [Test(Description =
            "Check that a decimal value can be written when the stream is in the opposite endian as the system's.")]
        public void WriteDecimalFlipTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 16 bytes after writing a decimal.")]
        public void WriteDecimalFlipPositionTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(decimal)));
            }
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be written when the stream is the same endian as the system's.")]
        public void WriteSingleNoFlipTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description =
            "Check that the stream is advanced 4 bytes after writing a single-precision floating-point value.")]
        public void WriteSingleNoFlipPositionTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(float)));
            }
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be written when the stream is the opposite endian as the system's.")]
        public void WriteSingleFlipTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description =
            "Check that the stream is advanced 4 bytes after writing a single-precision floating-point value.")]
        public void WriteSingleFlipPositionTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(float)));
            }
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be written when the stream is the same endian as the system's.")]
        public void WriteDoubleNoFlipTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description =
            "Check that the stream is advanced 8 bytes after writing a double-precision floating-point value.")]
        public void WriteDoubleNoFlipPositionTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(double)));
            }
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be written when the stream is the opposite endian as the system's.")]
        public void WriteDoubleFlipTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description =
            "Check that the stream is advanced 8 bytes after writing a double-precision floating-point value.")]
        public void WriteDoubleFlipPositionTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(double)));
            }
        }

        [Test(Description =
            "Check that a 16-bit integer can be written when the stream is the same endian as the system's.")]
        public void WriteInt16NoFlipTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after writing a 16-bit integer.")]
        public void WriteInt16NoFlipPositionTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(short)));
            }
        }

        [Test(Description =
            "Check that a 16-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteInt16FlipTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after writing a 16-bit integer.")]
        public void WriteInt16FlipPositionTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(short)));
            }
        }

        [Test(Description =
            "Check that a 32-bit integer can be written when the stream is the same endian as the system's.")]
        public void WriteInt32NoFlipTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after writing a 32-bit integer.")]
        public void WriteInt32NoFlipPositionTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(int)));
            }
        }

        [Test(Description =
            "Check that a 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteInt32FlipTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after writing a 32-bit integer.")]
        public void WriteInt32FlipPositionTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(int)));
            }
        }

        [Test(Description =
            "Check that a 64-bit integer can be written when the stream is the same endian as the system's.")]
        public void WriteInt64NoFlipTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after writing a 64-bit integer.")]
        public void WriteInt64NoFlipPositionTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(long)));
            }
        }

        [Test(Description =
            "Check that a 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteInt64FlipTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after writing a 64-bit integer.")]
        public void WriteInt64FlipPositionTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(long)));
            }
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be written when the stream is the same endian as the system's.")]
        public void WriteUInt16NoFlipTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after writing an unsigned 16-bit integer.")]
        public void WriteUInt16NoFlipPositionTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(ushort)));
            }
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteUInt16FlipTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 2 bytes after writing an unsigned 16-bit integer.")]
        public void WriteUInt16FlipPositionTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(ushort)));
            }
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteUInt32NoFlipTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after writing an unsigned 32-bit integer.")]
        public void WriteUInt32NoFlipPositionTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(uint)));
            }
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteUInt32FlipTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 4 bytes after writing an unsigned 32-bit integer.")]
        public void WriteUInt32FlipPositionTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(uint)));
            }
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteUInt64NoFlipTest()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after writing an unsigned 64-bit integer.")]
        public void WriteUInt64NoFlipPositionTest()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, !BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(ulong)));
            }
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        public void WriteUInt64FlipTest()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            {
                using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
                    writer.Write(value);
                Assert.That(output.ToArray(), Is.EqualTo(bytes));
            }
        }

        [Test(Description = "Check that the stream is advanced 8 bytes after writing an unsigned 64-bit integer.")]
        public void WriteUInt64FlipPositionTest()
        {
            const ulong value = 12345678901234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var output = new MemoryStream(bytes))
            using (var writer = new EndianAwareBinaryWriter(output, BitConverter.IsLittleEndian))
            {
                writer.Write(value);
                Assert.That(output.Position, Is.EqualTo(sizeof(ulong)));
            }
        }
    }
}