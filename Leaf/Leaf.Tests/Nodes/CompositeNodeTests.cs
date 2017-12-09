using System;
using System.Collections.Generic;
using System.Linq;
using Leaf.Nodes;
using NUnit.Framework;

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
            var node = new CompositeNode();
            Assert.That(node.Version, Is.EqualTo(1));
        }

        [Test(Description = "Check that the empty constructor creates an empty set.")]
        public void EmptyConstructorTest()
        {
            var node = new CompositeNode();
            Assert.That(node, Is.Empty);
        }

        [Test(Description = "Check that the contents constructor adds the same nodes.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void ContentsConstructorTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(compositeNode, Is.EquivalentTo(nodePairs));
        }

        [Test(Description = "Check that an exception is thrown for a null set of nodes.")]
        public void NullContentsConstructorTest()
        {
            Assert.That(() => { new CompositeNode(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown for null mixed in with nodes.")]
        [TestCaseSource(nameof(NodePairsWithNullValue))]
        [TestCaseSource(nameof(NodePairsWithNullName))]
        public void NullNodeContentsConstructorTest(KeyValuePair<string, Node>[] nodePairs)
        {
            Assert.That(() => { new CompositeNode(nodePairs); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that adding a key-value pair works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void AddPairTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = NodeBuilders.GenerateNamedNode(TestContext.CurrentContext.Random);
            compositeNode.Add(pair);
            Assert.That(compositeNode, Contains.Item(pair));
        }

        [Test(Description = "Check that an exception is thrown when adding a null node in a key-value pair.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void AddPairNullNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = new KeyValuePair<string, Node>("add", null);
            Assert.That(() => { compositeNode.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when adding a null key in a key-value pair.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void AddPairNullKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            var pair          = new KeyValuePair<string, Node>(null, newNode);
            Assert.That(() => { compositeNode.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when adding an existing key-value pair.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void AddExistingPairTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = nodePairs[1];
            Assert.That(() => { compositeNode.Add(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that the set is empty after clearing it.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void ClearTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            compositeNode.Clear();
            Assert.That(compositeNode, Is.Empty);
        }

        [Test(Description = "Check that a key-value pair can be found in the set.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void ContainsTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = nodePairs[1];
            Assert.That(compositeNode, Contains.Item(pair));
        }

        [Test(Description = "Check that false is returned when a key-value pair can't be found in the set.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void ContainsFalseTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = NodeBuilders.GenerateNamedNode(TestContext.CurrentContext.Random);
            Assert.That(compositeNode, Does.Not.Contain(pair));
        }

        [Test(Description = "Check that an exception is thrown when checking for a null node.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void ContainsNullNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = new KeyValuePair<string, Node>("contains", null);
            Assert.That(() => { compositeNode.Contains(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when checking for a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void ContainsNullKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            var pair          = new KeyValuePair<string, Node>(null, newNode);
            Assert.That(() => { compositeNode.Contains(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that nodes in the set can be copied to an array.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        public void CopyToTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var array         = new KeyValuePair<string, Node>[compositeNode.Count];
            compositeNode.CopyTo(array, 0);
            Assert.That(array, Is.EquivalentTo(compositeNode));
        }

        [Test(Description = "Check that an exception is thrown when the destination array is null.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void CopyToNullTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() => { compositeNode.CopyTo(null, 0); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when the starting index is negative.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void CopyToNegativeIndexTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var array         = new KeyValuePair<string, Node>[compositeNode.Count];
            Assert.That(() => { compositeNode.CopyTo(array, -3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test(Description =
            "Check that an exception is thrown when the starting index is past the bounds of the array.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void CopyToIndexTooLargeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var array         = new KeyValuePair<string, Node>[compositeNode.Count];
            Assert.That(() => { compositeNode.CopyTo(array, array.Length + 1); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the destination array is too small for the set.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void CopyToArrayTooSmallTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var array         = new KeyValuePair<string, Node>[compositeNode.Count - 1];
            Assert.That(() => { compositeNode.CopyTo(array, 0); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that removing a key-value pair works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemovePairTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var list          = nodePairs.ToList();
            var pair          = list[1];
            list.Remove(pair);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove(pair), Is.True);
                Assert.That(compositeNode, Is.EquivalentTo(list));
            });
        }

        [Test(Description =
            "Check that attempting to remove a key-value pair with a different key doesn't remove anything.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemovePairDifferentKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = new KeyValuePair<string, Node>("different", nodePairs[1].Value);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove(pair), Is.False);
                Assert.That(compositeNode, Is.EquivalentTo(nodePairs));
            });
        }

        [Test(Description =
            "Check that attempting to remove a key-value pair with a different node doesn't remove anything.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemovePairDifferentNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            var pair          = new KeyValuePair<string, Node>(nodePairs[1].Key, newNode);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove(pair), Is.False);
                Assert.That(compositeNode, Is.EquivalentTo(nodePairs));
            });
        }

        [Test(Description =
            "Check that attempting to remove an entirely different key-value pair doesn't remove anything.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemovePairFalseTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = NodeBuilders.GenerateNamedNode(TestContext.CurrentContext.Random);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove(pair), Is.False);
                Assert.That(compositeNode, Is.EquivalentTo(nodePairs));
            });
        }

        [Test(Description = "Check that an exception is thrown when the node is null.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void RemovePairNullNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = new KeyValuePair<string, Node>("remove", null);
            Assert.That(() => { compositeNode.Remove(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when the key is null.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void RemovePairNullKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = new KeyValuePair<string, Node>(null, new StringNode("remove"));
            Assert.That(() => { compositeNode.Remove(pair); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that the count is the expected amount.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void CountTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(compositeNode.Count, Is.EqualTo(nodePairs.Length));
        }

        [Test(Description = "Check that the read-only property returns the expected value.")]
        public void IsReadOnlyTest()
        {
            var compositeNode = new CompositeNode();
            Assert.That(compositeNode.IsReadOnly, Is.False);
        }

        [Test(Description = "Check that an existing key is found in the set.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void ContainsKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var key           = nodePairs[1].Key;
            Assert.That(compositeNode.ContainsKey(key), Is.True);
        }

        [Test(Description = "Check that a non-existent key is not found in the set.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void ContainsKeyFalseTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            const string key  = "contains";
            Assert.That(compositeNode.ContainsKey(key), Is.False);
        }

        [Test(Description = "Check that an exception is thrown when looking for a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void ContainsKeyNullTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() => { compositeNode.ContainsKey(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that adding a new node works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void AddTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var other         = NodeBuilders.GenerateNamedNode(TestContext.CurrentContext.Random);
            var list          = nodePairs.ToList();
            list.Add(other);
            compositeNode.Add(other.Key, other.Value);
            Assert.That(compositeNode, Is.EquivalentTo(list));
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a node with an existing key.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void AddExistingTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var existing      = nodePairs[1];
            Assert.That(() => { compositeNode.Add(existing.Key, existing.Value); }, Throws.ArgumentException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void AddNullKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            Assert.That(() => { compositeNode.Add(null, newNode); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to add a null node.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void AddNullNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() => { compositeNode.Add("Add", null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that removing a node by its key works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemoveTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var other         = nodePairs[1];
            var list          = nodePairs.ToList();
            list.Remove(other);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove(other.Key), Is.True);
                Assert.That(compositeNode, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that removing a non-existent node doesn't modify the set.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void RemoveFalseTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode.Remove("Remove"), Is.False);
                Assert.That(compositeNode, Is.EquivalentTo(nodePairs));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void RemoveNullTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() => { compositeNode.Remove(null); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that a node can be found by its key.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void TryGetValueTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = nodePairs[1];
            var expected      = pair.Value;
            var key           = pair.Key;
            Assert.Multiple(() =>
            {
                Node result;
                Assert.That(compositeNode.TryGetValue(key, out result), Is.True);
                Assert.That(result, Is.EqualTo(expected));
            });
        }

        [Test(Description = "Check that false is returned when a node can't be found.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void TryGetValueFalseTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Node result;
            Assert.That(compositeNode.TryGetValue("TryGetValue", out result), Is.False);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void TryGetValueNullTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Node result;
            Assert.That(() => { compositeNode.TryGetValue(null, out result); }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that a node can be retrieved.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void GetterTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var pair          = nodePairs.First();
            var expected      = pair.Value;
            var key           = pair.Key;
            Assert.That(compositeNode[key], Is.EqualTo(expected));
        }

        [Test(Description = "Check that an exception is thrown when a key doesn't exist.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void GetterNotFoundTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() =>
            {
                var _ = compositeNode["404"];
            }, Throws.InstanceOf<KeyNotFoundException>());
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void GetterNullTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() =>
            {
                var _ = compositeNode[null];
            }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that updating an existing key works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void SetterTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var list          = nodePairs.ToList();
            var key           = nodePairs[1].Key;
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            list[1]           = new KeyValuePair<string, Node>(key, newNode);
            compositeNode[key] = newNode;
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode[key], Is.EqualTo(newNode));
                Assert.That(compositeNode, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that setting a new key and node works as expected.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void SetterNewTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var randomizer    = TestContext.CurrentContext.Random;
            var compositeNode = new CompositeNode(nodePairs);
            var list          = nodePairs.ToList();
            var key           = randomizer.GetString();
            var newNode       = randomizer.NextNode();
            list.Add(new KeyValuePair<string, Node>(key, newNode));
            compositeNode[key] = newNode;
            Assert.Multiple(() =>
            {
                Assert.That(compositeNode[key], Is.EqualTo(newNode));
                Assert.That(compositeNode, Is.EquivalentTo(list));
            });
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null key.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void SetterNullKeyTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var newNode       = TestContext.CurrentContext.Random.NextNode();
            Assert.That(() => { compositeNode[null] = newNode; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an exception is thrown when attempting to use a null node.")]
        [TestCaseSource(nameof(RandomNodesCollections))]
        public void SetterNullNodeTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            Assert.That(() => { compositeNode["foo"] = null; }, Throws.ArgumentNullException);
        }

        [Test(Description = "Check that an expected set of keys is returned.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void KeysTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var keys          = nodePairs.Select(pair => pair.Key);
            Assert.That(compositeNode.Keys, Is.EquivalentTo(keys));
        }

        [Test(Description = "Check that an expected set of nodes is returned.")]
        [TestCaseSource(nameof(AllNodesCollections))]
        [TestCaseSource(nameof(PairsOfListNodes))]
        [TestCaseSource(nameof(PairsOfCompositeNodes))]
        public void ValuesTest(KeyValuePair<string, Node>[] nodePairs)
        {
            var compositeNode = new CompositeNode(nodePairs);
            var values        = nodePairs.Select(pair => pair.Value);
            Assert.That(compositeNode.Values, Is.EquivalentTo(values));
        }
        
        private static IEnumerable<KeyValuePair<string, Node>[]> RandomNodesCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var nodePairs = NodeBuilders.GenerateMultipleNamedNodes(randomizer);
                yield return nodePairs.ToArray();
            }
        }
        
        private static IEnumerable<KeyValuePair<string, Node>[]> AllNodesCollections()
        {
            var randomizer = TestContext.CurrentContext.Random;
            foreach (var elementType in Enum.GetValues(typeof(NodeType)).Cast<NodeType>())
            {
                if (elementType == NodeType.End || elementType == NodeType.List || elementType == NodeType.Composite)
                    continue;
                var nodes = NodeBuilders.GenerateMultipleOfType(randomizer, elementType).ToArray();
                yield return nodes.Select((node) =>
                {
                    var name = randomizer.GetString();
                    return new KeyValuePair<string, Node>(name, node);
                }).ToArray();
            }
        }

        private static IEnumerable<KeyValuePair<string, Node>[]> PairsOfListNodes()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var nodes = NodeBuilders.GenerateMultipleOfType(randomizer, NodeType.List).ToArray();
                yield return nodes.Select((node) =>
                {
                    var name = randomizer.GetString();
                    return new KeyValuePair<string, Node>(name, node);
                }).ToArray();
            }
        }

        private static IEnumerable<KeyValuePair<string, Node>[]> PairsOfCompositeNodes()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var nodes = NodeBuilders.GenerateMultipleOfType(randomizer, NodeType.Composite).ToArray();
                yield return nodes.Select((node) =>
                {
                    var name = randomizer.GetString();
                    return new KeyValuePair<string, Node>(name, node);
                }).ToArray();
            }
        }

        private static IEnumerable<KeyValuePair<string, Node>[]> NodePairsWithNullName()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var nodePairs     = NodeBuilders.GenerateMultipleNamedNodes(randomizer).ToList();
                var index         = randomizer.Next(nodePairs.Count);
                var newNode       = randomizer.NextNode();
                var malformedPair = new KeyValuePair<string, Node>(null, newNode);
                nodePairs.Insert(index, malformedPair);
                yield return nodePairs.ToArray();
            }
        }

        private static IEnumerable<KeyValuePair<string, Node>[]> NodePairsWithNullValue()
        {
            var randomizer = TestContext.CurrentContext.Random;
            for (var i = 0; i < Constants.RandomTestCount; ++i)
            {
                var nodePairs     = NodeBuilders.GenerateMultipleNamedNodes(randomizer).ToList();
                var index         = randomizer.Next(nodePairs.Count);
                var name          = randomizer.GetString();
                var malformedPair = new KeyValuePair<string, Node>(name, null);
                nodePairs.Insert(index, malformedPair);
                yield return nodePairs.ToArray();
            }
        }
    }
}