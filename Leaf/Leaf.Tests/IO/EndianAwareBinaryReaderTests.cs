using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;
using System.Linq;
using static System.Linq.Enumerable;

namespace Leaf.Tests.IO
{
    [TestFixture(TestOf = typeof(EndianAwareBinaryReader))]
    public class EndianAwareBinaryReaderTests
    {
        [Test(Description = "Verify that the constructor throws an exception when the stream is null.")]
        public void NullStreamTest()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream is closed.")]
        public void ClosedStreamTest()
        {
            using (var input = new MemoryStream(new byte[] {0, 1, 2, 3}))
            {
                input.Close();
                Assert.That(() => { new EndianAwareBinaryReader(input, true); }, Throws.ArgumentException);
            }
        }

        [Test(Description = "Verify that the constructor throws an exception when the stream can't be read from.")]
        public void NonReadableStreamTest()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
                Assert.That(() => { new EndianAwareBinaryReader(input, true); }, Throws.ArgumentException);
            File.Delete(filename);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is null.")]
        public void NullStreamEncodingTest()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true, Encoding.UTF8); },
                Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the \"encoding\" constructor throws an exception when the stream is closed.")]
        public void ClosedStreamEncodingTest()
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
        public void NonReadableStreamEncodingTest()
        {
            // Create temp file with append mode - this gives the test a stream with no read capability.
            var filename = Path.GetTempFileName();
            using (var input = File.Open(filename, FileMode.Append))
                Assert.That(() => { new EndianAwareBinaryReader(input, true, Encoding.UTF8); },
                    Throws.ArgumentException);
            File.Delete(filename);
        }

        [Test(Description = "Verify that the \"leave open\" constructor throws an exception when the stream is null.")]
        public void NullStreamLeaveOpenTest()
        {
            Assert.That(() => { new EndianAwareBinaryReader(null, true, Encoding.UTF8, true); },
                Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verify that the \"leave open\" constructor throws an exception when the stream is closed.")]
        public void ClosedStreamLeaveOpenTest()
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
        public void NonReadableStreamLeaveOpenTest()
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
        public void NullEncodingTest()
        {
            using (var input = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryReader(input, true, null); }, Throws.ArgumentNullException);
        }

        [Test(Description =
            "Verifies that the \"leave open\" constructor throws an exception when the encoding is null.")]
        public void NullEncodingLeaveOpenTest()
        {
            using (var input = new MemoryStream())
                Assert.That(() => { new EndianAwareBinaryReader(input, true, null, true); },
                    Throws.ArgumentNullException);
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is the same endian as the system's.")]
        public void ReadDecimalNoFlipTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is in the opposite endian as the system's.")]
        public void ReadDecimalFlipTest()
        {
            const decimal value = 1234567890m;
            var bytes = GetDecimalBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the same endian as the system's.")]
        public void ReadSingleNoFlipTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        public void ReadSingleFlipTest()
        {
            const float value = 123456.789f;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the same endian as the system's.")]
        public void ReadDoubleNoFlipTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        public void ReadDoubleFlipTest()
        {
            const double value = 123456.789d;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the same endian as the system's.")]
        public void ReadInt16NoFlipTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadInt16FlipTest()
        {
            const short value = -12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the same endian as the system's.")]
        public void ReadInt32NoFlipTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadInt32FlipTest()
        {
            const int value = -1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the same endian as the system's.")]
        public void ReadInt64NoFlipTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadInt64FlipTest()
        {
            const long value = -1234567890123456789;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the same endian as the system's.")]
        public void ReadUInt16NoFlipTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadUInt16FlipTest()
        {
            const ushort value = 12345;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadUInt32NoFlipTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, !BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        public void ReadUInt32FlipTest()
        {
            const uint value = 1234567890;
            var bytes = BitConverter.GetBytes(value);
            FlipByteArray(bytes);
            using (var input = new MemoryStream(bytes, false))
            using (var reader = new EndianAwareBinaryReader(input, BitConverter.IsLittleEndian))
                Assert.That(reader.ReadUInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64TestCases))]
        public void ReadUInt64NoFlipTest(ulong value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadUInt64(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64FlipTestCases))]
        public void ReadUInt64FlipTest(ulong value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadUInt64(), Is.EqualTo(value));
        }

        private static EndianAwareBinaryReader PrepareReader(byte[] bytes, bool flip = false)
        {
            var bigEndian = BitConverter.IsLittleEndian ? flip : !flip;
            var input     = new MemoryStream(bytes, false);
            return new EndianAwareBinaryReader(input, bigEndian);
        }
        
        private static readonly ulong[] SpecialUInt64Values = {0, ulong.MinValue, ulong.MaxValue};

        private static IEnumerable UInt64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextULong())
                .Union(SpecialUInt64Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable UInt64FlipTestCases()
        {
            foreach (object[] args in UInt64TestCases())
            {
                FlipByteArray((byte[])args[1]);
                yield return args;
            }
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
    }
}