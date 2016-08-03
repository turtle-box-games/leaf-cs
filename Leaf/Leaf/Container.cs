using System;
using System.IO;
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
        public Node Root
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a container with the specified root node.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <remarks>The lowest compatible engine type will be used for serialization.</remarks>
        public Container(Node root)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a container for a node with a specific storage engine.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <param name="engine">Engine that understands and serializes the node's structure.</param>
        public Container(Node root, Engine engine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a node structure from a stream.
        /// </summary>
        /// <param name="input">Stream to read a container from.</param>
        public static Container Read(Stream input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the contents of the container and its structure to a stream.
        /// </summary>
        /// <param name="output">Stream to write the container to.</param>
        public void Write(Stream output)
        {
            throw new NotImplementedException();
        }
    }
}
