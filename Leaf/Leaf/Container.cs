using System;
using Leaf.Nodes;

namespace Leaf
{
    /// <summary>
    /// Holds a node structure and information about its storage.
    /// This class is used to serialize and deserialize node data.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// Access to the root node of the structure.
        /// </summary>
        public Node Root { get; }

        /// <summary>
        /// Creates a container with the specified root node.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <remarks>The lowest compatible engine type will be used for serialization.</remarks>
        public Container(Node root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));

            Root = root;
        }
    }
}
