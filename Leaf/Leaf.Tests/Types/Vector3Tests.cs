using Leaf.Types;
using NUnit.Framework;

namespace Leaf.Tests.Types
{
    [TestFixture]
    public class Vector3Tests
    {
        private static Vector3 create(float x = 3f, float y = 4f, float z = 5f)
        {
            return new Vector3(x, y, z);
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
    }
}
