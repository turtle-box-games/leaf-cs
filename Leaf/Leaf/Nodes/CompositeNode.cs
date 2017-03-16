using System;
using System.Collections;
using System.Collections.Generic;
using Leaf.Serialization;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a set of named nodes.
    /// Keys are the node names and values are the actual nodes.
    /// Stores any number of any type of node in non-sequential order.
    /// </summary>
    public class CompositeNode : Node, IDictionary<string, Node>
    {
        private readonly Dictionary<string, Node> _nodes = new Dictionary<string, Node>();

        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Composite"/>.
        /// </summary>
        public override NodeType Type => NodeType.Composite;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is the maximum version needed by child nodes.
        /// </summary>
        public override int Version
        {
            get
            {
                var maxVersion = 1;
                // ReSharper disable once LoopCanBeConvertedToQuery - avoid dependency on LINQ library.
                foreach(var node in _nodes.Values)
                    if(node.Version > maxVersion)
                        maxVersion = node.Version;
                return maxVersion;
            }
        }

        /// <summary>
        /// Creates a new empty node.
        /// </summary>
        public CompositeNode()
        {
            // ...
        }

        /// <summary>
        /// Creates a new node with an initial set of items.
        /// </summary>
        /// <param name="nodes">Collection of nodes to store in the set.</param>
        /// <exception cref="ArgumentNullException">The set of <paramref name="nodes"/> is null.</exception>
        /// <exception cref="ArgumentException">One of the nodes or their name is null.</exception>
        public CompositeNode(IEnumerable<KeyValuePair<string, Node>> nodes)
        {
            if(nodes == null)
                throw new ArgumentNullException(nameof(nodes));
            foreach(var pair in nodes)
            {
                if(pair.Key == null || pair.Value == null)
                    throw new ArgumentException(nameof(nodes));
                _nodes.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Calls the correct method to serialize the node.
        /// Uses the strategy algorithm.
        /// </summary>
        /// <param name="serializer">Instance that handles serializing node data.</param>
        internal override void Serialize(INodeSerializer serializer)
        {
            serializer.Write(this);
        }

        /// <summary>
        /// Retrieves an enumerator used to iterate over nodes in the set.
        /// </summary>
        /// <returns>Name and node pair enumerator.</returns>
        public IEnumerator<KeyValuePair<string, Node>> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        /// <summary>
        /// Retrieves an enumerator used to iterative over items in the list.
        /// </summary>
        /// <returns>Generic enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a new node to the set.
        /// </summary>
        /// <param name="item">New node to add.</param>
        /// <exception cref="ArgumentException">The key or node is null.</exception>
        public void Add(KeyValuePair<string, Node> item)
        {
            if(item.Key == null || item.Value == null)
                throw new ArgumentException(nameof(item));
            _nodes.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Empties all nodes from the set.
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }

        /// <summary>
        /// Checks whether the set contains a node with a specified name.
        /// </summary>
        /// <param name="item">The node and its name to look for.</param>
        /// <returns>True if the node was found, false otherwise.</returns>
        /// <exception cref="ArgumentException">The key or node is null.</exception>
        public bool Contains(KeyValuePair<string, Node> item)
        {
            if(item.Key == null || item.Value == null)
                throw new ArgumentException(nameof(item));
            Node node;
            if(_nodes.TryGetValue(item.Key, out node))
                return node == item.Value;
            return false;
        }

        /// <summary>
        /// Copies the contents of the set to an array of names and node pairs.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="arrayIndex">Index in <paramref name="array"/> to start the copy at.</param>
        /// <exception cref="ArgumentNullException">The destination <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The starting index is negative.</exception>
        /// <exception cref="ArgumentException">The combination of array size
        /// and the starting index extend outside the bounds of <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<string, Node>[] array, int arrayIndex)
        {
            if(array == null)
                throw new ArgumentNullException(nameof(array));
            if(arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if(array.Length - arrayIndex < _nodes.Count)
                throw new ArgumentException();

            var i = arrayIndex;
            foreach(var pair in _nodes)
                array[i++] = pair;
        }

        /// <summary>
        /// Removes a node by its name and value.
        /// </summary>
        /// <param name="item">Node name and value to remove.</param>
        /// <returns>True if the node was found and removed, false otherwise.</returns>
        /// <exception cref="ArgumentException">The key or node is null.</exception>
        public bool Remove(KeyValuePair<string, Node> item)
        {
            if(item.Key == null || item.Value == null)
                throw new ArgumentException(nameof(item));
            Node node;
            if(!_nodes.TryGetValue(item.Key, out node))
                return false;
            return node == item.Value && _nodes.Remove(item.Key);
        }

        /// <summary>
        /// Retrieves the number of nodes in the set.
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// Checks whether the set is read-only.
        /// This property is always false.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Checks whether a node with the specified name exists in the set.
        /// </summary>
        /// <param name="key">Name of the node to look for.</param>
        /// <returns>True if a node with the specified name exists, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/> is null.</exception>
        public bool ContainsKey(string key)
        {
            return _nodes.ContainsKey(key);
        }

        /// <summary>
        /// Adds a new node to the set.
        /// </summary>
        /// <param name="key">Name of the node.</param>
        /// <param name="value">Actual node to store.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/>
        /// or <paramref name="value"/> is null.</exception>
        public void Add(string key, Node value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));
            _nodes.Add(key, value);
        }

        /// <summary>
        /// Removes a node from the set given its name.
        /// </summary>
        /// <param name="key">Name of the node to remove.</param>
        /// <returns>True if a node with the name specified exists and was removed, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/> is null.</exception>
        public bool Remove(string key)
        {
            return _nodes.Remove(key);
        }

        /// <summary>
        /// Attempts to retrieve a node from the set given its name.
        /// This method does not throw an exception if a node with the specified name doesn't exist.
        /// </summary>
        /// <param name="key">Name of the node to retrieve.</param>
        /// <param name="value">Node referenced by the name.</param>
        /// <returns>True if a node with the specified name exists and was retrieved, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="key"/> is null.</exception>
        public bool TryGetValue(string key, out Node value)
        {
            return _nodes.TryGetValue(key, out value);
        }

        /// <summary>
        /// Retrieves and updates nodes in the set.
        /// </summary>
        /// <param name="key">Name of the node to operate on.</param>
        /// <exception cref="ArgumentNullException">The key or node is null.</exception>
        public Node this[string key]
        {
            get { return _nodes[key]; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));
                _nodes[key] = value;
            }
        }

        /// <summary>
        /// Retrieve a list of node names.
        /// </summary>
        public ICollection<string> Keys => _nodes.Keys;

        /// <summary>
        /// Retrieve a list of nodes.
        /// </summary>
        public ICollection<Node> Values => _nodes.Values;
    }
}
