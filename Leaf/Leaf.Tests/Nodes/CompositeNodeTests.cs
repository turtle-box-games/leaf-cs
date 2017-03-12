using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Leaf.Nodes;

namespace Leaf.Tests.Nodes
{
    [TestFixture]
    public class CompositeNodeTests
    {
        /// <summary>
        /// Check that the reported node type is correct.
        /// </summary>
        [Test]
        public void TestTypeIdEmpty()
        {
            var node = new CompositeNode();
            Assert.AreEqual(NodeType.Composite, node.Type);
        }

        /// <summary>
        /// Check that the version is the expected value.
        /// </summary>
        [Test]
        public void TestVersion()
        {
            var node = GenerateCompositeNode();
            Assert.AreEqual(1, node.Version);
        }

        /// <summary>
        /// Check that the empty constructor creates an empty set.
        /// </summary>
        [Test]
        public void TestEmptyConstructor()
        {
            var node = new CompositeNode();
            Assert.IsEmpty(node);
        }

        /// <summary>
        /// Check that the contents constructor adds the same nodes.
        /// </summary>
        [Test]
        public void TestContentsConstructor()
        {
            var node = new CompositeNode(NodeSet);
            CollectionAssert.AreEqual(NodeSet, node);
        }

        /// <summary>
        /// Check that an exception is thrown for a null set of nodes.
        /// </summary>
        [Test]
        public void TestNullContentsConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => { new CompositeNode(null); });
        }

        /// <summary>
        /// Check that an exception is thrown for null mixed in with nodes.
        /// </summary>
        [Test]
        public void TestNullNodeContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new CompositeNode(NullNodeSet); });
        }

        /// <summary>
        /// Check that an exception is thrown for null as a key.
        /// </summary>
        [Test]
        public void TestNullKeyContentsConstructor()
        {
            Assert.Throws<ArgumentException>(() => { new CompositeNode(NullStringSet); });
        }

        /// <summary>
        /// Check that adding a key-value pair works as expected.
        /// </summary>
        [Test]
        public void TestAddPair()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", new StringNode("pair"));
            node.Add(pair);
            CollectionAssert.Contains(node, pair);
        }

        /// <summary>
        /// Check that an exception is thrown when adding a null node in a key-value pair.
        /// </summary>
        [Test]
        public void TestAddPairNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", null);
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        /// <summary>
        /// Check that an exception is thrown when adding a null key in a key-value pair.
        /// </summary>
        [Test]
        public void TestAddPairNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("pair"));
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        [Test]
        public void TestAddExistingPair()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.Throws<ArgumentException>(() => { node.Add(pair); });
        }

        /// <summary>
        /// Check that the set is empty after clearing it.
        /// </summary>
        [Test]
        public void TestClear()
        {
            var node = GenerateCompositeNode();
            node.Clear();
            Assert.IsEmpty(node);
        }

        /// <summary>
        /// Check that a key-value pair can be found in the set.
        /// </summary>
        [Test]
        public void TestContains()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.IsTrue(node.Contains(pair));
        }

        /// <summary>
        /// Check that false is returned when a key-value pair can't be found in the set.
        /// </summary>
        [Test]
        public void TestContainsFalse()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", new StringNode("false"));
            Assert.IsFalse(node.Contains(pair));
        }

        /// <summary>
        /// Check that an exception is thrown when checking for a null node.
        /// </summary>
        [Test]
        public void TestContainsNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", null);
            Assert.Throws<ArgumentException>(() => { node.Contains(pair); });
        }

        /// <summary>
        /// Check that an exception is thrown when checking for a null key.
        /// </summary>
        [Test]
        public void TestContainsNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("contains"));
            Assert.Throws<ArgumentException>(() => { node.Contains(pair); });
        }

        /// <summary>
        /// Check that nodes in the set can be copied to an array.
        /// </summary>
        [Test]
        public void TestCopyTo()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            set.CopyTo(array, 0);
            CollectionAssert.AreEqual(set, array);
        }

        /// <summary>
        /// Check that an exception is thrown when the destination array is null.
        /// </summary>
        [Test]
        public void TestCopyToNull()
        {
            var set = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { set.CopyTo(null, 0); });
        }

        /// <summary>
        /// Check that an exception is thrown when the starting index is negative.
        /// </summary>
        [Test]
        public void TestCopyToNegativeIndex()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.Throws<ArgumentOutOfRangeException>(() => { set.CopyTo(array, -3); });
        }

        /// <summary>
        /// Check that an exception is thrown when the starting index is past the bounds of the array.
        /// </summary>
        [Test]
        public void TestCopyToIndexTooLarge()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.Throws<ArgumentException>(() => { set.CopyTo(array, array.Length + 1); });
        }

        /// <summary>
        /// Check that an exception is thrown when the destination array is too small for the set.
        /// </summary>
        [Test]
        public void TestCopyToArrayTooSmall()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count - 1];
            Assert.Throws<ArgumentException>(() => { set.CopyTo(array, 0); });
        }

        /// <summary>
        /// Check that removing a key-value pair works as expected.
        /// </summary>
        [Test]
        public void TestRemovePair()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            var pair = list[1];
            list.Remove(pair);
            Assert.IsTrue(node.Remove(pair));
            CollectionAssert.AreEquivalent(list, node);
        }

        /// <summary>
        /// Check that attempting to remove a key-value pair with a different key doesn't remove anything.
        /// </summary>
        [Test]
        public void TestRemovePairDifferentKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("different", NodeSet[1].Value);
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        /// <summary>
        /// Check that attempting to remove a key-value pair with a different node doesn't remove anything.
        /// </summary>
        [Test]
        public void TestRemovePairDifferentNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(NodeSet[1].Key, new FlagNode(true));
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        /// <summary>
        /// Check that attempting to remove an entirely different key-value pair doesn't remove anything.
        /// </summary>
        [Test]
        public void TestRemovePairFalse()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", new FlagNode(false));
            Assert.IsFalse(node.Remove(pair));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        /// <summary>
        /// Check that an exception is thrown when the node is null.
        /// </summary>
        [Test]
        public void TestRemovePairNullNode()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", null);
            Assert.Throws<ArgumentException>(() => { node.Remove(pair); });
        }

        /// <summary>
        /// Check that an exception is thrown when the key is null.
        /// </summary>
        [Test]
        public void TestRemovePairNullKey()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("remove"));
            Assert.Throws<ArgumentException>(() => { node.Remove(pair); });
        }

        /// <summary>
        /// Check that the count is the expected amount.
        /// </summary>
        [Test]
        public void TestCount()
        {
            var node = GenerateCompositeNode();
            Assert.AreEqual(NodeSet.Length, node.Count);
        }

        /// <summary>
        /// Check that the read-only property returns the expected value.
        /// </summary>
        [Test]
        public void TestIsReadOnly()
        {
            var node = GenerateCompositeNode();
            Assert.IsFalse(node.IsReadOnly);
        }

        /// <summary>
        /// Check that an existing key is found in the set.
        /// </summary>
        [Test]
        public void TestContainsKey()
        {
            var node = GenerateCompositeNode();
            var key  = NodeSet[1].Key;
            Assert.IsTrue(node.ContainsKey(key));
        }

        /// <summary>
        /// Check that a non-existent key is not found in the set.
        /// </summary>
        [Test]
        public void TestContainsKeyFalse()
        {
            var node = GenerateCompositeNode();
            const string key = "contains";
            Assert.IsFalse(node.ContainsKey(key));
        }

        /// <summary>
        /// Check that an exception is thrown when looking for a null key.
        /// </summary>
        [Test]
        public void TestContainsKeyNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.ContainsKey(null); });
        }

        /// <summary>
        /// Check that adding a new node works as expected.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var node  = GenerateCompositeNode();
            var other = new KeyValuePair<string, Node>("Add", new StringNode("node"));
            var list  = NodeSet.ToList();
            list.Add(other);
            node.Add(other.Key, other.Value);
            CollectionAssert.AreEquivalent(list, node);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to add a node with an existing key.
        /// </summary>
        [Test]
        public void TestAddExisting()
        {
            var node = GenerateCompositeNode();
            var existing = NodeSet[1];
            Assert.Throws<ArgumentException>(() => { node.Add(existing.Key, existing.Value); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null key.
        /// </summary>
        [Test]
        public void TestAddNullKey()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Add(null, new FlagNode(false)); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to add a null node.
        /// </summary>
        [Test]
        public void TestAddNullNode()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Add("Add", null); });
        }

        /// <summary>
        /// Check that removing a node by its key works as expected.
        /// </summary>
        [Test]
        public void TestRemove()
        {
            var node  = GenerateCompositeNode();
            var other = NodeSet[1];
            var list  = NodeSet.ToList();
            list.Remove(other);
            Assert.IsTrue(node.Remove(other.Key));
            CollectionAssert.AreEquivalent(list, node);
        }

        /// <summary>
        /// Check that removing a non-existent node doesn't modify the set.
        /// </summary>
        [Test]
        public void TestRemoveFalse()
        {
            var node = GenerateCompositeNode();
            Assert.IsFalse(node.Remove("Remove"));
            CollectionAssert.AreEquivalent(NodeSet, node);
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null key.
        /// </summary>
        [Test]
        public void TestRemoveNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node.Remove(null); });
        }

        /// <summary>
        /// Check that a node can be found by its key.
        /// </summary>
        [Test]
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

        /// <summary>
        /// Check that false is returned when a node can't be found.
        /// </summary>
        [Test]
        public void TestTryGetValueFalse()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.False(node.TryGetValue("TryGetValue", out result));
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null key.
        /// </summary>
        [Test]
        public void TestTryGetValueNull()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.Throws<ArgumentNullException>(() => { node.TryGetValue(null, out result); });
        }

        /// <summary>
        /// Check that a node can be retrieved.
        /// </summary>
        [Test]
        public void TestGetter()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet.First();
            var expected = pair.Value;
            var key = pair.Key;
            Assert.AreEqual(expected, node[key]);
        }

        /// <summary>
        /// Check that an exception is thrown when a key doesn't exist.
        /// </summary>
        [Test]
        public void TestGetterNotFound()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var foo = node["404"];
            });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null key.
        /// </summary>
        [Test]
        public void TestGetterNull()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() =>
            {
                var foo = node[null];
            });
        }

        /// <summary>
        /// Check that updating an existing key works as expected.
        /// </summary>
        [Test]
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

        /// <summary>
        /// Check that setting a new key and node works as expected.
        /// </summary>
        [Test]
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

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null key.
        /// </summary>
        [Test]
        public void TestSetterNullKey()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node[null] = new StringNode("setter"); });
        }

        /// <summary>
        /// Check that an exception is thrown when attempting to use a null node.
        /// </summary>
        [Test]
        public void TestSetterNullNode()
        {
            var node = GenerateCompositeNode();
            Assert.Throws<ArgumentNullException>(() => { node["foo"] = null; });
        }

        /// <summary>
        /// Check that an expected set of keys is returned.
        /// </summary>
        [Test]
        public void TestKeys()
        {
            var node = GenerateCompositeNode();
            var values = NodeSet.Select(pair => pair.Key);
            CollectionAssert.AreEquivalent(values, node.Keys);
        }

        /// <summary>
        /// Check that an expected set of nodes is returned.
        /// </summary>
        [Test]
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
