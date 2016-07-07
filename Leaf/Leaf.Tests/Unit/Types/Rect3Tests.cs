using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Rect3Tests
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(x, rect.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(y, rect.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(z, rect.Z);
        }

        /// <summary>
        /// Test that the Width property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWidthGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(w, rect.Width);
        }

        /// <summary>
        /// Test that the Height property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestHeightGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(h, rect.Height);
        }

        /// <summary>
        /// Test that the Depth property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestDepthGetter()
        {
            const int x = 3, y = 4, z = 5, w = 6, h = 7, d = 8;
            var rect = new Rect3(x, y, z, w, h, d);
            Assert.AreEqual(d, rect.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestOriginWidth()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(w, rect.Width);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestOriginHeight()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(h, rect.Height);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestOriginDepth()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(d, rect.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the X property to 0.
        /// </summary>
        [Test]
        public void TestOriginX()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(0, rect.X);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Y property to 0.
        /// </summary>
        [Test]
        public void TestOriginY()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(0, rect.Y);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Z property to 0.
        /// </summary>
        [Test]
        public void TestOriginZ()
        {
            const int w = 3, h = 4, d = 5;
            var rect = new Rect3(w, h, d);
            Assert.AreEqual(0, rect.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the X property.
        /// </summary>
        [Test]
        public void TestStructX()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(position.X, rect.X);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Y property.
        /// </summary>
        [Test]
        public void TestStructY()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(position.Y, rect.Y);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Z property.
        /// </summary>
        [Test]
        public void TestStructZ()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(position.Z, rect.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestStructWidth()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(size.X, rect.Width);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestStructHeight()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(size.Y, rect.Height);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestStructDepth()
        {
            var position = new Point3(3, 4, 5);
            var size     = new Point3(6, 7, 8);
            var rect     = new Rect3(position, size);
            Assert.AreEqual(size.Z, rect.Depth);
        }
    }
}
