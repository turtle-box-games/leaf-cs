using System;
using NUnit.Framework.Internal;

namespace Leaf.Tests
{
    public static class RandomizerExtensions
    {
        /// <summary>
        /// Generates a random date and time.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>Random date and time.</returns>
        public static DateTime NextDateTime(this Randomizer randomizer)
        {
            var ticks = randomizer.NextLong(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks);
            return new DateTime(ticks);
        }

        /// <summary>
        /// Generates a random array of bytes.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <param name="length">Length of the byte array.</param>
        /// <returns>Array filled with random bytes.</returns>
        public static byte[] NextBytes(this Randomizer randomizer, int length = 20)
        {
            var buffer = new byte[length];
            randomizer.NextBytes(buffer);
            return buffer;
        }
    }
}