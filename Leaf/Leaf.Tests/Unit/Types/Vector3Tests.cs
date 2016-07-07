using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Vector3Tests
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const float x = 3f, y = 4f, z = 5f;
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(x, vector.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const float x = 3f, y = 4f, z = 5f;
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(y, vector.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const float x = 3f, y = 4f, z = 5f;
            var vector = new Vector3(x, y, z);
            Assert.AreEqual(z, vector.Z);
        }
    }
}
