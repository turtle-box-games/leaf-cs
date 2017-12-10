using System;
using System.Collections.Generic;
using System.Linq;
using Leaf.Nodes;
using NUnit.Framework;

namespace Leaf.Tests
{
    [TestFixture(TestOf = typeof(Container))]
    public class ContainerTests
    {
        [Test(Description = "Verify that the constructor throws an exception when the root node is null.")]
        public void NullRootTest()
        {
            Assert.That(() => { new Container(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Verify that the root node property is set properly.")]
        [TestCaseSource(nameof(AllNodes))]
        public Node RootNodeTest(Node root)
        {
            var container = new Container(root);
            return container.Root;
        }

        private static IEnumerable<TestCaseData> AllNodes()
        {
            var randomizer = TestContext.CurrentContext.Random;
            foreach (var type in Enum.GetValues(typeof(NodeType)).Cast<NodeType>())
            {
                if (type == NodeType.End)
                    continue;
                var node = randomizer.NextNodeOfType(type);
                yield return new TestCaseData(node).Returns(node);
            }
        }
    }
}