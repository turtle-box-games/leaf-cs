using System;
using System.Collections;
using System.Collections.Generic;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a set of nodes.
    /// Stores any number of the same type of node in sequential order.
    /// </summary>
    public class ListNode : Node, IList<Node>
    {
        private readonly List<Node> _nodes = new List<Node>();

        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.List"/>.
        /// </summary>
        public override NodeType Type => NodeType.List;

        /// <summary>
        /// Retrieve the ID for the node type of all elements in the list.
        /// This can be used to identify, serialize, and cat a node to its type.
        /// </summary>
        public NodeType ElementType { get; }

        /// <summary>
        /// Creates a new empty node.
        /// </summary>
        /// <param name="type">Node type of each element in the list.</param>
        /// <exception cref="ArgumentNullException">The set of <paramref name="nodes"/> is <c>null</c>.</exception>
        /// <exception cref="ArrayTypeMismatchException">One or more elements in <paramref name="nodes"/> do not match
        /// the type specified by <paramref name="type"/>.</exception>
        public ListNode(NodeType type)
        {
            ElementType = type;
        }

        /// <summary>
        /// Creates a new node with an initial set of items.
        /// </summary>
        /// <param name="type">Node type of each element in the list.</param>
        /// <param name="nodes">Collection of nodes to store in the list.</param>
        /// <exception cref="ArgumentNullException">The set of <paramref name="nodes"/> is <c>null</c>.</exception>
        /// <exception cref="ArrayTypeMismatchException">One or more elements in <paramref name="nodes"/> do not match
        /// the type specified by <paramref name="type"/>.</exception>
        public ListNode(NodeType type, IEnumerable<Node> nodes)
        {
            if(nodes == null)
                throw new ArgumentNullException(nameof(nodes));
            ElementType = type;
            foreach(var node in nodes)
            {
                if(node == null)
                    throw new ArgumentException(nameof(nodes));
                if(node.Type != ElementType)
                    throw new ArrayTypeMismatchException();
                _nodes.Add(node);
            }
        }

        /// <summary>
        /// Retrieves an enumerator used to iterate over nodes in the list.
        /// </summary>
        /// <returns>Node enumerator.</returns>
        public IEnumerator<Node> GetEnumerator()
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
        /// Adds a new node to the end of the list.
        /// </summary>
        /// <param name="item">New node to add.</param>
        /// <exception cref="ArgumentNullException">The new <paramref name="item"/> is null.</exception>
        /// <exception cref="ArrayTypeMismatchException">Attempted to insert a node into the list
        /// that is a different type than allowed.</exception>
        public void Add(Node item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            if(item.Type != ElementType)
                throw new ArrayTypeMismatchException();
            _nodes.Add(item);
        }

        /// <summary>
        /// Removes all nodes from the list.
        /// </summary>
        public void Clear()
        {
            _nodes.Clear();
        }

        /// <summary>
        /// Checks if the list contains a specific node.
        /// </summary>
        /// <param name="item">Node to look for.</param>
        /// <returns>True if the node is found in the list, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to look for is null.</exception>
        public bool Contains(Node item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            return _nodes.Contains(item);
        }

        /// <summary>
        /// Copy nodes from the list into an array.
        /// </summary>
        /// <param name="array">Array to copy nodes into.</param>
        /// <param name="arrayIndex">Index to start at in <paramref name="array"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The starting index is negative.</exception>
        /// <exception cref="ArgumentException">The combination of array size and index
        /// extends past the bounds of the array.</exception>
        public void CopyTo(Node[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes an existing node from the list.
        /// </summary>
        /// <param name="item">Node to look for and remove.</param>
        /// <returns>True if the <paramref name="item"/> was found and removed, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to remove is null.</exception>
        public bool Remove(Node item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            return _nodes.Remove(item);
        }

        /// <summary>
        /// Retrieves the number of items in the list.
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// Checks whether the list is read-only.
        /// This property is always false.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Searches for an node in the list.
        /// </summary>
        /// <param name="item">Node to look for.</param>
        /// <returns>The index of where the node was found in the list, or -1 if it wasn't found.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to find is null.</exception>
        public int IndexOf(Node item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            return _nodes.IndexOf(item);
        }

        /// <summary>
        /// Inserts a node into the list at the specified index.
        /// </summary>
        /// <param name="index">Index at which to insert the node.</param>
        /// <param name="item">New node to insert into the list.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to insert is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="index"/> is negative
        /// or outside the bounds of the list.</exception>
        /// <exception cref="ArrayTypeMismatchException">Attempted to insert a node into the list
        /// that is a different type than allowed.</exception>
        public void Insert(int index, Node item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            if(item.Type != ElementType)
                throw new ArrayTypeMismatchException();
            _nodes.Insert(index, item);
        }

        /// <summary>
        /// Removes a node from the list at a specified index.
        /// </summary>
        /// <param name="index">Index at which to remove a node.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="index"/> is negative
        /// or outside the bounds of the list.</exception>
        public void RemoveAt(int index)
        {
            _nodes.RemoveAt(index);
        }

        /// <summary>
        /// Retrieves and updates nodes in the list.
        /// </summary>
        /// <param name="index">Index of the node to operate on.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="index"/> is negative
        /// or outside the bounds of the list.</exception>
        /// <exception cref="ArgumentNullException">Attempted to set an element in the list to null.</exception>
        /// <exception cref="ArrayTypeMismatchException">Attempted to change a node
        /// in the list to a different type.</exception>
        public Node this[int index]
        {
            get { return _nodes[index]; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException();
                if(value.Type != ElementType)
                    throw new ArrayTypeMismatchException();
                _nodes[index] = value;
            }
        }
    }
}
