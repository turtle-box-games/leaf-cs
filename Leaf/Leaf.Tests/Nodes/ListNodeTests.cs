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
            Assert.That(node.Type, Is.EqualTo(NodeType.List));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = GenerateListNode();
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Check that the reported node type is correct.")]
        public void TestElementTypeIdEmpty()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType);
            Assert.That(node.ElementType, Is.EqualTo(elementType));
        }

        [Test(Description = "Check that the reported node type is correct.")]
        public void TestElementTypeIdContents()
        {
            const NodeType elementType = NodeType.String;
            var node = new ListNode(elementType, EmptyNodeSet);
            Assert.That(node.ElementType, Is.EqualTo(elementType));
        }

        [Test(Description = "Check that the empty constructor creates an empty list.")]
        public void TestEmptyConstructor()
        {
            var node = new ListNode(NodeType.String);
            Assert.That(node, Is.Empty);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        public void TestContentsConstructor()
        {
            var node = new ListNode(NodeSet.First().Type, NodeSet);
            Assert.That(node, Is.EqualTo(NodeSet));
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void TestNullContentsConstructor()
        {
            Assert.That(() => { new ListNode(NodeType.String, null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown for mixed node types.")]
        public void TestMixedContentsConstructor()
        {
            Assert.That(() => { new ListNode(MixedNodeSet.First().Type, MixedNodeSet); },
                Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        public void TestNullNodeContentsConstructor()
        {
            Assert.That(() => { new ListNode(NullNodeSet.First().Type, NullNodeSet); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that appending a node works.")]
        public void TestAdd()
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
        public void TestAddNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Add(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check  that an exception is thrown when attempting to append a node of a different type.")]
        public void TestAddTypeMismatch()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Add(new Int32Node(12345)); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that clearing the list empties it.")]
        public void TestClear()
        {
            var list = GenerateListNode();
            list.Clear();
            Assert.That(list, Is.Empty);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void TestContains()
        {
            var list = GenerateListNode();
            var node = list[1];
            Assert.That(list, Contains.Item(node));
        }

        [Test(Description = "Check that false is returned when an item can't be found in the list.")]
        public void TestNotContains()
        {
            var list = GenerateListNode();
            var node = new StringNode("contains");
            Assert.That(list, Does.Not.Contain(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to look for null.")]
        public void TestContainsNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Contains(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that nodes in the list can be copied to an array.")]
        public void TestCopyTo()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            list.CopyTo(array, 0);
            Assert.That(array, Is.EqualTo(list));
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        public void TestCopyToNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.CopyTo(null, 0); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        public void TestCopyToNegativeIndex()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.That(() => { list.CopyTo(array, -3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        public void TestCopyToIndexTooLarge()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count];
            Assert.That(() => { list.CopyTo(array, array.Length + 1); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small the list.")]
        public void TestCopyToArrayTooSmall()
        {
            var list  = GenerateListNode();
            var array = new Node[list.Count - 1];
            Assert.That(() => { list.CopyTo(array, 0); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that removing a node works as expected.")]
        public void TestRemove()
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
        public void TestRemoveNonExistent()
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
        public void TestRemoveNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Remove(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the count property returns the correct amount.")]
        public void TestCount()
        {
            var list = GenerateListNode();
            Assert.That(list, Has.Count.EqualTo(NodeSet.Length));
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void TestIsReadOnly()
        {
            var list = GenerateListNode();
            Assert.That(list.IsReadOnly, Is.False);
        }

        [Test(Description = "Check that an item can be found in the list.")]
        public void TestIndexOf()
        {
            const int index = 1;
            var list = GenerateListNode();
            var node = list[index];
            Assert.That(list.IndexOf(node), Is.EqualTo(index));
        }

        [Test(Description = "Check that a negative index is returned for an item that can't be found in the list.")]
        public void TestIndexOfNonExistent()
        {
            var list = GenerateListNode();
            var node = new StringNode("indexOf");
            Assert.That(list.IndexOf(node), Is.EqualTo(-1));
        }

        [Test(Description = "Check that an exception is thrown when looking for null.")]
        public void TestIndexOfNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.IndexOf(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that the insert method adds an item.")]
        public void TestInsert()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            list.Insert(1, node);
            Assert.That(list, Contains.Item(node));
        }

        [Test(Description = "Check that the insert method works for non-end indices.")]
        public void TestInsertMiddle()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(1, node);
            list.Insert(1, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that inserting an item at the start of the list works as expected.")]
        public void TestInsertFirst()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(0, node);
            list.Insert(0, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that inserting an item at the end of the list works as expected.")]
        public void TestInsertLast()
        {
            var list     = GenerateListNode();
            var node     = new StringNode("insert");
            var expected = NodeSet.ToList();
            expected.Insert(NodeSet.Length, node);
            list.Insert(list.Count, node);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert null.")]
        public void TestInsertNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.Insert(1, null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestInsertNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.That(() => { list.Insert(-1, node); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestInsertIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("insert");
            Assert.That(() => { list.Insert(list.Count + 1, node); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that an exception is thrown when attempting to insert a node of a different type.")]
        public void TestInsertTypeMismatch()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.That(() => { list.Insert(1, node); }, Throws.InstanceOf<ArrayTypeMismatchException>());
        }

        [Test(Description = "Check that removing an item at an index works as expected.")]
        public void TestRemoveAt()
        {
            const int index = 1;
            var list = GenerateListNode();
            list.RemoveAt(index);
            Assert.That(list, Does.Not.Contain(NodeSet[index]));
        }

        [Test(Description = "Check that removing an item at a middle index works as expected.")]
        public void TestRemoveAtMiddle()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(1);
            list.RemoveAt(1);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the first item in the list works as expected.")]
        public void TestRemoveAtFirst()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(0);
            list.RemoveAt(0);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that removing the last item in the list works as expected.")]
        public void TestRemoveAtLast()
        {
            var list = GenerateListNode();
            var expected = NodeSet.ToList();
            expected.RemoveAt(NodeSet.Length - 1);
            list.RemoveAt(list.Count - 1);
            Assert.That(list, Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestRemoveAtNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.RemoveAt(-1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestRemoveAtIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.That(() => { list.RemoveAt(list.Count + 1); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the getter works as expected.")]
        public void TestGetter()
        {
            var list = GenerateListNode();
            Assert.That(list[0], Is.EqualTo(NodeSet[0]));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestGetterNegativeIndex()
        {
            var list = GenerateListNode();
            Assert.That(() =>
            {
                var _ = list[-1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestGetterIndexTooLarge()
        {
            var list = GenerateListNode();
            Assert.That(() =>
            {
                var _ = list[list.Count + 1];
            }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description = "Check that the setter works as expected.")]
        public void TestSetter()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            list[1]  = node;
            Assert.That(list[1], Is.EqualTo(node));
        }

        [Test(Description = "Check that an exception is thrown when attempting to set an element to null.")]
        public void TestSetterNull()
        {
            var list = GenerateListNode();
            Assert.That(() => { list[1] = null; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a negative index.")]
        public void TestSetterNegativeIndex()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.That(() => { list[-1] = node; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to use an index outside the bounds of the list.")]
        public void TestSetterIndexTooLarge()
        {
            var list = GenerateListNode();
            var node = new StringNode("setter");
            Assert.That(() => { list[list.Count + 1] = node; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when attempting to set an element to a different node type.")]
        public void TestSetterTypeMismatch()
        {
            var list = GenerateListNode();
            var node = new Int32Node(12345);
            Assert.That(() => { list[1] = node; }, Throws.InstanceOf<ArrayTypeMismatchException>());
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