using NUnit.Framework;
using Leaf.Types;

namespace Leaf.Tests.Unit.Types
{
    [TestFixture]
    public class Bounds3Tests
    {
        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(x, bounds.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(y, bounds.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(z, bounds.Z);
        }

        /// <summary>
        /// Test that the Width property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWidthGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(w, bounds.Width);
        }

        /// <summary>
        /// Test that the Height property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestHeightGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(h, bounds.Height);
        }

        /// <summary>
        /// Test that the Depth property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestDepthGetter()
        {
            const float x = 3f, y = 4f, z = 5f, w = 6f, h = 7f, d = 8f;
            var bounds = new Bounds3(x, y, z, w, h, d);
            Assert.AreEqual(d, bounds.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestOriginWidth()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(w, bounds.Width);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestOriginHeight()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(h, bounds.Height);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestOriginDepth()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(d, bounds.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the X property to 0.
        /// </summary>
        [Test]
        public void TestOriginX()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(0, bounds.X);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Y property to 0.
        /// </summary>
        [Test]
        public void TestOriginY()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(0, bounds.Y);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Z property to 0.
        /// </summary>
        [Test]
        public void TestOriginZ()
        {
            const float w = 3f, h = 4f, d = 5f;
            var bounds = new Bounds3(w, h, d);
            Assert.AreEqual(0, bounds.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the X property.
        /// </summary>
        [Test]
        public void TestStructX()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(position.X, bounds.X);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Y property.
        /// </summary>
        [Test]
        public void TestStructY()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(position.Y, bounds.Y);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Z property.
        /// </summary>
        [Test]
        public void TestStructZ()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(position.Z, bounds.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestStructWidth()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(size.X, bounds.Width);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestStructHeight()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(size.Y, bounds.Height);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestStructDepth()
        {
            var position = new Vector3(3f, 4f, 5f);
            var size     = new Vector3(6f, 7f, 8f);
            var bounds   = new Bounds3(position, size);
            Assert.AreEqual(size.Z, bounds.Depth);
        }
    }
}
