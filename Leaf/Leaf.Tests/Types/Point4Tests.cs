using Leaf.Types;
using NUnit.Framework;

namespace Leaf.Tests.Types
{
    [TestFixture]
    public class Point4Tests
    {
        private static Point4 create(int x = 3, int y = 4, int z = 5, int w = 6)
        {
            return new Point4(x, y, z, w);
        }

        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const int v = 20;
            var point = create(x: v);
            Assert.AreEqual(v, point.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const int v = 20;
            var point = create(y: v);
            Assert.AreEqual(v, point.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const int v = 20;
            var point = create(z: v);
            Assert.AreEqual(v, point.Z);
        }

        /// <summary>
        /// Test that the W property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWGetter()
        {
            const int v = 20;
            var point = create(w: v);
            Assert.AreEqual(v, point.W);
        }
    }
}
