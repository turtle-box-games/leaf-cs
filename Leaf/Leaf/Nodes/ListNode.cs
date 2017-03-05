using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a set of nodes.
    /// Stores any number of the same type of node in sequential order.
    /// </summary>
    public class ListNode : Node, IList<Node>
    {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal ListNode Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the contents of the node to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal override void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an enumerator used to iterate over nodes in the list.
        /// </summary>
        /// <returns>Node enumerator.</returns>
        public IEnumerator<Node> GetEnumerator()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes all nodes from the list.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the list contains a specific node.
        /// </summary>
        /// <param name="item">Node to look for.</param>
        /// <returns>True if the node is found in the list, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to look for is null.</exception>
        public bool Contains(Node item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy nodes from the list into an array.
        /// </summary>
        /// <param name="array">Array to copy nodes into.</param>
        /// <param name="arrayIndex">Index to start at in <paramref name="array"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The starting index is negative
        /// or past the bounds of the <paramref name="array"/>.</exception>
        public void CopyTo(Node[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an existing node from the list.
        /// </summary>
        /// <param name="item">Node to look for and remove.</param>
        /// <returns>True if the <paramref name="item"/> was found and removed, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to remove is null.</exception>
        public bool Remove(Node item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the number of items in the list.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Checks whether the list is read-only.
        /// This property is always false.
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Searches for an node in the list.
        /// </summary>
        /// <param name="item">Node to look for.</param>
        /// <returns>The index of where the node was found in the list, or -1 if it wasn't found.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="item"/> to find is null.</exception>
        public int IndexOf(Node item)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a node from the list at a specified index.
        /// </summary>
        /// <param name="index">Index at which to remove a node.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="index"/> is negative
        /// or outside the bounds of the list.</exception>
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves and updates nodes in the list.
        /// </summary>
        /// <param name="index">Index of the node to operate on.</param>
        /// <exception cref="IndexOutOfRangeException">The <paramref name="index"/> is negative
        /// or outside the bounds of the list.</exception>
        /// <exception cref="ArgumentNullException">Attempted to set an element in the list to null.</exception>
        /// <exception cref="ArrayTypeMismatchException">Attempted to change a node
        /// in the list to a different type.</exception>
        public Node this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
