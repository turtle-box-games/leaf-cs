using System;
using System.Collections.Generic;
using System.IO;
using Leaf.IO;
using NUnit.Framework;

namespace Leaf.Tests.IO
{
    [TestFixture(Description = "Test that different types of values can be written and read")]
    public class EndianAwareBinaryReadWriteTests
    {
        [Test(Description = "Check that the same decimal value is read after being written")]
        [TestCaseSource(nameof(DecimalTestValues))]
        public decimal WriteReadDecimalTest(decimal value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadDecimal());
        }

        [Test(Description = "Check that the same 32-bit floating-point value is read after being written")]
        [TestCaseSource(nameof(SingleTestValues))]
        public float WriteReadSingleTest(float value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadSingle());
        }

        [Test(Description = "Check that the same 64-bit floating-point value is read after being written")]
        [TestCaseSource(nameof(DoubleTestValues))]
        public double WriteReadDoubleTest(double value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadDouble());
        }

        [Test(Description = "Check that the same 16-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int16TestValues))]
        public short WriteReadInt16Test(short value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt16());
        }

        [Test(Description = "Check that the same 32-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int32TestValues))]
        public int WriteReadInt32Test(int value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt32());
        }

        [Test(Description = "Check that the same 64-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int64TestValues))]
        public long WriteReadInt64Test(long value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt64());
        }

        [Test(Description = "Check that the same unsigned 16-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt16TestValues))]
        public ushort WriteReadUInt16Test(ushort value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt16());
        }

        [Test(Description = "Check that the same unsigned 32-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt32TestValues))]
        public uint WriteReadUInt32Test(uint value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt32());
        }

        [Test(Description = "Check that the same unsigned 64-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt64TestValues))]
        public ulong WriteReadUInt64Test(ulong value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt64());
        }
        
        [Test(Description = "Check that the same decimal value is read after being written")]
        [TestCaseSource(nameof(DecimalTestValues))]
        public decimal WriteReadDecimalFlipTest(decimal value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadDecimal(), true);
        }

        [Test(Description = "Check that the same 32-bit floating-point value is read after being written")]
        [TestCaseSource(nameof(SingleTestValues))]
        public float WriteReadSingleFlipTest(float value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadSingle(), true);
        }

        [Test(Description = "Check that the same 64-bit floating-point value is read after being written")]
        [TestCaseSource(nameof(DoubleTestValues))]
        public double WriteReadDoubleFlipTest(double value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadDouble(), true);
        }

        [Test(Description = "Check that the same 16-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int16TestValues))]
        public short WriteReadInt16FlipTest(short value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt16(), true);
        }

        [Test(Description = "Check that the same 32-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int32TestValues))]
        public int WriteReadInt32FlipTest(int value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt32(), true);
        }

        [Test(Description = "Check that the same 64-bit integer value is read after being written")]
        [TestCaseSource(nameof(Int64TestValues))]
        public long WriteReadInt64FlipTest(long value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadInt64(), true);
        }

        [Test(Description = "Check that the same unsigned 16-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt16TestValues))]
        public ushort WriteReadUInt16FlipTest(ushort value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt16(), true);
        }

        [Test(Description = "Check that the same unsigned 32-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt32TestValues))]
        public uint WriteReadUInt32FlipTest(uint value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt32(), true);
        }

        [Test(Description = "Check that the same unsigned 64-bit integer value is read after being written")]
        [TestCaseSource(nameof(UInt64TestValues))]
        public ulong WriteReadUInt64FlipTest(ulong value)
        {
            return WriteRead(writer => writer.Write(value), reader => reader.ReadUInt64(), true);
        }

        private static T WriteRead<T>(Action<EndianAwareBinaryWriter> writeAction,
            Func<EndianAwareBinaryReader, T> readAction, bool flip = false)
        {
            var bigEndian = BitConverter.IsLittleEndian ? flip : !flip;
            using (var stream = new MemoryStream())
            using (var writer = new EndianAwareBinaryWriter(stream, flip))
            {
                writeAction(writer);
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new EndianAwareBinaryReader(stream, bigEndian))
                    return readAction(reader);
            }
        }

        private static IEnumerable<TestCaseData> DecimalTestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextDecimal();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[]
                {decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> SingleTestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextFloat();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[]
            {
                0f, float.MinValue, float.MaxValue, float.Epsilon,
                float.NaN, float.NegativeInfinity, float.PositiveInfinity
            })
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> DoubleTestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextDouble();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[]
            {
                0d, double.MinValue, double.MaxValue, double.Epsilon,
                double.NaN, double.NegativeInfinity, double.PositiveInfinity
            })
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> Int16TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextShort();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new short[] {0, short.MinValue, short.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> Int32TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.Next();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[] {0, int.MinValue, int.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> Int64TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextLong();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[] {0, long.MinValue, long.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> UInt16TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextUShort();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[] {ushort.MinValue, ushort.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> UInt32TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextUInt();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[] {uint.MinValue, uint.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }

        private static IEnumerable<TestCaseData> UInt64TestValues()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var value = randomizer.NextULong();
                yield return new TestCaseData(value).Returns(value);
            }
            foreach (var value in new[] {ulong.MinValue, ulong.MaxValue})
                yield return new TestCaseData(value).Returns(value);
        }
    }
}