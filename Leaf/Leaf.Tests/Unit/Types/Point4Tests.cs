using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Point4Tests
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6;
            var point = new Point4(x, y, z, w);
            Assert.AreEqual(x, point.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6;
            var point = new Point4(x, y, z, w);
            Assert.AreEqual(y, point.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6;
            var point = new Point4(x, y, z, w);
            Assert.AreEqual(z, point.Z);
        }

        /// <summary>
        /// Test that the W property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6;
            var point = new Point4(x, y, z, w);
            Assert.AreEqual(w, point.W);
        }
    }
}
