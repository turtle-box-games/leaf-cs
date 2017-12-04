using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(ListNode))]
    public class ListNodeTests
    {
        private static readonly Node[] EmptyNodeSet = { };
        
        [Test(Description = "Check that the reported node type is correct.")]
        public void TypeIdTest()
        {
            var node = new ListNode(NodeType.String);
            Assert.That(node.Type, Is.EqualTo(NodeType.List));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = GenerateListNode();
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Check that the reported node type is correct.")]
        [TestCaseSource(nameof(RandomNodeTypes))]
        public void ElementTypeIdEmptyTest(NodeType elementType)
        {
            var node = new ListNode(elementType);
            Assert.That(node.ElementType, Is.EqualTo(elementType));
        }

        [Test(Description = "Check that the reported node type is correct.")]
        [TestCaseSource(nameof(RandomNodeTypes))]
        public void ElementTypeIdContentsTest(NodeType elementType)
        {
            var node = new ListNode(elementType, EmptyNodeSet);
            Assert.That(node.ElementType, Is.EqualTo(elementType));
        }

        [Test(Description = "Check that the empty constructor creates an empty list.")]
        public void EmptyConstructorTest()
        {
            var node = new ListNode(NodeType.String);
            Assert.That(node, Is.Empty);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void ContentsConstructorTest(NodeType elementType, Node[] elements)
        {
            var node = new ListNode(elementType, elements);
            Assert.That(node, Is.EqualTo(elements));
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void NullContentsConstructorTest()
        {
            Assert.That(() => { new ListNode(NodeType.String, null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown for mixed node types.")]
        [TestCaseSource(nameof(RandomMixedNodesCollections))]
        public void MixedContentsConstructorTest(NodeType elementType, Node[] elements)
        {
            Assert.That(() => { new ListNode(elementType, elements); },
                Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        public void NullNodeContentsConstructorTest()
        {
            Assert.That(() => { new ListNode(NullNodeSet.First().Type, NullNodeSet); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that appending a node works.")]
        public void AddTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("test");
            list.Add(node);
            var newList = NodeSet.Concat(new Node[] {node});
            Assert.Multiple(() =>
            {
                Assert.That(list, Contains.Item(node));
                Assert.That(list, Is.EqualTo(newList));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to append null.")]
        public void AddNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Add(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check  that an exception is thrown when attempting to append a node of a different type.")]
        public void AddTypeMismatchTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Add(new Int32Node(12345)); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that clearing the list empties it.")]
        public void ClearTest()
        {
            var list = GenerateListNode();
            list.Clear();
            Assert.That(list, Is.Empty);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void ContainsTest()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.That(list, Contains.Item(node));
        }

        [Test(Description = "Check that false is returned when an item can't be found in the list.")]
        public void NotContainsTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("contains");
            Assert.That(list, Does.Not.Contain(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to look for null.")]
        public void ContainsNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Contains(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that nodes in the list can be copied to an array.")]
        public void CopyToTest()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            list.CopyTo(array, 0);
            Assert.That(array, Is.EqualTo(list));
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        public void CopyToNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.CopyTo(null, 0); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        public void CopyToNegativeIndexTest()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.That(() => { list.CopyTo(array, -3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        public void CopyToIndexTooLargeTest()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.That(() => { list.CopyTo(array, array.Length + 1); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small the list.")]
        public void CopyToArrayTooSmallTest()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count - 1];
            Assert.That(() => { list.CopyTo(array, 0); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that removing a node works as expected.")]
        public void RemoveTest()
        {
            var list     = GenerateListNode();
            var node     = list[1];
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            Assert.Multiple(() =>
            {
                Assert.That(list.Remove(node), Is.True);
                Assert.That(list, Does.Not.Contain(node));
                Assert.That(list, Is.EqualTo(expected));
            });
        }

        [Test(Description = "Check that nothing happens when attempting to remove a non-existent node.")]
        public void RemoveNonExistentTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("remove");
            Assert.Multiple(() =>
            {
                Assert.That(list.Remove(node), Is.False);
                Assert.That(list, Is.EqualTo(NodeSet));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to remove null.")]
        public void RemoveNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Remove(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the count property returns the correct amount.")]
        public void CountTest()
        {
            var list = GenerateListNode();
            Assert.That(list, Has.Count.EqualTo(NodeSet.Length));
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void IsReadOnlyTest()
        {
            var list = GenerateListNode();
            Assert.That(list.IsReadOnly, Is.False);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void IndexOfTest()
        {
            const int index = 1;
            var list = GenerateListNode();
            var node = list[index];
            Assert.That(list.IndexOf(node), Is.EqualTo(index));
        }

        [Test(Description = "Check that a negative index is returned for an item that can't be found in the list.")]
        public void IndexOfNonExistentTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("indexOf");
            Assert.That(list.IndexOf(node), Is.EqualTo(-1));
        }

        [Test(Description = "Check that an exception is thrown when looking for null.")]
        public void IndexOfNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.IndexOf(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the insert method adds an item.")]
        public void InsertTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            list.Insert(1, node);
            Assert.That(list, Contains.Item(node));
        }

        [Test(Description = "Check that the insert method works for non-end indices.")]
        public void InsertMiddleTest()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(1, node);
            list.Insert(1, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that inserting an item at the start of the list works as expected.")]
        public void InsertFirstTest()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(0, node);
            list.Insert(0, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that inserting an item at the end of the list works as expected.")]
        public void InsertLastTest()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(NodeSet.Length, node);
            list.Insert(list.Count, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert null.")]
        public void InsertNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Insert(1, null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void InsertNegativeIndexTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.That(() => { list.Insert(-1, node); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void InsertIndexTooLargeTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.That(() => { list.Insert(list.Count + 1, node); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert a node of a different type.")]
        public void InsertTypeMismatchTest()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.That(() => { list.Insert(1, node); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that removing an item at an index works as expected.")]
        public void RemoveAtTest()
        {
            const int index = 1;
            var list = GenerateListNode();
            list.RemoveAt(index);
            Assert.That(list, Does.Not.Contain(NodeSet[index]));
        }

        [Test(Description = "Check that removing an item at a middle index works as expected.")]
        public void RemoveAtMiddleTest()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            list.RemoveAt(1);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the first item in the list works as expected.")]
        public void RemoveAtFirstTest()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(0);
            list.RemoveAt(0);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the last item in the list works as expected.")]
        public void RemoveAtLastTest()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(NodeSet.Length - 1);
            list.RemoveAt(list.Count - 1);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void RemoveAtNegativeIndexTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.RemoveAt(-1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void RemoveAtIndexTooLargeTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.RemoveAt(list.Count + 1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the getter works as expected.")]
        public void GetterTest()
        {
            var list = GenerateListNode();
            Assert.That(list[0], Is.EqualTo(NodeSet[0]));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void GetterNegativeIndexTest()
        {
            var list = GenerateListNode();
            Assert.That(() =>
            {
                var _ = list[-1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void GetterIndexTooLargeTest()
        {
            var list = GenerateListNode();
            Assert.That(() =>
            {
                var _ = list[list.Count + 1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the setter works as expected.")]
        public void SetterTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            list[1]  = node;
            Assert.That(list[1], Is.EqualTo(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to set an element to null.")]
        public void SetterNullTest()
        {
            var list = GenerateListNode();
            Assert.That(() => { list[1] = null; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void SetterNegativeIndexTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.That(() => { list[-1] = node; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void SetterIndexTooLargeTest()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.That(() => { list[list.Count + 1] = node; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to set an element to a different node type.")]
        public void SetterTypeMismatchTest()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.That(() => { list[1] = node; }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        private static readonly Node[] NodeSet =
        {
            new StringNode("foo"),
            new StringNode("bar"),
            new StringNode("baz")
        };

        private static readonly Node[] NullNodeSet =
        {
            new Int32Node(12345),
            null,
            new Int32Node(54321)
        };

        private static ListNode GenerateListNode()
        {
            return new ListNode(NodeSet[0].Type, NodeSet);
        }

        private static IEnumerable<NodeType> RandomNodeTypes()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
                yield return randomizer.NextNonNestableNodeType();
        }

        private static IEnumerable RandomNodeCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var elementType = randomizer.NextNonNestableNodeType();
                var elements    = NodeBuilders.GenerateMultipleOfType(randomizer, elementType).ToArray();
                yield return new object[] {elementType, elements};
            }
        }

        private static IEnumerable RandomMixedNodesCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var elementType = randomizer.NextNonNestableNodeType();
                var mixedInType = randomizer.NextNonNestableNodeType();
                while (mixedInType == elementType)
                    mixedInType = randomizer.NextNonNestableNodeType();
                var elements  = NodeBuilders.GenerateMultipleOfType(randomizer, elementType).ToList();
                var mixedNode = randomizer.NextNodeOfType(mixedInType);
                var index     = randomizer.Next(0, elements.Count);
                elements.Insert(index, mixedNode);
                yield return new object[] {elementType, elements.ToArray()};
            }
        }
    }
}