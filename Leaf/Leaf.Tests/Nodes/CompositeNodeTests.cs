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
        public void TypeIdEmptyTest()
        {
            var node = new CompositeNode();
            Assert.That(node.Type, Is.EqualTo(NodeType.Composite));
        }

        [Test(Description = "Check that the version is the expected value.")]
        public void VersionTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Check that the empty constructor creates an empty set.")]
        public void EmptyConstructorTest()
        {
            var node = new CompositeNode();
            Assert.That(node, Is.Empty);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        public void ContentsConstructorTest()
        {
            var node = new CompositeNode(NodeSet);
            Assert.That(node, Is.EquivalentTo(NodeSet));
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void NullContentsConstructorTest()
        {
            Assert.That(() => { new CompositeNode(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        public void NullNodeContentsConstructorTest()
        {
            Assert.That(() => { new CompositeNode(NullNodeSet); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown for null as a key.")]
        public void NullKeyContentsConstructorTest()
        {
            Assert.That(() => { new CompositeNode(NullStringSet); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that adding a key-value pair works as expected.")]
        public void AddPairTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", new StringNode("pair"));
            node.Add(pair);
            Assert.That(node, Contains.Item(pair));
        }

        [Test(Description = "Check that an exception is thrown when adding a null node in a key-value pair.")]
        public void AddPairNullNodeTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("add", null);
            Assert.That(() => { node.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when adding a null key in a key-value pair.")]
        public void AddPairNullKeyTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("pair"));
            Assert.That(() => { node.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when adding an existing key-value pair.")]
        public void AddExistingPairTest()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.That(() => { node.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that the set is empty after clearing it.")]
        public void ClearTest()
        {
            var node = GenerateCompositeNode();
            node.Clear();
            Assert.That(node, Is.Empty);
        }

        [Test(Description = "Check that a key-value pair can be found in the set.")]
        public void ContainsTest()
        {
            var node = GenerateCompositeNode();
            var pair = NodeSet[1];
            Assert.That(node, Contains.Item(pair));
        }

        [Test(Description = "Check that false is returned when a key-value pair can't be found in the set.")]
        public void ContainsFalseTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", new StringNode("false"));
            Assert.That(node, Does.Not.Contain(pair));
        }

        [Test(Description = "Check that an exception is thrown when checking for a null node.")]
        public void ContainsNullNodeTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("contains", null);
            Assert.That(() => { node.Contains(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when checking for a null key.")]
        public void ContainsNullKeyTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("contains"));
            Assert.That(() => { node.Contains(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that nodes in the set can be copied to an array.")]
        public void CopyToTest()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            set.CopyTo(array, 0);
            Assert.That(array, Is.EquivalentTo(set));
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        public void CopyToNullTest()
        {
            var set = GenerateCompositeNode();
            Assert.That(() => { set.CopyTo(null, 0); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        public void CopyToNegativeIndexTest()
        {
            var set = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.That(() => { set.CopyTo(array, -3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        public void CopyToIndexTooLargeTest()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count];
            Assert.That(() => { set.CopyTo(array, array.Length + 1); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small for the set.")]
        public void CopyToArrayTooSmallTest()
        {
            var set   = GenerateCompositeNode();
            var array = new KeyValuePair<string, Node>[set.Count - 1];
            Assert.That(() => { set.CopyTo(array, 0); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that removing a key-value pair works as expected.")]
        public void RemovePairTest()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            var pair = list[1];
            list.Remove(pair);
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove(pair), Is.True);
                Assert.That(node, Is.EquivalentTo(list));
            });
        }

        [Test(Description =
            "Check that attempting to remove a key-value pair with a different key doesn't remove anything.")]
        public void RemovePairDifferentKeyTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("different", NodeSet[1].Value);
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove(pair), Is.False);
                Assert.That(node, Is.EquivalentTo(NodeSet));
            });
        }

        [Test(Description =
            "Check that attempting to remove a key-value pair with a different node doesn't remove anything.")]
        public void RemovePairDifferentNodeTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(NodeSet[1].Key, new FlagNode(true));
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove(pair), Is.False);
                Assert.That(node, Is.EquivalentTo(NodeSet));
            });
        }

        [Test(Description =
            "Check that attempting to remove an entirely different key-value pair doesn't remove anything.")]
        public void RemovePairFalseTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", new FlagNode(false));
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove(pair), Is.False);
                Assert.That(node, Is.EquivalentTo(NodeSet));
            });
        }

        [Test(Description = "Check that an exception is thrown when the node is null.")]
        public void RemovePairNullNodeTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>("remove", null);
            Assert.That(() => { node.Remove(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the key is null.")]
        public void RemovePairNullKeyTest()
        {
            var node = GenerateCompositeNode();
            var pair = new KeyValuePair<string, Node>(null, new StringNode("remove"));
            Assert.That(() => { node.Remove(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that the count is the expected amount.")]
        public void CountTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(node.Count, Is.EqualTo(NodeSet.Length));
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void IsReadOnlyTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(node.IsReadOnly, Is.False);
        }

        [Test(Description = "Check that an existing key is found in the set.")]
        public void ContainsKeyTest()
        {
            var node = GenerateCompositeNode();
            var key = NodeSet[1].Key;
            Assert.That(node.ContainsKey(key), Is.True);
        }

        [Test(Description = "Check that a non-existent key is not found in the set.")]
        public void ContainsKeyFalseTest()
        {
            var node = GenerateCompositeNode();
            const string key = "contains";
            Assert.That(node.ContainsKey(key), Is.False);
        }

        [Test(Description = "Check that an exception is thrown when looking for a null key.")]
        public void ContainsKeyNullTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node.ContainsKey(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that adding a new node works as expected.")]
        public void AddTest()
        {
            var node  = GenerateCompositeNode();
            var other = new KeyValuePair<string, Node>("Add", new StringNode("node"));
            var list  = NodeSet.ToList();
            list.Add(other);
            node.Add(other.Key, other.Value);
            Assert.That(node, Is.EquivalentTo(list));
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a node with an existing key.")]
        public void AddExistingTest()
        {
            var node     = GenerateCompositeNode();
            var existing = NodeSet[1];
            Assert.That(() => { node.Add(existing.Key, existing.Value); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void AddNullKeyTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node.Add(null, new FlagNode(false)); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a null node.")]
        public void AddNullNodeTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node.Add("Add", null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that removing a node by its key works as expected.")]
        public void RemoveTest()
        {
            var node  = GenerateCompositeNode();
            var other = NodeSet[1];
            var list  = NodeSet.ToList();
            list.Remove(other);
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove(other.Key), Is.True);
                Assert.That(node, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that removing a non-existent node doesn't modify the set.")]
        public void RemoveFalseTest()
        {
            var node = GenerateCompositeNode();
            Assert.Multiple(() =>
            {
                Assert.That(node.Remove("Remove"), Is.False);
                Assert.That(node, Is.EquivalentTo(NodeSet));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void RemoveNullTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node.Remove(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that a node can be found by its key.")]
        public void TryGetValueTest()
        {
            var node     = GenerateCompositeNode();
            var pair     = NodeSet[1];
            var expected = pair.Value;
            var key      = pair.Key;
            Assert.Multiple(() =>
            {
                Node result;
                Assert.That(node.TryGetValue(key, out result), Is.True);
                Assert.That(result, Is.EqualTo(expected));
            });
        }

        [Test(Description = "Check that false is returned when a node can't be found.")]
        public void TryGetValueFalseTest()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.That(node.TryGetValue("TryGetValue", out result), Is.False);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void TryGetValueNullTest()
        {
            var node = GenerateCompositeNode();
            Node result;
            Assert.That(() => { node.TryGetValue(null, out result); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that a node can be retrieved.")]
        public void GetterTest()
        {
            var node     = GenerateCompositeNode();
            var pair     = NodeSet.First();
            var expected = pair.Value;
            var key      = pair.Key;
            Assert.That(node[key], Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when a key doesn't exist.")]
        public void GetterNotFoundTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() =>
            {
                var _ = node["404"];
            }, Throws.InstanceOf<KeyNotFoundException>());
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void GetterNullTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() =>
            {
                var _ = node[null];
            }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that updating an existing key works as expected.")]
        public void SetterTest()
        {
            var node    = GenerateCompositeNode();
            var list    = NodeSet.ToList();
            var key     = NodeSet[1].Key;
            var newNode = new StringNode("setter");
            list[1]     = new KeyValuePair<string, Node>(key, newNode);
            node[key]   = newNode;
            Assert.Multiple(() =>
            {
                Assert.That(node[key], Is.EqualTo(newNode));
                Assert.That(node, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that setting a new key and node works as expected.")]
        public void SetterNewTest()
        {
            var node = GenerateCompositeNode();
            var list = NodeSet.ToList();
            const string key = "new";
            var newNode = new StringNode("setter");
            list.Add(new KeyValuePair<string, Node>(key, newNode));
            node[key] = newNode;
            Assert.Multiple(() =>
            {
                Assert.That(node[key], Is.EqualTo(newNode));
                Assert.That(node, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        public void SetterNullKeyTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node[null] = new StringNode("setter"); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null node.")]
        public void SetterNullNodeTest()
        {
            var node = GenerateCompositeNode();
            Assert.That(() => { node["foo"] = null; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an expected set of keys is returned.")]
        public void KeysTest()
        {
            var node = GenerateCompositeNode();
            var keys = NodeSet.Select(pair => pair.Key);
            Assert.That(node.Keys, Is.EquivalentTo(keys));
        }

        [Test(Description = "Check that an expected set of nodes is returned.")]
        public void ValuesTest()
        {
            var node   = GenerateCompositeNode();
            var values = NodeSet.Select(pair => pair.Value);
            Assert.That(node.Values, Is.EquivalentTo(values));
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