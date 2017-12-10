using System;
using System.Collections;
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
        [TestCaseSource(nameof(DecimalTestCases))]
        public void ReadDecimalNoFlipTest(decimal value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a decimal value can be read when the stream is in the opposite endian as the system's.")]
        [TestCaseSource(nameof(DecimalFlipTestCases))]
        public void ReadDecimalFlipTest(decimal value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadDecimal(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(SingleTestCases))]
        public void ReadSingleNoFlipTest(float value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(SingleFlipTestCases))]
        public void ReadSingleFlipTest(float value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadSingle(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(DoubleTestCases))]
        public void ReadDoubleNoFlipTest(double value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(DoubleFlipTestCases))]
        public void ReadDoubleFlipTest(double value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadDouble(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int16TestCases))]
        public void ReadInt16NoFlipTest(short value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int16FlipTestCases))]
        public void ReadInt16FlipTest(short value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int32TestCases))]
        public void ReadInt32NoFlipTest(int value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int32FlipTestCases))]
        public void ReadInt32FlipTest(int value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int64TestCases))]
        public void ReadInt64NoFlipTest(long value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that a 64-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int64FlipTestCases))]
        public void ReadInt64FlipTest(long value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadInt64(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(UInt16TestCases))]
        public void ReadUInt16NoFlipTest(ushort value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt16FlipTestCases))]
        public void ReadUInt16FlipTest(ushort value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
                Assert.That(reader.ReadUInt16(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32TestCases))]
        public void ReadUInt32NoFlipTest(uint value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes))
                Assert.That(reader.ReadUInt32(), Is.EqualTo(value));
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be read when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32FlipTestCases))]
        public void ReadUInt32FlipTest(uint value, byte[] bytes)
        {
            using (var reader = PrepareReader(bytes, true))
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

        private static readonly decimal[] SpecialDecimalValues =
        {
            decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue
        };

        private static IEnumerable DecimalTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDecimal())
                .Union(SpecialDecimalValues);
            foreach (var value in values)
            {
                var bytes = GetDecimalBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable DecimalFlipTestCases()
        {
            return FlipTestCaseBytes(DecimalTestCases());
        }

        private static readonly float[] SpecialSingleValues =
        {
            0f, float.MinValue, float.MaxValue, float.Epsilon,
            float.NaN, float.NegativeInfinity, float.PositiveInfinity
        };

        private static IEnumerable SingleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextFloat())
                .Union(SpecialSingleValues);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable SingleFlipTestCases()
        {
            return FlipTestCaseBytes(SingleTestCases());
        }

        private static readonly double[] SpecialDoubleValues =
        {
            0d, double.MinValue, double.MaxValue, double.Epsilon,
            double.NaN, double.NegativeInfinity, double.PositiveInfinity
        };

        private static IEnumerable DoubleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDouble())
                .Union(SpecialDoubleValues);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable DoubleFlipTestCases()
        {
            return FlipTestCaseBytes(DoubleTestCases());
        }
        
        private static readonly short[] SpecialInt16Values = {0, short.MinValue, short.MaxValue};

        private static IEnumerable Int16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextShort())
                .Union(SpecialInt16Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable Int16FlipTestCases()
        {
            return FlipTestCaseBytes(Int16TestCases());
        }
        
        private static readonly int[] SpecialInt32Values = {0, int.MinValue, int.MaxValue};

        private static IEnumerable Int32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.Next())
                .Union(SpecialInt32Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable Int32FlipTestCases()
        {
            return FlipTestCaseBytes(Int32TestCases());
        }
        
        private static readonly long[] SpecialInt64Values = {0, long.MinValue, long.MaxValue};

        private static IEnumerable Int64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextLong())
                .Union(SpecialInt64Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable Int64FlipTestCases()
        {
            return FlipTestCaseBytes(Int64TestCases());
        }
        
        private static readonly ushort[] SpecialUInt16Values = {0, ushort.MinValue, ushort.MaxValue};

        private static IEnumerable UInt16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUShort())
                .Union(SpecialUInt16Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable UInt16FlipTestCases()
        {
            return FlipTestCaseBytes(UInt16TestCases());
        }
        
        private static readonly uint[] SpecialUInt32Values = {0, uint.MinValue, uint.MaxValue};

        private static IEnumerable UInt32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUInt())
                .Union(SpecialUInt32Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new object[] {value, bytes};
            }
        }

        private static IEnumerable UInt32FlipTestCases()
        {
            return FlipTestCaseBytes(UInt32TestCases());
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
            return FlipTestCaseBytes(UInt64TestCases());
        }

        private static IEnumerable FlipTestCaseBytes(IEnumerable sets)
        {
            foreach (object[] args in sets)
            {
                FlipByteArray((byte[])args[1]);
                yield return args;
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