using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests
{
    [TestFixture(TestOf = typeof(Container))]
    public class ContainerTests
    {
        [Test(Description = "Verify that the constructor throws an exception when the root node is null.")]
        public void TestNullRoot()
        {
            Assert.That(() => { new Container(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the root node property is set properly.")]
        public void TestRootNode()
        {
            var root = new Int32Node(50);
            var container = new Container(root);
            Assert.That(container.Root, Is.EqualTo(root));
        }
    }
}