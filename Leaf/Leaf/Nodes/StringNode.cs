using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a sequence of characters.
    /// This type is suitable for short and medium length text.
    /// The text is encoded with UTF-8.
    /// </summary>
    public class StringNode : Node
    {
        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public string Value
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public StringNode(string value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal StringNode Read(BinaryReader reader)
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
    }
}
