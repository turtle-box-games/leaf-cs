using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Typical integer value that can be stored.
    /// Stores a value from -2,147,483,648 to 2,147,483,647.
    /// </summary>
    public class IntNode : Node
    {
        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public int Value
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public IntNode(int value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal IntNode Read(BinaryReader reader)
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
