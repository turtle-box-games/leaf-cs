using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Leaf.IO;
using NUnit.Framework;
using static System.Linq.Enumerable;

namespace Leaf.Tests.IO
{
    [TestFixture(TestOf = typeof(EndianAwareBinaryWriter))]
    public class EndianAwareBinaryWriterTests
    {
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
        [TestCaseSource(nameof(DecimalTestCases))]
        public byte[] WriteDecimalNoFlipTest(decimal value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a decimal value can be written when the stream is in the opposite endian as the system's.")]
        [TestCaseSource(nameof(DecimalFlipTestCases))]
        public byte[] WriteDecimalFlipTest(decimal value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(SingleTestCases))]
        public byte[] WriteSingleNoFlipTest(float value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a single-precision floating-point value can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(SingleFlipTestCases))]
        public byte[] WriteSingleFlipTest(float value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(DoubleTestCases))]
        public byte[] WriteDoubleNoFlipTest(double value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a double-precision floating-point value can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(DoubleFlipTestCases))]
        public byte[] WriteDoubleFlipTest(double value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that a 16-bit integer can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int16TestCases))]
        public byte[] WriteInt16NoFlipTest(short value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a 16-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int16FlipTestCases))]
        public byte[] WriteInt16FlipTest(short value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that a 32-bit integer can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int32TestCases))]
        public byte[] WriteInt32NoFlipTest(int value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int32FlipTestCases))]
        public byte[] WriteInt32FlipTest(int value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that a 64-bit integer can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(Int64TestCases))]
        public byte[] WriteInt64NoFlipTest(long value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that a 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(Int64FlipTestCases))]
        public byte[] WriteInt64FlipTest(long value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be written when the stream is the same endian as the system's.")]
        [TestCaseSource(nameof(UInt16TestCases))]
        public byte[] WriteUInt16NoFlipTest(ushort value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that an unsigned 16-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt16FlipTestCases))]
        public byte[] WriteUInt16FlipTest(ushort value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32TestCases))]
        public byte[] WriteUInt32NoFlipTest(uint value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that an unsigned 32-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt32FlipTestCases))]
        public byte[] WriteUInt32FlipTest(uint value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64TestCases))]
        public byte[] WriteUInt64NoFlipTest(ulong value)
        {
            return UseWriter(writer => writer.Write(value));
        }

        [Test(Description =
            "Check that an unsigned 64-bit integer can be written when the stream is the opposite endian as the system's.")]
        [TestCaseSource(nameof(UInt64FlipTestCases))]
        public byte[] WriteUInt64FlipTest(ulong value)
        {
            return UseWriter(writer => writer.Write(value), true);
        }

        private static byte[] UseWriter(Action<EndianAwareBinaryWriter> action, bool flip = false)
        {
            var bigEndian = BitConverter.IsLittleEndian ? flip : !flip;
            using (var output = new MemoryStream())
            using (var writer = new EndianAwareBinaryWriter(output, bigEndian))
            {
                action(writer);
                return output.ToArray();
            }
        }

        private static readonly decimal[] SpecialDecimalValues =
        {
            decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue
        };

        private static IEnumerable<TestCaseData> DecimalTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDecimal())
                .Union(SpecialDecimalValues);
            foreach (var value in values)
            {
                var bytes = GetDecimalBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> DecimalFlipTestCases()
        {
            return FlipTestCaseBytes(DecimalTestCases());
        }

        private static readonly float[] SpecialSingleValues =
        {
            0f, float.MinValue, float.MaxValue, float.Epsilon,
            float.NaN, float.NegativeInfinity, float.PositiveInfinity
        };

        private static IEnumerable<TestCaseData> SingleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextFloat())
                .Union(SpecialSingleValues);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> SingleFlipTestCases()
        {
            return FlipTestCaseBytes(SingleTestCases());
        }

        private static readonly double[] SpecialDoubleValues =
        {
            0d, double.MinValue, double.MaxValue, double.Epsilon,
            double.NaN, double.NegativeInfinity, double.PositiveInfinity
        };

        private static IEnumerable<TestCaseData> DoubleTestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextDouble())
                .Union(SpecialDoubleValues);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> DoubleFlipTestCases()
        {
            return FlipTestCaseBytes(DoubleTestCases());
        }
        
        private static readonly short[] SpecialInt16Values = {0, short.MinValue, short.MaxValue};

        private static IEnumerable<TestCaseData> Int16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextShort())
                .Union(SpecialInt16Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> Int16FlipTestCases()
        {
            return FlipTestCaseBytes(Int16TestCases());
        }
        
        private static readonly int[] SpecialInt32Values = {0, int.MinValue, int.MaxValue};

        private static IEnumerable<TestCaseData> Int32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.Next())
                .Union(SpecialInt32Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> Int32FlipTestCases()
        {
            return FlipTestCaseBytes(Int32TestCases());
        }
        
        private static readonly long[] SpecialInt64Values = {0, long.MinValue, long.MaxValue};

        private static IEnumerable<TestCaseData> Int64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextLong())
                .Union(SpecialInt64Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> Int64FlipTestCases()
        {
            return FlipTestCaseBytes(Int64TestCases());
        }
        
        private static readonly ushort[] SpecialUInt16Values = {0, ushort.MinValue, ushort.MaxValue};

        private static IEnumerable<TestCaseData> UInt16TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUShort())
                .Union(SpecialUInt16Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> UInt16FlipTestCases()
        {
            return FlipTestCaseBytes(UInt16TestCases());
        }
        
        private static readonly uint[] SpecialUInt32Values = {0, uint.MinValue, uint.MaxValue};

        private static IEnumerable<TestCaseData> UInt32TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextUInt())
                .Union(SpecialUInt32Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
            }
        }

        private static IEnumerable<TestCaseData> UInt32FlipTestCases()
        {
            return FlipTestCaseBytes(UInt32TestCases());
        }
        
        private static readonly ulong[] SpecialUInt64Values = {0, ulong.MinValue, ulong.MaxValue};

        private static IEnumerable<TestCaseData> UInt64TestCases()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var values = Range(0, Constants.RandomTestCount)
                .Select(_ => randomizer.NextULong())
                .Union(SpecialUInt64Values);
            foreach (var value in values)
            {
                var bytes = BitConverter.GetBytes(value);
                yield return new TestCaseData(value).Returns(bytes);
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
                FlipByteArray((byte[])caseData.ExpectedResult);
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