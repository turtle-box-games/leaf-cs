using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Rect3Tests
    {
        private static Rect3 create(int x = 3, int y = 4, int z = 5, int w = 6, int h = 7, int d = 8)
        {
            return new Rect3(x, y, z, w, h, d);
        }

        private static Rect3 createOrigin(int w = 3, int h = 4, int d = 5)
        {
            return new Rect3(w, h, d);
        }

        private static Rect3 createStruct(int x = 3, int y = 4, int z = 5, int w = 6, int h = 7, int d = 8)
        {
            var position = new Point3(x, y, z);
            var size     = new Point3(w, h, d);
            return new Rect3(position, size);
        }

        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const int v = 20;
            var rect = create(x: v);
            Assert.AreEqual(v, rect.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const int v = 20;
            var rect = create(y: v);
            Assert.AreEqual(v, rect.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const int v = 20;
            var rect = create(z: v);
            Assert.AreEqual(v, rect.Z);
        }

        /// <summary>
        /// Test that the Width property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWidthGetter()
        {
            const int v = 20;
            var rect = create(w: v);
            Assert.AreEqual(v, rect.Width);
        }

        /// <summary>
        /// Test that the Height property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestHeightGetter()
        {
            const int v = 20;
            var rect = create(h: v);
            Assert.AreEqual(v, rect.Height);
        }

        /// <summary>
        /// Test that the Depth property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestDepthGetter()
        {
            const int v = 20;
            var rect = create(d: v);
            Assert.AreEqual(v, rect.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestOriginWidth()
        {
            const int v = 20;
            var rect = createOrigin(w: v);
            Assert.AreEqual(v, rect.Width);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestOriginHeight()
        {
            const int v = 20;
            var rect = createOrigin(h: v);
            Assert.AreEqual(v, rect.Height);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestOriginDepth()
        {
            const int v = 20;
            var rect = createOrigin(d: v);
            Assert.AreEqual(v, rect.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the X property to 0.
        /// </summary>
        [Test]
        public void TestOriginX()
        {
            var rect = createOrigin();
            Assert.AreEqual(0, rect.X);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Y property to 0.
        /// </summary>
        [Test]
        public void TestOriginY()
        {
            var rect = createOrigin();
            Assert.AreEqual(0, rect.Y);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Z property to 0.
        /// </summary>
        [Test]
        public void TestOriginZ()
        {
            var rect = createOrigin();
            Assert.AreEqual(0, rect.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the X property.
        /// </summary>
        [Test]
        public void TestStructX()
        {
            const int v = 20;
            var rect = createStruct(x: v);
            Assert.AreEqual(v, rect.X);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Y property.
        /// </summary>
        [Test]
        public void TestStructY()
        {
            const int v = 20;
            var rect = createStruct(y: v);
            Assert.AreEqual(v, rect.Y);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Z property.
        /// </summary>
        [Test]
        public void TestStructZ()
        {
            const int v = 20;
            var rect = createStruct(z: v);
            Assert.AreEqual(v, rect.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestStructWidth()
        {
            const int v = 20;
            var rect = createStruct(w: v);
            Assert.AreEqual(v, rect.Width);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestStructHeight()
        {
            const int v = 20;
            var rect = createStruct(h: v);
            Assert.AreEqual(v, rect.Height);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestStructDepth()
        {
            const int v = 20;
            var rect = createStruct(d: v);
            Assert.AreEqual(v, rect.Depth);
        }
    }
}
