using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;
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
        [TestCaseSource(nameof(DecimalTestCases))]
        public decimal ReadDecimalNoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadDecimal();
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is in the opposite endian as the system's.")]
        [TestCaseSource(nameof(DecimalFlipTestCases))]
        public decimal ReadDecimalFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadDecimal();
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(SingleTestCases))]
        public float ReadSingleNoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadSingle();
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(SingleFlipTestCases))]
        public float ReadSingleFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadSingle();
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(DoubleTestCases))]
        public double ReadDoubleNoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadDouble();
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(DoubleFlipTestCases))]
        public double ReadDoubleFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadDouble();
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int16TestCases))]
        public short ReadInt16NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadInt16();
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int16FlipTestCases))]
        public short ReadInt16FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadInt16();
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int32TestCases))]
        public int ReadInt32NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadInt32();
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int32FlipTestCases))]
        public int ReadInt32FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadInt32();
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int64TestCases))]
        public long ReadInt64NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadInt64();
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int64FlipTestCases))]
        public long ReadInt64FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadInt64();
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(UInt16TestCases))]
        public ushort ReadUInt16NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadUInt16();
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt16FlipTestCases))]
        public ushort ReadUInt16FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadUInt16();
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32TestCases))]
        public uint ReadUInt32NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadUInt32();
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32FlipTestCases))]
        public uint ReadUInt32FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadUInt32();
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64TestCases))]
        public ulong ReadUInt64NoFlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                return reader.ReadUInt64();
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64FlipTestCases))]
        public ulong ReadUInt64FlipTest(byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                return reader.ReadUInt64();
        }

        private static EndianAwareBinaryReader PrepareReader(byte[] bytes, bool flip = false)
        {
            var bigEndian = BitConverter.IsLittleEndian ? flip : !flip;
            var input     = new MemoryStream(bytes, false);
            return new EndianAwareBinaryReader(input, bigEndian);
        }

        private static IEnumerable<TestCaseData> DecimalTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDecimal())
                .Union(new[] {
                    decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue
                });
            foreach (var value in values)
            {
                var bytes = GetDecimalBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> DecimalFlipTestCases()
        {
            return FlipTestCaseBytes(DecimalTestCases());
        }

        private static IEnumerable<TestCaseData> SingleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextFloat())
                .Union(new[] {
                    0f, float.MinValue, float.MaxValue, float.Epsilon,
                    float.NaN, float.NegativeInfinity, float.PositiveInfinity
                });
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> SingleFlipTestCases()
        {
            return FlipTestCaseBytes(SingleTestCases());
        }

        private static IEnumerable<TestCaseData> DoubleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDouble())
                .Union(new[] {
                    0d, double.MinValue, double.MaxValue, double.Epsilon,
                    double.NaN, double.NegativeInfinity, double.PositiveInfinity
                });
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> DoubleFlipTestCases()
        {
            return FlipTestCaseBytes(DoubleTestCases());
        }

        private static IEnumerable<TestCaseData> Int16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextShort())
                .Union(new short[] {0, short.MinValue, short.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> Int16FlipTestCases()
        {
            return FlipTestCaseBytes(Int16TestCases());
        }

        private static IEnumerable<TestCaseData> Int32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.Next())
                .Union(new[] {0, int.MinValue, int.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> Int32FlipTestCases()
        {
            return FlipTestCaseBytes(Int32TestCases());
        }

        private static IEnumerable<TestCaseData> Int64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextLong())
                .Union(new[] {0, long.MinValue, long.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> Int64FlipTestCases()
        {
            return FlipTestCaseBytes(Int64TestCases());
        }

        private static IEnumerable<TestCaseData> UInt16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUShort())
                .Union(new ushort[] {0, ushort.MinValue, ushort.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> UInt16FlipTestCases()
        {
            return FlipTestCaseBytes(UInt16TestCases());
        }

        private static IEnumerable<TestCaseData> UInt32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUInt())
                .Union(new uint[] {0, uint.MinValue, uint.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> UInt32FlipTestCases()
        {
            return FlipTestCaseBytes(UInt32TestCases());
        }

        private static IEnumerable<TestCaseData> UInt64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextULong())
                .Union(new ulong[] {0, ulong.MinValue, ulong.MaxValue});
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(bytes).Returns(value);
            }
        }

        private static IEnumerable<TestCaseData> UInt64FlipTestCases()
        {
            return FlipTestCaseBytes(UInt64TestCases());
        }

        private static IEnumerable<TestCaseData> FlipTestCaseBytes(IEnumerable<TestCaseData> sets)
        {
            foreach (var caseData in sets)
            {
                FlipByteArray((byte[])caseData.Arguments[0]);
                yield return caseData;
            }
        }
        
        private static byte[] GetDecimalBytes(decimal value)
        {
            var ints  = decimal.GetBits(value);
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
                var b    = bytes[i];
                bytes[i] = bytes[j];
                bytes[j] = b;
            }
        }
    }
}