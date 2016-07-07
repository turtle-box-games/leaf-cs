using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Vector4Tests
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f;
            var vector = new Vector4(x, y, z, w);
            Assert.AreEqual(x, vector.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f;
            var vector = new Vector4(x, y, z, w);
            Assert.AreEqual(y, vector.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f;
            var vector = new Vector4(x, y, z, w);
            Assert.AreEqual(z, vector.Z);
        }

        /// <summary>
        /// Test that the W property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f;
            var vector = new Vector4(x, y, z, w);
            Assert.AreEqual(w, vector.W);
        }
    }
}
