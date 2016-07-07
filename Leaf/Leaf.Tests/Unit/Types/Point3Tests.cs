using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Point3Tests
    {
        private static Point3 create(int x = 3, int y = 4, int z = 5)
        {
            return new Point3(x, y, z);
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
    }
}
