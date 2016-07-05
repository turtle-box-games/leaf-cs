using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Point3Test
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const int x = 3, y = 4, z = 5;
            var point = new Point3(x, y, z);
            Assert.AreEqual(x, point.X);
        }

        /// <summary>
        /// Test that the X property's setter updates the value.
        /// </summary>
        [Test]
        public void TestXSetter()
        {
            const int x = 3, y = 4, z = 5, newX = 7;
            var point = new Point3(x, y, z);
            point.X = newX;
            Assert.AreEqual(newX, point.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const int x = 3, y = 4, z = 5;
            var point = new Point3(x, y, z);
            Assert.AreEqual(y, point.Y);
        }

        /// <summary>
        /// Test that the Y property's setter updates the value.
        /// </summary>
        [Test]
        public void TestYSetter()
        {
            const int x = 3, y = 4, z = 5, newY = 7;
            var point = new Point3(x, y, z);
            point.Y = newY;
            Assert.AreEqual(newY, point.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const int x = 3, y = 4, z = 5;
            var point = new Point3(x, y, z);
            Assert.AreEqual(z, point.Z);
        }

        /// <summary>
        /// Test that the Z property's setter updates the value.
        /// </summary>
        [Test]
        public void TestZSetter()
        {
            const int x = 3, y = 4, z = 5, newZ = 7;
            var point = new Point3(x, y, z);
            point.Z = newZ;
            Assert.AreEqual(newZ, point.Y);
        }
    }
}
