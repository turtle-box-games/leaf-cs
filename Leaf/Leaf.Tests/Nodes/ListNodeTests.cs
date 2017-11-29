using System;
using System.Linq;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(ListNode))]
    public class ListNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeId()
        {
            var node = new ListNode(NodeType.String);
            Assert.AreEqual(NodeType.List, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = GenerateListNode();
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Check that the reported node type is correct.")]
        public void TestElementTypeIdEmpty()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType);
            Assert.AreEqual(elementType, node.ElementType);
        }

        [Test(Description = "Check that the reported node type is correct.")]
        public void TestElementTypeIdContents()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType, EmptyNodeSet);
            Assert.AreEqual(elementType, node.ElementType);
        }

        [Test(Description = "Check that the empty constructor creates an empty list.")]
        public void TestEmptyConstructor()
        {
            var node = new ListNode(NodeType.String);
            Assert.IsEmpty(node);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        public void TestContentsConstructor()
        {
            var node = new ListNode(NodeSet.First().Type, NodeSet);
            CollectionAssert.AreEqual(NodeSet, node);
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void TestNullContentsConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { new ListNode(NodeType.String, null); });
        }

        [Test(Description = "Check that an exception is thrown for mixed node types.")]
        public void TestMixedContentsConstructor()
        {
            Assert.Throws<ArrayTypeMismatchException>(() => { new ListNode(MixedNodeSet.First().Type, MixedNodeSet); });
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        public void TestNullNodeContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new ListNode(NullNodeSet.First().Type, NullNodeSet); });
        }

        [Test(Description = "Check that appending a node works.")]
        public void TestAdd()
        {
            var list = GenerateListNode();
            var node = new StringNode("test");
            list.Add(node);
            CollectionAssert.AreEqual(NodeSet.Concat(new Node[] {node}), list);
        }

        [Test(Description = "Check that an exception is thrown when attempting to append null.")]
        public void TestAddNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Add(null); });
        }

        [Test(Description = "Check  that an exception is thrown when attempting to append a node of a different type.")]
        public void TestAddTypeMismatch()
        {
            var list = GenerateListNode();
            Assert.Throws<ArrayTypeMismatchException>(() => { list.Add(new Int32Node(12345)); });
        }

        [Test(Description = "Check that clearing the list empties it.")]
        public void TestClear()
        {
            var list = GenerateListNode();
            list.Clear();
            CollectionAssert.IsEmpty(list);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void TestContains()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.True(list.Contains(node));
        }

        [Test(Description = "Check that false is returned when an item can't be found in the list.")]
        public void TestNotContains()
        {
            var list = GenerateListNode();
            var node = new StringNode("contains");
            Assert.False(list.Contains(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to look for null.")]
        public void TestContainsNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Contains(null); });
        }

        [Test(Description = "Check that nodes in the list can be copied to an array.")]
        public void TestCopyTo()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            list.CopyTo(array, 0);
            CollectionAssert.AreEqual(list, array);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        public void TestCopyToNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.CopyTo(null, 0); });
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        public void TestCopyToNegativeIndex()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.CopyTo(array, -3); });
        }

        [Test(Description = "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        public void TestCopyToIndexTooLarge()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.Throws<ArgumentException>(() => { list.CopyTo(array, array.Length + 1); });
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small the list.")]
        public void TestCopyToArrayTooSmall()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count - 1];
            Assert.Throws<ArgumentException>(() => { list.CopyTo(array, 0); });
        }

        [Test(Description = "Check that removing a node works as expected.")]
        public void TestRemove()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.True(list.Remove(node));
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that nothing happens when attempting to remove a non-existent node.")]
        public void TestRemoveNonExistent()
        {
            var list = GenerateListNode();
            var node = new StringNode("remove");
            Assert.False(list.Remove(node));
            CollectionAssert.AreEqual(NodeSet, list);
        }

        [Test(Description = "Check that an exception is thrown when attempting to remove null.")]
        public void TestRemoveNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Remove(null); });
        }

        [Test(Description = "Check that the count property returns the correct amount.")]
        public void TestCount()
        {
            var list = GenerateListNode();
            Assert.AreEqual(NodeSet.Length, list.Count);
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void TestIsReadOnly()
        {
            var list = GenerateListNode();
            Assert.False(list.IsReadOnly);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void TestIndexOf()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.AreEqual(1, list.IndexOf(node));
        }

        [Test(Description = "Check that a negative index is returned for an item that can't be found in the list.")]
        public void TestIndexOfNonExistent()
        {
            var list = GenerateListNode();
            var node = new StringNode("indexOf");
            Assert.AreEqual(-1, list.IndexOf(node));
        }

        [Test(Description = "Check that an exception is thrown when looking for null.")]
        public void TestIndexOfNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.IndexOf(null); });
        }

        [Test(Description = "Check that the insert method works.")]
        public void TestInsert()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(1, node);
            list.Insert(1, node);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that inserting an item at the start of the list works as expected.")]
        public void TestInsertFirst()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(0, node);
            list.Insert(0, node);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that inserting an item at the end of the list works as expected.")]
        public void TestInsertLast()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(NodeSet.Length, node);
            list.Insert(list.Count, node);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert null.")]
        public void TestInsertNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Insert(1, null); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestInsertNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.Insert(-1, node); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestInsertIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.Insert(list.Count + 1, node); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert a node of a different type.")]
        public void TestInsertTypeMismatch()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.Throws<ArrayTypeMismatchException>(() => { list.Insert(1, node); });
        }

        [Test(Description = "Check that removing an item at an index works as expected.")]
        public void TestRemoveAt()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            list.RemoveAt(1);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that removing the first item in the list works as expected.")]
        public void TestRemoveAtFirst()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(0);
            list.RemoveAt(0);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that removing the last item in the list works as expected.")]
        public void TestRemoveAtLast()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(NodeSet.Length - 1);
            list.RemoveAt(list.Count - 1);
            CollectionAssert.AreEqual(expected, list);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestRemoveAtNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.RemoveAt(-1); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestRemoveAtIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.RemoveAt(list.Count + 1); });
        }

        [Test(Description = "Check that the getter works as expected.")]
        public void TestGetter()
        {
            var list = GenerateListNode();
            Assert.AreEqual(NodeSet[0], list[0]);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestGetterNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { var node = list[-1]; });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestGetterIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var node = list[list.Count + 1];
            });
        }

        [Test(Description = "Check that the setter works as expected.")]
        public void TestSetter()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            list[1] = node;
            Assert.AreEqual(node, list[1]);
        }

        [Test(Description = "Check that an exception is thrown when attempting to set an element to null.")]
        public void TestSetterNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list[1] = null; });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestSetterNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list[-1] = node; });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestSetterIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list[list.Count + 1] = node; });
        }

        [Test(Description = "Check that an exception is thrown when attempting to set an element to a different node type.")]
        public void TestSetterTypeMismatch()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.Throws<ArrayTypeMismatchException>(() => { list[1] = node; });
        }

        private static readonly Node[] EmptyNodeSet = { };

        private static readonly Node[] NodeSet =
        {
            new StringNode("foo"),
            new StringNode("bar"),
            new StringNode("baz")
        };

        private static readonly Node[] MixedNodeSet =
        {
            new Int32Node(12345),
            new StringNode("foobar"),
            new Int32Node(54321)
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
    }
}
