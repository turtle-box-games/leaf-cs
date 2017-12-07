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
            var node = new ListNode(NodeType.String);
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
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
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
        [TestCaseSource(nameof(RandomNodesWithNullCollections))]
        public void NullNodeContentsConstructorTest(NodeType elementType, Node[] elements)
        {
            Assert.That(() => { new ListNode(elementType, elements); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that appending a node works.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void AddTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            var newNode  = TestContext.CurrentContext.Random.NextNodeOfType(elementType);
            listNode.Add(newNode);
            Assert.Multiple(() =>
            {
                Assert.That(listNode, Contains.Item(newNode));
                Assert.That(listNode, Is.EqualTo(elements.Union(new[] {newNode})));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to append null.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void AddNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Add(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check  that an exception is thrown when attempting to append a node of a different type.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void AddTypeMismatchTest(NodeType elementType, Node[] elements)
        {
            var listNode   = new ListNode(elementType, elements);
            var randomizer = TestContext.CurrentContext.Random;
            var mixedType  = randomizer.NextNonNestableNodeType();
            while (mixedType == elementType)
                mixedType = randomizer.NextNonNestableNodeType();
            var mixedNode = randomizer.NextNodeOfType(mixedType);
            Assert.That(() => { listNode.Add(mixedNode); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that clearing the list empties it.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void ClearTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            listNode.Clear();
            Assert.That(listNode, Is.Empty);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void ContainsTest(NodeType elementType, Node[] elements)
        {
            var listNode   = new ListNode(elementType, elements);
            var randomizer = TestContext.CurrentContext.Random;
            var toFind     = elements[randomizer.Next(elements.Length)];
            Assert.That(listNode, Contains.Item(toFind));
        }

        [Test(Description = "Check that false is returned when an item can't be found in the list.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void NotContainsTest(NodeType elementType, Node[] elements)
        {
            var listNode   = new ListNode(elementType, elements);
            var randomizer = TestContext.CurrentContext.Random;
            var node       = randomizer.NextNodeOfType(elementType);
            Assert.That(listNode, Does.Not.Contain(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to look for null.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void ContainsNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Contains(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that nodes in the list can be copied to an array.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void CopyToTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            var array    = new Node[elements.Length];
            listNode.CopyTo(array, 0);
            Assert.That(array, Is.EqualTo(elements));
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void CopyToNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.CopyTo(null, 0); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void CopyToNegativeIndexTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            var array    = new Node[elements.Length];
            Assert.That(() => { listNode.CopyTo(array, -3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void CopyToIndexTooLargeTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            var array    = new Node[elements.Length];
            Assert.That(() => { listNode.CopyTo(array, array.Length + 1); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small the list.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void CopyToArrayTooSmallTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var trimLength = randomizer.Next(elements.Length) + 1;
            var listNode   = new ListNode(elementType, elements);
            var array      = new Node[elements.Length - trimLength];
            Assert.That(() => { listNode.CopyTo(array, 0); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that removing a node works as expected.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var toRemove   = elements[randomizer.Next(elements.Length)];
            var listNode   = new ListNode(elementType, elements);
            var expected   = elements.ToList();
            expected.Remove(toRemove);
            Assert.Multiple(() =>
            {
                Assert.That(listNode.Remove(toRemove), Is.True);
                Assert.That(listNode, Does.Not.Contain(toRemove));
                Assert.That(listNode, Is.EqualTo(expected));
            });
        }

        [Test(Description = "Check that nothing happens when attempting to remove a non-existent node.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveNonExistentTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var node       = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            Assert.Multiple(() =>
            {
                Assert.That(listNode.Remove(node), Is.False);
                Assert.That(listNode, Is.EqualTo(elements));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to remove null.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Remove(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the count property returns the correct amount.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void CountTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(listNode, Has.Count.EqualTo(elements.Length));
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void IsReadOnlyTest()
        {
            var listNode = new ListNode(NodeType.String);
            Assert.That(listNode.IsReadOnly, Is.False);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void IndexOfTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var index      = randomizer.Next(elements.Length);
            var node       = elements[index];
            var listNode   = new ListNode(elementType, elements);
            Assert.That(listNode.IndexOf(node), Is.EqualTo(index));
        }

        [Test(Description = "Check that a negative index is returned for an item that can't be found in the list.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void IndexOfNonExistentTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var node       = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            Assert.That(listNode.IndexOf(node), Is.EqualTo(-1));
        }

        [Test(Description = "Check that an exception is thrown when looking for null.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void IndexOfNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.IndexOf(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the insert method works for non-end indices.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void InsertTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            listNode.Insert(1, newNode);
            Assert.That(listNode, Contains.Item(newNode));
        }

        [Test(Description = "Check that inserting an item at the start of the list works as expected.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void InsertFirstTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            listNode.Insert(0, newNode);
            Assert.That(listNode, Contains.Item(newNode));
        }

        [Test(Description = "Check that inserting an item at the end of the list works as expected.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void InsertLastTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            listNode.Insert(elements.Length, newNode);
            Assert.That(listNode, Contains.Item(newNode));
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert null.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void InsertNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Insert(1, null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void InsertNegativeIndexTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Insert(-1, newNode); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void InsertIndexTooLargeTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Insert(listNode.Count + 1, newNode); },
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert a node of a different type.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void InsertTypeMismatchTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var mixedType  = randomizer.NextNonNestableNodeType();
            while (mixedType == elementType)
                mixedType = randomizer.NextNonNestableNodeType();
            var mixedNode = randomizer.NextNodeOfType(mixedType);
            var listNode  = new ListNode(elementType, elements);
            Assert.That(() => { listNode.Insert(1, mixedNode); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that removing an item at a middle index works as expected.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveAtTest(NodeType elementType, Node[] elements)
        {
            const int index = 1;
            var listNode    = new ListNode(elementType, elements);
            var expected    = elements.ToList();
            expected.RemoveAt(index);
            listNode.RemoveAt(index);
            Assert.That(listNode, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the first item in the list works as expected.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveAtFirstTest(NodeType elementType, Node[] elements)
        {
            const int index = 0;
            var listNode    = new ListNode(elementType, elements);
            var expected    = elements.ToList();
            expected.RemoveAt(index);
            listNode.RemoveAt(index);
            Assert.That(listNode, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the last item in the list works as expected.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveAtLastTest(NodeType elementType, Node[] elements)
        {
            var index    = elements.Length - 1;
            var listNode = new ListNode(elementType, elements);
            var expected = elements.ToList();
            expected.RemoveAt(index);
            listNode.RemoveAt(index);
            Assert.That(listNode, Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveAtNegativeIndexTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.RemoveAt(-1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void RemoveAtIndexTooLargeTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode.RemoveAt(elements.Length + 1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the getter works as expected.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void GetterTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(listNode[0], Is.EqualTo(elements[0]));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void GetterNegativeIndexTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() =>
            {
                var _ = listNode[-1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void GetterIndexTooLargeTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() =>
            {
                var _ = listNode[elements.Length + 1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the setter works as expected.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void SetterTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var listNode   = new ListNode(elementType, elements);
            var newNode    = randomizer.NextNodeOfType(elementType);
            listNode[1]    = newNode;
            Assert.That(listNode[1], Is.EqualTo(newNode));
        }

        [Test(Description = "Check that an exception is thrown when attempting to set an element to null.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void SetterNullTest(NodeType elementType, Node[] elements)
        {
            var listNode = new ListNode(elementType, elements);
            Assert.That(() => { listNode[1] = null; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void SetterNegativeIndexTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements); 
            Assert.That(() => { listNode[-1] = newNode; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        [TestCaseSource(nameof(RandomNodeCollections))]
        public void SetterIndexTooLargeTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var newNode    = randomizer.NextNodeOfType(elementType);
            var listNode   = new ListNode(elementType, elements);
            Assert.That(() => { listNode[elements.Length + 1] = newNode; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to set an element to a different node type.")]
        [TestCaseSource(nameof(AllTypesNodeCollections))]
        [TestCaseSource(nameof(ListOfListCollections))]
        [TestCaseSource(nameof(ListOfCompositeCollections))]
        public void SetterTypeMismatchTest(NodeType elementType, Node[] elements)
        {
            var randomizer = TestContext.CurrentContext.Random;
            var mixedType  = randomizer.NextNonNestableNodeType();
            while (mixedType == elementType)
                mixedType = randomizer.NextNonNestableNodeType();
            var mixedNode = randomizer.NextNodeOfType(mixedType);
            var listNode  = new ListNode(elementType, elements);
            Assert.That(() => { listNode[1] = mixedNode; }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        private static IEnumerable AllTypesNodeCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            foreach (var elementType in Enum.GetValues(typeof(NodeType)).Cast<NodeType>())
            {
                if (elementType == NodeType.End || elementType == NodeType.List || elementType == NodeType.Composite)
                    continue;
                var elements = NodeBuilders.GenerateMultipleOfType(randomizer, elementType).ToArray();
                yield return new object[] {elementType, elements};
            }
        }

        private static IEnumerable ListOfListCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var listCount = randomizer.Next(5, 20);
                var listNodes = new ListNode[listCount];
                for (var j = 0; j < listCount; ++j)
                {
                    var elementType = randomizer.NextNonNestableNodeType();
                    var elements    = NodeBuilders.GenerateMultipleOfType(randomizer, elementType);
                    listNodes[j]    = new ListNode(elementType, elements);
                }
                yield return new object[] {NodeType.List, listNodes};
            }
        }

        private static IEnumerable ListOfCompositeCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var compositeCount = randomizer.Next(5, 20);
                var compositeNodes = new CompositeNode[compositeCount];
                for (var j = 0; j < compositeCount; ++j)
                {
                    var pairs = NodeBuilders.GenerateNamedNodes(randomizer);
                    compositeNodes[j] = new CompositeNode(pairs);
                }
                yield return new object[] {NodeType.Composite, compositeNodes};
            }
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

        private static IEnumerable RandomNodesWithNullCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var elementType = randomizer.NextNonNestableNodeType();
                var elements    = NodeBuilders.GenerateMultipleOfType(randomizer, elementType).ToList();
                var index       = randomizer.Next(0, elements.Count);
                elements.Insert(index, null);
                yield return new object[] {elementType, elements.ToArray()};
            }
        }
    }
}