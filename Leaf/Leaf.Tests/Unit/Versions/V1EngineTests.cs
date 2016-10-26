using NUnit.Framework;
using Leaf.Versions;

namespace Leaf.Tests.Unit.Versions
{
    [TestFixture]
    public class V1EngineTests
    {
        /// <summary>
        /// Verify that the version reported by the engine is 1.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var engine = new V1Engine();
            Assert.AreEqual(1, engine.Version);
        }
    }
}
