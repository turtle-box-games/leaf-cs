using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Vector4Tests
    {
        private static Vector4 create(float x = 3f, float y = 4f, float z = 5f, float w = 6f)
        {
            return new Vector4(x, y, z, w);
        }

        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const float v = 20f;
            var vector = create(x: v);
            Assert.AreEqual(v, vector.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const float v = 20f;
            var vector = create(y: v);
            Assert.AreEqual(v, vector.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const float v = 20f;
            var vector = create(z: v);
            Assert.AreEqual(v, vector.Z);
        }

        /// <summary>
        /// Test that the W property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWGetter()
        {
            const float v = 20f;
            var vector = create(w: v);
            Assert.AreEqual(v, vector.W);
        }
    }
}
