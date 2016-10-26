using Leaf.Types;
using NUnit.Framework;

namespace Leaf.Tests.Types
{
    [TestFixture]
    public class Bounds3Tests
    {
        private static Bounds3 create(float x = 3f, float y = 4f, float z = 5f, float w = 6f, float h = 7f, float d = 8f)
        {
            return new Bounds3(x, y, z, w, h, d);
        }

        private static Bounds3 createOrigin(float w = 3f, float h = 4f, float d = 5f)
        {
            return new Bounds3(w, h, d);
        }

        private static Bounds3 createStruct(float x = 3f, float y = 4f, float z = 5f, float w = 6f, float h = 7f, float d = 8f)
        {
            var position = new Vector3(x, y, z);
            var size     = new Vector3(w, h, d);
            return new Bounds3(position, size);
        }

        /// <summary>
        /// Test that the X property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestXGetter()
        {
            const float v = 20f;
            var bounds = create(x: v);
            Assert.AreEqual(v, bounds.X);
        }

        /// <summary>
        /// Test that the Y property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestYGetter()
        {
            const float v = 20f;
            var bounds = create(y: v);
            Assert.AreEqual(v, bounds.Y);
        }

        /// <summary>
        /// Test that the Z property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestZGetter()
        {
            const float v = 20f;
            var bounds = create(z: v);
            Assert.AreEqual(v, bounds.Z);
        }

        /// <summary>
        /// Test that the Width property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestWidthGetter()
        {
            const float v = 20f;
            var bounds = create(w: v);
            Assert.AreEqual(v, bounds.Width);
        }

        /// <summary>
        /// Test that the Height property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestHeightGetter()
        {
            const float v = 20f;
            var bounds = create(h: v);
            Assert.AreEqual(v, bounds.Height);
        }

        /// <summary>
        /// Test that the Depth property's getter returns the correct value.
        /// </summary>
        [Test]
        public void TestDepthGetter()
        {
            const float v = 20f;
            var bounds = create(d: v);
            Assert.AreEqual(v, bounds.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestOriginWidth()
        {
            const float v = 20f;
            var bounds = createOrigin(w: v);
            Assert.AreEqual(v, bounds.Width);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestOriginHeight()
        {
            const float v = 20f;
            var bounds = createOrigin(h: v);
            Assert.AreEqual(v, bounds.Height);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestOriginDepth()
        {
            const float v = 20f;
            var bounds = createOrigin(d: v);
            Assert.AreEqual(v, bounds.Depth);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the X property to 0.
        /// </summary>
        [Test]
        public void TestOriginX()
        {
            var bounds = createOrigin();
            Assert.AreEqual(0f, bounds.X);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Y property to 0.
        /// </summary>
        [Test]
        public void TestOriginY()
        {
            var bounds = createOrigin();
            Assert.AreEqual(0f, bounds.Y);
        }

        /// <summary>
        /// Test that the origin-based constructor sets the Z property to 0.
        /// </summary>
        [Test]
        public void TestOriginZ()
        {
            var bounds = createOrigin();
            Assert.AreEqual(0f, bounds.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the X property.
        /// </summary>
        [Test]
        public void TestStructX()
        {
            const float v = 20f;
            var bounds = createStruct(x: v);
            Assert.AreEqual(v, bounds.X);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Y property.
        /// </summary>
        [Test]
        public void TestStructY()
        {
            const float v = 20f;
            var bounds = createStruct(y: v);
            Assert.AreEqual(v, bounds.Y);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Z property.
        /// </summary>
        [Test]
        public void TestStructZ()
        {
            const float v = 20f;
            var bounds = createStruct(z: v);
            Assert.AreEqual(v, bounds.Z);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Width property.
        /// </summary>
        [Test]
        public void TestStructWidth()
        {
            const float v = 20f;
            var bounds = createStruct(w: v);
            Assert.AreEqual(v, bounds.Width);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Height property.
        /// </summary>
        [Test]
        public void TestStructHeight()
        {
            const float v = 20f;
            var bounds = createStruct(h: v);
            Assert.AreEqual(v, bounds.Height);
        }

        /// <summary>
        /// Test that the position/size constructor sets the Depth property.
        /// </summary>
        [Test]
        public void TestStructDepth()
        {
            const float v = 20f;
            var bounds = createStruct(d: v);
            Assert.AreEqual(v, bounds.Depth);
        }
    }
}
