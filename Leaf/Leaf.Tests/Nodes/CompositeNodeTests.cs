using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture(TestOf = typeof(CompositeNode))]
    public class CompositeNodeTests
    {
        [Test(Description = "Check that the reported node type is correct.")]
        public void TestTypeIdEmpty()
        {
            var node = new CompositeNode();
            Assert.AreEqual(NodeType.Composite, node.Type);
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void TestVersion()
        {
            var node = GenerateCompositeNode();
            Assert.AreEqual(1, node.Version);
        }

        [Test(Description = "Check that the empty constructor creates an empty set.")]
        public void TestEmptyConstructor()
        {
            var node = new CompositeNode();
            Assert.IsEmpty(node);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        public void TestContentsConstructor()
        {
            var node = new CompositeNode(NodeSet);
            CollectionAssert.AreEqual(NodeSet, node);
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void TestNullContentsConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { new CompositeNode(null); });
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        public void TestNullNodeContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new CompositeNode(NullNodeSet); });
        }

        [Test(Description = "Check that an exception is thrown for null as a key.")]
        public void TestNullKeyContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new CompositeNode(NullStringSet); });
        }

        [Test(Description = "Check that adding a key-value pair works as expected.")]
        public void TestAddPair()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", new StringNode("pair"));
            node.Add(pair);
            CollectionAssert.Contains(node, pair);
        }

        [Test(Description = "Check that an exception is thrown when adding a null node in a key-value pair.")]
        public void TestAddPairNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", null);
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        [Test(Description = "Check that an exception is thrown when adding a null key in a key-value pair.")]
        public void TestAddPairNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("pair"));
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        [Test(Description = "Check that an exception is thrown when adding an existing key-value pair.")]
        public void TestAddExistingPair()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        [Test(Description = "Check that the set is empty after clearing it.")]
        public void TestClear()
        {
            var node = GenerateCompositeNode();
            node.Clear();
            Assert.IsEmpty(node);
        }

        [Test(Description = "Check that a key-value pair can be found in the set.")]
        public void TestContains()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.IsTrue(node.Contains(pair));
        }

        [Test(Description = "Check that false is returned when a key-value pair can't be found in the set.")]
        public void TestContainsFalse()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", new StringNode("false"));
            Assert.IsFalse(node.Contains(pair));
        }

        [Test(Description = "Check that an exception is thrown when checking for a null node.")]
        public void TestContainsNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", null);
            Assert.Throws<ArgumentException>(() => { node.Contains(pair); });
        }

        [Test(Description = "Check that an exception is thrown when checking for a null key.")]
        public void TestContainsNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("contains"));
            Assert.Throws<ArgumentException>(() => { node.Contains(pair); });
        }

        [Test(Description = "Check that nodes in the set can be copied to an array.")]
        public void TestCopyTo()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            set.CopyTo(array, 0);
            CollectionAssert.AreEqual(set, array);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        public void TestCopyToNull()
        {
            var set = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { set.CopyTo(null, 0); });
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        public void TestCopyToNegativeIndex()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.Throws<ArgumentOutOfRangeException>(() => { set.CopyTo(array, -3); });
        }

        [Test(Description = "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        public void TestCopyToIndexTooLarge()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.Throws<ArgumentException>(() => { set.CopyTo(array, array.Length + 1); });
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small for the set.")]
        public void TestCopyToArrayTooSmall()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count - 1];
            Assert.Throws<ArgumentException>(() => { set.CopyTo(array, 0); });
        }

        [Test(Description = "Check that removing a key-value pair works as expected.")]
        public void TestRemovePair()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            var pair = list[1];
            list.Remove(pair);
            Assert.IsTrue(node.Remove(pair));
            CollectionAssert.AreEquivalent(list, node);
        }

        [Test(Description = "Check that attempting to remove a key-value pair with a different key doesn't remove anything.")]
        public void TestRemovePairDifferentKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("different", NodeSet[1].Value);
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        [Test(Description = "Check that attempting to remove a key-value pair with a different node doesn't remove anything.")]
        public void TestRemovePairDifferentNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(NodeSet[1].Key, new FlagNode(true));
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        [Test(Description = "Check that attempting to remove an entirely different key-value pair doesn't remove anything.")]
        public void TestRemovePairFalse()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", new FlagNode(false));
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        [Test(Description = "Check that an exception is thrown when the node is null.")]
        public void TestRemovePairNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", null);
            Assert.Throws<ArgumentException>(() => { node.Remove(pair); });
        }

        [Test(Description = "Check that an exception is thrown when the key is null.")]
        public void TestRemovePairNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("remove"));
            Assert.Throws<ArgumentException>(() => { node.Remove(pair); });
        }

        [Test(Description = "Check that the count is the expected amount.")]
        public void TestCount()
        {
            var node = GenerateCompositeNode();
            Assert.AreEqual(NodeSet.Length, node.Count);
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void TestIsReadOnly()
        {
            var node = GenerateCompositeNode();
            Assert.IsFalse(node.IsReadOnly);
        }

        [Test(Description = "Check that an existing key is found in the set.")]
        public void TestContainsKey()
        {
            var node = GenerateCompositeNode();
            var key  = NodeSet[1].Key;
            Assert.IsTrue(node.ContainsKey(key));
        }

        [Test(Description = "Check that a non-existent key is not found in the set.")]
        public void TestContainsKeyFalse()
        {
            var node = GenerateCompositeNode();
            const string key = "contains";
            Assert.IsFalse(node.ContainsKey(key));
        }

        [Test(Description = "Check that an exception is thrown when looking for a null key.")]
        public void TestContainsKeyNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.ContainsKey(null); });
        }

        [Test(Description = "Check that adding a new node works as expected.")]
        public void TestAdd()
        {
            var node  = GenerateCompositeNode();
            var other = new KeyValuePair<string, Node>("Add", new StringNode("node"));
            var list  = NodeSet.ToList();
            list.Add(other);
            node.Add(other.Key, other.Value);
            CollectionAssert.AreEquivalent(list, node);
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a node with an existing key.")]
        public void TestAddExisting()
        {
            var node = GenerateCompositeNode();
            var existing = NodeSet[1];
            Assert.Throws<ArgumentException>(() => { node.Add(existing.Key, existing.Value); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TestAddNullKey()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Add(null, new FlagNode(false)); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a null node.")]
        public void TestAddNullNode()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Add("Add", null); });
        }

        [Test(Description = "Check that removing a node by its key works as expected.")]
        public void TestRemove()
        {
            var node  = GenerateCompositeNode();
            var other = NodeSet[1];
            var list  = NodeSet.ToList();
            list.Remove(other);
            Assert.IsTrue(node.Remove(other.Key));
            CollectionAssert.AreEquivalent(list, node);
        }

        [Test(Description = "Check that removing a non-existent node doesn't modify the set.")]
        public void TestRemoveFalse()
        {
            var node = GenerateCompositeNode();
            Assert.IsFalse(node.Remove("Remove"));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TestRemoveNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Remove(null); });
        }

        [Test(Description = "Check that a node can be found by its key.")]
        public void TestTryGetValue()
        {
            var node = GenerateCompositeNode();
            Node result;
            var pair = NodeSet[1];
            var expected = pair.Value;
            var key = pair.Key;
            Assert.IsTrue(node.TryGetValue(key, out result));
            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Check that false is returned when a node can't be found.")]
        public void TestTryGetValueFalse()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.False(node.TryGetValue("TryGetValue", out result));
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TestTryGetValueNull()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.Throws<ArgumentNullException>(() => { node.TryGetValue(null, out result); });
        }

        [Test(Description = "Check that a node can be retrieved.")]
        public void TestGetter()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet.First();
            var expected = pair.Value;
            var key = pair.Key;
            Assert.AreEqual(expected, node[key]);
        }

        [Test(Description = "Check that an exception is thrown when a key doesn't exist.")]
        public void TestGetterNotFound()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var foo = node["404"];
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TestGetterNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                var foo = node[null];
            });
        }

        [Test(Description = "Check that updating an existing key works as expected.")]
        public void TestSetter()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            var key  = NodeSet[1].Key;
            var newNode = new StringNode("setter");
            list[1] = new KeyValuePair<string, Node>(key, newNode);
            node[key] = newNode;
            Assert.AreEqual(node[key], newNode);
            CollectionAssert.AreEquivalent(list, node);
        }

        [Test(Description = "Check that setting a new key and node works as expected.")]
        public void TestSetterNew()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            const string key = "new";
            var newNode = new StringNode("setter");
            list.Add(new KeyValuePair<string, Node>(key, newNode));
            node[key] = newNode;
            CollectionAssert.AreEquivalent(list, node);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TestSetterNullKey()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node[null] = new StringNode("setter"); });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null node.")]
        public void TestSetterNullNode()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node["foo"] = null; });
        }

        [Test(Description = "Check that an expected set of keys is returned.")]
        public void TestKeys()
        {
            var node = GenerateCompositeNode();
            var values = NodeSet.Select(pair => pair.Key);
            CollectionAssert.AreEquivalent(values, node.Keys);
        }

        [Test(Description = "Check that an expected set of nodes is returned.")]
        public void TestValues()
        {
            var node = GenerateCompositeNode();
            var values = NodeSet.Select(pair => pair.Value);
            CollectionAssert.AreEquivalent(values, node.Values);
        }

        private static readonly KeyValuePair<string, Node>[] NodeSet =
        {
            new KeyValuePair<string, Node>("foo", new StringNode("bar")),
            new KeyValuePair<string, Node>("num", new Int32Node(12345)),
            new KeyValuePair<string, Node>("dec", new Float32Node(1.23f))
        };

        private static readonly KeyValuePair<string, Node>[] NullNodeSet =
        {
            new KeyValuePair<string, Node>("foo", new StringNode("bar")),
            new KeyValuePair<string, Node>("num", null),
            new KeyValuePair<string, Node>("dec", new Float32Node(1.23f))
        };

        private static readonly KeyValuePair<string, Node>[] NullStringSet =
        {
            new KeyValuePair<string, Node>("foo", new StringNode("bar")),
            new KeyValuePair<string, Node>(null, new Int32Node(12345)),
            new KeyValuePair<string, Node>("dec", new Float32Node(1.23f))
        };

        private static CompositeNode GenerateCompositeNode()
        {
            return new CompositeNode(NodeSet);
        }
    }
}
