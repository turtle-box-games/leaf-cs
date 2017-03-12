using System;
using System.Linq;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class ListNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeId()
        {
            var node = new ListNode(NodeType.String);
            Assert.AreEqual(NodeType.List, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = GenerateListNode();
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestElementTypeIdEmpty()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType);
            Assert.AreEqual(elementType, node.ElementType);
        }

        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestElementTypeIdContents()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType, EmptyNodeSet);
            Assert.AreEqual(elementType, node.ElementType);
        }

        /// <summary>
        /// Check that the empty constructor creates an empty list.
        /// </summary>
        [Test]
        public void TestEmptyConstructor()
        {
            var node = new ListNode(NodeType.String);
            Assert.IsEmpty(node);
        }

        /// <summary>
        /// Check that the contents constructor adds the same nodes.
        /// </summary>
        [Test]
        public void TestContentsConstructor()
        {
            var node = new ListNode(NodeSet.First().Type, NodeSet);
            CollectionAssert.AreEqual(NodeSet, node);
        }

        /// <summary>
        /// Check that an exception is thrown for a null set of nodes.
        /// </summary>
        [Test]
        public void TestNullContentsConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { new ListNode(NodeType.String, null); });
        }

        /// <summary>
        /// Check that an exception is thrown for mixed node types.
        /// </summary>
        [Test]
        public void TestMixedContentsConstructor()
        {
            Assert.Throws<ArrayTypeMismatchException>(() => { new ListNode(MixedNodeSet.First().Type, MixedNodeSet); });
        }

        /// <summary>
        /// Check that an exception is thrown for null mixed in with nodes.
        /// </summary>
        [Test]
        public void TestNullNodeContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new ListNode(NullNodeSet.First().Type, NullNodeSet); });
        }

        /// <summary>
        /// Check that appending a node works.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var list = GenerateListNode();
            var node = new StringNode("test");
            list.Add(node);
            CollectionAssert.AreEqual(NodeSet.Concat(new Node[] {node}), list);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to append null.
        /// </summary>
        [Test]
        public void TestAddNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Add(null); });
        }

        /// <summary>
        /// Check  that an exception is thrown when attempting to append a node of a different type.
        /// </summary>
        [Test]
        public void TestAddTypeMismatch()
        {
            var list = GenerateListNode();
            Assert.Throws<ArrayTypeMismatchException>(() => { list.Add(new Int32Node(12345)); });
        }

        /// <summary>
        /// Check that clearing the list empties it.
        /// </summary>
        [Test]
        public void TestClear()
        {
            var list = GenerateListNode();
            list.Clear();
            CollectionAssert.IsEmpty(list);
        }

        /// <summary>
        /// Check that an item can be found in the list.
        /// </summary>
        [Test]
        public void TestContains()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.True(list.Contains(node));
        }

        /// <summary>
        /// Check that false is returned when an item can't be found in the list.
        /// </summary>
        [Test]
        public void TestNotContains()
        {
            var list = GenerateListNode();
            var node = new StringNode("contains");
            Assert.False(list.Contains(node));
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to look for null.
        /// </summary>
        [Test]
        public void TestContainsNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Contains(null); });
        }

        /// <summary>
        /// Check that nodes in the list can be copied to an array.
        /// </summary>
        [Test]
        public void TestCopyTo()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            list.CopyTo(array, 0);
            CollectionAssert.AreEqual(list, array);
        }

        /// <summary>
        /// Check that an exception is thrown when the destination array is null.
        /// </summary>
        [Test]
        public void TestCopyToNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.CopyTo(null, 0); });
        }

        /// <summary>
        /// Check that an exception is thrown when the starting index is negative.
        /// </summary>
        [Test]
        public void TestCopyToNegativeIndex()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.CopyTo(array, -3); });
        }

        /// <summary>
        /// Check that an exception is thrown when the starting index is past the bounds of the array.
        /// </summary>
        [Test]
        public void TestCopyToIndexTooLarge()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.Throws<ArgumentException>(() => { list.CopyTo(array, array.Length + 1); });
        }

        /// <summary>
        /// Check that an exception is thrown when the destination array is too small the list.
        /// </summary>
        [Test]
        public void TestCopyToArrayTooSmall()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count - 1];
            Assert.Throws<ArgumentException>(() => { list.CopyTo(array, 0); });
        }

        /// <summary>
        /// Check that removing a node works as expected.
        /// </summary>
        [Test]
        public void TestRemove()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.True(list.Remove(node));
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that nothing happens when attempting to remove a non-existent node.
        /// </summary>
        [Test]
        public void TestRemoveNonExistent()
        {
            var list = GenerateListNode();
            var node = new StringNode("remove");
            Assert.False(list.Remove(node));
            CollectionAssert.AreEqual(NodeSet, list);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to remove null.
        /// </summary>
        [Test]
        public void TestRemoveNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Remove(null); });
        }

        /// <summary>
        /// Check that the count property returns the correct amount.
        /// </summary>
        [Test]
        public void TestCount()
        {
            var list = GenerateListNode();
            Assert.AreEqual(NodeSet.Length, list.Count);
        }

        /// <summary>
        /// Check that the read-only property returns the expected value.
        /// </summary>
        [Test]
        public void TestIsReadOnly()
        {
            var list = GenerateListNode();
            Assert.False(list.IsReadOnly);
        }

        /// <summary>
        /// Check that an item can be found in the list.
        /// </summary>
        [Test]
        public void TestIndexOf()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.AreEqual(1, list.IndexOf(node));
        }

        /// <summary>
        /// Check that a negative index is returned for an item that can't be found in the list.
        /// </summary>
        [Test]
        public void TestIndexOfNonExistent()
        {
            var list = GenerateListNode();
            var node = new StringNode("indexOf");
            Assert.AreEqual(-1, list.IndexOf(node));
        }

        /// <summary>
        /// Check that an exception is thrown when looking for null.
        /// </summary>
        [Test]
        public void TestIndexOfNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.IndexOf(null); });
        }

        /// <summary>
        /// Check that the insert method works.
        /// </summary>
        [Test]
        public void TestInsert()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(1, node);
            list.Insert(1, node);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that inserting an item at the start of the list works as expected.
        /// </summary>
        [Test]
        public void TestInsertFirst()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(0, node);
            list.Insert(0, node);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that inserting an item at the end of the list works as expecte.d
        /// </summary>
        [Test]
        public void TestInsertLast()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(NodeSet.Length, node);
            list.Insert(list.Count, node);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to insert null.
        /// </summary>
        [Test]
        public void TestInsertNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list.Insert(1, null); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a negative index.
        /// </summary>
        [Test]
        public void TestInsertNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.Insert(-1, node); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use an index outside the bounds of the list.
        /// </summary>
        [Test]
        public void TestInsertIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.Insert(list.Count + 1, node); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to insert a node of a different type.
        /// </summary>
        [Test]
        public void TestInsertTypeMismatch()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.Throws<ArrayTypeMismatchException>(() => { list.Insert(1, node); });
        }

        /// <summary>
        /// Check that removing an item at an index works as expected.
        /// </summary>
        [Test]
        public void TestRemoveAt()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            list.RemoveAt(1);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that removing the first item in the list works as expected.
        /// </summary>
        [Test]
        public void TestRemoveAtFirst()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(0);
            list.RemoveAt(0);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that removing the last item in the list works as expected.
        /// </summary>
        [Test]
        public void TestRemoveAtLast()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(NodeSet.Length - 1);
            list.RemoveAt(list.Count - 1);
            CollectionAssert.AreEqual(expected, list);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a negative index.
        /// </summary>
        [Test]
        public void TestRemoveAtNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.RemoveAt(-1); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use an index outside the bounds of the list.
        /// </summary>
        [Test]
        public void TestRemoveAtIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { list.RemoveAt(list.Count + 1); });
        }

        /// <summary>
        /// Check that the getter works as expected.
        /// </summary>
        [Test]
        public void TestGetter()
        {
            var list = GenerateListNode();
            Assert.AreEqual(NodeSet[0], list[0]);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a negative index.
        /// </summary>
        [Test]
        public void TestGetterNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() => { var node = list[-1]; });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use an index outside the bounds of the list.
        /// </summary>
        [Test]
        public void TestGetterIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var node = list[list.Count + 1];
            });
        }

        /// <summary>
        /// Check that the setter works as expected.
        /// </summary>
        [Test]
        public void TestSetter()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            list[1] = node;
            Assert.AreEqual(node, list[1]);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to set an element to null.
        /// </summary>
        [Test]
        public void TestSetterNull()
        {
            var list = GenerateListNode();
            Assert.Throws<ArgumentNullException>(() => { list[1] = null; });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a negative index.
        /// </summary>
        [Test]
        public void TestSetterNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list[-1] = node; });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use an index outside the bounds of the list.
        /// </summary>
        [Test]
        public void TestSetterIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.Throws<ArgumentOutOfRangeException>(() => { list[list.Count + 1] = node; });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to set an element to a different node type.
        /// </summary>
        [Test]
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
