using System;
using System.IO;
using Leaf.Nodes;
using Leaf.Versions;

namespace Leaf
{
    /// <summary>
    /// Holds a node structure and information about its storage.
    /// This class is used to serialize and deserialize node data.
    /// </summary>
    public class Container
    {
        private readonly Engine _engine;

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
            if(root == null)
                throw new ArgumentNullException(nameof(root));

            Root    = root;
            _engine = new V1Engine();
        }

        /// <summary>
        /// Creates a container for a node with a specific storage engine.
        /// </summary>
        /// <param name="root">Root node containing all data in the structure.</param>
        /// <param name="engine">Engine that understands and serializes the node's structure.</param>
        public Container(Node root, Engine engine)
        {
            if(root == null)
                throw new ArgumentNullException(nameof(root));
            if(engine == null)
                throw new ArgumentNullException(nameof(engine));

            Root    = root;
            _engine = engine;
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
